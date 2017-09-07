using Dashboard.Data.EF.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Data.Entities;

namespace Dashboard.API.EF.Repository
{
    public class PictureRepo : IPictureRepo
    {
        public void AddPicture(Picture picture)
        {
            throw new NotImplementedException();
        }

        public void DeletePicture(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Picture> GetActivities()
        {
            throw new NotImplementedException();
        }

        public Picture UpdatePicture(Picture picture)
        {
            throw new NotImplementedException();
        }
    }
}
