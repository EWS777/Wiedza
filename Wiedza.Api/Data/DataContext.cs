using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data.Models;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;

namespace Wiedza.Api.Data;

public sealed class DataContext : DbContext
{
    public DbSet<Publication> Publications { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Complaint> Complaints { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<AttachmentFile> Files { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageFile> MessageFiles { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Service> Services { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Verification> Verifications { get; set; }
    public DbSet<WebsiteBalance> WebsiteBalances { get; set; }
    public DbSet<Withdraw> Withdraws { get; set; }
    
    public DbSet<PersonComplaint> PersonComplaints { get; set; }
    public DbSet<PublicationComplaint> PublicationComplaints { get; set; }
    public DbSet<UserSalt> UserSalts { get; set; }


    private static bool _isFirstCreation = true;

    public DataContext(DbContextOptions<DataContext> options, ILogger<DataContext> logger) : base(options)
    {
        if (_isFirstCreation is false) return;
        try
        {
            Database.Migrate();
        }
        catch(Exception exception)
        {
            logger.LogCritical(exception, "Database migration is failed!");
        }

        _isFirstCreation = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}