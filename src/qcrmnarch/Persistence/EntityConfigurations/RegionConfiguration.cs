using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Application.Dtos;
using Nest;

namespace Persistence.EntityConfigurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("Regions").HasKey(r => r.Id);

        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.Name).HasColumnName("Name").IsRequired();
        builder.Property(r => r.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(r => r.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(r => r.DeletedDate).HasColumnName("DeletedDate");


        builder.HasMany(c => c.Cities)
            .WithOne(ca => ca.Region)
            .HasForeignKey(ca => ca.RegionId) // Foreign Key'i burada belirtiyoruz
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Country)
            .WithMany(ca => ca.Regions)
            .HasForeignKey(ca => ca.CountryId) // Foreign Key'i burada belirtiyoruz
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(r => !r.DeletedDate.HasValue);
        builder.HasData(_seedsRegion);

    }
    private IEnumerable<Region> _seedsRegion
    {
        get
        {
            var regions = new List<(int Id,int CountryId, string Name)>
            {
            (1, 225 , "Akdeniz B�lgesi"),
            (2, 225 , "Do�u Anadolu B�lgesi"),
            (3, 225 , "Ege B�lgesi"),
            (4, 225 , "G�neydo�u Anadolu B�lgesi"),
            (5, 225 , "�� Anadolu B�lgesi"),
            (6, 225 , "Karadeniz B�lgesi"),
            (7, 225 , "Marmara B�lgesi")
            };
            foreach (var reg in regions)
            {
                yield return new Region
                {
                    Id = reg.Id,
                    CountryId = reg.CountryId,
                    Name = reg.Name

                };
            }

        }
    }


}