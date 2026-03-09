using System.Data;
using ApnaDhobi.Infrastructure.Models;

namespace ApnaDhobi.Infrastructure.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection Create(DbSettings dbSettings);
}
