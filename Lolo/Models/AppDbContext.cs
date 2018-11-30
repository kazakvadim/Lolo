using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models
{
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Blog>()
                .HasOne(p => p.User)
                .WithMany()
                .IsRequired();
            //modelBuilder.Entity<Tag>()
            //    .HasOne(p => p.User)
            //    .WithMany()
            //    .IsRequired();
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithOne();
            modelBuilder.Entity<PostTag>()
                .HasKey(t => new { t.PostId, t.TagId });

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.Tags)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>()
               .HasOne(pt => pt.Tag)
               .WithMany(t => t.PostTags)
               .HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<Post>()
             .HasMany<Comment>(p => p.Comments)
             .WithOne();


            base.OnModelCreating(modelBuilder);
        }

    }
}
