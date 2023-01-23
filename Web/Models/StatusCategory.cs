using System.ComponentModel.DataAnnotations.Schema;

namespace EscortBookUser.Web.Models;

[Table("status_category", Schema = "public")]
public class StatusCategory : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("active")]
    public bool Active { get; set; } = true;
}
