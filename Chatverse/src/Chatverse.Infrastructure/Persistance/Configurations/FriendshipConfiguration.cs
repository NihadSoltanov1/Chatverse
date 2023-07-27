using Chatverse.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatverse.Infrastructure.Persistance.Configurations.Common;

namespace Chatverse.Infrastructure.Persistance.Configurations
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ConfigureAuditableBaseEntity();
            builder.Property(f => f.SenderId).IsRequired();
            builder.Property(f => f.ReceiverId).IsRequired();
        }
    }
}
