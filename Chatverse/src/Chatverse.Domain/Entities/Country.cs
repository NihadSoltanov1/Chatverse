namespace Chatverse.Domain.Entities;
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }

