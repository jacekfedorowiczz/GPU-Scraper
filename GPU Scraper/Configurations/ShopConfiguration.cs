using GPU_Scraper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPU_Scraper.Configurations
{
    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.URL)
                .IsRequired();
        }
    }
}
