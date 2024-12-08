namespace GoWorksheet.Infrastructure.Services.Interfaces
{
    public interface IPdfService
    {
        Task<byte[]> GenerateWorksheetPdfAsync(int worksheetId);
        Task<string> SaveWorksheetPdfAsync(int worksheetId);
    }
}