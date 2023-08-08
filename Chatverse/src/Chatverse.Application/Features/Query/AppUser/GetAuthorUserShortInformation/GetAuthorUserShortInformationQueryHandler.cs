using Chatverse.Application.Common.Interfaces;
using Chatverse.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.AppUser.GetAuthorUserShortInformation
{
    public class GetAuthorUserShortInformationQueryHandler : IRequestHandler<GetAuthorUserShortInformationQueryRequest, GetAuthorUserShortInformationQueryResponse>
    {
       private readonly IApplicationDbContext _context;
       private readonly UserManager<Domain.Identity.AppUser> _userManager;
       private readonly ICurrentUserService _currentUserService;

        public GetAuthorUserShortInformationQueryHandler(IApplicationDbContext context, UserManager<Domain.Identity.AppUser> userManager, ICurrentUserService currentUserService)
        {
            _context = context;
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<GetAuthorUserShortInformationQueryResponse> Handle(GetAuthorUserShortInformationQueryRequest request, CancellationToken cancellationToken)
        {
            var getCurrentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            return new GetAuthorUserShortInformationQueryResponse()
            {
                FullName = getCurrentUser.FullName,
                About = getCurrentUser.About,
                Username = getCurrentUser.UserName,
                Privicy = getCurrentUser.Privicy ? "Private" : "Public",
                CityAndCounty = $"{_context.Cities.FirstOrDefault(x=>x.Id==getCurrentUser.CityId).Name}, {_context.Countries.FirstOrDefault(x => x.Id == getCurrentUser.CountryId).Name}",
                BackgroundPicture = getCurrentUser.BackgroundPicture,
                ProfilePicture = getCurrentUser.ProfilePicture,
              BirthDate = getCurrentUser.BirthDate
                
            };
        }
    }
}
