using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCL.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCL.Server.Configuration
{
    public class FestivalSeedConfiguration : IEntityTypeConfiguration<Festival>
    {
        public void Configure(EntityTypeBuilder<Festival> builder)
        {
            builder.HasData(
                new Festival
                {
                    Id = 1,
                    Name = "ZoukOut Singapore",
                    Description = "A place to party with your significant other",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Festival
                {
                    Id = 2,
                    Name = "Ultra Singapore",
                    Description = "A place to party with your significant other",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Festival
                {
                    Id = 3,
                    Status = "Nil",
                    //Description = "A place to party with your significant other",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
                );
        }
    }
}
