namespace LinuxPackages.Web.Mvc.Infrastructure.Mappings
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper.QueryableExtensions;
    using App_Start;

    public static class QueryableExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }
}