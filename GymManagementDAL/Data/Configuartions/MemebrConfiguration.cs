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
    internal class MemebrConfiguration:GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(m => m.Created_At)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");
            base.Configure(builder);
        }
    }
}
