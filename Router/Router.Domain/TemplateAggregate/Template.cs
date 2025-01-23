using System;
using System.Data.Common;
using System.Reflection;
using System.Text;
using Router.Domain.MessageAggregate;

namespace Router.Domain.TemplateAggregate;

public class Template : AggregateRoot
{
    private TemplateStatus _status;
    private IReadOnlyList<decimal> _messageIds = [];
    private string _text = "";
    public string Title { get; private set; }
    public TemplateType Type { get; private set; }
    public decimal TenantId { get; private set; }    

    public Template(string title, TemplateType type, decimal tenantId)
    {
        Title = title;
        _status = TemplateStatus.Pending;
        Type = type;
        TenantId = tenantId;
    }

    public IReadOnlyList<decimal> MessageIds => _messageIds;
    public TemplateStatus Status => _status;    
    public string Text => _text;

    public void AddMessageId(decimal messageId)
    {
        if (!_messageIds.Contains(messageId))
        {
            _messageIds.ToList().Add(messageId);
        }
        
        throw new ArgumentException($"Message with Id: {messageId} already exists.");
    }

    public Message CreateMessage(
        string sub, string tPhone, string rPhone)
    {
        if (_status is not TemplateStatus.Approved)
            throw new ArgumentException($"Template status must be in an approved state.");

        return new Message(sub, tPhone, rPhone,  Id);
    }
}

public enum TemplateStatus
{
    Pending, Approved, denied
}

public enum TemplateType
{
    OptIn, Help, Standard
}
