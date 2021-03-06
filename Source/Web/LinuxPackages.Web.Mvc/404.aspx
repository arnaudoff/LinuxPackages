﻿<% Response.StatusCode = 404 %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Error - LinuxPackages</title>
    <link rel="stylesheet" href="/Content/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="/Content/Site.css" />
</head>
<body>
    <div class="container body-content">
        <div class="error">
            <div class="error-code m-b-10 m-t-20">404 <i class="fa fa-warning"></i></div>
            <h3 class="font-bold">We couldn't find the requested page</h3>

            <div class="error-desc">
                Sorry, but the page you are looking for was either not found or does not exist. <br />
                Try refreshing the page or click the button below to go back to the Homepage.
                <div>
                    <a class=" login-detail-panel-button btn" href="/">
                        <i class="fa fa-arrow-left"></i>
                        Go back to the homepage
                    </a>
                </div>
            </div>
        </div>
        <hr />
        <footer class="text-center">
            <p>LinuxPackages by Ivaylo Arnaudov</p>
            <p>Open source on <a href="http://github.com/arnaudoff/LinuxPackages">GitHub</a></p>
        </footer>
    </div>
</body>
</html>