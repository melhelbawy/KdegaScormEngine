using Kdega.ScormEngine.Application.Interfaces;
using Kdega.ScormEngine.Infrastructure.Storage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using System.IO.Compression;
using System.Text;
using System.Web;

namespace Kdega.ScormEngine.Infrastructure.Storage.LocalSystemStorage;
public class LocalSystemStorageProvider : LocalSystemDirectoryManager, IObjectManager
{
    public LocalSystemStorageProvider(IOptions<LocalSystemSettings> localSystemSettings) : base(localSystemSettings)
    {
    }

    public string FileFullPath(string path)
    {
        return DirectoryPath + LocalSystemSettings.Separator + path;
    }

    public async Task<bool> ObjectExistAsync(string objectPath) =>
        await Task.Run(() => File.Exists(objectPath));

    public async Task<string> UploadObjectAsync(string objectPath, IFormFile file, bool useUniqueName = false)
    {
        if (file.Length <= 0) throw new Exception("File is not Valid");

        await CreateStorageMonthlyContainer();
        var newFileName = Guid.NewGuid() + $"{Path.GetExtension(file.FileName)}";
        var filePath = GenerateRelativePath(newFileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        return GenerateDirectoryName() + LocalSystemSettings.Separator + newFileName;

    }

    public async Task<string> UploadObjectAsync(byte[] file, string fileName, string? path = null)
    {
        if (file.Length <= 0) throw new Exception("File is not Valid");

        await CreateStorageMonthlyContainer();
        var newFileName = Guid.NewGuid() + $"{Path.GetExtension(fileName)}";
        var filePath = GenerateRelativePath(newFileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await File.WriteAllBytesAsync(filePath, file);
        return GenerateDirectoryName() + LocalSystemSettings.Separator + newFileName;
    }

    public async Task<string> ReplaceObjectAsync(string objectPath, IFormFile file, bool useUniqueName = false)
    {
        throw new NotImplementedException();
    }

    public async Task<string> ReplaceObjectAsync(byte[] file, string fileName, string? path = null)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UnZipFileTo(IFormFile zipFile)
    {
        if (zipFile.Length <= 0) throw new Exception("File not Valid!");

        await CreateStorageMonthlyContainer();
        var scormUniqueDirectoryName = Guid.NewGuid().ToString();
        var scormDestinationPath = GenerateRelativePath(scormUniqueDirectoryName);
        // Extract to local folder
        await using var stream = zipFile.OpenReadStream();
        using ZipArchive archive = new(stream, ZipArchiveMode.Read);
        archive.ExtractToDirectory(scormDestinationPath);
        var scormPath = $"{GenerateDirectoryName()}{LocalSystemSettings.Separator}{scormUniqueDirectoryName}";
        return scormPath;

    }

    public async Task DeleteObjectAsync(string objectPath)
    {
        throw new NotImplementedException();
    }

    public async Task MoveObjectAsync(string objectPath, string newDirectoryPath)
    {
        throw new NotImplementedException();
    }

    public async Task<Stream> GetObjectStreamAsync(string objectPath)
    {
        var parsedObjectPath = ParsObjectPath(objectPath);
        if (parsedObjectPath == null) throw new FileNotFoundException();
        var fullFilePath = BuildCleanObjectPath(parsedObjectPath);
        if (!File.Exists(fullFilePath)) throw new FileNotFoundException();

        var memoryStream = new MemoryStream();
        await using var fileStream =
            new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        await fileStream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;

    }

    public string GenerateRelativePath(string fileName)
    {
        return DirectoryPath + LocalSystemSettings.Separator + GenerateDirectoryName() + LocalSystemSettings.Separator + fileName;
    }
    private string BuildCleanObjectPath(string objectUniqueName)
    {
        return $"{DirectoryPath}{LocalSystemSettings.Separator}{objectUniqueName}"
            .Replace(@"//", LocalSystemSettings.Separator.ToString())
            .Replace(@"\\", LocalSystemSettings.Separator.ToString())
            .Replace(@"\/", LocalSystemSettings.Separator.ToString())
            .Replace("/", LocalSystemSettings.Separator.ToString())
            .Replace("\\", LocalSystemSettings.Separator.ToString());
    }

    private static bool IsObjectPathEncode(string objectPath) => HtmlUtils.HtmlDecode(objectPath) != objectPath;

    private static string ParsObjectPath(string objectPath)
    {
        var isEquals = IsObjectPathEncode(objectPath);
        return isEquals ? objectPath : HttpUtility.UrlDecode(objectPath, Encoding.UTF8);
    }
}
