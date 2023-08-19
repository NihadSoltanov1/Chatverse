using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.AppUser.UpdateInformation
{
    public class UpdateInformationCommandRequest : IRequest<IResult>
    {
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? ProfilePicture { get; set; }
        public bool Privicy { get; set; }
        public string? About { get; set; }
    }






}
