using GoWorksheet.Infrastructure.Models;
using GoWorksheet.Infrastructure.Data.Repositories.Base;

namespace GoWorksheet.Infrastructure.Data.Repositories
{
    public interface IProblemRepository : IRepository<Problem>
    {
        Task<IEnumerable<Problem>> GetByTypeAsync(int typeId);
        Task<IEnumerable<Problem>> GetByDifficultyAsync(int difficultyId);
        Task<IEnumerable<Problem>> GetByTagsAsync(IEnumerable<int> tagIds);
        Task<Problem?> GetWithDetailsAsync(int id);
        Task<IEnumerable<Problem>> GetActiveProblemsAsync();
        Task AddTagsToProblemAsync(int problemId, IEnumerable<int> tagIds);
        Task RemoveTagsFromProblemAsync(int problemId, IEnumerable<int> tagIds);
    }
}