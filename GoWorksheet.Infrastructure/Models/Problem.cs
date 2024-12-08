using GoWorksheet.Infrastructure.Models.Common;

namespace GoWorksheet.Infrastructure.Models{
    public class Problem : BaseEntity
    {
        public int TypeID { get; set; }
        public int DifficultyID { get; set; }
        public string? ImagePath { get; set; }
        public bool IsActive { get; set; }
        
        public required virtual ProblemType Type { get; set; }
        public required virtual DifficultyLevel Difficulty { get; set; }
        public required virtual ICollection<WorksheetProblem> WorksheetProblems { get; set; }
        public required virtual ICollection<ProblemTag> ProblemTags { get; set; }
    }
}
