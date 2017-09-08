using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dashboard.Data.Entities
{
    public class ProjectMap
    {
        public ProjectMap(EntityTypeBuilder<Project> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Title).IsRequired();
            entityBuilder.Property(t => t.Description).IsRequired();
            entityBuilder.Property(t => t.StartDate).IsRequired();
            entityBuilder.Property(t => t.StopDate).IsRequired();

        }
    }
}
