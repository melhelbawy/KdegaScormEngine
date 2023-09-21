using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Application.Interfaces;
using Kdega.ScormEngine.Infrastructure.Storage.Helpers;
using Kdega.ScormEngine.Infrastructure.Storage.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;

namespace Kdega.ScormEngine.Infrastructure.Storage.LocalSystemStorage;
public class LocalSystemDirectoryManager : IDirectoryManger
{
    public LocalSystemSettings LocalSystemSettings { get; }
    private readonly ILogger<LocalSystemDirectoryManager> _logger;
    public string DirectoryPath { get; }
    public LocalSystemDirectoryManager(IOptions<LocalSystemSettings> localSystemSettings)
    {
        LocalSystemSettings = localSystemSettings.Value;

        using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
            .SetMinimumLevel(LogLevel.Trace)
            .AddConsole());
        _logger = loggerFactory.CreateLogger<LocalSystemDirectoryManager>();
        DirectoryPath = CreateMainDirectory()!.Result;
    }

    private async Task<string>? CreateMainDirectory()
    {
        var osPlatform = OsPlatformHelper.GetOperatingSystem();
        string path;
        if (osPlatform == OSPlatform.Linux)
        {
            path = Path.Combine(LocalSystemSettings.LinuxRootDirectory, LocalSystemSettings.MainDirectory);
            if (await DirectoryExistAsync(path))
                return path;
            Directory.CreateDirectory(path);
            return path;
        }

        path = Path.Combine(LocalSystemSettings.WindowsRootDirectory, LocalSystemSettings.MainDirectory);
        if (await DirectoryExistAsync(path))
            return path;
        return path;
    }
    public string GenerateDirectoryName() => DateTimeOffset.Now.ToString("MMyyyy");
    public async Task<List<StorageObject>> ListDirectoryAsync(string directoryPath)
    {
        return await Task.Run(() =>
        {
            var absolutePath = DirectoryPath + LocalSystemSettings.Separator + directoryPath;
            var directories = Directory.GetDirectories(absolutePath);

            var directoryList = directories
                .Select(directory => new DirectoryInfo(directory))
                .Select(directoryInfo => new StorageObject() { Name = directoryInfo.Name, Path = $"{directoryPath}/{directoryInfo.Name}", IsDirectory = true, })
                .ToList();

            var directoryFiles = Directory.GetFiles(absolutePath);

            directoryList.AddRange(directoryFiles
                .Select(file => new FileInfo(file))
                .Select(fileInfo => new StorageObject() { Name = fileInfo.Name, Path = $"{directoryPath}/{fileInfo.Name}", IsDirectory = false, }));

            return directoryList;
        });
    }
    public async Task<bool> DirectoryExistAsync(string directory) =>
        await Task.Run(() => Directory.Exists(directory));
    public async Task CreateStorageMonthlyContainer()
    {
        var path = Path.Combine(await CreateMainDirectory()!, GenerateDirectoryName());
        if (!await DirectoryExistAsync(path))
            await CreateDirectoryIfNotExistAsync(GenerateDirectoryName());
    }
    public async Task CreateDirectoryIfNotExistAsync(string directoryName)
    {
        var path = Path.Combine(await CreateMainDirectory()!, directoryName);
        Directory.CreateDirectory(path);
    }
    public async Task DeleteDirectoryAsync(string directoryName) =>
        await Task.Run(() => Directory.Delete(directoryName));
    public async Task DeleteNoneEmptyDirectoryAsync(string directoryName) =>
        await Task.Run(() => Directory.Delete(directoryName, true));
}

