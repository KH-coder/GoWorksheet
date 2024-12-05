public class Problem
{
    public int ID { get; set; }
    public int TypeID { get; set; }
    public int DifficultyID { get; set; }
    public string? ImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    
    public required virtual ProblemType Type { get; set; }
    public required virtual DifficultyLevel Difficulty { get; set; }
    public required virtual ICollection<WorksheetProblem> WorksheetProblems { get; set; }
    public required virtual ICollection<ProblemTag> ProblemTags { get; set; }
}