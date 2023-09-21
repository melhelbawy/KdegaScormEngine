using Kdega.ScormEngine.Application.Common.Models;

namespace Kdega.ScormEngine.Application.Interfaces;
public interface IDirectoryManger
{
    /// <summary>
    /// List all directory in a spscifc directory
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    Task<List<StorageObject>> ListDirectoryAsync(string directoryPath);
    /// <summary>
    /// Validate if an directory is Exist.
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    Task<bool> DirectoryExistAsync(string directory);
    /// <summary>
    /// Create a new directory if not exist.
    /// </summary>
    /// <param name="directoryName"></param>
    /// <returns></returns>
    Task CreateDirectoryIfNotExistAsync(string directoryName);
    /// <summary>
    /// Delete an empty directory
    /// </summary>
    /// <param name="directoryName"></param>
    /// <returns></returns>
    Task DeleteDirectoryAsync(string directoryName);
    /// <summary>
    /// Forced delete for None empty directory.
    /// </summary>
    /// <param name="directoryName"></param>
    /// <returns></returns>
    Task DeleteNoneEmptyDirectoryAsync(string directoryName);
}
