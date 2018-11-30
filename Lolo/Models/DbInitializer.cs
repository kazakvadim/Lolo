using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models
{
    public class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            //foreach (var a in context.Tags)
            //{
            //    context.Tags.Remove(a);
            //}
            //context.SaveChanges();
            if (!context.Tags.Any())
            {
                context.AddRange
                (
                    new Tag { Title = "Traveling" },
                    new Tag { Title = "Food" },
                    new Tag { Title = "Hobby" }
                );
            }
            context.SaveChanges();
            //var y = context.Tags.ToList();
            //foreach (var a in context.Posts)
            //{
            //    context.Posts.Remove(a);
            //}
            //context.SaveChanges();
            var z = context.Tags.Where(p => p.Title == "Traveling").ToList();
            if (!context.Posts.Any())
            {
                context.AddRange
                (
                new Post
                {
                    Title = "The first post",
                    DatePublished = DateTime.Now,
                    Content = "bla bla bla",
                    PostUrl = "unsplash.jpg",
                    Blog = context.Blogs.Single(p => p.Title == "Home"),
                    //Tags = context.Tags.Where(p => p.Title == "Traveling").ToList()
                },
                new Post
                {
                    Title = "The second post",
                    DatePublished = DateTime.Now,
                    Content = "bla bla bla",
                    PostUrl = "unsplash2.jpg",
                    Blog = context.Blogs.FirstOrDefault(p => p.Title == "Home")
                    //Tags = context.Tags.Where(p => p.Title == "Food").ToList()
                },
                new Post
                {
                    Title = "The third post",
                    DatePublished = DateTime.Now,
                    PostUrl = "unsplash3.jpg",
                    Content = "bla bla bla",
                    Blog = context.Blogs.FirstOrDefault(p => p.Title == "Home")
                },
                new Post
                {
                    Title = "The fourth post",
                    DatePublished = DateTime.Now,
                    PostUrl = "unsplash4.jpg",
                    Content = "bla bla bla",
                    Blog = context.Blogs.FirstOrDefault(p => p.Title == "Home")
                }
                );
            }
          

            context.SaveChanges();
        }
    }
}
