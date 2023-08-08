using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.AppUser.GetAuthorUserShortInformation
{
    public record GetAuthorUserShortInformationQueryResponse
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string? ProfilePicture { get; set; }
        public string? BackgroundPicture { get; set; }
        public string? About { get; set; }
        public string? CityAndCounty { get; set; }
        public string Privicy { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
