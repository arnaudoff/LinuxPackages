namespace LinuxPackages.Web.Mvc.Infrastructure.ActionFilters
{
    using System.Linq;
    using System.Web.Mvc;

    using Helpers.Contracts;
    using Ninject;

    public class HashCheck : ActionFilterAttribute
    {
        public string[] ParametersToCheck { get; set; }

        [Inject]
        public IUrlIdentifierProvider UrlIdentifierProvider { private get; set; }

        public HashCheck(params string[] paramsToCheck)
        {
            this.ParametersToCheck = paramsToCheck;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionParameters = filterContext.ActionParameters;
            foreach (var param in actionParameters)
            {
                if (this.ParametersToCheck.Contains(param.Key))
                {
                    if (!this.UrlIdentifierProvider.IsHashValid((string)param.Value))
                    {
                        filterContext.Result = new HttpNotFoundResult(string.Format("Invalid URL parameter {0}", param.Key));
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}