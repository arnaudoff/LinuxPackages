namespace LinuxPackages.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Constants;
    using Common.Utilities;
    using LinuxPackages.Common.Contracts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class DataSeeder
    {
        private readonly LinuxPackagesDbContext context;
        private readonly IRandomGenerator randomGenerator;

        public DataSeeder(LinuxPackagesDbContext context)
            : this(context, new RandomGenerator())
        {
        }

        public DataSeeder(LinuxPackagesDbContext context, IRandomGenerator randomGenerator)
        {
            this.context = context;
            this.randomGenerator = randomGenerator;
        }

        public void Seed()
        {
            this.SeedUsers();
            this.SeedDistributions();
            this.SeedRepositories();
            this.SeedArchitectures();
            this.SeedLicenses();
        }

        private void SeedUsers()
        {
            if (!this.context.Users.Any())
            {
                var adminRole = new IdentityRole { Name = GlobalConstants.AdminRoleName, Id = Guid.NewGuid().ToString() };
                this.context.Roles.Add(adminRole);

                var hasher = new PasswordHasher();

                var adminUser = new User
                {
                    UserName = "ivo@test.com",
                    Email = "ivo@test.com",
                    FirstName = "Ivaylo",
                    LastName = "Arnaudov",
                    PasswordHash = hasher.HashPassword("testtest"),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                adminUser.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = adminUser.Id });
                this.context.Users.Add(adminUser);

                for (int i = 1; i <= 5; i++)
                {
                    var currentUser = new User
                    {
                        UserName = string.Format("user{0}@linuxpackages.net", i),
                        Email = string.Format("user{0}@linuxpackages.net", i),
                        FirstName = this.randomGenerator.GenerateRandomString(10),
                        LastName = this.randomGenerator.GenerateRandomString(20),
                        PasswordHash = hasher.HashPassword(string.Format("user{0}", i)),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    this.context.Users.Add(currentUser);
                }
            }

            this.context.SaveChanges();
        }

        private void SeedDistributions()
        {
            if (!this.context.Distributions.Any())
            {
                var distros = new List<Distribution>()
                {
                    new Distribution { Name = "Ubuntu", Version = "14.04.3 LTS", Maintainer = "Canonical Ltd.", Url = "http://www.ubuntu.com/" },
                    new Distribution { Name = "Slackware", Version = "14.1", Maintainer = "dev team", Url = "http://www.slackware.com/" },
                    new Distribution { Name = "Linux Mint", Version = "17.3", Maintainer = "dev team", Url = "http://www.linuxmint.com/" },
                    new Distribution { Name = "Gentoo Linux", Version = "Rolling", Maintainer = "Gentoo Foundation, Inc.", Url = "https://www.gentoo.org/" },
                    new Distribution { Name = "Fedora", Version = "23", Maintainer = "Fedora Project", Url = "https://getfedora.org/" },
                    new Distribution { Name = "Debian", Version = "8.3", Maintainer = "Debian Project", Url = "https://www.debian.org/" },
                    new Distribution { Name = "ArchLinux", Version = "Rolling", Maintainer = "dev team", Url = "https://www.archlinux.org/" },
                    new Distribution { Name = "CentOS", Version = "7.0", Maintainer = "CentOS Project", Url = "https://www.centos.org/" },
                    new Distribution { Name = "PuppyLinux", Version = "6.3", Maintainer = "Puppy Foundation", Url = "http://puppylinux.org/" },
                    new Distribution { Name = "Knoppix", Version = "7.4.2", Maintainer = "dev team", Url = "http://www.knopper.net/knoppix/" },
                };

                distros.ForEach(d => this.context.Distributions.Add(d));
            }

            this.context.SaveChanges();
        }

        private void SeedRepositories()
        {
            if (!this.context.Repositories.Any())
            {
                var repos = new List<Repository>()
                {
                    new Repository { Name = "development", Url = "http://www.linuxpackages.net/repository/development" },
                    new Repository { Name = "testing", Url = "http://www.linuxpackages.net/repository/testing" },
                    new Repository { Name = "stable", Url = "http://www.linuxpackages.net/repository/stable" },
                    new Repository { Name = "multilib", Url = "http://www.linuxpackages.net/repository/multilib" }
                };

                repos.ForEach(r => this.context.Repositories.Add(r));
            }

            this.context.SaveChanges();
        }

        private void SeedArchitectures()
        {
            if (!this.context.Architectures.Any())
            {
                var archs = new List<Architecture>()
                {
                    new Architecture { Name = "x86" },
                    new Architecture { Name = "amd64" },
                    new Architecture { Name = "ARM" }
                };

                archs.ForEach(a => this.context.Architectures.Add(a));
            }

            this.context.SaveChanges();
        }

        private void SeedLicenses()
        {
            if (!this.context.Licenses.Any())
            {
                var licenses = new List<License>()
                {
                    new License
                    {
                        Name = "Apache 2.0",
                        Description = "You can do what you like with the software, as long as you include the required notices. This permissive license contains a patent license from the contributors of the code.",
                        Url = "https://tldrlegal.com/license/apache-license-2.0-(apache-2.0)#fulltext"
                    },
                    new License
                    {
                        Name = "GPL v3",
                        Description = "You may copy, distribute and modify the software as long as you track changes/dates in source files. Any modifications to or software including (via compiler) GPL-licensed code must also be made available under the GPL along with build & install instructions.",
                        Url = "https://tldrlegal.com/license/gnu-general-public-license-v3-(gpl-3)#fulltext"
                    },
                    new License
                    {
                        Name = "MIT",
                        Description = "A short, permissive software license. Basically, you can do whatever you want as long as you include the original copyright and license notice in any copy of the software/source.",
                        Url = "https://tldrlegal.com/license/mit-license#fulltext"
                    },
                    new License
                    {
                        Name = "BSD 2 FreeBSD/Simplified",
                        Description = "The BSD 2-clause license allows you almost unlimited freedom with the software so long as you include the BSD copyright notice in it (found in Fulltext). Many other licenses are influenced by this one.",
                        Url = "https://tldrlegal.com/license/bsd-2-clause-license-(freebsd)#fulltext"
                    },
                    new License
                    {
                        Name = "GNU LGPL-3.0",
                        Description = "This license is mainly applied to libraries. You may copy, distribute and modify the software provided that modifications are described and licensed for free under LGPL. Derivatives works (including modifications or anything statically linked to the library) can only be redistributed under LGPL, but applications that use the library don't have to be.",
                        Url = "https://tldrlegal.com/license/gnu-lesser-general-public-license-v3-(lgpl-3)#fulltext"
                    }
                };

                licenses.ForEach(l => this.context.Licenses.Add(l));
            }

            this.context.SaveChanges();
        }
    }
}