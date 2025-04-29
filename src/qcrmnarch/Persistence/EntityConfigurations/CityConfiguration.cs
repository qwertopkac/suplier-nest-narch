using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities").HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.Name).HasColumnName("Name").IsRequired();
        builder.Property(c => c.CountryId).HasColumnName("CountryId").IsRequired();
        builder.Property(c => c.RegionId).HasColumnName("RegionId").IsRequired();
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(ca => ca.Region)
       .WithMany(c => c.Cities)
       .HasForeignKey(ca => ca.RegionId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ca => ca.Country)
        .WithMany(c => c.Cities)
        .HasForeignKey(ca => ca.CountryId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
        builder.HasData(_seedsCity);


    }
    private IEnumerable<City> _seedsCity
    {
        get
        {
            var cities = new List<(int Id, int CountryId, int RegionId, string Name)>
            {
                 (1,225, 1, "Adana" ),
                 (2,225, 4, "Ad�yaman" ),
                 (3,225, 3, "Afyonkarahisar" ),
                 (4,225, 2, "A�r�" ),
                 (5,225, 6, "Amasya"),
                 (6,225, 5, "Ankara"),
                 (7,225, 1, "Antalya"),
                 (8,225, 6, "Artvin"),
                 (9,225, 3, "Ayd�n"),
                 (10,225, 7, "Bal�kesir"),
                 (11,225, 7, "Bilecik"),
                 (12,225, 2, "Bing�l"),
                 (13,225, 2, "Bitlis"),
                 (14,225, 6, "Bolu"),
                 (15,225, 1, "Burdur"),
                 (16,225, 7, "Bursa"),
                 (17,225, 7, "�anakkale"),
                 (18,225, 5, "�ank�r�"),
                 (19,225, 6, "�orum"),
                 (20,225, 3, "Denizli"),
                 (21,225, 4, "Diyarbak�r"),
                 (22,225, 7, "Edirne"),
                 (23,225, 2, "Elaz��"),
                 (24,225, 2, "Erzincan"),
                 (25,225, 2, "Erzurum"),
                 (26,225, 5, "Eski�ehir"),
                 (27,225, 4, "Gaziantep"),
                 (28,225, 6, "Giresun"),
                 (29,225, 6, "G�m��hane"),
                 (30,225, 2, "Hakkari"),
                 (31,225, 1, "Hatay"),
                 (32,225, 1, "Isparta"),
                 (33,225, 1, "Mersin"),
                 (34,225, 7, "�stanbul"),
                 (35,225, 3, "�zmir"),
                 (36,225, 2, "Kars"),
                 (37,225, 6, "Kastamonu"),
                 (38,225, 5, "Kayseri"),
                 (39,225, 7, "K�rklareli"),
                 (40,225, 5, "K�r�ehir"),
                 (41,225, 7, "Kocaeli"),
                 (42,225, 5, "Konya"),
                 (43,225, 3, "K�tahya"),
                 (44,225, 2, "Malatya"),
                 (45,225, 3, "Manisa"),
                 (46,225, 1, "Kahramanmara�"),
                 (47,225, 4, "Mardin"),
                 (48,225, 3, "Mu�la"),
                 (49,225, 2, "Mu�"),
                 (50,225, 5, "Nev�ehir"),
                 (51,225, 5, "Ni�de"),
                 (52,225, 6, "Ordu"),
                 (53,225, 6, "Rize"),
                 (54,225, 7, "Sakarya"),
                 (55,225, 6, "Samsun"),
                 (56,225, 4, "Siirt"),
                 (57,225, 6, "Sinop"),
                 (58,225, 5, "Sivas"),
                 (59,225, 7, "Tekirda�"),
                 (60,225, 6, "Tokat"),
                 (61,225, 6, "Trabzon"),
                 (62,225, 2, "Tunceli"),
                 (63,225, 4, "�anl�urfa"),
                 (64,225, 3, "U�ak"),
                 (65,225, 2, "Van"),
                 (66,225, 5, "Yozgat"),
                 (67,225, 6, "Zonguldak"),
                 (68,225, 5, "Aksaray"),
                 (69,225, 6, "Bayburt"),
                 (70,225, 5, "Karaman"),
                 (71,225, 5, "K�r�kkale"),
                 (72,225, 4, "Batman"),
                 (73,225, 4, "��rnak"),
                 (74,225, 6, "Bart�n"),
                 (75,225, 2, "Ardahan"),
                 (76,225, 2, "I�d�r"),
                 (77,225, 7, "Yalova"),
                 (78,225, 6, "Karab�k"),
                 (79,225, 4, "Kilis"),
                 (80,225, 1, "Osmaniye"),
                 (81,225, 6, "D�zce")
            };
            foreach (var city in cities)
            {
                yield return new City
                {
                    Id = city.Id,
                    CountryId = city.CountryId,
                    RegionId = city.RegionId,
                    Name = city.Name
                };
            }

        }
    }

}