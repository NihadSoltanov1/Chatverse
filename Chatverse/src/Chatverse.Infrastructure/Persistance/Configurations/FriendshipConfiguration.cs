namespace Chatverse.Infrastructure.Persistance.Configurations;

public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
{
    public void Configure(EntityTypeBuilder<Friendship> builder)
    {
        builder.ConfigureAuditableBaseEntity();
        builder.Property(f => f.SenderId).IsRequired();
        builder.Property(f => f.ReceiverId).IsRequired();
    }
}
