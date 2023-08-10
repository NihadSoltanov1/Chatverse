using Chatverse.UI.DTOs.PostFile;
using Chatverse.UI.ViewModels.Post;
using Newtonsoft.Json;

namespace Chatverse.UI.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<string> FileUploadToRoot(IFormFileCollection files)
        {
            List<string> paths = new List<string>();
            if (files is null) return null;
            foreach (IFormFile file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var fileUniqueName = DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(file.FileName);
                    var folderPath = Path.Combine(_env.WebRootPath, "postfiles");
                    var fullPath = Path.Combine(folderPath, fileUniqueName);
                    string rootFolder = @"wwwroot\";
                    string returnPath = fullPath.Substring(fullPath.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase) + rootFolder.Length).Replace("\\", "/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }
                    paths.Add(fullPath);
                }

            }
            return paths;
        
        }

        public void FileDeleteFromRoot(string responsePath)
        {
            List<PostFilePathDto> FilesPath = JsonConvert.DeserializeObject<List<PostFilePathDto>>(responsePath);
            FilesPath.ForEach(path =>
            {
                var wwwrootPath = Path.Combine(_env.WebRootPath);
                var fullPath = Path.Combine(wwwrootPath, path.filePath);
                if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
            });
        }

    }
}
