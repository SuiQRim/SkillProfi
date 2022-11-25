using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using SkillProfi;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata;

namespace SkillProfiApi.Data
{
    public class SkillProfiDbContext : DbContext
    {
        public SkillProfiDbContext(DbContextOptions<SkillProfiDbContext> options)
            : base(options) => Database.EnsureCreated();   // создаем базу данных при первом обращении

        public DbSet<Consultation> Consultations { get; set; }

        public DbSet<SkillProfi.Project> Projects { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Consultation>().HasData(new Consultation[]
            {
                new Consultation()
                {
                    Id = Guid.NewGuid(),
                    Name = "Тест1",
                    Description="Тест1",
                    EMail="Test1@gmail.com",
                    Status = ConsultationStatus.Received,
                    Created = DateTime.Now
                },
                new Consultation()
                {
                    Id = Guid.NewGuid(),
                    Name = "Тест2",
                    Description="Тест2",
                    EMail="Test1@gmail.com",
                    Status = ConsultationStatus.Completed,
                    Created = DateTime.Now
                }
            });

            modelBuilder.Entity<SkillProfi.Project>().HasData(new SkillProfi.Project[]
            {
                new SkillProfi.Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Тест1",
                    Description="Тест1",
                    PictureName= "0d01043b-d33b-4cbc-9099-df3485de8e81",
                    Created = DateTime.Now
                },
                new SkillProfi.Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Тест2",
                    Description="Тест2",
                    PictureName = Guid.NewGuid().ToString(),
                    Created = DateTime.Now
                }
            });

            modelBuilder.Entity<Blog>().HasData(new Blog[]
            {
                new Blog()
                {
                    Id = Guid.NewGuid(),
                    Title = "Тест1",
                    Description="Тест1",
                    PictureName="4b79de2e-91f2-48a8-a64c-a66ab0dc19fc",
                    Created = DateTime.Now
                },
                new Blog()
                {
                    Id = Guid.NewGuid(),
                    Title = "Тест2",
                    Description="Тест2",
                    PictureName= Guid.NewGuid().ToString(),
                    Created = DateTime.Now
                }
            });

            modelBuilder.Entity<Service>().HasData(new Service[]
            {
                new Service()
                {
                    Id = Guid.NewGuid(),
                    Title = "Тест1",
                    Description="Тест1",
                    Created = DateTime.Now
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    Title = "Тест2",
                    Description="Тест2",
                    Created = DateTime.Now
                }
           });

            modelBuilder.Entity<Account>().HasData(new Account[]
            {
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Name = "admin",
                    Password= "admin",
                }
            });
        }
    }
}
