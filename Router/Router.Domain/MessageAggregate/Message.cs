using System;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Router.Domain.MessageAggregate;

public class Message
{
    private MessageStatus _status;
    private string _body = "";
    private readonly List<string> _messageLogs = [];    
    public string GuidId { get; private set; }
    public string Subject { get; private set; }
    public string RecipientPhone { get; private set; }
    public string TenantPhone { get; private set; }
    public decimal TemplateId { get; private set; }

    public Message(string subject, string tenantPhone, string recipientPhone, decimal templateId)
    {
        GuidId = Guid.NewGuid().ToString();        
        Subject = subject;
        RecipientPhone = recipientPhone;
        TenantPhone = tenantPhone;
        TemplateId = templateId;
    }

    public IReadOnlyCollection<string> MessageLogs => _messageLogs.ToList();

    public MessageStatus Status => _status;

    public string Body => _body;

    public string BuildMessageBody(string text, Dictionary<string, string> fields)
    {
        Regex re = new Regex(@"\{([^\}]+)\}", RegexOptions.Compiled);
        
        if (fields is null)
            return text;

        return re.Replace(text, delegate(Match match)
        {
            return fields[match.Groups[1].Value];
        });        
    }

    public void AddMessageLog(string log)
    {
        if (!_messageLogs.Contains(log))
        {
            _messageLogs.Add(log);
        }

        else throw new ArgumentException($"The log: {log} already exists.");
    }

    public void UpdateStatus(MessageStatus status)
    {
        if(_status != status)
        {
            _status = status;
        }
    }
}

public enum MessageStatus
{
    Accepted,
    Rejected,
    Approved,
    Denied,
    PendingReview,
    DataLocked,
    Transmitted,
    Failed
}

public static class MessageLogs
{
    public const string ACCEPTED = "Message was accepted successfully.";
    public const string REJECTED = "Message rejected - Invalid input data.";
    public const string APPROVED = "Message has been approved for transmission.";
    public const string DENIED_OPTIN = "Message denied transmission - Recipient has not opted in.";
    public const string DENIED_BLOCKED = "Message denied transmission - Recipient blocked.";
    public const string DENIED_PII = "Message denied transmission - PII found.";
    public const string PENDING_REVIEW = "Message pending PII review.";
    public const string DATA_LOCKED = "Message has been data locked - PII has been confirmed.";
    public const string TRANSMITTED = "Message has been transmitted to the provider.";
    public const string FAILED = "Message failed to be transmitted to the provider.";
}
