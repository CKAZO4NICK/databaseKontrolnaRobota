

using KontrolnaRobota.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KontrolnaRobota.Database.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Product")
                .HasKey(check => check.Id);
        
            builder.HasOne<CheckEntity>(product => product.Check)
                .WithMany(check => check.Products)
                .HasForeignKey(product => product.CheckFK)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
