using Microsoft.EntityFrameworkCore;

namespace InsertMillionRecords;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<TaskDetails> TaskDetails { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskComments> TaskComments { get; set; }
    public DbSet<TaskAttachments> TaskAttachments { get; set; }
    public DbSet<TaskHistory> TaskHistories { get; set; }
    public DbSet<TaskTags> TaskTags { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User -> Project (One-to-Many)
        modelBuilder.Entity<Project>()
            .HasOne(p => p.User)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Project -> TaskDetails (One-to-Many)
        modelBuilder.Entity<TaskDetails>()
            .HasOne(td => td.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(td => td.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // TaskDetails -> TaskComments (One-to-Many)
        modelBuilder.Entity<TaskComments>()
            .HasOne(tc => tc.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(tc => tc.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // TaskDetails -> TaskAttachments (One-to-Many)
        modelBuilder.Entity<TaskAttachments>()
            .HasOne(ta => ta.Task)
            .WithMany(t => t.Attachments)
            .HasForeignKey(ta => ta.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // TaskDetails -> TaskHistory (One-to-Many)
        modelBuilder.Entity<TaskHistory>()
            .HasOne(th => th.Task)
            .WithMany(t => t.Histories)
            .HasForeignKey(th => th.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        // TaskDetails -> TaskTags (One-to-Many)
        modelBuilder.Entity<TaskTags>()
            .HasOne(tt => tt.Task)
            .WithMany(t => t.Tags)
            .HasForeignKey(tt => tt.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }


}
