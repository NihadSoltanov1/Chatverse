using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);
        Task<bool> CopyToFileAsync(string path, IFormFile file);
        Task<string> FileRenameAsync(string fileName);
       
    }
}
