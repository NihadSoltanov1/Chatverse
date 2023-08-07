using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Comment.GetCommentByPostId
{
    public class GetCommentByPostIdQueryResponse
    {
        public List<GetCommentByPostId> Comments { get; set; }
    }

    public class GetCommentByPostId
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
    }
}
