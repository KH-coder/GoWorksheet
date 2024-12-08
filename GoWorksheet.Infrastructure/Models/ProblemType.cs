using GoWorksheet.Infrastructure.Models.Common;

namespace GoWorksheet.Infrastructure.Models{
    public class ProblemType : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        
        public required virtual ICollection<Problem> Problems { get; set; }
    }
}