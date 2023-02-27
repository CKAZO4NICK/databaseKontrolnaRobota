

using KontrolnaRobota.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KontrolnaRobota.Database.Configurations
{
    public class CheckConfiguration : IEntityTypeConfiguration<CheckEntity>
    {
        public void Configure(EntityTypeBuilder<CheckEntity> builder)
        {
            builder.ToTable("Checks")
                .HasKey(check => check.Id);

            builder.HasOne<BuyerEntity>(check => check.Buyer)
                .WithMany(buyer => buyer.Checks)
                .HasForeignKey(check => check.BuyerFK)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
