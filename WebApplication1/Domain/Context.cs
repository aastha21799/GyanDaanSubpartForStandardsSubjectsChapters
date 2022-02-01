using Microsoft.EntityFrameworkCore;
using System;

namespace WebApplication1.Domain
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            :base(options)
        {
            //Can disable tracking if we are not doing anything with the retreived data
            //See the get apis
            //They fetch the data but don't make any changes to it
            //Later on tracking behaviour needs to be added and this line needs to be removed
            //Learn more about tracking behaviour
            //TBD
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        public DbSet<Standard> Standards { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<StandardSubject> StandardSubjects { get; set; }

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
            modelBuilder.Entity<Chapter>()
                .HasIndex(s => new { s.chapterName, s.level }).IsUnique();
            modelBuilder.Entity<Chapter>()
                .HasOne(s => s.standardSubject)
                .WithMany(s => s.chapters)
                .HasForeignKey(s => s.standardSubjectId);
                
        }
    }
}
