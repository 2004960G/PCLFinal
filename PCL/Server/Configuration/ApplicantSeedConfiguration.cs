using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCL.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PCL.Server.Configuration
{
    public class ApplicantSeedConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.HasData(
                new Applicant
                {
                    Id = 1,
                    Name = "Sarah Teo",
                    //Description = "A cozy space to chill and chat with people",
                    ContactNumber = "94756288",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Applicant
                {
                    Id = 2,
                    Name = "James adam",
                    //Description = "Good coffee good environment For a good date",
                    ContactNumber = "94572844",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Applicant
                {
                    Id = 3,
                    Name = "Adam Tan",
                    //Description = "OutDoor and windy place for a date",
                    ContactNumber = "86774922",
                    DateCreated = DateTime.Now.AddMonths(-3),
                    DateUpdated = DateTime.Now.AddMonths(-3),
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
                );
        }
    }
}
