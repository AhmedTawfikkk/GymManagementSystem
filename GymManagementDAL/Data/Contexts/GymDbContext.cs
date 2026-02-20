using GymManagementDAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Contexts
{
    public class GymDbContext:DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options): base(options) 
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.; Database=GymManagement; Trusted_connection=true; TrustServerCertificate=true;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Member> members { get; set; }  
        public DbSet<HealthRecord> healthRecords { get; set; }
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<Plan> Plans {  get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<MemberSession> memberSessions { get; set; }
        public DbSet<MemebrShip> memebrPlans { get; set; }

    }
}
