public class ProblemTag
{
    public int ProblemID { get; set; }
    public int TagID { get; set; }
    
    public required virtual Problem Problem { get; set; }
    public required virtual Tag Tag { get; set; }
}