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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ConfigureAuditableBaseEntity();
            builder.Property(c => c.Content).IsRequired();
            builder.Property(c => c.AppUserId).IsRequired();
            builder.Property(c => c.PostId).IsRequired();
        }
    }
}
