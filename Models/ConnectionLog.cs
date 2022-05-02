using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscortBookUser.Models
{
    [Table("connection_log", Schema = "public")]
    public class ConnectionLog
    {
        [Column("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("last_connection")]
        public DateTime LastConnection { get; set; } = DateTime.UtcNow;
    }
}
