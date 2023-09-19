namespace Chatverse.Application.Common.Interfaces;
    public interface IApplicationDbContext 
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<HubConnection> HubConnections { get; set; }
        public DbSet<NotificationCategory> NotificationCategories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SocialAccount> SocialAccounts { get; set; }
        public DbSet<Story> Stories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

