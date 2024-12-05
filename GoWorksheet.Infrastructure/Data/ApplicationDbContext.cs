using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public required DbSet<ProblemType> ProblemTypes { get; set; }
    public required DbSet<DifficultyLevel> DifficultyLevels { get; set; }
    public required DbSet<Problem> Problems { get; set; }
    public required DbSet<Worksheet> Worksheets { get; set; }
    public required DbSet<Tag> Tags { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorksheetProblem>()
            .HasKey(wp => new { wp.WorksheetID, wp.ProblemID });
            
        modelBuilder.Entity<ProblemTag>()
            .HasKey(pt => new { pt.ProblemID, pt.TagID });
            
        modelBuilder.Entity<Tag>()
            .HasIndex(t => t.Name)
            .IsUnique();
    }
}