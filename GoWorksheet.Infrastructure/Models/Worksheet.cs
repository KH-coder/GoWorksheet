using GoWorksheet.Infrastructure.Models.Common;

namespace GoWorksheet.Infrastructure.Models{
    public class Worksheet : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? TargetLevel { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsPublished { get; set; }
        
        public required virtual ICollection<WorksheetProblem> WorksheetProblems { get; set; }
    }
}