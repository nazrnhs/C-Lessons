using Microsoft.AspNetCore.Http;
using System.IO;

namespace BizlandWeb.Utilites
{
    public static class FileExtension
    {
        public static bool CheckFileType(this IFormFile file, string fileType)
        {
            return file.ContentType.Contains(fileType + "/");

        }

        public static bool CheckFileSize(this IFormFile file, int fileSize)

        {
            return file.Length/1024 > fileSize;
        }

        public static async Task<string> SaveFileAsync(this IFormFile file, string root, string folder)
        {
            string uniqueName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string path = Path.Combine(root, "assets", "img", folder, uniqueName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return uniqueName;
        }

        public static void DeleteFile(this IFormFile file, string root, string folder, string fileName)
        {
            string path = Path.Combine(root, "assets", "img", folder, fileName);

            if (File.Exists(path))
            {

                File.Delete(path);
            }

        }

    }
}
