using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymManagementBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string[] Extensions = { ".png", ".jpg", ".jpeg" };
        private readonly double MaxSize = 5*1024*1024;

        public string? Upload(string FolderName, IFormFile file)
        {
            try
            {
                if (FolderName is null || file is null || file.Length == 0) return null;
                var Extension = Path.GetExtension(file.FileName).ToLower();
                if (!Extensions.Contains(Extension)) return null;
                if (file.Length > MaxSize) return null;
                var serverPath = Directory.GetCurrentDirectory();
                var folderPath = Path.Combine(serverPath,"wwwroot", "images", FolderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = Guid.NewGuid().ToString() + Extension;
                var filePath = Path.Combine(folderPath, fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                return fileName;
            }
            catch

            {
                Console.WriteLine("Failed To Upload The File");
                return null;
            }




        }
        public bool Delete(string FolderName, string fileName)
        {
            try
            {
                if(string.IsNullOrEmpty(FolderName) || string.IsNullOrEmpty(fileName)) return false;
                var ServerPath = Directory.GetCurrentDirectory();
                var FullPath =Path.Combine(ServerPath,"wwwroot","Images", FolderName, fileName);
                if (File.Exists(FullPath))
                {
                    File.Delete(FullPath);
                    return true;
                }
                else
                {
                      return false;
                }
            }
            catch
            {
                Console.WriteLine("Failed To Delete The File");
                return false;
            }
        }
    }
}
