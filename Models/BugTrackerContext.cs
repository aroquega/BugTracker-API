using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    public class BugTrackerContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<IssueLabel> IssueLabels { get; set; }
        
        public BugTrackerContext(DbContextOptions<BugTrackerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IssueLabel>().HasKey(il => new
            {
                il.IssueId,
                il.LabelId
            });
        }
    }
}