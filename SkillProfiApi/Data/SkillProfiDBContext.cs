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
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении

        }
        public DbSet<Consultation> Consultations { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Picture> Pictures { get; set; }

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

            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();

            modelBuilder.Entity<Post>().HasData(new Post[]
            {
                new Post()
                {
                    Id = Guid.NewGuid(),
                    PictureId= id1,
                    Title = "Тест1",
                    Description="Тест1",
                    Created = DateTime.Now
                },
                new Post()
                {
                    Id = Guid.NewGuid(),
                    PictureId= id2,
                    Title = "Тест2",
                    Description="Тест2",
                    Created = DateTime.Now
                }
            });

            modelBuilder.Entity<Picture>().HasData(new Picture[]
            {
                new Picture()
                {
                    Id = id1,
                    Name = "Картина 1",
                },
                new Picture()
                {
                    Id = id2,
                    Name = "Картина 2",
                }
            });

        }
    }
}
