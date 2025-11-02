using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Forum.BL.Domain;
using Microsoft.Extensions.Logging;

namespace Forum.DAL.EF;

public class UserDbContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserMessage> UserMessages { get; set; } 

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite("Data Source=User.db");

        optionsBuilder.LogTo(message =>
            Debug.WriteLine(message), LogLevel.Information);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserMessage>()
            .HasKey(um => um.Id);

        modelBuilder.Entity<UserMessage>()
            .HasOne(um => um.User)
            .WithMany(u => u.UserMessages)
            .HasForeignKey("FK_UserMessage_UserId");

        modelBuilder.Entity<UserMessage>()
            .HasOne(um => um.Message)
            .WithMany(m => m.UserMessages)
            .HasForeignKey("FK_UserMessage_MessageId");

        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Message>().HasKey(m => m.Id);
        modelBuilder.Entity<Comment>().HasKey(c => c.Id);
        
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ParentMessage)
            .WithMany(m => m.Comments);
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Name)
            .IsUnique();

        modelBuilder.Entity<Message>()
            .HasIndex(m => m.Content)
            .IsUnique();
    }
    
    public bool CreateDatabase(bool dropDatabase)
    {
        if (dropDatabase)
        {
            Database.EnsureDeleted();
        }
        return Database.EnsureCreated();
    }
}