using Microsoft.AspNetCore.Http;

namespace Kdega.ScormEngine.Application.Interfaces;
public interface IObjectManager
{
    string FileFullPath(string path);
    /// <summary>
    /// Validate if the object is Exist.
    /// </summary>
    /// <param name="objectPath"></param>
    /// <returns></returns>
    Task<bool> ObjectExistAsync(string objectPath);
    /// <summary>
    /// Save file in Object Storage and return full object Path with FileName
    /// </summary>
    /// <param name="objectPath">Path without FileName ex: "example"</param>
    /// <param name="file">IFormFile</param>
    /// <param name="useUniqueName">If true: OVERWRITE the main fileName</param>
    /// <returns>Full Object path with FileName ex: "example/fileName.ex"</returns>
    Task<string> UploadObjectAsync(string objectPath, IFormFile file, bool useUniqueName = false);
    /// <summary>
    /// Save file in Object Storage and return full object Path with FileName
    /// </summary>
    /// <param name="file"> file bytes </param>
    /// <param name="fileName"></param>
    /// <param name="path">path without FileName</param>
    /// <returns>Full Object path with FileName ex: "example/fileName.ex"</returns>
    Task<string> UploadObjectAsync(byte[] file, string fileName, string? path = null);

    /// <summary>
    /// Save/Replace file in Object Storage and return full object Path with FileName
    /// </summary>
    /// <param name="objectPath">Path without FileName ex: "example"</param>
    /// <param name="file">IFormFile</param>
    /// <param name="useUniqueName">If true: OVERWRITE the main fileName</param>
    /// <returns>Full Object path with FileName ex: "example/fileName.ex"</returns>
    Task<string> ReplaceObjectAsync(string objectPath, IFormFile file, bool useUniqueName = false);
    /// <summary>
    /// Save/Replace file in Object Storage and return full object Path with FileName
    /// </summary>
    /// <param name="file"> file bytes </param>
    /// <param name="fileName"></param>
    /// <param name="path">path without FileName</param>
    /// <returns>Full Object path with FileName ex: "example/fileName.ex"</returns>
    Task<string> ReplaceObjectAsync(byte[] file, string fileName, string? path = null);
    /// <summary>
    /// Unzipping Zip files.
    /// </summary>
    /// <param name="destinationPath"></param>
    /// <param name="zipFile"></param>
    /// <returns></returns>
    public Task<string> UnZipFileTo(IFormFile zipFile);
    Task DeleteObjectAsync(string objectPath);
    Task MoveObjectAsync(string objectPath, string newDirectoryPath);
    Task<Stream> GetObjectStreamAsync(string objectPath);
}
