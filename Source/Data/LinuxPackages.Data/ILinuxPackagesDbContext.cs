﻿namespace LinuxPackages.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;

    public interface ILinuxPackagesDbContext
    {
        IDbSet<Package> Packages { get; set; }

        IDbSet<Distribution> Distributions { get; set; }

        IDbSet<Rating> PackageRatings { get; set; }

        IDbSet<PackageComment> PackageComments { get; set; }

        IDbSet<Screenshot> Screenshots { get; set; }

        IDbSet<Issue> Issues { get; set; }

        IDbSet<IssueReply> IssueReplies { get; set; }

        IDbSet<Repository> Repositories { get; set; }

        IDbSet<Architecture> Architectures { get; set; }

        IDbSet<License> Licenses { get; set; }

        IDbSet<Dependency> Dependencies { get; set; }

        IDbSet<Avatar> Avatars { get; set; }

        IDbSet<Category> Categories { get; set; }

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        int SaveChanges();

        void Dispose();
    }
}