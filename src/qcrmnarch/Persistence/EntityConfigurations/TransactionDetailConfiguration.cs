using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TransactionDetailConfiguration : IEntityTypeConfiguration<TransactionDetail>
{
    public void Configure(EntityTypeBuilder<TransactionDetail> builder)
    {
        // Ana tabloyu yap�land�rma
        builder.ToTable("TransactionDetails");
        builder.HasKey(td => td.Id);

        builder.Property(td => td.Id).HasColumnName("Id").IsRequired();
        builder.Property(td => td.TransactionId).HasColumnName("TransactionId").IsRequired();
        builder.Property(td => td.Quantity).HasColumnName("Quantity").IsRequired();
        builder.Property(td => td.UnitPrice).HasColumnName("UnitPrice").HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(td => td.TotalPrice).HasColumnName("TotalPrice").HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(td => td.Discount).HasColumnName("Discount").HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(td => td.Tax).HasColumnName("Tax").HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(td => td.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(td => td.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(td => td.DeletedDate).HasColumnName("DeletedDate");

        // Soft delete i�in filtre
        builder.HasQueryFilter(td => !td.DeletedDate.HasValue);

        // Transaction ili�kisini yap�land�rma
        builder.HasOne(td => td.Transaction)
            .WithMany(t => t.TransactionDetails)
            .HasForeignKey(td => td.TransactionId);

        // OfferDetail ayr� tabloya yaz�l�r
        builder.OwnsOne(td => td.TransactionOfferDetail, offer =>
        {
            offer.ToTable("TransactionOfferDetails"); // Ayr� tablo
            offer.HasKey(o => o.TransactionDetailId); // Primary key
            offer.Property(o => o.TransactionDetailId).HasColumnName("TransactionDetailId").ValueGeneratedNever();
            offer.Property(o => o.AdditionalOfferInfo).HasColumnName("AdditionalOfferInfo");
            offer.Property(o => o.Discount).HasColumnName("Discount");
        });

        // TransactionOrderDetail ayr� tabloya yaz�l�r
        builder.OwnsOne(td => td.TransactionOrderDetail, orderDetail =>
        {
            orderDetail.ToTable("TransactionOrderDetails"); // Ayr� tablo
            orderDetail.HasKey(o => o.TransactionDetailId); // Primary key
            orderDetail.Property(o => o.TransactionDetailId).HasColumnName("TransactionDetailId").ValueGeneratedNever();
            orderDetail.Property(o => o.ExpectedDeliveryDate).HasColumnName("ExpectedDeliveryDate");
        });

        // TransactionInvoiceDetail ayr� tabloya yaz�l�r
        builder.OwnsOne(td => td.TransactionInvoiceDetail, invoiceDetail =>
        {
            invoiceDetail.ToTable("TransactionInvoiceDetails"); // Ayr� tablo
            invoiceDetail.HasKey(i => i.TransactionDetailId); // Primary key
            invoiceDetail.Property(i => i.TransactionDetailId).HasColumnName("TransactionDetailId").ValueGeneratedNever();
            invoiceDetail.Property(i => i.PaymentDueDate).HasColumnName("PaymentDueDate");
        });

        // TransactionWaybillDetail ayr� tabloya yaz�l�r
        builder.OwnsOne(td => td.TransactionWaybillDetail, waybillDetail =>
        {
            waybillDetail.ToTable("TransactionWaybillDetails"); // Ayr� tablo
            waybillDetail.HasKey(w => w.TransactionDetailId); // Primary key
            waybillDetail.Property(w => w.TransactionDetailId).HasColumnName("TransactionDetailId").ValueGeneratedNever();
            waybillDetail.Property(w => w.TaxCode).HasColumnName("TaxCode");
            waybillDetail.Property(w => w.DueDate).HasColumnName("DueDate");
        });
    }
}
