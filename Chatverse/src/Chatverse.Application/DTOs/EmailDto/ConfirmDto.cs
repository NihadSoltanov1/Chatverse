using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.DTOs.EmailDto
{
    public class ConfirmDto
    {
        public string userId { get; set; }
        public string token { get; set; }
    }
}
