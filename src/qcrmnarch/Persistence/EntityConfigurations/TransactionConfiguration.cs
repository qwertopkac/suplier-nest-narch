using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;
public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        // Ana 'Transactions' tablosunu yap�land�rma
        builder.ToTable("Transactions");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
        builder.Property(t => t.CompanyId).HasColumnName("CompanyId").IsRequired();
        builder.Property(t => t.TransactionType).HasColumnName("TransactionType").IsRequired();
        builder.Property(t => t.TransactionDate).HasColumnName("TransactionDate").IsRequired();
        builder.Property(t => t.Status).HasColumnName("Status").HasMaxLength(50).IsRequired();
        builder.Property(t => t.TotalAmount).HasColumnName("TotalAmount").HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(t => t.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(t => t.DeletedDate).HasColumnName("DeletedDate");

        // Soft delete i�in filtre
        builder.HasQueryFilter(t => !t.DeletedDate.HasValue);

        // �irket ili�kisini yap�land�rma
        builder.HasOne(t => t.Company)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CompanyId);

        // TransactionOffer ayr� tabloya yaz�l�r
        builder.OwnsOne(t => t.TransactionOffer, offer =>
        {
            offer.ToTable("TransactionOffers");
            offer.Property(o => o.TransactionId).HasColumnName("TransactionId").ValueGeneratedNever();
            offer.HasKey(o => o.TransactionId);
            offer.Property(o => o.SpecialOfferNote).HasColumnName("SpecialOfferNote");
        });

        // TransactionOrder ayr� tabloya yaz�l�r
        builder.OwnsOne(t => t.TransactionOrder, order =>
        {
            order.ToTable("TransactionOrders");
            order.Property(o => o.TransactionId).HasColumnName("TransactionId").ValueGeneratedNever();
            order.HasKey(o => o.TransactionId);
            order.Property(o => o.OrderDate).HasColumnName("OrderDate");
        });

        // TransactionInvoice ayr� tabloya yaz�l�r
        builder.OwnsOne(t => t.TransactionInvoice, invoice =>
        {
            invoice.ToTable("TransactionInvoices");
            invoice.Property(i => i.TransactionId).HasColumnName("TransactionId").ValueGeneratedNever();
            invoice.HasKey(i => i.TransactionId);
            invoice.Property(i => i.InvoiceDate).HasColumnName("InvoiceDate");
        });

        // TransactionWaybill ayr� tabloya yaz�l�r
        builder.OwnsOne(t => t.TransactionWaybill, waybill =>
        {
            waybill.ToTable("TransactionWaybills");
            waybill.Property(w => w.TransactionId).HasColumnName("TransactionId").ValueGeneratedNever();
            waybill.HasKey(w => w.TransactionId);
            waybill.Property(w => w.Note).HasColumnName("Note");
        });

        // TransactionDetails ili�kisini yap�land�rma
        builder.HasMany(t => t.TransactionDetails)
            .WithOne(td => td.Transaction)
            .HasForeignKey(td => td.TransactionId);
    }
}
