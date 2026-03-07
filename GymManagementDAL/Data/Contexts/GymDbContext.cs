using GymManagementDAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Contexts
{
    public class GymDbContext:IdentityDbContext<ApplicationUser>
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                    
                 b.Property(x => x.FirstName)
                .HasColumnType("varchar")
                .HasMaxLength(50);
                b.Property(x => x.LastName)
                .HasColumnType("varchar")
                .HasMaxLength(50);
               
              }

               );
                
                
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
