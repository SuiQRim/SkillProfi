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
                    Status = "Получена",
                    Created = DateTime.Now
                },
                new Consultation()
                {
                    Id = Guid.NewGuid(),
                    Name = "Тест2",
                    Description="Тест2",
                    EMail="Test1@gmail.com",
                    Status = "В работе",
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
                    PictureName= "Проект1", 
                    Created = DateTime.Now
                },
                new SkillProfi.Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Тест2",
                    Description="Тест2",
                    PictureName = "Проект2",
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
                    PictureName="Блог1",
                    Created = DateTime.Now
                },
                new Blog()
                {
                    Id = Guid.NewGuid(),
                    Title = "Тест2",
                    Description="Тест2",
                    PictureName="Блог2",
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
        }
    }
}
