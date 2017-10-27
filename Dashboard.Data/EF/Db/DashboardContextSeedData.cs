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


        public static async Task<bool> SeedData(DashboardContext ctx)
        {

            ctx.Database.EnsureCreated();


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

            var employees = new Employee[]
            {
            new Employee{FirstName="Andrés",LastName="Alexander"},
            new Employee{FirstName="Katrina",LastName="Rosales"},
            new Employee{FirstName="Kriszta",LastName="Barta"},
            new Employee{FirstName="Jeff",LastName="Efe"},
            new Employee{FirstName="Ivan",LastName="Programet"},
            new Employee{FirstName="Per",LastName="Justice"},
            new Employee{FirstName="Laura",LastName="Norman"},
            new Employee{FirstName="Nino",LastName="Olivetto"}
            };
            foreach (Employee employee in employees)
            {
                ctx.Employees.Add(employee);
            }
            await ctx.SaveChangesAsync();

            if (ctx.Location.Any())
            {
                return false;   // DB has been seeded
            }
            var locations = new Location[]
            {
                new Location{ Address = "Storgatan 1", City ="Malmö"},
                new Location {  Address = "Storgatan 1", City = "Helsingborg"},
                new Location {  Address = "Storgatan 1", City = "Lund"},
                new Location{ Address = "Storgatan 2", City ="Malmö"},
                new Location {  Address = "Storgatan 2", City = "Helsingborg"},
                new Location {  Address = "Storgatan 2", City = "Lund"},
                new Location {  Address = "Storgatan 3", City = "Lund"}

            };
            foreach (Location location in locations)
            {
                ctx.Location.Add(location);
            }
            await ctx.SaveChangesAsync();

            if (ctx.Clients.Any())
            {
                return false;   // DB has been seeded
            }
            var clients = new Client[]
            {
                new Client{ ClientName = "Sigma",Description ="It Consulting", Location = ctx.Location.FirstOrDefault(x => x.City == "Malmö")},
                new Client{ ClientName = "Combitech",Description ="It Consulting", Location = ctx.Location.FirstOrDefault(x => x.City == "Malmö") },
                new Client { ClientName = "ÅF",Description ="It Consulting", Location = ctx.Location.FirstOrDefault(x => x.City == "Malmö")}
            };
            foreach (Client client in clients)
            {
                ctx.Clients.Add(client);
            }
            await ctx.SaveChangesAsync();

            if (ctx.Phases.Any())
            {
                return false;   // DB has been seeded
            }
            var phases = new Phase[]
            {
                new Phase { PhaseName = "Analisys", },
                new Phase { PhaseName = "Design", },
                new Phase { PhaseName = "Implementation", },
                new Phase { PhaseName = "Testing", },
                new Phase { PhaseName = "Deployment", }
            };
            if (ctx.Tasks.Any())
            {
                return false;   // DB has been seeded
            }
            var tasks = new Entities.Task[]
            {
                new Entities.Task { TaskName = "Customer Requirements", Phase = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Analisys")},
                new Entities.Task { TaskName = "User Requirements",  Phase = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Analisys")},
                new Entities.Task { TaskName = "Customer Requirements", Phase = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Analisys")},
                new Entities.Task { TaskName = "Entities Design",  Phase = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Analisys")},
                new Entities.Task { TaskName = "Db Design", Phase = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Design")},
                new Entities.Task { TaskName = "Entities Design",  Phase = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Design")},
                new Entities.Task { TaskName = "UI Design", Phase = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Design")},
                new Entities.Task { TaskName = "Mockups",  Phase = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Design")}
            };
            if (ctx.Projects.Any())
            {
                return false;   // DB has been seeded
            }
            var projects = new Project[]
            {
            new Project{ProjectName="Dashboard",  StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Phases = phases, Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "Sigma")},
            new Project{ProjectName="Booking System", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Phases = phases, Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "Sigma")},
            new Project{ProjectName="Macroeconomics Count System", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Phases = phases, Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "Sigma")},
            new Project{ProjectName="Calculus System", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Phases = phases, Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "Combitech")},
            new Project{ProjectName="Particle Entanglement Props Count", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Phases = phases, Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "ÅF")},
            new Project{ProjectName="Composition Light Photons", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Phases = phases, Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "Combitech")},
            new Project{ProjectName="Literature Search Motor", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Phases = phases, Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "ÅF")}
            };
            foreach (Project project in projects)
            {
                ctx.Projects.Add(project);
            }
            await ctx.SaveChangesAsync();
            if (ctx.Assignments.Any())
            {
                return false;   // DB has been seeded
            }
            var assignments = new Assignment[]
            {
            new Assignment{EmployeeId = employees.Single(x => x.FirstName == "Andrés").EmployeeId, ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},
            new Assignment{EmployeeId = employees.Single(x => x.FirstName == "Kat").EmployeeId, ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},
            new Assignment{EmployeeId = employees.Single(x => x.FirstName == "Kriszt").EmployeeId, ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},
            new Assignment{EmployeeId = employees.Single(x => x.FirstName == "Jeff").EmployeeId, ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},

            };

            foreach (var assignment in assignments)
            {
                ctx.Assignments.Add(assignment);
            }
            await ctx.SaveChangesAsync();
            return true;
        }
    }
}
