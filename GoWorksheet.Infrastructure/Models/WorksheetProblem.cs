public class WorksheetProblem
{
    public int WorksheetID { get; set; }
    public int ProblemID { get; set; }
    public int ProblemOrder { get; set; }
    
    public required virtual Worksheet Worksheet { get; set; }
    public required virtual Problem Problem { get; set; }
}