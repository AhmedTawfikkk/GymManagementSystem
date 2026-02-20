using GymManagementDAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configuartions
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(s =>
            {
                s.HasCheckConstraint("SessionCapacityCheck", "Capacity between 1 and 25");
                s.HasCheckConstraint("SessionEndDateCheck", "EndDate > StartDate");
            });

            builder.HasOne(s=>s.category)
                .WithMany(c=>c.Sessions)
                .HasForeignKey(s=>s.categoryId);

            builder.HasOne(s => s.trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId);      
        }
    }
}
