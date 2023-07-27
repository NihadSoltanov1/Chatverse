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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ConfigureAuditableBaseEntity();
            builder.Property(p => p.AppUserId).IsRequired();
        }
    }
}
