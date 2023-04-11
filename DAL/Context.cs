using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;

namespace DAL;

public class Context: DbContext
{
    private readonly IConfiguration _config;
    public Context(IConfiguration config): base()
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_config["DB_CONNECTION_STRING"]);
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Organization>()
            .HasData(new List<Organization>
            {
                new()
                {
                    Id = 1,
                    Name = "ООО БУП"
                },
                new()
                {
                    Id = 2,
                    Name = "ОАО СУС"
                }
            });
        modelBuilder.Entity<User>()
            .HasData(new List<User>()
            {
                new()
                {
                    Id = 1,
                    OrganizationId = 1,
                    LastName = "Зубенко",
                    FirstName = "Михаил",
                    MiddleName = "Петрович",
                    PhoneNumber = "8-800-555-35-35",
                    Email = "sus@gmail.com"
                },
                new()
                {
                    Id = 2,
                    OrganizationId = 2,
                    LastName = "Гагарин",
                    FirstName = "Василий",
                    MiddleName = "Петрович",
                    PhoneNumber = "8-800-555-35-35",
                    Email = "some@gmail.com"
                }
            });
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> User { get; set; }

    public DbSet<Organization> Organization { get; set; }
}