namespace Chatverse.Infrastructure.Persistance.Configurations;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ConfigureAuditableBaseEntity();
        builder.Property(l => l.AppUserId).IsRequired();
        builder.Property(l => l.PostId).IsRequired();
    }
}
