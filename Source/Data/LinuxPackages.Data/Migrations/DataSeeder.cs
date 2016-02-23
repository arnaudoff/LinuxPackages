namespace LinuxPackages.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.Constants;
    using Common.Contracts;
    using Common.Utilities;

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
            this.SeedCategories();
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

        private void SeedCategories()
        {
            if (!this.context.Categories.Any())
            {
                var categories = new List<Category>()
                {
                    new Category() { Name = "Administration Utilities", Description = "Utilities to administer system resources, manage user accounts, etc." },
                    new Category() { Name = "Mono/CLI", Description = "Everything about Mono and the Common Language Infrastructure." },
                    new Category() { Name = "Communication Programs", Description = "Software to use your modem in the old fashioned style." },
                    new Category() { Name = "Databases", Description = "Database Servers and Clients." },
                    new Category() { Name = "Debug packages", Description = "Packages providing debugging information for executables and shared libraries." },
                    new Category() { Name = "Development", Description = "Development utilities, compilers, development environments, libraries, etc." },
                    new Category() { Name = "Documentation", Description = "FAQs, HOWTOs and other documents trying to explain everything related to Debian, and software needed to browse documentation (man, info, etc)." },
                    new Category() { Name = "Editors", Description = "Software to edit files. Programming environments." },
                    new Category() { Name = "Electronics", Description = "Electronics utilities." },
                    new Category() { Name = "Embedded software", Description = "Software suitable for use in embedded applications." },
                    new Category() { Name = "Fonts", Description = "Font packages." },
                    new Category() { Name = "Games", Description = "Programs to spend a nice time with after all this setting up." },
                    new Category() { Name = "GNOME", Description = "The GNOME desktop environment, a powerful, easy to use set of integrated applications." },
                    new Category() { Name = "GNU R", Description = "Everything about GNU R, a statistical computation and graphics system." },
                    new Category() { Name = "GNUstep", Description = "The GNUstep environment." },
                    new Category() { Name = "Graphics", Description = "Editors, viewers, converters... Everything to become an artist." },
                    new Category() { Name = "Ham Radio", Description = "Software for ham radio." },
                    new Category() { Name = "Haskell", Description = "Everything about Haskell." },
                    new Category() { Name = "Web Servers", Description = "Web servers and their modules." },
                    new Category() { Name = "Interpreters", Description = "All kind of interpreters for interpreted languages. Macro processors." },
                    new Category() { Name = "Java", Description = "Everything about Java." },
                    new Category() { Name = "KDE", Description = "The K Desktop Environment, a powerful, easy to use set of integrated applications." },
                    new Category() { Name = "Kernels", Description = "Operating System Kernels and related modules." },
                    new Category() { Name = "Library development", Description = "Libraries necessary for developers to write programs that use them." },
                    new Category() { Name = "Libraries", Description = "Libraries to make other programs work. They provide special features to developers." },
                    new Category() { Name = "Lisp", Description = "Everything about Lisp." },
                    new Category() { Name = "Language packs", Description = "Localization support for big software packages." },
                    new Category() { Name = "Mail", Description = "Programs to route, read, and compose E-mail messages." },
                    new Category() { Name = "Mathematics", Description = "Math software." },
                    new Category() { Name = "Miscellaneous", Description = "Miscellaneous utilities that didn't fit well anywhere else." },
                    new Category() { Name = "Network", Description = "Daemons and clients to connect your system to the world." },
                    new Category() { Name = "Newsgroups", Description = "Software to access Usenet, to set up news servers, etc." },
                    new Category() { Name = "OCaml", Description = "Everything about OCaml, an ML language implementation." },
                    new Category() { Name = "Old Libraries", Description = "Old versions of libraries, kept for backward compatibility with old applications." },
                    new Category() { Name = "Other OS's and file systems", Description = "Software to run programs compiled for other operating systems, and to use their filesystems." },
                    new Category() { Name = "Perl", Description = "Everything about Perl, an interpreted scripting language." },
                    new Category() { Name = "PHP", Description = "Everything about PHP." },
                    new Category() { Name = "Python", Description = "Everything about Python, an interpreted, interactive object oriented language." },
                    new Category() { Name = "Ruby", Description = "Everything about Ruby, an interpreted object oriented language." },
                    new Category() { Name = "Science", Description = "Basic tools for scientific work" },
                    new Category() { Name = "Shells", Description = "Command shells. Friendly user interfaces for beginners." },
                    new Category() { Name = "Sound", Description = "Utilities to deal with sound: mixers, players, recorders, CD players, etc." },
                    new Category() { Name = "TeX", Description = "The famous typesetting software and related programs." },
                    new Category() { Name = "Text Processing", Description = "Utilities to format and print text documents." },
                    new Category() { Name = "Utilities", Description = "Utilities for file/disk manipulation, backup and archive tools, system monitoring, input systems, etc." },
                    new Category() { Name = "Version Control Systems", Description = "Version control systems and related utilities." },
                    new Category() { Name = "Video", Description = "Video viewers, editors, recording, streaming." },
                    new Category() { Name = "Virtual packages", Description = "Virtual packages." },
                    new Category() { Name = "Web Software", Description = "Web servers, browsers, proxies, download tools etc." },
                    new Category() { Name = "X Window System software", Description = "X servers, libraries, fonts, window managers, terminal emulators and many related applications." },
                    new Category() { Name = "Xfce", Description = "Xfce, a fast and lightweight Desktop Environment." },
                    new Category() { Name = "Zope/Plone Framework", Description = "Zope Application Server and Plone Content Managment System." },
                };

                categories.ForEach(c => this.context.Categories.Add(c));
            }

            this.context.SaveChanges();
        }
    }
}