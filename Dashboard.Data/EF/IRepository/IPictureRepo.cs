using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.EF.IRepository
{
    interface IPictureRepo
    {
        ICollection<Picture> GetActivities();
        void AddPicture(Picture picture);
        Picture UpdatePicture(Picture picture);
        void DeletePicture(int id);
    }
}
