using GoWorksheet.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace GoWorksheet.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _baseStoragePath;
        private readonly string _baseUrl;
        private readonly ILogger<FileStorageService> _logger;

        public FileStorageService(
            IConfiguration configuration,
            ILogger<FileStorageService> logger)
        {
            _baseStoragePath = configuration["Storage:BasePath"] 
                ?? throw new ArgumentNullException("Storage:BasePath");
            _baseUrl = configuration["Storage:BaseUrl"] 
                ?? throw new ArgumentNullException("Storage:BaseUrl");
            _logger = logger;
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
        {
            try
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
                var filePath = Path.Combine(_baseStoragePath, uniqueFileName);
                
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using (var fileStreamWrite = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(fileStreamWrite);
                }

                _logger.LogInformation("File saved successfully: {FilePath}", filePath);
                return uniqueFileName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving file: {FileName}", fileName);
                throw;
            }
        }

        public async Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(_baseStoragePath, filePath);
                if (File.Exists(fullPath))
                {
                    await Task.Run(() => File.Delete(fullPath));
                    _logger.LogInformation("File deleted successfully: {FilePath}", filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file: {FilePath}", filePath);
                throw;
            }
        }

        public async Task<Stream> GetFileAsync(string filePath)
        {
            var fullPath = Path.Combine(_baseStoragePath, filePath);
            if (!File.Exists(fullPath))
            {
                _logger.LogWarning("File not found: {FilePath}", filePath);
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            return await Task.FromResult(File.OpenRead(fullPath));
        }

        public string GetFileUrl(string filePath)
        {
            return $"{_baseUrl.TrimEnd('/')}/{filePath}";
        }
    }
}