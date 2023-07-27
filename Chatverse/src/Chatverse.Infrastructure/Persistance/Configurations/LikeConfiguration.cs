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
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ConfigureAuditableBaseEntity();
            builder.Property(l => l.AppUserId).IsRequired();
            builder.Property(l => l.PostId).IsRequired();
        }
    }
}
