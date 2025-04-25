ASP.NET Core MVC E-Commerce Application
============================================================

This is a simple e-commerce website built using ASP.NET Core MVC with cookie-based authentication (no Identity), 
SQL Server for the database, and Redis for session storage.

🛠️ Technologies Used
---------------------
- ASP.NET Core MVC
- Microsoft SQL Server (SSMS 2022)
- Redis Server
- Entity Framework Core
- Bootstrap 5 & Bootstrap Icons
- Toastr.js for notifications

🧰 Prerequisites
----------------
Make sure the following are installed on your system:

- .NET 8 SDK: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- SQL Server Management Studio 2022 (SSMS): https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms
- Redis: https://github.com/tporadowski/redis/releases
- Visual Studio 2022 (with ASP.NET and web development workload): https://visualstudio.microsoft.com/
- Make sure to install these Nuget packages in your solution :-
	1. BCrypt.Net-Next
	2. Microsoft.EntityFrameworkCore
	3. Microsoft.EntityFrameworkCore.SqlServer
	4. Microsoft.EntityFrameworkCore.Tools
	5. Microsoft.Extensions.Caching.StackExchangeRedis
	6. Microsoft.VisualStudio.Web.CodeGeneration.Design

🧾 Setup Instructions
----------------------

1. Clone the Repository
-----------------------
    git clone https://github.com/MD-Coditas/WebAssign1.git
    cd WebAssign1

2. Configure Database
---------------------
- Open the solution in Visual Studio.
- Go to appsettings.json and update the DefaultConnection string with your local SQL Server instance details.

    "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=WebAssign1;Trusted_Connection=True;TrustServerCertificate=True;"
    }

- Run the EF Core migrations to create the database schema by opening the Tools > NuGet Package Manager > Package Manager Console:

    add-migration <migration_name>
    
    update-database

> Make sure your SQL Server instance is running and accessible.

3. Configure Redis
------------------
Ensure Redis is installed and running on the default port (6379).
If Redis is on a different port or host, update it in Program.cs:

    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "localhost:6379";
    });

4. Run the Application
----------------------

👤 Admin Access
---------------
By default, the admin user is identified by email:
    admin@example.com

> You need to register with "admin@example.com" for creating the admin account. If you want to change the admin email, you can change it in AccountController.cs where the adminEmail string is.

✅ Notes
--------
- To log in as different users, manually register them (or use a seeded user model).
- Redis is used for storing shopping cart sessions.

🧑‍💻 Author
-----------
Developed by Mohit Desale
Made with ❤️ using ASP.NET Core MVC.
