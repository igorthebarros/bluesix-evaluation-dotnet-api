using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> entity)
        {
            entity.ToTable("SaleItem", schema: "sale_item_schema");

            entity.HasKey(x => x.Id).HasName("PK_SALE_ITEM");
            entity.HasIndex(x => x.SaleId).HasDatabaseName("IX_SALE_ITEM_SALE_ID").IsUnique();

            entity.Property(x => x.SaleId)
                .IsRequired();

            entity.Property(x => x.ProductId)
                .IsRequired();

            entity.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Quantity)
                .IsRequired();

            entity.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(si => si.IsDiscountAvailable)
                .IsRequired();

            entity.Property(si => si.WasDiscountApplied)
                .IsRequired();

            entity.Property(si => si.AmountOfDiscountApplied)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("now()");

            entity.Property(x => x.UpdatedAt)
                .HasColumnName("UpdatedAt");

            entity.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
