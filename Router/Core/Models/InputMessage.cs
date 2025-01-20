using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class InputMessage
{
    [Required]
    public string? TenantPhone { get; set; }
    
    [Required]
    public Dictionary<string, string>? Keywords { get; set; }

    [Required]
    public decimal TemplateId { get; set; }

    [Required]
    public string? Subject { get; set; }

    [Required]
    public List<string>? RecipientPhones { get; set; }
}
