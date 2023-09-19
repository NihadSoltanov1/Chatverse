namespace Chatverse.Infrastructure.Services;

public class FileService : IFileService
{
    readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<bool> CopyToFileAsync(string path, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync:false);
            await fileStream.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public Task<string> FileRenameAsync(string fileName)
    {
        throw new Exception();
    }

    public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
    {

        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> datas = new List<(string fileName, string path)>();
        List<bool> results = new(); 
        
        foreach(IFormFile file in files)
        {
            string fileNewName = await FileRenameAsync(file.FileName);
            bool result = await CopyToFileAsync($"{uploadPath}//{fileNewName}", file);
            datas.Add((fileNewName, $"{uploadPath}//{fileNewName}"));
            results.Add(result);
        }
        if (results.TrueForAll(r => r.Equals(true))) return datas;
        return null;
    }
}
