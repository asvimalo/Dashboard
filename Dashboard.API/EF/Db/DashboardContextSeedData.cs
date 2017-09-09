using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard.API.EF.Db
{
    public static class DashboardContextSeedData
    {
        
        public static void SeedData(DashboardContext ctx)
        {
            // first, clear the database.  This ensures we can always start 
            // fresh with each demo.  Not advised for production environments, obviously :-)
            ctx.Database.EnsureCreated();
            //_ctx.Commitments.RemoveRange(_ctx.Commitments);
            //_ctx.Pictures.RemoveRange(_ctx.Pictures);
            //_ctx.Projects.RemoveRange(_ctx.Projects);
            //_ctx.Users.RemoveRange(_ctx.Users);
            //context.SaveChanges();

            if (!ctx.Users.Any() || !ctx.Pictures.Any() || !ctx.Projects.Any() || !ctx.Commitments.Any())
            {
                var consult1 = new User() { FirstName = "Katrina", LastName = "Rosales", PersonNr = "8602018796" };
                var consult2 = new User() { FirstName = "Andrés", LastName = "Rosales", PersonNr = "86020183256" };
                var consult3 = new User() { FirstName = "Kriszta", LastName = "Rosales", PersonNr = "8602017896" };
                var consult4 = new User() { FirstName = "Jeff", LastName = "Rosales", PersonNr = "8602011234" };
                // init seed data
                var project = new Project() { Title = "Dashboard App", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Description = "Dashboard API/Web app to keep track of our consults", };
                ctx.Projects.Add(project);

                var users = new List<User>()
                {
                    consult1,
                    consult2,
                    consult3,
                    consult4
                };

                ctx.Users.AddRange(users);
                #region Pictures
                var pictures = new List<Picture>()
                {
                        new Picture()
                        {

                             Title = "Andres",
                             FileName = "andres.jpg",


                        },
                        new Picture()
                        {

                             Title = "Katrina",
                             FileName = "Katrina.jpg",


                        },
                        new Picture()
                        {

                             Title = "Kriszta",
                             FileName = "Kriszta.jpg",


                        },
                        new Picture()
                        {

                             Title = "Jeff",
                             FileName = "Jeff.jpg",


                        }

                };
                ctx.Pictures.AddRange(pictures);
                #endregion
                #region Commitments
                var commitments = new List<Commitment>()
            {
                new Commitment()
                {

                    Name = "Dashboard",
                    User = consult1,
                    Project = project

                },
                new Commitment()
                {

                    Name = "Dashboard",
                    User = consult2,
                    Project = project
                },
                new Commitment()
                {

                    Name = "Dashboard",
                    User = consult3,
                    Project = project
                },
                new Commitment()
                {

                    Name = "Dashboard",
                    User = consult4,
                    Project = project
                }
            };
                #endregion



                ctx.Commitments.AddRange(commitments);
                ctx.SaveChanges();
            }
            else
                return;
        }
    }
}
