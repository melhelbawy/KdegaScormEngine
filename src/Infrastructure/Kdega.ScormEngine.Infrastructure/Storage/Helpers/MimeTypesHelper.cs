namespace Kdega.ScormEngine.Infrastructure.Storage.Helpers;
public static class MimeTypesHelper
{
    public static string GetObjectType(this string objectPath)
    {
        var extension = Path.GetExtension(objectPath).ToLowerInvariant();
        return MimeTypes.ContainsKey(extension)
            ? MimeTypes[extension] : "application/octet-stream";
    }

    public static Dictionary<string, string> MimeTypes =>
        new()
        {
            {".csv", "text/csv"},
            {".txt", "text/plain"},
            {".css", "text/css"},
            {".htm", "text/html"},
            {".html", "text/html"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
            {".json", "application/json"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"},
            {".aac", "audio/aac"},
            {".mp3", "audio/mpeg"},
            {".avi", "video/x-msvideo"},
            {".mp4", "video/mp4"},
            {".mpeg", "video/mpeg"},
            {".ico" , "image/x-icon" },
            {".zip" , "application/x-zip-compressed" }
        };
}
