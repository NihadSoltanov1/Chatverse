global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Chatverse.Application.Common.Interfaces.MongoDb;
global using Chatverse.Domain.Common;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Chatverse.Domain.Entities;
global using Chatverse.Infrastructure.Persistance.Configurations.Common;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Chatverse.Application.Common.Interfaces;
global using Chatverse.Domain.Identity;
global using Chatverse.Infrastructure.Persistance.Interceptors;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.VisualBasic;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Http;
global using System.Net.Mail;
global using System.Net;
global using Microsoft.AspNetCore.Identity;
global using Chatverse.Application.Exceptions;
global using Microsoft.AspNetCore.WebUtilities;
global using Chatverse.Application.Common.Results;
global using Microsoft.AspNetCore.Hosting;

global using Google.Apis.Auth.OAuth2;
global using Google.Cloud.Storage.V1;
global using Microsoft.Extensions.Configuration;

global using Chatverse.Application.Features.Command.Story.DeleteStory;
global using Hangfire;
global using MediatR;

global using Chatverse.Application.Common.Security.Jwt;
global using Chatverse.Application.DTOs.Token;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;


global using Chatverse.Infrastructure.Persistance;
global using Chatverse.Infrastructure.Services;
global using Microsoft.Extensions.DependencyInjection;
global using Chatverse.Infrastructure.MongoDB.Persistance.Settings;
global using Chatverse.Application.Common.Interfaces.MongoDb;
global using Microsoft.Extensions.Options;
