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
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.name)
                 .HasColumnType("varchar")
                 .HasMaxLength(50);

            builder.Property(e => e.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(e => e.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("ValidEmailCheck", "Email Like '_%@_%._%'");
                t.HasCheckConstraint("validPhoneCheck", "Phone Like '01%' and Phone not like '%[^0-9]%'");

            });
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            builder.OwnsOne(c => c.address, Addressbuiler =>
            {
                Addressbuiler.Property(a => a.street)
                .HasColumnType("varchar")
                .HasColumnName("Street")
                .HasMaxLength(30);
                Addressbuiler.Property(a => a.City)
               .HasColumnType("varchar")
               .HasColumnName("City")
               .HasMaxLength(30);

                Addressbuiler.Property(a => a.BuildingNumber)
                .HasColumnName("BuildingNumber");
            });
        }
    }
}
