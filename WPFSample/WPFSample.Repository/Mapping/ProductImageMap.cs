using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WPFSample.Domain;
using WPFSample.Repository.Implementation;

namespace WPFSample.Repository.Mapping
{
    public class ProductImageMap : MappingBase, IEntityTypeConfiguration<ProductImage>
    {
        protected override string TABLE_NAME => "ProductImage";
        private const string PATH_COLUMN = "Path";
        private const string PRODUCT_ID_COLUMN = "ProductId";
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(p => p.ProductId);

            builder.Property(p => p.Id)
                .HasColumnName(ID_COLUMN);

            builder.Property(p => p.Path)
                .HasColumnName(PATH_COLUMN)
                .HasMaxLength(MaxLengthConstValues.MAX_LENGTH_PATH_IMAGE)
                .IsRequired();

            builder.Property(p => p.ProductId)
                .HasColumnName(PRODUCT_ID_COLUMN)
                .IsRequired();
        }
    }
}
