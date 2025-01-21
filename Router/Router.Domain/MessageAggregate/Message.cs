using System;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Router.Domain.MessageAggregate;

public class Message : AggregateRoot
{
    private MessageStatus _status;
    private string _body = "";
    private readonly List<string> _messageLogs = [];    
    public string GuidId { get; private set; } = null!;
    public string Subject { get; private set; } = null!;
    public string RecipientPhone { get; private set; } = null!;
    public string TenantPhone { get; private set; } = null!;
    public decimal TemplateId { get; private set; }

    public Message(string subject, string tenantPhone, string recipientPhone, decimal templateId)
    {
        GuidId = Guid.NewGuid().ToString();        
        Subject = subject;
        RecipientPhone = recipientPhone;
        TenantPhone = tenantPhone;
        TemplateId = templateId;
        _status = MessageStatus.Accepted;
    }

    public Message() { }

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
