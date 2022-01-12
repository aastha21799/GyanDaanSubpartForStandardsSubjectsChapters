using Microsoft.EntityFrameworkCore;
using System;
using Domain;

namespace Data
{
    public class TopicContext : DbContext
    {
        public DbSet<Standard> Standards { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<StandardSubject> StandardSubjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                    "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = LatestTable04;"
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relationship Many to Many
            modelBuilder.Entity<StandardSubject>()
                .HasIndex(s => new { s.standardId, s.subjectId}).IsUnique();

            modelBuilder.Entity<StandardSubject>()
                .HasOne(s => s.standard)
                .WithMany(s => s.standardSubjects)
                .HasForeignKey(s => s.standardId);

            modelBuilder.Entity<StandardSubject>()
                .HasOne(s => s.subject)
                .WithMany(s => s.standardSubjects)
                .HasForeignKey(s => s.subjectId);

            //Relationship One to Many
            modelBuilder.Entity<Topic>()
                .HasIndex(s => new { s.topicName, s.level }).IsUnique();
            modelBuilder.Entity<Topic>()
                .HasOne(s => s.standardSubject)
                .WithMany(s => s.topics)
                .HasForeignKey(s => s.standardSubjectId);
                
        }
    }
}
