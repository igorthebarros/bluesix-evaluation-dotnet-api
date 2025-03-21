using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> entity)
        {
            entity.ToTable("Sales", schema: "sales_schema");

            entity.HasKey(x => x.Id).HasName("PK_SALE");
            entity.HasIndex(x =>  x.Id).HasDatabaseName("IX_SALE").IsUnique();


            entity.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.CustomerId)
                .IsRequired()
                .HasColumnName("CustomerId");

            entity.Property(x => x.PriceTotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");


            entity.Property(x => x.ProductsTotalAmount)
                .IsRequired();

            entity.Property(x => x.Branch)
                .HasColumnName("Branch")
                .HasMaxLength(100);

            entity.Property(x => x.IsCanceled)
                .IsRequired();

            entity.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("now()");

            entity.Property(x => x.UpdatedAt)
                .HasColumnName("UpdatedAt");
        }
    }
}
