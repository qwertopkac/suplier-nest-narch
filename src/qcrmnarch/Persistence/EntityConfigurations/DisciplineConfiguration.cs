using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.InteropServices;
using System;

namespace Persistence.EntityConfigurations;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.ToTable("Disciplines").HasKey(d => d.Id);

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.Name).HasColumnName("Name").IsRequired();
        builder.Property(d => d.Code).HasColumnName("Code");
        builder.Property(d => d.Description).HasColumnName("Description");
        builder.Property(d => d.JobFunctionId).HasColumnName("JobFunctionId").IsRequired();
        //builder.Property(d => d.JobFunction).HasColumnName("JobFunction").IsRequired();
        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");
       

        builder.HasData(_seedsDiscipline);
        builder.HasQueryFilter(d => !d.DeletedDate.HasValue);
    }

    private IEnumerable<Discipline> _seedsDiscipline
    {
        get
        {
            var disciplines = new List<(int Id, int jobfunctionId, string Name,string Code)>
            {
                (1,1,"Elektrik","Electrical"),
                (2,1,"Mekanik","Mechanical"),
                (3,1,"End�stri","Industrial"),
                (4,1,"Tasar�m","Design"),
                (5,1,"�n�aat","Civil"),
                (6,1,"Mimari","Architectural"),
                (7,1,"Bilgisayar Bilimi","ComputerScience"),
                (8,1,"Havac�l�k ve Uzay","Aerospace"),
                (9,1,"�evre","Environmental"),
                (10,1,"Kimya","Chemical"),
                (11,1,"Dan��manl�k","Consulting"),
                (12,1,"Di�er","Other"),

                (13,2,"Tedarik Zinciri","SupplyChain"),
                (14,2,"Sat�n Alma","Purchasing"),
                (15,2,"Tedarik","Procurement"),
                (16,2,"Lojistik","Logistics"),
                (17,2,"Toplu Ta��ma","Transportation"),
                (18,2,"Depolama","Warehousing"),
                (19,2,"Da��t�m","Distribution"),
                (20,2,"Envanter","Inventory"),
                (21,2,"Malzemeler","Materials"),
                (22,2,"Dan��manl�k","Consulting"),

                (23,3,"�retim M�hendisli�i","ProductEngineering"),
                (24,3,"�retim Y�netimi","ProductManagement"),
                (25,3,"Kalite Kontrol","Quality"),
                (26,3,"Operasyonlar","Operations"),
                (27,3,"Bak�m & Onar�m","MaintenanceRepair"),
                (28,3,"Dan��manl�k","Consulting"),

                (29,4,"M�hendislik Y�netimi", "ManagementEngineering"),
                (30,4,"Tedarik Y�netimi","ManagementProcurement"),
                (31,4,"Operasyon Y�netimi", "ManagementOperations"),
                (32,4,"BT","Management IT"  ),

                ( 33,5,"Sat��","Sales"),
                ( 34,5,"Pazarlama","SalesMarketing"),
                ( 35,5,"Sat�� ve Pazarlama","SalesSalesMarketing"),
                ( 36,5,"Acenteler","SalesAgency"),
                ( 37,5,"Dan��manl�k","SalesConsultant"),

                (38,6,"Dan��man","Other Consultant"),
                (39,6,"��renci","Other Student"),
                (40,6,"Emekli","Other Retired"),
                (41,6,"E�itimci","Other Educator"),


            };
            foreach (var dis in disciplines)
            {
                yield return new Discipline
                {
                    Id = dis.Id,
                    JobFunctionId = dis.jobfunctionId,
                    Name = dis.Name,
                    Code = dis.Code

                };
            }

        }
    }
}