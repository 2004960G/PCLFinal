using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCL.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCL.Server.Configuration
{
    public class MovieSeedConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasData(
                new Movie
                {
                    Id = 1,
                    Name = "Marvel Avenger",
                    Description = "A good movie for nerdy couple",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Movie
                {
                    Id = 2,
                    Name = "John Wick",
                    Description = "An Action Packed Movie to keep you at the edge of your seat",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Movie
                {
                    Id = 3,
                    Name = "Lego Movie",
                    Description = "Fun Light hearted movie good for a realxing date",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
               
                new Movie
                {
                    Id = 4,
                    //Name = "Lego Movie",
                    //Description = "Fun Light hearted movie good for a realxing date",
                    Status = "Nil",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
                );
        }
    }
}
