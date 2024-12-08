using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoWorksheet.Infrastructure.Models;

namespace GoWorksheet.Infrastructure.Data.Configurations
{
    public class ProblemTypeConfiguration : IEntityTypeConfiguration<ProblemType>
    {
        public void Configure(EntityTypeBuilder<ProblemType> builder)
        {
            builder.ToTable("ProblemTypes");

            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pt => pt.Description)
                .HasMaxLength(500);

            builder.Property(pt => pt.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}