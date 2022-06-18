using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WPFSample.Domain;
using WPFSample.Repository.Implementation;

namespace WPFSample.Repository.Mapping
{
    public class ProductMap : MappingBase, IEntityTypeConfiguration<Product>
    {
        protected override string TABLE_NAME => "Product";
        private readonly string TITLE_COLUMN = "Title";
        private readonly string DESCRIPTION_COLUMN = "Description";
        private readonly string QUANTITY_COLUMN = "Quantity";
        private readonly string PRICE_COLUMN = "Price";


        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).
                HasColumnName(ID_COLUMN);

            builder.Property(p => p.Title)
                .HasColumnName(TITLE_COLUMN)
                .HasMaxLength(MaxLengthConstValues.MAX_LENGTH_PRODUCT_TITLE)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName(DESCRIPTION_COLUMN)
                .HasMaxLength(MaxLengthConstValues.MAX_LENGTH_PRODUCT_DESCRIPTION);

            builder.Property(p => p.Quantity)
                .HasColumnName(QUANTITY_COLUMN)
                .HasMaxLength(MaxLengthConstValues.MAX_LENGTH_PRODUCT_QUANTITY);

            builder.Property(p => p.Price)
                .HasColumnName(PRICE_COLUMN)
                .HasMaxLength(MaxLengthConstValues.MAX_LENGTH_PRODUCT_PRICE)
                .IsRequired();
        }
    }
}
