using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.EF.Db
{
    public class DashboardContextSeedData
    {
        private DashboardContext _ctx;

        public DashboardContextSeedData(DashboardContext ctx)
        {
            _ctx = ctx;
        }
        public  void DashboardCtxSeedData()
        {
            // first, clear the database.  This ensures we can always start 
            // fresh with each demo.  Not advised for production environments, obviously :-)

            //context.Pictures.RemoveRange(context.Pictures);
            //context.SaveChanges();

            // init seed data
            //var project = new Project() { Title = "Dashboard App", StartDate = new DateTime(2017, 09, 06, 10, 00, 00), StopDate = new DateTime(2017, 09, 06, 10, 00, 00), Description = "Dashboard API/Web app to keep track of our consults", };

            var users = new List<User>()
            {
                new User() {FirstName = "Katrina",LastName = "Rosales", PersonNr = "8602018796"},
                new User() {FirstName = "Andrés",LastName = "Rosales", PersonNr = "86020183256"},
                new User() {FirstName = "Kriszta",LastName = "Rosales", PersonNr = "8602017896"},
                new User() {FirstName = "Jeff",LastName = "Rosales", PersonNr = "8602011234"}

            };
            #region Pictures
            //var pictures = new List<_Picture>()
            //{
            //    new _Picture()
            //    {
            //         PictureId = 1,
            //         Title = "Andres",
            //         FileName = "andres.jpg",
            //         UserId = 2

            //    },
            //    new _Picture()
            //    {
            //         PictureId = 2,
            //         Title = "Katrina",
            //         FileName = "Katrina.jpg",
            //         UserId = 1

            //    },
            //    new _Picture()
            //    {
            //         PictureId = 3,
            //         Title = "Kriszta",
            //         FileName = "Kriszta.jpg",
            //         UserId = 1

            //    },
            //    new _Picture()
            //    {
            //         PictureId = 4,
            //         Title = "Jeff",
            //         FileName = "Jeff.jpg",
            //         UserId = 1

            //    },

            //}; 
            #endregion
            #region Commitments
            //var commitments = new List<Commitment>()
            //{
            //    new Commitment()
            //    {
            //        CommitmentId = 1,
            //        Name = "Dashboard",
            //        User = new User() {FirstName = "Katrina",LastName = "Rosales", PersonNr = "8602018796"},
            //        Project = project

            //    },
            //    new Commitment()
            //    {
            //        CommitmentId = 2,
            //        Name = "Dashboard",
            //        User = new User() {FirstName = "Katrina",LastName = "Rosales", PersonNr = "8602018796"},
            //        Project = project
            //    },
            //    new Commitment()
            //    {
            //        CommitmentId = 3,
            //        Name = "Dashboard",
            //        UserId = 3,
            //        Project = project
            //    },
            //    new Commitment()
            //    {
            //        CommitmentId = 4,
            //        Name = "Dashboard",
            //        UserId = 4,
            //        Project = project
            //    }
            //}; 
            #endregion
            //_ctx.Projects.Add(project);
            _ctx.Users.AddRange(users);
           // _ctx.Pictures.AddRange(pictures);
            //_ctx.Commitments.AddRange(commitments);
            _ctx.SaveChanges();
        }
    }
}
