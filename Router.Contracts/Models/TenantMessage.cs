using System;
using System.ComponentModel.DataAnnotations;

namespace Router.Contracts.Models;

public class TenantMessage
{
    [Required]
    public string? Body { get; set; }

    [Required]
    public string? Subject { get; set; }

    [Required]
    [Phone]
    public string? ToPhone { get; set; }

    [Required]
    public decimal TemplateId { get; set; }
}
