namespace Chatverse.Infrastructure.Persistance.Configurations.Common;

public static class ConfigurationExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureAuditableBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : BaseAuditableEntity
    {
        builder.Property(e => e.CreatedDate).HasDefaultValueSql("Getutcdate()");
        return builder;
    }
}
