using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Common.Interfaces
{
    public interface IGoogleCloudService
    {
        void UploadFileToCloud(string fullPath);
    }
}
