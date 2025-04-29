using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class IndustryConfiguration : IEntityTypeConfiguration<Industry>
{
    public void Configure(EntityTypeBuilder<Industry> builder)
    {
        builder.ToTable("Industries").HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id").IsRequired();
        builder.Property(i => i.Name).HasColumnName("Name").IsRequired();
        builder.Property(i => i.ParentIndustryId).HasColumnName("ParentIndustryId");
        builder.Property(i => i.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(i => i.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(i => i.DeletedDate).HasColumnName("DeletedDate");

        builder.HasMany(i => i.SubIndustries)
            .WithOne(i => i.ParentIndustry)
            .HasForeignKey(i => i.ParentIndustryId) // Foreign Key'i burada belirtiyoruz
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasData(_seedsIndustry);
        builder.HasQueryFilter(i => !i.DeletedDate.HasValue);
    }

    private IEnumerable<Industry> _seedsIndustry
    {
        get
        {
            var industries = new List<(int Id, string Name,int? ParentIndustry)>
            {
 

           (1, "Havac�l�k ve Savunma", null ),
           (2, "Tar�m ve Ormanc�l�k", null),
           (3, "Otomotiv", null),
           (4, "�� Hizmetleri", null),
           (5, "Kimyasallar", null),
           (6, "Yap�", null),
           (7, "Da��t�m, Toptan, Perakende", null),
           (8, "E�itim", null),
           (9, "Elektrik Ekipmanlar�", null),
           (10, "Elektronik", null),
           (11, "M�hendislik ve Teknik Hizmetler", null),
           (12, "G�da, ��ecek, T�t�n", null),
           (13, "H�k�met ve Askeriye", null),
           (14, "Makineler", null),
           (15, "�retim (belirtilmemi�)", null),
           (16, "T�bbi ve Sa�l�k", null),
           (17, "Metaller - Ham, �ekillendirilmi�, �retilmi�", null),
           (18, "Madencilik, Petrol ve Gaz, Ta� Oca��", null),
           (19, "Di�er", null),
           (20, "Ka��t, Ka��t �r�nleri ve Bask�", null),
           (21, "Plastikler ve Kau�uk", null),
           (22, "Tekstil, Giyim, Deri", null),
           (23, "Ta��mac�l�k ve Lojistik", null),
           (24, "Kamu Hizmetleri ve Telekom�nikasyon", null),
            };
            foreach (var ind in industries)
            {
                yield return new Industry
                {
                    Id = ind.Id,
                    Name = ind.Name,
                    ParentIndustryId = ind.ParentIndustry

                };
            }

        }
    }



}