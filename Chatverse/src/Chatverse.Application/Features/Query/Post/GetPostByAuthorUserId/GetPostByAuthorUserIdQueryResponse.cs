using Chatverse.Application.Features.Query.Comment.GetCommentByPostId;
using Chatverse.Application.Features.Query.Post.GetPostByFriend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Post.GetPostByAuthorUserId
{
    

    public class GetPostByAuthorUserIdQueryResponse
    {
        public List<GetMyPosts> Posts { get; set; }
    }

    public class GetMyPosts
    {
        public int PostId { get; set; }
        public string FullName { get; set; }
        public string? Content { get; set; }
        public List<string>? Media { get; set; }
        public List<GetCommentByPostId> Comments { get; set; }

        public DateTime CreateDate { get; set; }
        public bool? IsLike { get; set; }
    }
}
