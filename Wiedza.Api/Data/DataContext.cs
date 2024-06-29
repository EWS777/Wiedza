using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data.Models;
using Wiedza.Core.Models;
using Wiedza.Core.Models.Complaints;

namespace Wiedza.Api.Data;

public sealed class DataContext : DbContext
{
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<AttachmentFile> Files { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageFile> MessageFiles { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Publication> Publications { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Verification> Verifications { get; set; }
    public DbSet<WebsiteBalance> WebsiteBalances { get; set; }
    public DbSet<Withdraw> Withdraws { get; set; }

    public DbSet<MessageComplaint> MessageComplaints { get; set; }
    public DbSet<PersonComplaint> PersonComplaints { get; set; }
    public DbSet<PublicationComplaint> PublicationComplaints { get; set; }

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