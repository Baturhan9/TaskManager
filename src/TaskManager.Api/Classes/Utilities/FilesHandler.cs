using Microsoft.AspNetCore.Mvc;
using TaskManager.Consts;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Classes.Utilities
{
    public class FilesHandler
    {
        private readonly string _storagePath;
        private readonly ILogger<FilesHandler> _logger;

        public FilesHandler(ILogger<FilesHandler> logger, IConfiguration configuration)
        {
            _logger = logger;
            _storagePath =
                configuration.GetValue<string>(SystemEnvironments.STORAGE_PATH) ?? "Uploads";
            Directory.CreateDirectory(_storagePath);
        }

        public FileStreamResult GetFileByPath(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    _logger.LogWarning($"file in {filePath} don't exist");
                    throw new FileNotFoundException($"File {filePath} not found");
                }

                var fileStream = File.OpenRead(filePath);
                var fileName = Path.GetFileName(filePath);

                return new FileStreamResult(fileStream, "application/octet-stream")
                {
                    FileDownloadName = fileName,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public string UploadFile(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_storagePath, uniqueFileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file");
                throw new Exception("File upload failed", ex);
            }
        }

        public async Task DeleteFileAsync(string filePath)
        {

            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"File not found at path: {filePath}");
                throw new Exception($"Files not found at path: {filePath}");
            }

            try
            {
                const int maxAttempts = 3;
                for (int attempt = 1; attempt <= maxAttempts; attempt++)
                {
                    try
                    {
                        File.Delete(filePath);
                        _logger.LogInformation($"File successfully deleted: {filePath}");
                        return;
                    }
                    catch (IOException ex) when (attempt < maxAttempts)
                    {
                        _logger.LogWarning(
                            $"Attempt {attempt} failed to delete file {filePath}. Retrying..."
                        );
                        await Task.Delay(100 * attempt); // Экспоненциальная задержка
                    }
                }

                throw new IOException($"Failed to delete file after {maxAttempts} attempts");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, $"Permission denied when deleting file: {filePath}");
                throw new UnauthorizedAccessException(
                    $"No permission to delete file: {Path.GetFileName(filePath)}",
                    ex
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error deleting file: {filePath}");
                throw new Exception($"Could not delete file: {Path.GetFileName(filePath)}", ex);
            }
        }
    }
}
