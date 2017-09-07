using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.EF.IRepository
{
    interface IAccountRepo
    {
        ICollection<User> GetUsers();
        void AddUser(User user);
        User UpdateUser(User user);
        void DeleteUser(int id);

    }
}
