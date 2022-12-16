using GPU_Scraper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPU_Scraper.Configurations
{
    public class GPUConfiguration : IEntityTypeConfiguration<GPU>
    {
        public void Configure(EntityTypeBuilder<GPU> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.LowestPrice)
                .IsRequired()
                .HasPrecision(7, 2);

            builder.Property(x => x.HighestPrice)
                .IsRequired()
                .HasPrecision(7, 2);

            builder.HasOne(x => x.LowestPriceShop)
                .WithMany(s => s.GPUs)
                .HasForeignKey(x => x.LowestPriceShopId);

            builder.HasOne(x => x.HighestPriceShop)
                .WithMany(s => s.GPUs)
                .HasForeignKey(x => x.HighestPriceShopId);
        }
    }
}
