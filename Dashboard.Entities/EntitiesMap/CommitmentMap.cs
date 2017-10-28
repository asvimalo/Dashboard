using Dashboard.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.EntitiesMap
{
    public class CommitmentMap
    {
        public CommitmentMap(EntityTypeBuilder<Commitment> entityBuilder)
        {

            //entityBuilder.HasKey(c => c.CommitmentId);
            //entityBuilder.Property(t => t.Name).IsRequired();
            //entityBuilder.Property(t => t.ProjectId).IsRequired();
            //entityBuilder.Property(t => t.UserId).IsRequired();
            //entityBuilder.Property(t => t.Project).IsRequired();
            //entityBuilder.Property(t => t.Project).IsRequired();

        }
    }
}
