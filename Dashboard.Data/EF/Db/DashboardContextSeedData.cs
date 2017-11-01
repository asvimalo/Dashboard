using Dashboard.Entities;
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
            //    new Picture{Title="andres",FileName="~/Images/andres.jpg"},
            //    new Picture{Title="katrina",FileName="Dashboard.API/Images/katrina.jpg"},
            //    new Picture{Title="kriszta",FileName="Dashboard.API/Images/kriszta.jpg"},
            //    new Picture{Title="jeff",FileName= "Dashboard.API/Images/jeff.jpg"}
            //};
            //foreach (Picture picture in pictures)
            //{
            //    ctx.Pictures.Add(picture);
            //}
            //await ctx.SaveChangesAsync();

            if (ctx.Employees.Any())
            {
                return false;   // DB has been seeded
            }

            var employees = new Employee[]
            {
            new Employee{FirstName="Andrés",LastName="Alexander",PersonNr="456987",ImageName="andres", ImagePath=$"~/Images/andres.jpg" },
            new Employee{FirstName="Katrina",LastName="Rosales",ImageName="katrina", ImagePath=$"~/Images/katrina.jpg" },
            new Employee{FirstName="Kriszta",LastName="Barta",ImageName="kriszta", ImagePath=$"~/Images/kriszta.jpg" },
            new Employee{FirstName="Jeff",LastName="Efe",ImageName="jeff", ImagePath=$"~/Images/jeff.jpg" },
            new Employee{FirstName="Ivan",LastName="Programet",ImageName="andres", ImagePath=$"~/Images/andres.jpg" },
            new Employee{FirstName="Per",LastName="Justice",ImageName="andres", ImagePath=$"~/Images/andres.jpg" },
            new Employee{FirstName="Laura",LastName="Norman",ImageName="andres", ImagePath=$"~/Images/andres.jpg" },
            new Employee{FirstName="Nino",LastName="Olivetto",ImageName="andres", ImagePath=$"~/Images/andres.jpg" }
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
                new Location {  Address = "Storgatan 10", City = "Helsingborg"},
                new Location {  Address = "Storgatan 100", City = "Lund"},
                new Location{ Address = "Storgatan 2", City ="Malmö"},
                new Location {  Address = "Storgatan 20", City = "Helsingborg"},
                new Location {  Address = "Storgatan 200", City = "Lund"},
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
                new Client{ ClientName = "Sigma",Description ="It Consulting", LocationId = ctx.Location.FirstOrDefault(x => x.Address == "Storgatan 1").LocationId},
                new Client{ ClientName = "Combitech",Description ="It Consulting", LocationId = ctx.Location.FirstOrDefault(x => x.Address == "Storgatan 2").LocationId },
                new Client { ClientName = "ÅF",Description ="It Consulting", LocationId = ctx.Location.FirstOrDefault(x => x.Address == "Storgatan 200").LocationId}
            };
            foreach (Client client in clients)
            {
                ctx.Clients.Add(client);
            }
            await ctx.SaveChangesAsync();
         
            if (ctx.Projects.Any())
            {
                return false;   // DB has been seeded
            }
            var projects = new Project[]
            {
            new Project{ProjectName="Dashboard",  StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00),  Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "Sigma")},
            new Project{ProjectName="Booking System", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "Sigma")},
            new Project{ProjectName="Macroeconomics Count System", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00),  Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "Sigma")},
            new Project{ProjectName="Calculus System", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00),  Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "Combitech")},
            new Project{ProjectName="Particle Entanglement Props Count", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00),  Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "ÅF")},
            new Project{ProjectName="Composition Light Photons", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00),  Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "Combitech")},
            new Project{ProjectName="Literature Search Motor", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00),  Notes = "Dashboard API/Web app to keep track of our consults", Client = ctx.Clients.FirstOrDefault(x => x.ClientName == "ÅF")}
            };
            foreach (Project project in projects)
            {
                ctx.Projects.Add(project);
            }
            await ctx.SaveChangesAsync();
            if (ctx.Phases.Any())
            {
                return false;   // DB has been seeded
            }
            var phases = new Phase[]
            {
                new Phase { PhaseName = "Analisys", ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},
                new Phase { PhaseName = "Design", ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},
                new Phase { PhaseName = "Implementation", ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},
                new Phase { PhaseName = "Testing",ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId },
                new Phase { PhaseName = "Deployment", ProjectId = projects.Single(x => x.ProjectName == "Dashboard").ProjectId},
                new Phase { PhaseName = "Analisys", ProjectId = projects.Single(x => x.ProjectName == "Booking System").ProjectId},
                new Phase { PhaseName = "Design", ProjectId = projects.Single(x => x.ProjectName == "Booking System").ProjectId},
                new Phase { PhaseName = "Implementation", ProjectId = projects.Single(x => x.ProjectName == "Booking System").ProjectId},
                new Phase { PhaseName = "Testing",ProjectId = projects.Single(x => x.ProjectName == "Booking System").ProjectId },
                new Phase { PhaseName = "Deployment", ProjectId = projects.Single(x => x.ProjectName == "Booking System").ProjectId}
            };
            foreach (Phase phase in phases)
            {
                ctx.Phases.Add(phase);
            }
            await ctx.SaveChangesAsync();
            if (ctx.Tasks.Any())
            {
                return false;   // DB has been seeded
            }
            var tasks = new Entities.Task[]
            {
                new Entities.Task { TaskName = "Customer Requirements", PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Analisys").PhaseId},
                new Entities.Task { TaskName = "User Requirements",  PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Analisys").PhaseId},
                new Entities.Task { TaskName = "Customer Requirements", PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Analisys").PhaseId},
                new Entities.Task { TaskName = "Entities Design",  PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Analisys").PhaseId},
                new Entities.Task { TaskName = "Db Design", PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Design").PhaseId},
                new Entities.Task { TaskName = "Entities Design",  PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Design").PhaseId},
                new Entities.Task { TaskName = "UI Design", PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Design").PhaseId},
                new Entities.Task { TaskName = "Mockups",  PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Design").PhaseId},
                new Entities.Task { TaskName = "Customer Requirements", PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Implementation").PhaseId},
                new Entities.Task { TaskName = "User Requirements",  PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Implementation").PhaseId},
                new Entities.Task { TaskName = "Customer Requirements", PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Implementation").PhaseId},
                new Entities.Task { TaskName = "Entities Design",  PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Implementation").PhaseId},
                new Entities.Task { TaskName = "Db Design", PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Testing").PhaseId},
                new Entities.Task { TaskName = "Entities Design",  PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Testing").PhaseId},
                new Entities.Task { TaskName = "UI Design", PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Testing").PhaseId},
                new Entities.Task { TaskName = "Mockups",  PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Testing").PhaseId},
                new Entities.Task { TaskName = "Db Design", PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Deployment").PhaseId},
                new Entities.Task { TaskName = "Entities Design",  PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Deployment").PhaseId},
                new Entities.Task { TaskName = "UI Design", PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Deployment").PhaseId},
                new Entities.Task { TaskName = "Mockups",  PhaseId = ctx.Phases.FirstOrDefault(x => x.PhaseName == "Deployment").PhaseId}
            };
            foreach (Entities.Task task in tasks)
            {
                ctx.Tasks.Add(task);
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
            if (ctx.Knowledges.Any())
            {
                return false;   // DB has been seeded
            }
            var knowledges = new Knowledge[]
            {
            new Knowledge{KnowledgeName = "C#", Description = "Backend developer" },
            new Knowledge{KnowledgeName = "Java", Description = "Backend developer"},
            new Knowledge{KnowledgeName = "Scrum Master", Description = "The organiser"},
            new Knowledge{KnowledgeName = "Project Manager", Description = "El Jefe"},
            new Knowledge{KnowledgeName = "JavaScript", Description = "FrontEnd developer"},

            };

            foreach (var Knowledge in knowledges)
            {
                ctx.Knowledges.Add(Knowledge);
            }
            await ctx.SaveChangesAsync();
            if (ctx.AcquiredKnowledges.Any())
            {
                return false;   // DB has been seeded
            }
            var acquiredKnowledges = new AcquiredKnowledge[]
            {
                new AcquiredKnowledge{ EmployeeId = employees.Single(x => x.FirstName == "Andres").EmployeeId, KnowledgeId = knowledges.Single(x => x.KnowledgeName == "C#" ).KnowledgeId},
                new AcquiredKnowledge{EmployeeId = employees.Single(x => x.FirstName == "Katrina").EmployeeId, KnowledgeId = knowledges.Single(x => x.KnowledgeName == "JavaScript" ).KnowledgeId},
                new AcquiredKnowledge{EmployeeId = employees.Single(x => x.FirstName == "Kriszta").EmployeeId, KnowledgeId = knowledges.Single(x => x.KnowledgeName == "Java" ).KnowledgeId},
                new AcquiredKnowledge{EmployeeId = employees.Single(x => x.FirstName == "Jeff").EmployeeId, KnowledgeId = knowledges.Single(x => x.KnowledgeName == "Project Manager" ).KnowledgeId}

            };

            foreach (var acquiredKnowledge in acquiredKnowledges)
            {
                ctx.AcquiredKnowledges.Add(acquiredKnowledge);
            }
            await ctx.SaveChangesAsync();
            return true;
        }
    }
}
