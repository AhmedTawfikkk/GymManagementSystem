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
    internal class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.HasKey(x => new { x.SessionId, x.MemeberId });
            builder.Ignore("id");
            builder.Property(x => x.Created_At)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
