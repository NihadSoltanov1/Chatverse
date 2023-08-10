using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Features.Command.Post.CreatePost;
using Chatverse.Application.Features.Command.Post.DeletePost;
using Chatverse.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Chatverse.Application.Features.Command.Post.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommandRequest, IDataResult<List<UpdatePostCommandResponse>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGoogleCloudService _googleCloudService;
        public UpdatePostCommandHandler(IApplicationDbContext context, IGoogleCloudService googleCloudService)
        {
            _context = context;
            _googleCloudService = googleCloudService;
        }
        string rootFolder = @"postfiles/";
        string rootPath = @"wwwroot\";
        public async Task<IDataResult<List<UpdatePostCommandResponse>>> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
        {

            Domain.Entities.Post updatePost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == request.UpdatePostId);
            var oldFiles = _context.PostImages.Where(i => i.PostId == request.UpdatePostId).ToList();
            List<UpdatePostCommandResponse> filePaths = new List<UpdatePostCommandResponse>();
            switch (request)
            {
             
                case { UpdateContent: null, UpdateMedia: not null }:
                    {
                       
                        if (oldFiles is not null)
                        {
                            oldFiles.ForEach(oldFile =>
                            {
                                UpdatePostCommandResponse _fileresponsePath = new UpdatePostCommandResponse() { OldFilePath = oldFile.FilePath };
                                filePaths.Add(_fileresponsePath);
                                var oldCloudPath = oldFile.FilePath.Substring(oldFile.FilePath.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase) + rootFolder.Length).Replace("\\", " /");
                                _googleCloudService.DeleteFileToCloud(oldCloudPath);
                            });
                            switch (oldFiles.Count.CompareTo(request.UpdateMedia.Count))
                            {
                                case 0:
                                    {
                                        
                                        var count = 0;
                                        oldFiles.ForEach(oldFile =>
                                        {
                                            _googleCloudService.UploadFileToCloud(request.UpdateMedia[count]);

                                            oldFile.FilePath = request.UpdateMedia[count].Substring(request.UpdateMedia[count].IndexOf(rootPath, StringComparison.OrdinalIgnoreCase) + rootPath.Length).Replace("\\", "/");
                                            count++;
                                        });
                                        _context.PostImages.UpdateRange(oldFiles);
                                        await _context.SaveChangesAsync(cancellationToken);
                                        break;
                                    }
                                case 1 :
                                case -1:
                                    {
                                        _context.PostImages.RemoveRange(oldFiles);
                                       await _context.SaveChangesAsync(cancellationToken);
                                        List<PostImage> newPostImages = new List<PostImage>();
                                        request.UpdateMedia.ForEach(file =>
                                        {
                                            _googleCloudService.UploadFileToCloud(file);

                                            var returnPath = file.Substring(file.IndexOf(rootPath, StringComparison.OrdinalIgnoreCase) + rootPath.Length).Replace("\\", "/");
                                            PostImage postImage = new PostImage()
                                            {
                                                FilePath = returnPath,
                                                PostId = request.UpdatePostId,
                                                State = true
                                            };
                                            newPostImages.Add(postImage);
                                        });
                                        _context.PostImages.AddRange(newPostImages);
                                        
                                        await _context.SaveChangesAsync(cancellationToken);
                                        break;                                    
                                    }
                               
                            }
                        }
                        else
                        {
                            List<PostImage> postImages = new List<PostImage>();
                            request.UpdateMedia.ForEach(file =>
                            {
                              
                                var returnPath = file.Substring(file.IndexOf(rootPath, StringComparison.OrdinalIgnoreCase) + rootPath.Length).Replace("\\", "/");
                                PostImage postImage = new PostImage()
                                {
                                    FilePath = returnPath,
                                    PostId = request.UpdatePostId,
                                    State = true
                                };
                                postImages.Add(postImage);
                            });
                            await _context.PostImages.AddRangeAsync(postImages);
                            await _context.SaveChangesAsync(cancellationToken);
                        }
                        request.UpdateMedia.ForEach(reqFile =>
                        {
                            _googleCloudService.UploadFileToCloud(reqFile);
                        });
                        _context.Posts.Update(updatePost);
                       await _context.SaveChangesAsync(cancellationToken);

                        break;
                    }
                case { UpdateContent: not null, UpdateMedia: null }:
                    {
                        updatePost.Content = request.UpdateContent;
                        _context.Posts.Update(updatePost);
                        await _context.SaveChangesAsync(cancellationToken);
                        break;
                    }
                case { UpdateContent: not null, UpdateMedia: not null}:

                    if (oldFiles is not null)
                    {
                        oldFiles.ForEach(oldFile =>
                        {
                            UpdatePostCommandResponse _fileresponsePath = new UpdatePostCommandResponse() { OldFilePath = oldFile.FilePath };
                            filePaths.Add(_fileresponsePath);
                            var oldCloudPath = oldFile.FilePath.Substring(oldFile.FilePath.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase) + rootFolder.Length).Replace("\\", " /");
                            _googleCloudService.DeleteFileToCloud(oldCloudPath);
                        });
                        switch (oldFiles.Count.CompareTo(request.UpdateMedia.Count))
                        {
                            case 0:
                                {
                                    var count = 0;
                                    oldFiles.ForEach(oldFile =>
                                    {
                                        var returnPath = request.UpdateMedia[count].Substring(request.UpdateMedia[count].IndexOf(rootPath, StringComparison.OrdinalIgnoreCase) + rootPath.Length).Replace("\\", "/");
                                        oldFile.FilePath = returnPath;
                                        count++;
                                    });
                                    _context.PostImages.UpdateRange(oldFiles);
                                    await _context.SaveChangesAsync(cancellationToken);
                                    break;
                                }
                            case 1:
                            case -1:
                                {
                                    _context.PostImages.RemoveRange(oldFiles);
                                    await _context.SaveChangesAsync(cancellationToken);
                                    List<PostImage> newPostImages = new List<PostImage>();
                                    request.UpdateMedia.ForEach(file =>
                                    {
                                        var returnPath = file.Substring(file.IndexOf(rootPath, StringComparison.OrdinalIgnoreCase) + rootPath.Length).Replace("\\", "/");

                                        PostImage postImage = new PostImage()
                                        {
                                            FilePath = returnPath,
                                            PostId = request.UpdatePostId,
                                            State = true
                                        };
                                        newPostImages.Add(postImage);
                                    });
                                    _context.PostImages.AddRange(newPostImages);

                                    await _context.SaveChangesAsync(cancellationToken);
                                    break;
                                }

                        }
                    }
                    else
                    {
                        List<PostImage> postImages = new List<PostImage>();
                        request.UpdateMedia.ForEach(file =>
                        {
                            string returnPath = file.Substring(file.IndexOf(rootPath, StringComparison.OrdinalIgnoreCase) + rootPath.Length).Replace("\\", "/");
                            PostImage postImage = new PostImage()
                            {
                                FilePath = returnPath,
                                PostId = request.UpdatePostId,
                                State = true
                            };
                            postImages.Add(postImage);
                        });
                        await _context.PostImages.AddRangeAsync(postImages);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    request.UpdateMedia.ForEach(reqFile =>
                     {
                        _googleCloudService.UploadFileToCloud(reqFile);
                    });     
                    updatePost.Content = request.UpdateContent;
                    _context.Posts.Update(updatePost);
                    await _context.SaveChangesAsync(cancellationToken);
                    break;
            }           
           return new SuccessDataResult<List<UpdatePostCommandResponse>>(filePaths, "Post update successfully");
        }
    }
}
