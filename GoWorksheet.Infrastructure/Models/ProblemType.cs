public class ProblemType
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public required virtual ICollection<Problem> Problems { get; set; }
}