using System.ComponentModel.DataAnnotations.Schema;

namespace EscortBookUser.Models
{
    [Table("request_log", Schema = "public")]
    public class RequestLog : BaseEntity
    {
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("component")]
        public string Component { get; set; }

        [Column("path")]
        public string Path { get; set; }

        [Column("method")]
        public string Method { get; set; }

        [Column("payload")]
        public string Payload { get; set; }
    }
}
