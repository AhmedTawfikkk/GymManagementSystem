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
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("members").HasKey(h=>h.Id);
            builder.HasOne<Member>()
                .WithOne(m => m.HealthRecord)
                .HasForeignKey<HealthRecord>(h => h.Id);

            builder.Ignore(h => h.Created_At);
            builder.Ignore(h => h.Updated_At);   // made by default from entity
        }
    }
}
