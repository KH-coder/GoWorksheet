using Microsoft.EntityFrameworkCore;
using GoWorksheet.Infrastructure.Models;
using GoWorksheet.Infrastructure.Data.Configurations;
using GoWorksheet.Infrastructure.Models.Common;
using Microsoft.Extensions.DependencyInjection;

namespace GoWorksheet.Infrastructure.Data
{
    public class GoWorksheetContext : DbContext
    {
        public GoWorksheetContext(DbContextOptions<GoWorksheetContext> options)
            : base(options)
        {
        }

        public DbSet<Problem> Problems { get; set; }
        public DbSet<ProblemType> ProblemTypes { get; set; }
        public DbSet<DifficultyLevel> DifficultyLevels { get; set; }
        public DbSet<Worksheet> Worksheets { get; set; }
        public DbSet<WorksheetProblem> WorksheetProblems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProblemTag> ProblemTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations
            modelBuilder.ApplyConfiguration(new ProblemConfiguration());
            modelBuilder.ApplyConfiguration(new ProblemTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DifficultyLevelConfiguration());
            modelBuilder.ApplyConfiguration(new WorksheetConfiguration());
            modelBuilder.ApplyConfiguration(new WorksheetProblemConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new ProblemTagConfiguration());

            // Add UpdatedAt trigger function
            modelBuilder.HasDbFunction(() => UpdateUpdatedAtColumn());

            // Create triggers for UpdatedAt columns
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.GetProperties();
                var updatedAtProperty = properties
                    .FirstOrDefault(p => p.Name == "UpdatedAt");

                if (updatedAtProperty != null)
                {
                    modelBuilder.Entity(entityType.Name)
                        .ToTable(tb => tb.HasTrigger("TR_" + entityType.GetTableName() + "_UpdatedAt"));
                }
            }
        }

        private static DateTime UpdateUpdatedAtColumn() => throw new NotSupportedException();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }

    // Extension method for service registration
    public static class GoWorksheetContextExtensions
    {
        public static IServiceCollection AddGoWorksheetContext(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<GoWorksheetContext>(options =>
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("GoWorksheet.Infrastructure");
                    npgsqlOptions.EnableRetryOnFailure(3);
                })
            );

            return services;
        }
    }
}