using System;

namespace Router.Contracts.Models;

public class MessageDto
{
    public TenantMessage? TenantMessage { get; set; }
    public MessageStatus Status { get; set; }
    public List<string>? MessageLogs { get; set; }
    public string? GuidId { get; set; } = Guid.NewGuid().ToString();
}

public enum MessageStatus
{
    Accepted,
    Rejected,
    Approved,
    Blocked,
    Transmitted,
    Declined
}

public class MessageLog
{
    public const string ACCEPTED_LOG = "Message is accepted.";
    public const string REJECTED_LOG = "Message rejected. Mal-configured.";
    public const string APPROVED_LOG = "Message does not contain PII. Approved for transmission.";
    public const string BLOCKED_PII_LOG = "Message contains PII. Blocked for transmission.";
    public const string TRANSMITTED_LOG = "Message validated. Approved for transmisison.";
    public const string DECLINED_LOG = "Message verified contains PII. Declined for transmission.";
}
