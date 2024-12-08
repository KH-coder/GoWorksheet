using GoWorksheet.Infrastructure.Models.Common;

namespace GoWorksheet.Infrastructure.Models{
    public class DifficultyLevel : BaseEntity
    {
        public required string Name { get; set; }
        
        public required virtual ICollection<Problem> Problems { get; set; }
    }
}