using GymManagementDAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configuartions
{
    internal class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.name)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(p => p.Description)
               .HasColumnType("varchar")
               .HasMaxLength(100);
            builder.Property(p => p.Price)
              .HasPrecision(10, 2);

            builder.ToTable(p =>
            {
                p.HasCheckConstraint("PlanDurationCheck", "DurationDays between 1 and 365");
            });

        }
    }
}
