public class Worksheet
{
    public int ID { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? TargetLevel { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsPublished { get; set; }
    
    public required virtual ICollection<WorksheetProblem> WorksheetProblems { get; set; }
}