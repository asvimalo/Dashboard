using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.EF.Db
{
    public static class DashboardContextSeedData
    {
        public static void DashboardCtxSeedData(this DashboardContext context)
        {
            // first, clear the database.  This ensures we can always start 
            // fresh with each demo.  Not advised for production environments, obviously :-)

            context.Pictures.RemoveRange(context.Pictures);
            context.SaveChanges();

            // init seed data
            var projects = new List<Project>()
            {
                new Project() {ProjectId = 1, Title = "Dashboard App", StartDate = new DateTime(2017,09,06,10,00,00), StopDate = new DateTime(2017,09,06,10,00,00), Description = "Dashboard API/Web app to keep track of our consults" }
            };
            var users = new List<User>()
            {
                new User() {UserId = 1, FirstName = "Katrina",LastName = "Rosales", PersonNr = "8602018796"},
                new User() {UserId = 2,FirstName = "Andrés",LastName = "Rosales", PersonNr = "86020183256"},
                new User() {UserId = 3,FirstName = "Kriszta",LastName = "Rosales", PersonNr = "8602017896"},
                new User() {UserId = 4,FirstName = "Jeff",LastName = "Rosales", PersonNr = "8602011234"}

            };
            var pictures = new List<Picture>()
            {
                new Picture()
                {
                     
                     Title = "Andres",
                     FileName = "andres.jpg",
                     UserId = 2,
                     
                     
                },
                new Picture()
                {
                    
                     Title = "Katrina",
                     FileName = "Katrina.jpg",
                     UserId = 1
                     
                },
                new Picture()
                {
                     UserId = 3,
                     Title = "Kriszta",
                     FileName = "Kriszta.jpg",
                     
                },
                new Picture()
                {
                     UserId = 4,
                     Title = "Jeff",
                     FileName = "Jeff.jpg",
                     
                },
                
            };
            var commitments = new List<Commitment>()
            {
                new Commitment(){CommitmentId = 1, Name = "Dashboard", UserId = 1, ProjectId = 1 },
                new Commitment(){CommitmentId = 2, Name = "Dashboard", UserId = 2, ProjectId = 1 },
                new Commitment(){CommitmentId = 3, Name = "Dashboard", UserId = 3, ProjectId = 1 },
                new Commitment(){CommitmentId = 4, Name = "Dashboard", UserId = 4, ProjectId = 1 }
            };
            context.Projects.AddRange(projects);
            context.Users.AddRange(users);
            context.Pictures.AddRange(pictures);
            context.Commitments.AddRange(commitments);
            context.SaveChanges();
        }
    }
}
