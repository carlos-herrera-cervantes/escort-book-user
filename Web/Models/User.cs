using System.ComponentModel.DataAnnotations.Schema;

namespace EscortBookUser.Web.Models;

[Table("user", Schema = "public")]
public class User : BaseEntity
{
    [Column("user_id")]
    public string UserId { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; }

    [Column("last_name")]
    public string LastName { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("deleted")]
    public bool Delete { get; set; } = false;
}
