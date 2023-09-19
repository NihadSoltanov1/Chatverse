namespace Chatverse.Infrastructure.Services;

public class GoogleCloudService : IGoogleCloudService
{
    private readonly IConfiguration _configuration;

    public GoogleCloudService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    GoogleCredential google = GoogleCredential.FromFile(@"C:\Users\Nihad Soltanov\Downloads\turnkey-clover-395407-8458ee823a17.json");
    public void UploadFileToCloud(string fullPath)
    {
        var storage = StorageClient.Create(google);
        var bucket = storage.GetBucket(_configuration["GoogleCloud:CloudBucket"]);
        using (FileStream uploadFileStream = System.IO.File.OpenRead(fullPath))
        {
            string objectName = Path.GetFileName(fullPath);
            storage.UploadObject(_configuration["GoogleCloud:CloudBucket"], objectName, null, uploadFileStream);
        }
    }
   
    public void DeleteFileToCloud(string fileName)
    {
        var storage = StorageClient.Create(google);
        storage.DeleteObject(_configuration["GoogleCloud:CloudBucket"], fileName);
    }
}
