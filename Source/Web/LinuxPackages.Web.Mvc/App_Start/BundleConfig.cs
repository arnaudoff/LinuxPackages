namespace LinuxPackages.Web.Mvc
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/rateit")
                .Include("~/Scripts/jquery.rateit.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/kendo")
                .Include("~/Scripts/kendo/kendo.custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // Custom form validation
            bundles.Add(new ScriptBundle("~/bundles/validation")
                .Include("~/Scripts/validation.js"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/rateit")
                .Include("~/Content/rateit.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap")
                .Include("~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/customcss")
                .Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendo")
                .Include(
                    "~/Content/kendo/kendo.common-office365.min.css",
                    "~/Content/kendo/kendo.office365.min.css"));
        }
    }
}
