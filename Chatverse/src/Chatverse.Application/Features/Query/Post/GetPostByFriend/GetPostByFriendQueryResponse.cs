using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Post.GetPostByFriend
{
    public record GetPostByFriendQueryResponse
    {
        public List<GetFriendsPosts> Posts { get; set; }
    }

    public class GetFriendsPosts
    {
        public string FullName { get; set; }
        public string? Content { get; set; }
        public string? Media { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
