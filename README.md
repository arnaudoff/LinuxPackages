# LinuxPackages

LinuxPackages is a web application whose main idea is to serve as a unified place for storing and maintaining linux package archives for all distributions ever created.

[![Build status](https://ci.appveyor.com/api/projects/status/b54649ukgqibht8j/branch/master?svg=true)](https://ci.appveyor.com/project/arnaudoff/linuxpackages/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/arnaudoff/LinuxPackages/badge.svg?branch=master)](https://coveralls.io/github/arnaudoff/LinuxPackages?branch=master)

## Features
- Authentication and authorization, two user roles - user (maintainer) and administrator.
  - Supports custom user avatars
  - Supports password changing
  - Supports user profile with latest uploaded packages and issues for the user
- Package addition
  - Multiple screenshots upload
  - Multiple package maintainers
  - Supports packages dependency
- Package listing
  - Sortable/pageable/groupable/filterable list of all packages
- Issue addition
  - Supports rich text editing/formatting
- Issue listing
  - Sortable/pageable/groupable/filterable list of all issues
- Package details
  - Supports download of the package
  - Supports 1-5 rating of a package
  - Supports package comments (rick text editing), pagable
- Issue details
  - Supports issue replies
  - Supports issue closing
    - An issue can be either closed by its owner or user who's in the maintainers list
- Home page
  - Displays the most recent issues
  - Displays the most downloaded packages
  - Displays the top maintainers
- Administration area
  - The home page displays the recently posted issues, recently uploaded packages, recently added package comments and issue replies
  - The packages administration page
    - Supports editing and deleting of packages
    - Supports PDF/Excel exporting of packages
    - Shows statistics for the package upload distribution in the last one month
  - The issues administration page
    - Supports editing and deleting of issues
    - Supports PDF/Excel exporting of packages
  - The distributions administration page
    - Supports creating, editing and deleting of distributions
  - The architectures administration page
    - Supports creating, editing and deleting of architectures
  - The licenses administration page
    - Supports creating, editing and deleting of licenses

## Project architecture

- Assembly Common/LinuxPackages.Common
  - contains constants shared throughout the code base; including utility classes
- Assembly Data/LinuxPackages.Data
  - contains the core database configuration: the database context, migrations configuration, data seeding etc.
- Assembly Data/LinuxPackages.Data.Models
  - contains the database models for the application; including enumerations
- Assembly Services/LinuxPackages.Services.Data
  - contains classes that represent one level abstraction over the repositories
- Assembly Web/LinuxPackages.Web.Mvc
  - contains the main ASP.NET MVC application: controllers, views, viewmodels etc.
- Assembly Tools/LinuxPackages.Tools.Crawler
  - contains a simple crawler that scraps some packages, their description and categories from `https://packages.debian.org/stable/` and exports them to a CSV that can be used to train an Azure ML model
- Assembly Tests/LinuxPackages.Common.Tests
  - contains tests for the common classes, i.e utility classes
- Assembly Tests/LinuxPackages.Web.Mvc.Tests
  - contains controller, route and some nice day maybe integration tests

## Disclaimer

The purpose of this project is entirely educational. It relies heavily on the KendoUI ASP.NET MVC wrappers such as Kendo Grid, Kendo Editor, Kendo TabStrip etc. that are available only in the professional edition of the library. However, the project includes the *30-day trial* version of KendoUI, which can be downloaded [here](http://www.telerik.com/aspnet-mvc).

## Screenshots

<img src="http://i.imgur.com/932Uft4.png" />
<img src="http://i.imgur.com/pHoIWma.png" />
<img src="http://i.imgur.com/3ECWbHN.png" />
<img src="http://i.imgur.com/3Mz6Ui3.png" />
