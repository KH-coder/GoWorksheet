using GoWorksheet.Infrastructure.Models;

namespace GoWorksheet.Infrastructure.Services.Interfaces
{
    public interface IWorksheetService
    {
        Task<Worksheet> CreateWorksheetAsync(Worksheet worksheet);
        Task<Worksheet> UpdateWorksheetAsync(Worksheet worksheet);
        Task<bool> DeleteWorksheetAsync(int id);
        Task<bool> AddProblemToWorksheetAsync(int worksheetId, int problemId, int order);
        Task<bool> RemoveProblemFromWorksheetAsync(int worksheetId, int problemId);
        Task<bool> ReorderProblemsAsync(int worksheetId, Dictionary<int, int> problemOrders);
    }
}