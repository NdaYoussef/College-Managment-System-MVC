
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniManagementSystem.Domain.Models;
namespace UniManagementSystem.Infrastructure.DBContext
{
    public class UniSystemContext : IdentityDbContext<ApplicationUser>
    {
        public UniSystemContext(DbContextOptions<UniSystemContext> options)
               : base(options)
        {
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }  
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Chat> Chats { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);

            //builder.Entity<StudentCourse>()
            //    .HasKey(sc => new { sc.StudentId, sc.CourseId });

            //builder.Entity<StudentCourse>()
            //    .HasOne(sc => sc.Student)
            //    .WithMany(s => s.StudentCourses)
            //    .HasForeignKey(sc => sc.StudentId);

            //builder.Entity<StudentCourse>()
            //    .HasOne(sc => sc.Course)
            //    .WithMany(c => c.StudentCourses)
            //    .HasForeignKey(sc => sc.CourseId);
            base.OnModelCreating(builder);
            builder.Entity<UserCourse>()
           .HasKey(sc => new { sc.UserId, sc.CourseId });

            builder.Entity<UserCourse>()
           .HasOne(sc => sc.User)
           .WithMany(s => s.UserCourses)
           .HasForeignKey(sc => sc.UserId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

           // builder.Entity<StudentExam>()
           //.HasKey(se => new { se.StudentId, se.ExamId });

           // builder.Entity<StudentExam>()
           //     .HasOne(se => se.Student)
           //     .WithMany(s => s.StudentExams)
           //     .HasForeignKey(se => se.StudentId)
           //     .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<StudentExam>()
            //    .HasOne(se => se.Exam)
            //    .WithMany(e => e.StudentExams)
            //    .HasForeignKey(se => se.ExamId)
            //    .OnDelete(DeleteBehavior.Restrict);


         //   builder.Entity<Attendance>()
         //.HasOne(a => a.Student)
         //.WithMany(s => s.Attendances)
         //.HasForeignKey(a => a.StudentId)
         //.OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Attendance>()
            //    .HasOne(a => a.Course)
            //    .WithMany(c => c.Attendances)
            //    .HasForeignKey(a => a.CourseId)
            //    .OnDelete(DeleteBehavior.Restrict);


            //builder.Entity<ApplicationUser>(b =>
            //     b.Property(u => u.Role)
            //     .HasConversion<string>()
            //     .IsRequired());


            builder.Entity<ApplicationUser>()
                .Property(u => u.DateOfBirth)
                .HasColumnType("timestamp without time zone");


        }
    }
}

