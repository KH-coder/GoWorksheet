namespace GoWorksheet.Infrastructure.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string fileName);
        Task<bool> DeleteFileAsync(string filePath);
        Task<Stream> GetFileAsync(string filePath);
        string GetFileUrl(string filePath);
    }
}