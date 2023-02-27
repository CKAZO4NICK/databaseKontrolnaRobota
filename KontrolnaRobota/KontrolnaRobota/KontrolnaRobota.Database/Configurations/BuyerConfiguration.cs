

using KontrolnaRobota.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace KontrolnaRobota.Database.Configurations
{
    public class BuyerConfiguration : IEntityTypeConfiguration<BuyerEntity>
    {
        public void Configure(EntityTypeBuilder<BuyerEntity> builder)
        {
           builder
                .ToTable("Buyer")
                .HasKey(buyer => buyer.Id);
        }
    }
}
