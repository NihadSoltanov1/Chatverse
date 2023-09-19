namespace Chatverse.Infrastructure.Persistance.Configurations;

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
