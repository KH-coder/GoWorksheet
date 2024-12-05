public class DifficultyLevel
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public required virtual ICollection<Problem> Problems { get; set; }
}