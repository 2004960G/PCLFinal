using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCL.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PCL.Server.Configuration
{
    public class CafeSeedConfiguration : IEntityTypeConfiguration<Cafe>
    {
        public void Configure(EntityTypeBuilder<Cafe> builder)
        {
            builder.HasData(
                new Cafe
                {
                    Id = 1,
                    Name = "Starbucks",
                    Description = "A cozy space to chill and chat with people",
                    DateCreated = DateTime.Now.AddMonths(+5),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Cafe
                {
                    Id = 2,
                    Name = "Chock Full Of Bean",
                    Description = "Good coffee good environment For a good date",
                    DateCreated = DateTime.Now.AddMonths(+4),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Cafe
                {
                    Id = 3,
                    Name = "The Coastal Settlement",
                    Description = "OutDoor and windy place for a date",
                    DateCreated = DateTime.Now.AddMonths(+3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                 new Cafe
                 {
                     Id = 4,
                     Status = "Nil",
                     //Description = "OutDoor and windy place for a date",
                     DateCreated = DateTime.Now.AddMonths(+3),
                     DateUpdated = DateTime.Now.AddMonths(-3),
                     CreatedBy = "System",
                     UpdatedBy = "System"
                 }
                );
        }
    }
}
