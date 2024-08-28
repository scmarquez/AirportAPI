using AirportAPI.Domain.Model;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {  
        _configuration = configuration; 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Flight> Flights { get; set; }  
}