using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories").HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.Name).HasColumnName("Name").IsRequired();
        builder.Property(c => c.Description).HasColumnName("Description").IsRequired();
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        // Self-referencing relationship for subcategories
        builder.HasOne(c => c.ParentCategory)
               .WithMany(c => c.SubCategories)
               .HasForeignKey(c => c.ParentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(builder => builder.Products)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);  
        
        builder.HasMany(builder => builder.CompanyCategories)
               .WithOne(cc => cc.Category)
               .HasForeignKey(cc => cc.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(_seedsCategory);
        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
    }

 
        private static IEnumerable<Category> _seedsCategory
        {
            get
            {
                var categories = new List<(int Id, string Name, string Description, int? ParentCategoryId)>
            {
                (1, "Metal Malzemeler", "Metaller ve ala��mlar", null),
                (2, "Plastik ve Polimer Malzemeler", "Plastik ve polimer bazl� malzemeler", null),
                (3, "Ah�ap ve Ah�ap �r�nleri", "Ah�ap bazl� malzemeler", null),
                (4, "Seramik ve Cam Malzemeler", "Seramik ve cam malzemeler", null),
                (5, "�n�aat Malzemeleri", "�n�aat ve yap� malzemeleri", null),
                (6, "Kimya ve Kimyasal Malzemeler", "Kimyasal maddeler ve �r�nler", null),
                (7, "Tekstil ve Tekstil �r�nleri", "Tekstil ve kuma� �r�nleri", null),
                (8, "Elektronik ve Elektrik Malzemeleri", "Elektronik ve elektrik bile�enleri", null),
                (9, "Enerji Malzemeleri", "Enerji �retim ve depolama malzemeleri", null),
                (10, "Otomotiv Malzemeleri", "Otomotiv sekt�r� i�in malzemeler", null),
                (11, "T�bbi ve Sa�l�k Malzemeleri", "T�bbi cihazlar ve sa�l�k �r�nleri", null),
                (12, "Ambalaj Malzemeleri", "Ambalaj sekt�r�ne y�nelik malzemeler", null),
                (13, "Tar�m ve Tar�m Malzemeleri", "Tar�m makineleri ve �r�nleri", null),
                (14, "Havac�l�k ve Uzay Malzemeleri", "Havac�l�k ve uzay teknolojileri", null),
                (15, "Di�er �zel Malzemeler", "�zel ve ileri teknoloji malzemeler", null),

                // Metal Malzemeler
                (16, "�elik", "Paslanmaz �elik, yap� �eli�i", 1),
                (17, "Al�minyum", "Al�minyum �r�nleri", 1),
                (18, "Bak�r", "Bak�r malzemeleri", 1),
                (19, "Pirin�", "Pirin� ala��mlar�", 1),
                (20, "Bronz", "Bronz malzemeleri", 1),
                (21, "D�k�m Malzemeleri", "Demir d�k�m, �elik d�k�m", 1),
                (22, "Tel ve Kablo Malzemeleri", "Metal bazl� kablolar", 1),

                // Plastik ve Polimer Malzemeler
                (23, "Termoplastikler", "PVC, PE, PP, ABS", 2),
                (24, "Termosetler", "Epoksi, polyester, poli�retan", 2),
                (25, "Kau�uk Malzemeler", "Do�al ve sentetik kau�uk", 2),
                (26, "Kompozit Malzemeler", "Cam elyaf, karbon fiber", 2),

                // Ah�ap ve Ah�ap �r�nleri
                (27, "Kereste", "�am, me�e, kay�n", 3),
                (28, "Kontrplak", "Ah�ap levhalar", 3),
                (29, "MDF ve Sunta", "Mobilya yap�m malzemeleri", 3),
                (30, "Ah�ap Kaplamalar", "Ah�ap y�zey kaplamalar�", 3),

                // Seramik ve Cam Malzemeler
                (31, "Seramik Karolar", "�� ve d�� kaplamalar", 4),
                (32, "Cam Malzemeler", "D�z cam, temperli cam, lamine cam", 4),
                (33, "Refrakter Malzemeler", "Y�ksek s�cakl��a dayan�kl� seramikler", 4),

                // �n�aat Malzemeleri
                (34, "�imento ve Beton �r�nleri", "Binalar ve altyap�lar i�in", 5),
                (35, "Tu�la ve Briket", "Duvar malzemeleri", 5),
                (36, "Yal�t�m Malzemeleri", "Strafor, ta� y�n�, cam y�n�", 5),
                (37, "�at� Malzemeleri", "Kiremit, �elik �at�, membranlar", 5),
                (38, "Boya ve Kaplamalar", "�� ve d�� boya malzemeleri", 5),

                // Kimya ve Kimyasal Malzemeler
                (39, "End�striyel Kimyasallar", "Asitler, bazlar, ��z�c�ler", 6),
                (40, "Yap��t�r�c�lar ve S�zd�rmazl�k Malzemeleri", "Farkl� end�striyel uygulamalar", 6),
                (41, "Boya ve Kaplama Kimyasallar�", "Boyama i�lemleri i�in", 6),
                (42, "Temizlik Kimyasallar�", "Ev ve end�striyel kullan�mlar", 6),

                // Tekstil ve Tekstil �r�nleri
                (43, "Do�al Lifler", "Pamuk, y�n, ipek", 7),
                (44, "Sentetik Lifler", "Polyester, naylon, akrilik", 7),
                (45, "Tekstil Kuma�lar�", "�rme, dokuma, non-woven", 7),
                (46, "Tekstil Kimyasallar�", "Boya, apre malzemeleri", 7),

                 // Elektronik ve Elektrik Malzemeleri
                (50, "Elektronik Komponentler", "Diren�, kapasit�r, entegre devreler", 8),
                (51, "Kablo ve Ba�lant� Elemanlar�", "Elektrik ba�lant� ekipmanlar�", 8),
                (52, "Piller ve Bataryalar", "Enerji depolama ��z�mleri", 8),
                (53, "Yar� �letken Malzemeler", "Elektronik devre elemanlar�", 8),

                // Enerji Malzemeleri
                (54, "Yenilenebilir Enerji Malzemeleri", "G�ne� panelleri, r�zgar t�rbini par�alar�", 9),
                (55, "Petrol ve Gaz End�strisi Malzemeleri", "Boru, vana, pompa", 9),
                (56, "Enerji Depolama Malzemeleri", "Lityum iyon piller, yak�t h�creleri", 9),

                // Otomotiv Malzemeleri
                (57, "Otomotiv Par�alar�", "Motor par�alar�, �asi, aksesuarlar", 10),
                (58, "Lastik ve Jantlar", "Ara� lastikleri ve jantlar", 10),
                (59, "Otomotiv Elektroni�i", "Ara� i�i elektronik sistemler", 10),

                // T�bbi ve Sa�l�k Malzemeleri
                (60, "T�bbi Cihazlar ve Ekipmanlar", "Hastane ve sa�l�k ekipmanlar�", 11),
                (61, "Cerrahi Malzemeler", "Ameliyat ve t�bbi m�dahale ekipmanlar�", 11),
                (62, "Medikal Tekstil �r�nleri", "T�bbi kullan�m i�in tekstil", 11),
                (63, "Biyomalzemeler", "Biyomedikal uygulamalar i�in malzemeler", 11),

                // Ambalaj Malzemeleri
                (64, "Karton ve Ka��t Ambalaj", "Karton kutu, ka��t torba", 12),
                (65, "Plastik Ambalaj", "Po�et, �i�e, kutu", 12),
                (66, "Metal Ambalaj", "Teneke kutu, al�minyum folyo", 12),
                (67, "Cam Ambalaj", "Cam �i�eler ve kaplar", 12),

                // Tar�m ve Tar�m Malzemeleri
                (68, "Tar�m Makineleri ve Ekipmanlar�", "Tar�m sekt�r�ne y�nelik makineler", 13),
                (69, "G�bre ve Toprak D�zenleyiciler", "Tar�m i�in kimyasal ve organik g�breler", 13),
                (70, "Sulama Sistemleri", "Tar�msal sulama ��z�mleri", 13),
                (71, "Sera Malzemeleri", "Sera yap�m� ve ekipmanlar�", 13),

                // Havac�l�k ve Uzay Malzemeleri
                (72, "Kompozit Malzemeler", "Karbon fiber, aramid fiber", 14),
                (73, "Y�ksek Performansl� Ala��mlar", "Havac�l�k ve uzay i�in �zel ala��mlar", 14),

                // Di�er Malzemeler
                (47, "Nanomalzemeler", "Geli�mi� nanoteknoloji malzemeleri", 15),
                (48, "Ak�ll� Malzemeler", "�ekil haf�zal� ala��mlar, piezoelektrik malzemeler", 15),
                (49, "Biyobozunur Malzemeler", "�evre dostu malzemeler", 15)
            };

                foreach (var cat in categories)
                {
                    yield return new Category
                    {
                        Id = cat.Id,
                        Name = cat.Name,
                        Description = cat.Description,
                        ParentCategoryId = cat.ParentCategoryId
                    };
                }
            }
        }
    }

