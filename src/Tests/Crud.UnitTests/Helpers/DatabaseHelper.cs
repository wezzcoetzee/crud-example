using Crud.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Crud.UnitTests.Helpers;

public class DatabaseHelper
{
    public DbContextOptions<ApplicationDbContext> GetInMemoryDb => new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "crud")
        .Options;
}