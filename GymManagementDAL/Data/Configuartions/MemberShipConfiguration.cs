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
    internal class MemberShipConfiguration : IEntityTypeConfiguration<MemebrShip>
    {
        public void Configure(EntityTypeBuilder<MemebrShip> builder)
        {
            builder.Property(m => m.Created_At)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");
                

            builder.HasKey(x => new { x.MemberId, x.Planid });
            builder.Ignore(x=>x.Id);
        }
    }
}
