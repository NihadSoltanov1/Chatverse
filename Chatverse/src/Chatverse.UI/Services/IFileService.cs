namespace Chatverse.UI.Services
{
    public interface IFileService
    {
        public List<string> FileUploadToRoot(IFormFileCollection files);
        public void FileDeleteFromRoot(string responsePath);
    }
}
