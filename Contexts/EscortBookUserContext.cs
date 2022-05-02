using EscortBookUser.Models;
using Microsoft.EntityFrameworkCore;

namespace EscortBookUser.Contexts
{
    public class EscortBookUserContext : DbContext
    {
        public EscortBookUserContext(DbContextOptions<EscortBookUserContext> options) : base(options) {}

        public DbSet<Avatar> Avatars { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ConnectionLog> ConnectionLogs { get; set; }

        public DbSet<RequestLog> RequestLogs { get; set; }

        public DbSet<Dictum> Dictums { get; set; }

        public DbSet<StatusCategory> StatusCategories { get; set; }
    }
}