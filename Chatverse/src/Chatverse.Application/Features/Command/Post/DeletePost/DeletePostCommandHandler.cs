using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using Chatverse.Application.Features.Command.Post.CreatePost;
using Chatverse.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Post.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommandRequest, IDataResult<List<DeletePostCommandResponse>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGoogleCloudService _googleCloudService;
        public DeletePostCommandHandler(IApplicationDbContext context, IGoogleCloudService googleCloudService)
        {
            _context = context;
            _googleCloudService = googleCloudService;
        }

        public async Task<IDataResult<List<DeletePostCommandResponse>>> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
        {

            Domain.Entities.Post deletePost = _context.Posts.FirstOrDefault(p => p.Id == request.Id);
            List<PostImage> postImages = await _context.PostImages.Where(x => x.PostId == deletePost.Id).ToListAsync();
            List<Domain.Entities.Comment> comments = await _context.Comments.Where(x => x.PostId == deletePost.Id).ToListAsync();
            List<DeletePostCommandResponse> filePaths = new List<DeletePostCommandResponse>();
            string rootFolder = @"postfiles/";
        
            if (postImages is not null)
            {

                postImages.ForEach(image =>
                {
                    DeletePostCommandResponse _fileresponsePath = new DeletePostCommandResponse() { OldFilePath = image.FilePath };
                   var newPath =  image.FilePath.Substring(image.FilePath.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase) + rootFolder.Length).Replace("\\", " /");
                    _googleCloudService.DeleteFileToCloud(newPath);
                    filePaths.Add(_fileresponsePath);
                });
                
                _context.PostImages.RemoveRange(postImages);
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (comments is not null)
            {
                _context.Comments.RemoveRange(comments);
                await _context.SaveChangesAsync(cancellationToken);
            }
            if (deletePost is null) throw new NotFoundException("Post not found");
            _context.Posts.Remove(deletePost);
            await _context.SaveChangesAsync(cancellationToken);
            return new SuccessDataResult<List<DeletePostCommandResponse>>(filePaths, "Post deleted successfully");
        }
    }
}
