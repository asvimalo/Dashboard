using Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.EntitiesMap
{
    public class PictureMap
    {
        public PictureMap(EntityTypeBuilder<Picture> entityBuilder)
        {
            //entityBuilder.HasKey(t => t.PictureId);
            //entityBuilder.Property(t => t.Title).IsRequired();
            //entityBuilder.Property(t => t.FileName).IsRequired();
            //entityBuilder.Property(t => t.User);
            

        }
    }
}
