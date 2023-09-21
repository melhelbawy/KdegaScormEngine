namespace Kdega.ScormEngine.Infrastructure.Storage.Helpers;
public static class PathsHelper
{
    public static List<string> GetPathSegments(this string path) =>
        path?.ToAltDirectorySeparatorPath()
            .Split(Path.AltDirectorySeparatorChar).ToList() ?? new List<string>();

    public static string TrimEndSeparator(this string path) =>
        path.TrimEnd(Path.DirectorySeparatorChar).TrimEnd(Path.AltDirectorySeparatorChar);

    public static string SkipFirstSegment(this string path) =>
        string.Join(Path.AltDirectorySeparatorChar, path.GetPathSegments().Skip(1));

    public static string GetUniqueName(this string fileName, bool uniqueName) =>
        uniqueName ? $"{Guid.NewGuid()}{Path.GetExtension(fileName)}" : fileName;

    public static string AltCombine(this string path1, params string[] paths) =>
        Path.Combine(paths.Reverse().Append(path1 ?? "").Reverse().ToArray())
            .ToAltDirectorySeparatorPath();

    public static string ToAltDirectorySeparatorPath(this string path) =>
        path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
}