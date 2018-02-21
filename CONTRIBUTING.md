
# ShopCoreApp

Update database using command line
Change conection string in appconfig.json

Package Manger Console or cd project ~/sourceCode/ShopCoreApp/ShopCoreApp

`"update-database" (Package Manger Console) or "dotnet ef database update" (dotnet core commad)`
# Application using ASP.NET CORE 2.0
    Project
        + 1. Presentation
            -- ShopCoreApp(MVC Application)
            -- ShopCoreApp.WebApi(Web API)
        + 2. Application
            -- ShopCoreApp.Service
        + 3. Domain
            -- ShopCoreApp.Data
            -- ShopCoreApp.Data.EF
            -- ShopCoreApp.Data.MDEF (MySQK Database)
        + 4. Infrastructure
            -- ShopCoreApp.Infrastructure
            -- ShopCoreApp.Utilities
# Database SQL SERVER 2016 
    Default db Name ShopCoreApp
# Resful Api Project
    Using Identity Token Provider
