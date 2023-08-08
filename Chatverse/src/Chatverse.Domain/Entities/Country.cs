using Chatverse.Domain.Common;
using Chatverse.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
