namespace Chatverse.Infrastructure.Persistance.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ConfigureAuditableBaseEntity();
        builder.Property(p => p.AppUserId).IsRequired();
    }
}
