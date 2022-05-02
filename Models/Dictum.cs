using System.ComponentModel.DataAnnotations.Schema;

namespace EscortBookUser.Models
{
    [Table("dictum", Schema = "public")]
    public class Dictum : BaseEntity
    {
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("from_user")]
        public string FromUser { get; set; }

        [Column("status_category_id")]
        public string StatusCategoryId { get; set; }

        [Column("comment")]
        public string Comment { get; set; }
    }
}
