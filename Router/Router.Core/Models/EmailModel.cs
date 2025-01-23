using System;

namespace Router.Core.Models;

public class EmailModel
{
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public string? FromAddress { get; set; }
    public List<string>? ToAddresses { get; set; }
}
