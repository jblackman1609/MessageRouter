using System;

namespace Router.Domain.Models;

public class Tenant
{
    public string? Name { get; set; }
    public string? FromPhone { get; set; }
    public string? ClientId { get; set; }
    public string? ResponseUrl { get; set; }
    public string? Type { get; set; }
    public List<Template>? Templates { get; set; }
}
