using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.SocialAccount.CreateSocialAccount
{
    public class CreateSocialAccountCommandRequest : IRequest<IResult>
    {
        public List<SocialMedia> SocialMedias { get; set; }
    }
    public class SocialMedia
    {
        public string Category { get; set; }
        public string Url { get; set; }
    }
}
