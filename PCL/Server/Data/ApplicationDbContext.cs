using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PCL.Server.Configuration;
using PCL.Server.Models;
using PCL.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCL.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MyActivity>MyActivities { get; set; }
        public DbSet<Romance> Romances { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CafeSeedConfiguration());
            builder.ApplyConfiguration(new DinnerSeedConfiguration());
            builder.ApplyConfiguration(new MovieSeedConfiguration());
            builder.ApplyConfiguration(new FestivalSeedConfiguration());
            builder.ApplyConfiguration(new ApplicantSeedConfiguration());
        }
    }
}
