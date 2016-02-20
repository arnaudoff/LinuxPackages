namespace LinuxPackages.Web.Mvc.Tests.Setups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LinuxPackages.Data.Models;
    using LinuxPackages.Data.Repositories;
    using Moq;

    internal static class Repositories
    {
        internal static IRepository<Package> GetPackagesRepository()
        {
            var repository = new Mock<IRepository<Package>>();

            repository.Setup(r => r.All()).Returns(() =>
            {
                return new List<Package>
                {
                    new Package
                    {
                        Id = 1,
                        Name = "hexchat",
                        Description = "random IRC client",
                        Distribution = new Distribution
                        {
                            Name = "Ubuntu",
                            Version = "14.04",
                            Maintainer = "Canonical",
                            Url = "http://ubuntu.com"
                        },
                        Repository = new Repository
                        {
                            Name = "testing",
                            Url = "http://lnxpckgs.net/repository/testing"
                        },
                        Architecture = new Architecture
                        {
                            Name = "x86"
                        },
                        License = new License
                        {
                            Name = "MIT",
                            Description = "MIT License",
                            Url = "http://example.com"
                        },
                        FileName = "hexchat.tar.gz",
                        Size = 700000,
                        UploadedOn = new DateTime(2016, 2, 15, 22, 30, 30),
                        Downloads = 10,
                        Ratings = new List<Rating>
                        {
                            new Rating
                            {
                                Value = 5,
                            },
                            new Rating
                            {
                                Value = 4,
                            }
                        },
                        Maintainers = new List<User>
                        {
                            new User
                            {
                                Email = "pesho@example.com",
                                UserName = "pesho@example.com",
                                FirstName = "Pesho",
                                LastName = "Petrov"
                            },
                            new User
                            {
                                Email = "gosho@example.com",
                                UserName = "gosho@example.com",
                                FirstName = "Georgi",
                                LastName = "Georgiev"
                            }
                        }
                    },
                    new Package
                    {
                        Id = 2,
                        Name = "vim",
                        Description = "the best text editor out there",
                        Distribution = new Distribution
                        {
                            Name = "ArchLinux",
                            Version = "Rolling",
                            Maintainer = "dev team",
                            Url = "https://www.archlinux.org/"
                        },
                        Repository = new Repository
                        {
                            Name = "stable",
                            Url = "http://lnxpckgs.net/repository/stable"
                        },
                        Architecture = new Architecture
                        {
                            Name = "x64"
                        },
                        License = new License
                        {
                            Name = "MIT",
                            Description = "MIT License",
                            Url = "http://example.com"
                        },
                        FileName = "vim.tar.gz",
                        Size = 700000,
                        UploadedOn = new DateTime(2016, 2, 15, 22, 30, 30),
                        Downloads = 10,
                        Ratings = new List<Rating>
                        {
                            new Rating
                            {
                                Value = 5,
                            },
                            new Rating
                            {
                                Value = 5,
                            }
                        },
                        Maintainers = new List<User>
                        {
                            new User
                            {
                                Email = "bram@example.com",
                                UserName = "bram@example.com",
                                FirstName = "Bram",
                                LastName = "Moolenaar"
                            }
                        }
                    }
                }.AsQueryable();
            });

            return repository.Object;
        }

        internal static IRepository<IssueReply> GetIssueRepliesRepository()
        {
            var repository = new Mock<IRepository<IssueReply>>();

            repository.Setup(r => r.All()).Returns(() =>
            {
                return new List<IssueReply>
                {
                    new IssueReply
                    {
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt",
                        CreatedOn = new DateTime(2016, 2, 15, 22, 30, 30),
                        Author = new User
                        {
                            Email = "gosho@example.com",
                            UserName = "gosho@example.com",
                            FirstName = "Georgi",
                            LastName = "Georgiev"
                        }
                    },
                    new IssueReply
                    {
                        Content = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        CreatedOn = new DateTime(2016, 2, 15, 22, 30, 30),
                        Author = new User
                        {
                            Email = "pesho@example.com",
                            UserName = "pesho@example.com",
                            FirstName = "Pesho",
                            LastName = "Petrov"
                        },
                    },
                }.AsQueryable();
            });

            return repository.Object;
        }

        internal static IRepository<Issue> GetIssuesRepository()
        {
            var repository = new Mock<IRepository<Issue>>();

            repository.Setup(r => r.All()).Returns(() =>
            {
                return new List<Issue>
                {
                    new Issue
                    {
                        Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                        Severity = IssueSeverityType.Critical,
                        State = IssueStateType.Closed,
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt",
                        OpenedOn = new DateTime(2016, 2, 15, 22, 30, 30),
                        PositiveVotes = 1,
                        NegativeVotes = 2,
                        Package = new Package
                        {
                            Name = "vim",
                        },
                        Author = new User
                        {
                            Email = "gosho@example.com",
                            UserName = "gosho@example.com",
                            FirstName = "Georgi",
                            LastName = "Georgiev"
                        },
                    },
                    new Issue
                    {
                        Title = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem",
                        Severity = IssueSeverityType.Low,
                        State = IssueStateType.Opened,
                        Content = "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?",
                        OpenedOn = new DateTime(2016, 2, 15, 22, 30, 30),
                        PositiveVotes = 1,
                        NegativeVotes = 2,
                        Package = new Package
                        {
                            Name = "hexchat",
                        },
                        Author = new User
                        {
                            Email = "bram@example.com",
                            UserName = "bram@example.com",
                            FirstName = "Bram",
                            LastName = "Moolenaar"
                        }
                    }
                }.AsQueryable();
            });

            return repository.Object;
        }

        internal static IRepository<Rating> GetPackageRatingsRepository()
        {
            return null;
        }

        internal static IRepository<PackageComment> GetPackageCommentsRepository()
        {
            var repository = new Mock<IRepository<PackageComment>>();

            repository.Setup(r => r.All()).Returns(() =>
            {
                return new List<PackageComment>
                {
                    new PackageComment
                    {
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt",
                        CreatedOn = new DateTime(2016, 2, 15, 22, 30, 30),
                        Package = new Package
                        {
                            Name = "vim",
                        },
                        Author = new User
                        {
                            Email = "gosho@example.com",
                            UserName = "gosho@example.com",
                            FirstName = "Georgi",
                            LastName = "Georgiev"
                        },
                    },
                    new PackageComment
                    {
                        Content = "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?",
                        CreatedOn = new DateTime(2016, 2, 15, 22, 30, 30),
                        Package = new Package
                        {
                            Name = "hexchat",
                        },
                        Author = new User
                        {
                            Email = "bram@example.com",
                            UserName = "bram@example.com",
                            FirstName = "Bram",
                            LastName = "Moolenaar"
                        }
                    }
                }.AsQueryable();
            });

            return repository.Object;
        }

        internal static IRepository<Dependency> GetDependenciesRepository()
        {
            return null;
        }

        internal static IRepository<User> GetUsersRepository()
        {
            var repository = new Mock<IRepository<User>>();

            repository.Setup(r => r.All()).Returns(() =>
            {
                return new List<User>
                {
                    new User
                    {
                        Email = "pesho@example.com",
                        UserName = "pesho@example.com",
                        FirstName = "Pesho",
                        LastName = "Petrov"
                    },
                    new User
                    {
                        Id = "123abc",
                        Email = "gosho@example.com",
                        UserName = "gosho@example.com",
                        FirstName = "Georgi",
                        LastName = "Georgiev"
                    }
                }.AsQueryable();
            });

            return repository.Object;
        }
    }
}
