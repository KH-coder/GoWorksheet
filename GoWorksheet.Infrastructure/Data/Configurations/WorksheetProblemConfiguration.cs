using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoWorksheet.Infrastructure.Models;

namespace GoWorksheet.Infrastructure.Data.Configurations
{
    public class WorksheetProblemConfiguration : IEntityTypeConfiguration<WorksheetProblem>
    {
        public void Configure(EntityTypeBuilder<WorksheetProblem> builder)
        {
            builder.ToTable("WorksheetProblems");

            builder.HasKey(wp => new { wp.WorksheetID, wp.ProblemID });

            builder.Property(wp => wp.ProblemOrder)
                .IsRequired();

            builder.HasOne(wp => wp.Worksheet)
                .WithMany(w => w.WorksheetProblems)
                .HasForeignKey(wp => wp.WorksheetID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wp => wp.Problem)
                .WithMany(p => p.WorksheetProblems)
                .HasForeignKey(wp => wp.ProblemID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(wp => wp.WorksheetID);
            builder.HasIndex(wp => wp.ProblemID);
        }
    }
}