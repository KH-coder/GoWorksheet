using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GoWorksheet.Infrastructure.Models;
using GoWorksheet.Infrastructure.Data.Repositories.Base;

namespace GoWorksheet.Infrastructure.Data.Repositories
{
    public class ProblemRepository : Repository<Problem>, IProblemRepository
    {
        private new readonly GoWorksheetContext _context;

        public ProblemRepository(GoWorksheetContext context) 
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Problem>> GetByTypeAsync(int typeId)
        {
            return await _context.Problems
                .Where(p => p.TypeID == typeId && p.IsActive)
                .Include(p => p.Type)
                .Include(p => p.Difficulty)
                .ToListAsync();
        }

        public async Task<IEnumerable<Problem>> GetByDifficultyAsync(int difficultyId)
        {
            return await _context.Problems
                .Where(p => p.DifficultyID == difficultyId && p.IsActive)
                .Include(p => p.Type)
                .Include(p => p.Difficulty)
                .ToListAsync();
        }

        public async Task<IEnumerable<Problem>> GetByTagsAsync(IEnumerable<int> tagIds)
        {
            return await _context.Problems
                .Where(p => p.IsActive && p.ProblemTags.Any(pt => tagIds.Contains(pt.TagID)))
                .Include(p => p.Type)
                .Include(p => p.Difficulty)
                .Include(p => p.ProblemTags)
                    .ThenInclude(pt => pt.Tag)
                .ToListAsync();
        }

        public async Task<Problem?> GetWithDetailsAsync(int id)
        {
            return await _context.Problems
                .Include(p => p.Type)
                .Include(p => p.Difficulty)
                .Include(p => p.ProblemTags)
                    .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<IEnumerable<Problem>> GetActiveProblemsAsync()
        {
            return await _context.Problems
                .Where(p => p.IsActive)
                .Include(p => p.Type)
                .Include(p => p.Difficulty)
                .ToListAsync();
        }

        public async Task AddTagsToProblemAsync(int problemId, IEnumerable<int> tagIds)
        {
            var problem = await _context.Problems
                .Include(p => p.ProblemTags)
                .FirstOrDefaultAsync(p => p.Id == problemId);

            if (problem == null)
                throw new KeyNotFoundException($"Problem with ID {problemId} not found.");

            foreach (var tagId in tagIds)
            {
                if (!problem.ProblemTags.Any(pt => pt.TagID == tagId))
                {
                    problem.ProblemTags.Add(new ProblemTag 
                    { 
                        ProblemID = problemId, 
                        TagID = tagId 
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveTagsFromProblemAsync(int problemId, IEnumerable<int> tagIds)
        {
            var problem = await _context.Problems
                .Include(p => p.ProblemTags)
                .FirstOrDefaultAsync(p => p.Id == problemId);

            if (problem == null)
                throw new KeyNotFoundException($"Problem with ID {problemId} not found.");

            var tagsToRemove = problem.ProblemTags.Where(pt => tagIds.Contains(pt.TagID));
            foreach (var tag in tagsToRemove)
            {
                problem.ProblemTags.Remove(tag);
            }

            await _context.SaveChangesAsync();
        }
    }
}