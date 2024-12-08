using GoWorksheet.Infrastructure.Models.Common;

namespace GoWorksheet.Infrastructure.Models{
    public class Tag : BaseEntity
    {
        public required string Name { get; set; }
        
        public required virtual ICollection<ProblemTag> ProblemTags { get; set; }
    }
}