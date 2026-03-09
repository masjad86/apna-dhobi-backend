using ApnaDhobiCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ApnaDhobi.Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories;

public interface IAppDbContext
{
    DbSet<User> Users {get;set;}
    DbSet<Customer> Customers {get;set;}
    DbSet<Vendor> Vendors {get;set;}
}
