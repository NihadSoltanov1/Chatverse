using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Domain.Common
{
    public class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {

        [Key]
        public TKey Id { get; set; } = default!;
    }

    public class BaseEntity : BaseEntity<int> { }
}
