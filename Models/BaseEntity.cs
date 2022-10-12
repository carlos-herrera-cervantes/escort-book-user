using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscortBookUser.Models;

public class BaseEntity
{
    [Column("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
