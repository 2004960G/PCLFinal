using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCL.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PCL.Server.Configuration
{
    public class DinnerSeedConfiguration : IEntityTypeConfiguration<Dinner>
    {
        public void Configure(EntityTypeBuilder<Dinner> builder)
        {
            builder.HasData(
                new Dinner
                {
                    Id = 1,
                    Name = "Spize",
                    Description = " A romantic place to go to",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Dinner
                {
                    Id=2,
                    Name = "Supper Club",
                    Description= "Fun place windy atmosphere overall a nice place for a date",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Dinner
                {
                    Id = 3,
                    Name = "Coachman Inn Restaurant",
                    Description = "A Popular Spot For Couples",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Dinner
                {
                    Id = 4,
                    Name = "Whitegrass Restaurant",
                    Description = "A French Fare With Japanese Techniques",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Dinner
                {
                    Id = 5,
                    Status = "Nil",
                    //Description = "Fun place windy atmosphere overall a nice place for a date",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
                );
        }
    }
}
