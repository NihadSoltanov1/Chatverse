namespace Chatverse.Application.Common.Interfaces;
    public interface IGoogleCloudService
    {
        void UploadFileToCloud(string fullPath);
        void DeleteFileToCloud(string fileName);
    }

