namespace MessengerAPI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MessengerContext : DbContext
    {
        public MessengerContext()
            : base("name=MessengerContext")
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMessage> GroupMessages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserMessage> UserMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("GroupUser").MapLeftKey("GroupId").MapRightKey("UserId"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.GroupMessages)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.SenderId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserMessages)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.ReceiverId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserMessages1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.SenderId);
        }
    }
}
