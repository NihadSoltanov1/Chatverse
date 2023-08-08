using Chatverse.Application.Common.Interfaces;
using Chatverse.Domain.Entities;
using Chatverse.Domain.Identity;
using Chatverse.Infrastructure.Persistance.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Infrastructure.Persistance
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>, IApplicationDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _interceptor;
        public AppDbContext(
                          DbContextOptions<AppDbContext> options,
                          AuditableEntitySaveChangesInterceptor interceptor)
                          : base(options)
        {
            _interceptor = interceptor;
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImage> PostImages { get ; set ; }
        public DbSet<City> Cities { get ; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Friendship>()
                .HasOne(f => f.Sender)
                .WithMany(u => u.SentFriendRequests)
                .HasForeignKey(f => f.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(f => f.Receiver)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>()
                .HasOne(p => p.AppUser)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<City>()
               .HasOne(p => p.Country)
               .WithMany(u => u.Cities)
               .HasForeignKey(p => p.CountryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppUser>()
              .HasOne(p => p.Country)
              .WithMany(u => u.AppUsers)
              .HasForeignKey(p => p.CountryId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppUser>()
             .HasOne(p => p.City)
             .WithMany(u => u.AppUsers)
             .HasForeignKey(p => p.CityId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(l => l.AppUser)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Restrict);
           builder.Entity<PostImage>().HasOne(i=>i.Post).WithMany(i=>i.PostImages).HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_interceptor);

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }

}
