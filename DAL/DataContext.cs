﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //Взаимодействие с базой данных в Entity Framework Core происходит
    //посредством специального класса - контекста данных.
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }
        
        // Уточнения для создания моделей БД
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Модификации (Поле Емеил и Юзернейм доожны быть уникальным)
            modelBuilder
                .Entity<User>()
                .HasIndex(f => f.Email)
                .IsUnique();
            modelBuilder
               .Entity<User>()
               .HasIndex(f => f.Name)
               .IsUnique();

            /////Для сложной связи подписчиков
            modelBuilder
                  .Entity<Subscription>()
                  .HasOne(it => it.User)
                       .WithMany(it => it.Subscriptions)
                       .HasForeignKey(it => it.UserId)
                       .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .Entity<Subscription>()
                .HasOne(it => it.SubUser)
                .WithMany(it => it.Subscribers)
                .HasForeignKey(it => it.SubUserId)
                .OnDelete(DeleteBehavior.NoAction);
            ////
            ///
            //сопоставление с таблицей
            modelBuilder.Entity<Avatar>().ToTable(nameof(Avatars));
            modelBuilder.Entity<PostContent>().ToTable(nameof(PostContent));
        }


        // Переопределел метод конфигурации
        // Указывает где у нас будут прописываться миграции (API>Migrations)
        // скачать пакет npgsql для миграций
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(b => b.MigrationsAssembly("Api")); 

        // Оповещаем об появлении новых таблиц
        public DbSet<User> Users => Set <User>();
        public DbSet<UserSession> UserSessions => Set<UserSession>();
        public DbSet<Attach> Attaches => Set<Attach>();
        public DbSet<Avatar> Avatars => Set<Avatar>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<PostContent> PostContents => Set<PostContent>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
       
        public DbSet<CommentLike> CommentLikes => Set<CommentLike>();
        public DbSet<PostLike> PostLikes => Set<PostLike>();


    }
}
