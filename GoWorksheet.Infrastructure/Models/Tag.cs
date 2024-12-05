public class Tag
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public required virtual ICollection<ProblemTag> ProblemTags { get; set; }
}