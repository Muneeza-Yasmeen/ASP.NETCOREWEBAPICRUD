using Microsoft.EntityFrameworkCore;

namespace ASP.NETCOREWEBAPICRUD.Context
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        public DbSet<Users> User { get; set; }
        public DbSet<Taskss> Tasks { get; set; }
        public DbSet<Categories> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary key for Users
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Name);
                entity.Property(e => e.Name).ValueGeneratedNever();
            });

            // Configure foreign key relationships for Taskss
            modelBuilder.Entity<Taskss>(entity =>
            {
                entity.HasOne(t => t.User)
                      .WithMany()
                      .HasForeignKey(t => t.Name)
                      .HasPrincipalKey(u => u.Name);

                entity.HasOne(t => t.Category)
                      .WithMany()
                      .HasForeignKey(t => t.CategoryId);
            });

            // Seed data for Users
            modelBuilder.Entity<Users>().HasData(
                new Users { Name = "John", Designation = "Developer", Address = "New York", Password = "XYZ1Inc", Email = "2021cs661@student.uet.edu.pk" },
                new Users { Name = "Chris", Designation = "Manager", Address = "New York", Password = "ABC2Inc", Email = "2021cs662@student.uet.edu.pk" },
                new Users { Name = "Mukesh", Designation = "Consultant", Address = "New Delhi", Password = "XYZ3Inc", Email = "2021cs663@student.uet.edu.pk" }
            );

            // Seed data for Categories
            modelBuilder.Entity<Categories>().HasData(
                new Categories { CategoryId = 1, Name = "Work" },
                new Categories { CategoryId = 2, Name = "Home" },
                new Categories { CategoryId = 3, Name = "Leisure" }
            );

            // Seed data for Tasks
            modelBuilder.Entity<Taskss>().HasData(
                new Taskss { TaskId = 1, Title = "Initial Task 1", Description = "This is a seed task", IsCompleted = false, Priority = 1, DueDate = DateTime.Now.AddDays(7), Name = "John", CategoryId = 1 },
                new Taskss { TaskId = 2, Title = "Initial Task 2", Description = "This is another seed task", IsCompleted = false, Priority = 2, DueDate = DateTime.Now.AddDays(10), Name = "Chris", CategoryId = 2 }
            );
        }
    }
}
