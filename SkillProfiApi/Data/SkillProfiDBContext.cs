﻿using Microsoft.EntityFrameworkCore;
using SkillProfi;
using SkillProfi.Blog;
using SkillProfi.Consult;
using SkillProfi.Project;
using SkillProfi.Service;
using SkillProfiApi.Models;

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

        public DbSet<Project> Projects { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<UserAccount> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Consultation>().HasData(new Consultation[]
            {
                new Consultation()
                {
                    Id = Guid.NewGuid(),
                    Name = "Roman",
                    Description="Срочно нужна помощь?",
                    EMail="roman_super@gmail.com",
                    Status = ConsultationStatus.Received,
                    Created = new(2022,8,10)
                },                
                new Consultation()
				{
					Id = Guid.NewGuid(),
					Name = "Joseph",
					Description="oooooh my god",
					EMail="Joseph@gmail.com",
					Status = ConsultationStatus.Received,
					Created = new(2022,11,26)
				},
                new Consultation()
				{
					Id = Guid.NewGuid(),
					Name = "А как написать вам в поддержку?",
					Description="Oleg",
					EMail="Oleg_Tinkov@gmail.com",
					Status = ConsultationStatus.Completed,
					Created = new(2022,12,1)
				},
				new Consultation()
                {
                    Id = Guid.NewGuid(),
                    Name = "Max",
                    Description="Хочу записатся",
                    EMail="Max@gmail.com",
                    Status = ConsultationStatus.Completed,
                    Created = DateTime.Now
                }

            });

            modelBuilder.Entity<Project>().HasData(new Project[]
            {
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Новый регламент Formula E",
                    Description="Мы совместно с FIA, создаем новый регламент, который даст командам пространство для внедрения новых технологий, сохраняя безопастность и зрелещность гонок.",
                    PictureName= "0d01043b-d33b-4cbc-9099-df3485de8e81",
                    Created = DateTime.Now
                },
                new Project()
                {
                    Id = Guid.NewGuid(),
                    Title = "Owlboy - Игра, которая покорит милионы сердец",
                    Description="Скоро на выставке электронных развлечений (E3) мы представим абсолютно гениальную игру. Подобных ей просто не сущетсвует.",
                    PictureName = "42c6a28c-7fd5-4596-96a5-07cd145d4263",
                    Created = DateTime.Now
                }
            });

            modelBuilder.Entity<Blog>().HasData(new Blog[]
            {
                new Blog()
                {
                    Id = Guid.NewGuid(),
                    Title = "Почему Olija инди игра, не реализовавшая свой потенциал",
                    Description="Olija неплохая инди игра. Но уровни-головоломки, которые являются главными геймплейными состовляющими этоой игры, были слабо сделаны. В Оле можно было хотя бы на час игры сделать больше уровней головоломками. Механики в игре идеальнно подходят для этого, но увы головоломок слишком мало и сделаны они так, что ты попробовал новую механику и забыл. Особенности игры почти тебе не понадобится, потому что в бою малопригодны, а головоломок с ней почти нет.\r\n\r\nИсточник - Отзыв Сукрима в Steam\r\nhttps://steamcommunity.com/id/SuiQRim/recommended/1297330/",
                    PictureName="4b79de2e-91f2-48a8-a64c-a66ab0dc19fc",
                    Created = DateTime.Now
                },
                new Blog()
                {
                    Id = Guid.NewGuid(),
                    Title = "Велосезон 2022",
                    Description="Покатался по 5 трассам в Лен-области. Все трассы требовали высокой техники от марафонщика. Вообще марафоны было тяжело назвать марафонами. Они скорее гонки. Доехать не легко, но у всех цель побить личный рекорд или занять высокую позицию. Дух пробирал.",
                    PictureName= "b50a2dcd-714f-4703-804c-bfe84c181223",
                    Created = DateTime.Now
                }
            });

            modelBuilder.Entity<Service>().HasData(new Service[]
            {
                new Service()
                {
                    Id = Guid.NewGuid(),
                    Title = "Персональное менторство",
                    Description="Мы предоставим вам личного ментора.",
                    Created = DateTime.Now
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    Title = "Проводник в Гейм-индустрию",
                    Description="Получите помощь от величайшего сукрима (от др. геймерского SuiQRim)",
                    Created = DateTime.Now
                }
           });

            modelBuilder.Entity<UserAccount>().HasData(new UserAccount[]
            {
                new UserAccount()
                {
                    Id = Guid.NewGuid(),
                    Login = "admin",
                    Password= "admin",
                }
            });
        }
    }
}
