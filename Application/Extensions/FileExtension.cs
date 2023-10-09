using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.Extensions;

public static partial class Extension
{
    //IHostEnvironment
    static public string GetImagePhysicalPath(this IHostEnvironment env, string fileName)
    {
        return Path.Combine(env.ContentRootPath,"Images", fileName);
    }

    static public string GetRandomImagePath(this IFormFile file, string prefix = "")
    {
        string extension = Path.GetExtension(file.FileName);//.jpg,.jpeg,

        if (!string.IsNullOrWhiteSpace(prefix))
        {
            prefix = $"{prefix}-";
        }

        return $"{prefix}{Guid.NewGuid()}{extension}".ToLower();
    }
    async static public Task<string> SaveAsync(this IHostEnvironment env, IFormFile file,
       string imageName)
    {
        string fullPath = env.GetImagePhysicalPath(imageName);

        using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            await file.CopyToAsync(fs);
        }

        return fullPath;
    }

    async static public Task<string> SaveAsync(this IHostEnvironment env, IFormFile file,
        string imageName, CancellationToken cancellationToken)
    {
        string fullPath = env.GetImagePhysicalPath(imageName);

        using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            await file.CopyToAsync(fs, cancellationToken);
        }

        return fullPath;
    }


    static public void ArchiveImage(this IHostEnvironment env, string fileName)
    {
        var imageActualPath = Path.Combine(env.ContentRootPath,"Images", fileName);

        if (File.Exists(imageActualPath))
        {
            File.Delete(imageActualPath);
        }
    }


    static public bool CheckFileType(this IFormFile file, string type)
    {
        return file.ContentType.Contains(type);
    }

    static public bool CheckFileSize(this IFormFile file, int kb)
    {
        return file.Length / 1024 <= kb;
    }

}
