using Chatverse.Domain.Common;
using Chatverse.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
