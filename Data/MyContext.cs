//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Data
//{
//    public class MyContext : DbContext
//    {

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer(
//                    "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = TopicsContextDataUsingPosts1"
//                );
//        }

//        public DbSet<Post> Posts { get; set; }
//        public DbSet<Tag> Tags { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<PostTag>()
//                .HasKey(t => new { t.PostId, t.TagId });

//            //modelBuilder.Entity<PostTag>()
//            //    .HasOne(pt => pt.Post)
//            //    .WithMany(p => p.PostTags)
//            //    .HasForeignKey(pt => pt.PostId);

//            //modelBuilder.Entity<PostTag>()
//            //    .HasOne(pt => pt.Tag)
//            //    .WithMany(t => t.PostTags)
//            //    .HasForeignKey(pt => pt.TagId);
//        }
//    }

//    public class Post
//    {
//        public int PostId { get; set; }
//        public string Title { get; set; }
//        public string Content { get; set; }

//        public List<PostTag> PostTags { get; set; }
//    }

//    public class Tag
//    {
//        public Tag()
//        { 
//        }
        
//        public Tag(string id)
//        {
//            TagId = id;
//        }
//        public string TagId { get; set; }

//        public List<PostTag> PostTags { get; set; }
//    }

//    public class PostTag
//    {
//        public int PostId { get; set; }

//        public string TagId { get; set; }

//    }
//}
