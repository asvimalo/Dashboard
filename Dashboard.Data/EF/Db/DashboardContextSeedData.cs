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


            if (ctx.Pictures.Any())
            {
                return false;
            }
            var pictures = new Picture[]
            {
                new Picture{Title="andres",FileName="Dashboard.API/Images/andres.jpg"},
                new Picture{Title="katrina",FileName="Dashboard.API/Images/katrina.jpg"},
                new Picture{Title="kriszta",FileName="Dashboard.API/Images/kriszta.jpg"},
                new Picture{Title="jeff",FileName= "Dashboard.API/Images/jeff.jpg"}
            };
            foreach (Picture picture in pictures)
            {
                ctx.Pictures.Add(picture);
            }
            await ctx.SaveChangesAsync();
            
            if (ctx.Users.Any())
                {
                    return false;   // DB has been seeded
                }

            var users = new User[]
            {
            new User{FirstName="Andrés",LastName="Alexander",PictureId = ctx.Pictures.Single(x => x.Title == "andres").PictureId},
            new User{FirstName="Katrina",LastName="Rosales",PictureId = ctx.Pictures.Single(x => x.Title == "katrina").PictureId},
            new User{FirstName="Kriszta",LastName="Barta",PictureId = ctx.Pictures.Single(x => x.Title == "kriszta").PictureId},
            new User{FirstName="Jeff",LastName="Barzdukas",PictureId = ctx.Pictures.Single(x => x.Title == "jeff").PictureId},
            //new User{FirstName="Ivan",LastName="Programet",PictureId = ctx.Pictures.Single(x => x.Title == "ivan").PictureId}
            //new User{FirstName="Peggy",LastName="Justice", PictureId },
            //new User{FirstName="Laura",LastName="Norman"},
            //new User{FirstName="Nino",LastName="Olivetto"}
            };
            foreach (User user in users)
            {
                ctx.Users.Add(user);
            }
            await ctx.SaveChangesAsync();
            if (ctx.Projects.Any())
            {
                return false;   // DB has been seeded
            }
            var projects = new Project[]
            {
            new Project{Title="Dashboard",  StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Description = "Dashboard API/Web app to keep track of our consults"},
            new Project{Title="Microeconomics",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Description = "Dashboard API/Web app to keep track of our consults"},
            new Project{Title="Macroeconomics",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Description = "Dashboard API/Web app to keep track of our consults"},
            new Project{Title="Calculus",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Description = "Dashboard API/Web app to keep track of our consults"},
            new Project{Title="Trigonometry",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Description = "Dashboard API/Web app to keep track of our consults"},
            new Project{Title="Composition",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Description = "Dashboard API/Web app to keep track of our consults"},
            new Project{Title="Literature",StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Description = "Dashboard API/Web app to keep track of our consults"}
            };
            foreach (Project project in projects)
            {
                ctx.Projects.Add(project);
            }
            await ctx.SaveChangesAsync();
            if (ctx.Commitments.Any())
            {
                return false;   // DB has been seeded
            }
            var commitments = new Commitment[]
            {
            new Commitment{Name="Sigma It Consulting", UserId = users.Single(x => x.FirstName == "Andrés").UserId, ProjectId = projects.Single(x => x.Title == "Dashboard").ProjectId},
            new Commitment{Name="Sigma It Consulting", UserId = users.Single(x => x.FirstName == "Andrés").UserId, ProjectId = projects.Single(x => x.Title == "Dashboard").ProjectId},
            new Commitment{Name="Sigma It Consulting", UserId = users.Single(x => x.FirstName == "Andrés").UserId, ProjectId = projects.Single(x => x.Title == "Dashboard").ProjectId},
            new Commitment{Name="Sigma It Consulting", UserId = users.Single(x => x.FirstName == "Andrés").UserId, ProjectId = projects.Single(x => x.Title == "Dashboard").ProjectId},
            new Commitment{Name="Sigma It Consulting", UserId = users.Single(x => x.FirstName == "Andrés").UserId, ProjectId = projects.Single(x => x.Title == "Dashboard").ProjectId},
            new Commitment{Name="Sigma It Consulting", UserId = users.Single(x => x.FirstName == "Andrés").UserId, ProjectId = projects.Single(x => x.Title == "Dashboard").ProjectId},
            new Commitment{Name="Sigma It Consulting", UserId = users.Single(x => x.FirstName == "Andrés").UserId, ProjectId = projects.Single(x => x.Title == "Dashboard").ProjectId},

            };

            foreach (Commitment commitment in commitments)
            {
                ctx.Commitments.Add(commitment);
            }
            await ctx.SaveChangesAsync();
            return true;
         }
    }
}
