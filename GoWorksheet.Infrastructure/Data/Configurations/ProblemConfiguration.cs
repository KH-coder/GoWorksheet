using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoWorksheet.Infrastructure.Models;

namespace GoWorksheet.Infrastructure.Data.Configurations
{
    public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
    {
        public void Configure(EntityTypeBuilder<Problem> builder)
        {
            builder.ToTable("Problems");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.TypeID)
                .IsRequired();

            builder.Property(p => p.DifficultyID)
                .IsRequired();

            builder.Property(p => p.ImagePath)
                .HasMaxLength(255);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(p => p.Type)
                .WithMany(t => t.Problems)
                .HasForeignKey(p => p.TypeID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Difficulty)
                .WithMany(d => d.Problems)
                .HasForeignKey(p => p.DifficultyID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.TypeID);
            builder.HasIndex(p => p.DifficultyID);
        }
    }
}