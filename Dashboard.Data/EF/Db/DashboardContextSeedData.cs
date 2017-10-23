using Dashboard.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.Db
{
    public static class DashboardContextSeedData
    {
        

        //public static async Task<bool> SeedData(DashboardContext ctx)
        //{
           
        //    ctx.Database.EnsureCreated();


            //if (ctx.Pictures.Any())
            //{
            //    return false;
            //}
            //var pictures = new Picture[]
            //{
            //    new Picture{Title="andres",FileName="Dashboard.API/Images/andres.jpg"},
            //    new Picture{Title="katrina",FileName="Dashboard.API/Images/katrina.jpg"},
            //    new Picture{Title="kriszta",FileName="Dashboard.API/Images/kriszta.jpg"},
            //    new Picture{Title="jeff",FileName= "Dashboard.API/Images/jeff.jpg"}
            //};
            //foreach (Picture picture in pictures)
            //{
            //    ctx.Pictures.Add(picture);
            //}
            //await ctx.SaveChangesAsync();
            
            //if (ctx.Employees.Any())
            //    {
            //        return false;   // DB has been seeded
            //    }

            //var employees = new Employee[]
            //{
            //new Employee{FirstName="Andrés",LastName="Alexander",PictureId = ctx.Pictures.Single(x => x.Title == "andres").PictureId},
            //new Employee{FirstName="Katrina",LastName="Rosales",PictureId = ctx.Pictures.Single(x => x.Title == "katrina").PictureId},
            //new Employee{FirstName="Kriszta",LastName="Barta",PictureId = ctx.Pictures.Single(x => x.Title == "kriszta").PictureId},
            //new Employee{FirstName="Jeff",LastName="Barzdukas",PictureId = ctx.Pictures.Single(x => x.Title == "jeff").PictureId},
            ////new User{FirstName="Ivan",LastName="Programet",PictureId = ctx.Pictures.Single(x => x.Title == "ivan").PictureId}
            ////new User{FirstName="Peggy",LastName="Justice", PictureId },
            ////new User{FirstName="Laura",LastName="Norman"},
            ////new User{FirstName="Nino",LastName="Olivetto"}
            //};
            //foreach (Employee employee in employees)
            //{
            //    ctx.Employees.Add(employee);
            //}
            //await ctx.SaveChangesAsync();
            //if (ctx.Projects.Any())
            //{
            //    return false;   // DB has been seeded
            //}
            //var projects = new Project[]
            //{
            //new Project{ProjectName="Dashboard",  StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Notes = "Dashboard API/Web app to keep track of our consults"},
            //new Project{ProjectName="Microeconomics",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Notes = "Dashboard API/Web app to keep track of our consults"},
            //new Project{ProjectName="Macroeconomics",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Notes = "Dashboard API/Web app to keep track of our consults"},
            //new Project{ProjectName="Calculus",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Notes = "Dashboard API/Web app to keep track of our consults"},
            //new Project{ProjectName="Trigonometry",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Notes = "Dashboard API/Web app to keep track of our consults"},
            //new Project{ProjectName="Composition",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Notes = "Dashboard API/Web app to keep track of our consults"},
            //new Project{ProjectName="Literature",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Notes = "Dashboard API/Web app to keep track of our consults"}
            //};
            //foreach (Project project in projects)
            //{
            //    ctx.Projects.Add(project);
            //}
            //await ctx.SaveChangesAsync();
            //if (ctx.Assignments.Any())
            //{
            //    return false;   // DB has been seeded
            //}
            //var assignments = new Assignment[]
            //{
            //new Assignment{EmployeeId = employees.Single(x => x.FirstName == "Andrés").EmployeeId, ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},
            //new Assignment{EmployeeId = employees.Single(x => x.FirstName == "Kat").EmployeeId, ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},
            //new Assignment{EmployeeId = employees.Single(x => x.FirstName == "Kriszt").EmployeeId, ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},
            //new Assignment{EmployeeId = employees.Single(x => x.FirstName == "Jeff").EmployeeId, ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},

            //};

            //foreach (var assignment in assignments)
            //{
            //    ctx.Assignments.Add(assignment);
            //}
            //await ctx.SaveChangesAsync();
            //return true;
         //}
    }
}
