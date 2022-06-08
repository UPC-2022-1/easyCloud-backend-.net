using Microsoft.EntityFrameworkCore;

namespace easyCloud.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<User.Domain.Models.User> Users { get; set; }
    public DbSet<Record.Domain.Models.Record> Records { get; set; }
    public DbSet<Quote.Domain.Models.Quote> Quotes { get; set; }
    public DbSet<Provider.Domain.Models.Provider> Providers { get; set; }
    private readonly IConfiguration _configuration;
        
    public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySQL(_configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User.Domain.Models.User>().ToTable("Users");
        builder.Entity<User.Domain.Models.User>().HasKey(p => p.Id);
        builder.Entity<User.Domain.Models.User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User.Domain.Models.User>().Property(p => p.Name).IsRequired().HasMaxLength(30);
        builder.Entity<User.Domain.Models.User>().Property(p => p.Phone).IsRequired();
        builder.Entity<User.Domain.Models.User>().Property(p => p.Email).IsRequired().HasMaxLength(60);
        builder.Entity<User.Domain.Models.User>().Property(p => p.Password).IsRequired().HasMaxLength(30);
        builder.Entity<Quote.Domain.Models.Quote>().ToTable("Quotes");
        builder.Entity<Quote.Domain.Models.Quote>().HasKey(p => p.Id);
        builder.Entity<Quote.Domain.Models.Quote>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Quote.Domain.Models.Quote>().Property(p => p.Date).IsRequired();
        builder.Entity<Quote.Domain.Models.Quote>().Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Entity<Quote.Domain.Models.Quote>().Property(p => p.Price).IsRequired().HasMaxLength(5);
        builder.Entity<Quote.Domain.Models.Quote>().Property(p => p.CloudService).IsRequired().HasMaxLength(20);
        builder.Entity<Quote.Domain.Models.Quote>().Property(p => p.Title).IsRequired().HasMaxLength(20);
        builder.Entity<Quote.Domain.Models.Quote>().Property(p => p.UserId).IsRequired();
        builder.Entity<Record.Domain.Models.Record>().ToTable("Records");
        builder.Entity<Record.Domain.Models.Record>().HasKey(p => p.Id);
        builder.Entity<Record.Domain.Models.Record>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        
        builder.Entity<Record.Domain.Models.Record>().Property(p => p.ProviderId).IsRequired();
        builder.Entity<Record.Domain.Models.Record>().Property(p => p.QuoteId).IsRequired();
        builder.Entity<Record.Domain.Models.Record>().Property(p => p.UserId).IsRequired();
        
        builder.Entity<Provider.Domain.Models.Provider>().ToTable("Providers");
        builder.Entity<Provider.Domain.Models.Provider>().Property(p=>p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Provider.Domain.Models.Provider>().Property(p => p.Name).IsRequired().HasMaxLength(30);
        builder.Entity<Provider.Domain.Models.Provider>().Property(p => p.Website).IsRequired();
    }
}