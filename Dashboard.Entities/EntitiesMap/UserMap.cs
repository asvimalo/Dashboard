using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Entities
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<Employee> entityBuilder)
        {
            //entityBuilder.HasKey(t => t.UserId);
            //entityBuilder.Property(t => t.FirstName).IsRequired();
            //entityBuilder.Property(t => t.LastName).IsRequired();
            //entityBuilder.Property(t => t.PersonNr).IsRequired();
            

        }
    }
}
