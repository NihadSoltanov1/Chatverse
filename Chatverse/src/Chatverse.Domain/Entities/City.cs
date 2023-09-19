namespace Chatverse.Domain.Entities;
    public class City : BaseEntity
    {
        public string Name { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }

