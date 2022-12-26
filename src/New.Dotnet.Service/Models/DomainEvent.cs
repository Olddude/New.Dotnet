using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace New.Dotnet.Service.Models;

public class DomainEvent
{
    public DomainEvent()
    {
    }

    public DomainEvent(string type, string payload, string? errors = null)
    {
        Id = Guid.NewGuid();
        Type = type;
        Payload = payload;
        Errors = errors;
        CreatedAt = DateTime.UtcNow;
    }
    
    [Required]
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Type { get; set; }

    [Required]
    [Column(TypeName = "jsonb")]
    public string Payload { get; set; }
    
    [Column(TypeName = "jsonb")]
    public string? Errors { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
}