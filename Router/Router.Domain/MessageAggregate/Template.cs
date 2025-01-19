using System;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Router.Domain.MessageAggregate;

public class Template : AggregateRoot
{
    private TemplateStatus _status;
    private IReadOnlyList<decimal> _messageIds = [];
    private string _text = "";
    public string Title { get; private set; }
    public TemplateType Type { get; private set; }
    public decimal TenantId { get; private set; }
    

    public Template(decimal id, string title, TemplateType type) :
        base(id)
    {
        Title = title;
        _status = TemplateStatus.Pending;
        Type = type;
    }

    public IReadOnlyList<decimal> MessageIds => _messageIds;
    public TemplateStatus Status => _status;    
    public string Text => _text;

    public void UpdateStatus(TemplateStatus status)
    {
        if (_status != status)
            _status = status;

        throw new ArgumentException($"Status is already set to {status}");
    }

    public void AddMessageId(decimal messageId)
    {
        if (!_messageIds.Contains(messageId))
        {
            _messageIds.ToList().Add(messageId);
        }
        
        throw new ArgumentException($"Message with Id: {messageId} already exists.");
    }

    public void AddText(string text)
    {
        if (!_text.Contains(text))
        {
            _text = text;
        }

        else throw new ArgumentException($"Temnplate already has '{text}' as text.");
    }

    public Message Create(string sub, string tPhone, string rPhone)
    {
        if (_status != TemplateStatus.Approved)
        {
            throw new ArgumentException($"Template must be approved for use.");
        }
        
        return new Message(sub, tPhone, rPhone, this.Id);
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
