﻿using Dashboard.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dashboard.EntitiesMap
{
    public class ProjectMap
    {
        public ProjectMap(EntityTypeBuilder<Project> entityBuilder)
        {
            //entityBuilder.HasKey(t => t.ProjectId);
            //entityBuilder.Property(t => t.Title).IsRequired();
            //entityBuilder.Property(t => t.StartDate).IsRequired();
            //entityBuilder.Property(t => t.StopDate).IsRequired();

        }
    }
}