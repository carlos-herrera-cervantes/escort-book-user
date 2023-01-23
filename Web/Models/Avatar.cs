using System.ComponentModel.DataAnnotations.Schema;

namespace EscortBookUser.Web.Models;

[Table("avatar", Schema = "public")]
public class Avatar : BaseEntity
{
    [Column("user_id")]
    public string UserId { get; set; }

    [Column("path")]
    public string Path { get; set; }
}
