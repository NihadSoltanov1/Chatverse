namespace Chatverse.Domain.Common;

    public class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {

        [Key]
        public TKey Id { get; set; } = default!;
    }

    public class BaseEntity : BaseEntity<int> { }

