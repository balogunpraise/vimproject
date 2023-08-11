using Core.Entities;
using Core.Entities.LinkingEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Course> Coureses { get; set; }
        public DbSet<MusicScore> MusicScores { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<UserAssignment> UserAssignments { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<CourseTransaction> Transactions { get; set; }
        public DbSet<InternalMessages> InternalMessages { get; set; }
        public DbSet<TutorialVideos> TutorialVideos { get; set;}
    }
}
