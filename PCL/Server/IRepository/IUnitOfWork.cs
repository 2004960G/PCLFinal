using PCL.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCL.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Movie> Movies { get; }
        IGenericRepository<Dinner> Dinners { get; }
        IGenericRepository<Cafe> Cafes { get; }
        IGenericRepository<Festival> Festivals { get; }
        IGenericRepository<MyActivity> MyActivities { get; }
        IGenericRepository<Romance> Romances { get; }
        IGenericRepository<Applicant> Applicants { get; }
    }
}
