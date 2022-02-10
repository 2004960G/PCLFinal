using PCL.Server.Data;
using PCL.Server.IRepository;
using PCL.Server.Models;
using PCL.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PCL.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Movie> _movies;
        private IGenericRepository<Dinner> _dinners;
        private IGenericRepository<Festival> _festivals;
        private IGenericRepository<Cafe> _cafes;
        private IGenericRepository<Romance> _romances;
        private IGenericRepository<MyActivity> _myactivities;
        private IGenericRepository<Applicant> _applicants;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Movie> Movies
            => _movies ??= new GenericRepository<Movie>(_context);
        public IGenericRepository<Dinner> Dinners
            => _dinners ??= new GenericRepository<Dinner>(_context);
        public IGenericRepository<Festival> Festivals
            => _festivals ??= new GenericRepository<Festival>(_context);
        public IGenericRepository<Cafe> Cafes
            => _cafes ??= new GenericRepository<Cafe>(_context);
        public IGenericRepository<Romance> Romances
            => _romances ??= new GenericRepository<Romance>(_context);
        public IGenericRepository<MyActivity> MyActivities
            => _myactivities ??= new GenericRepository<MyActivity>(_context);
        public IGenericRepository<Applicant> Applicants
            => _applicants ??= new GenericRepository<Applicant>(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
