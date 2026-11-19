# SE Lab 6 ASP.NET MVC

Student: Lê Hoàng Trí — 524H0199

ASP.NET MVC 5 application targeting .NET Framework 4.8. It uses Entity Framework 6 to map an existing SQL Server database (Database First workflow), provides CRUD for Students, Courses, Instructors, Departments and Enrollments, protects the application with a database-backed login, and generates course-filtered student reports with PDF export.

## Run

1. Open `Database/SchoolDB.sql` in SQL Server Management Studio and execute it.
2. Update the `SchoolDbContext` connection string in `Web.config` if your SQL Server instance is not `.\SQLEXPRESS`.
3. Open `SELab6.sln` in Visual Studio and press F5.
4. Sign in with `admin` / `Lab6@123`.

GitHub repository intended for submission: https://github.com/trihoangle17/SELab06
