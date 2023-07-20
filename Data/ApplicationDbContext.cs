using Microsoft.EntityFrameworkCore;
using CVAPI.Models;

namespace CVAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

            
        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Qualification> Qualification { get; set; }
        public DbSet<WorkExperience> WorkExperience { get; set; }
        public DbSet<Referee> Referee { get; set; }
    }
}
