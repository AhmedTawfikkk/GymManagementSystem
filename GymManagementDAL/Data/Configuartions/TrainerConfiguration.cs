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
    internal class TrainerConfiguration : GymUserConfiguration<Trainer>,IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(m => m.Created_At)
              .HasColumnName("HireDate")
              .HasDefaultValueSql("GETDATE()");
            base.Configure(builder);
        }
    }
}
