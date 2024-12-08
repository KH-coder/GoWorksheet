using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoWorksheet.Infrastructure.Models;

namespace GoWorksheet.Infrastructure.Data.Configurations
{
    public class ProblemTagConfiguration : IEntityTypeConfiguration<ProblemTag>
    {
        public void Configure(EntityTypeBuilder<ProblemTag> builder)
        {
            builder.ToTable("ProblemTags");

            builder.HasKey(pt => new { pt.ProblemID, pt.TagID });

            builder.HasOne(pt => pt.Problem)
                .WithMany(p => p.ProblemTags)
                .HasForeignKey(pt => pt.ProblemID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pt => pt.Tag)
                .WithMany(t => t.ProblemTags)
                .HasForeignKey(pt => pt.TagID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(pt => pt.ProblemID);
            builder.HasIndex(pt => pt.TagID);
        }
    }
}