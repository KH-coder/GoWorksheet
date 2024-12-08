using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoWorksheet.Infrastructure.Models;

namespace GoWorksheet.Infrastructure.Data.Configurations
{
    public class WorksheetConfiguration : IEntityTypeConfiguration<Worksheet>
    {
        public void Configure(EntityTypeBuilder<Worksheet> builder)
        {
            builder.ToTable("Worksheets");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.Description)
                .HasMaxLength(500);

            builder.Property(w => w.TargetLevel)
                .HasMaxLength(50);

            builder.Property(w => w.CreatedBy)
                .HasMaxLength(100);

            builder.Property(w => w.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(w => w.IsPublished)
                .HasDefaultValue(false);
        }
    }
}