using System;

namespace Router.Domain.MessageAggregate;

public class Message : AggregateRoot
{
    public string GuidId { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    private MessageStatus _status;
    private readonly List<string> _messageLogs = [];
    public string RecipientPhone { get; private set; }

    public Message(decimal id, string subject, string body, string recipientPhone) : 
        base(id)
    {
        GuidId = Guid.NewGuid().ToString();        
        Subject = subject;
        Body = body;
        RecipientPhone = recipientPhone;
    }

    public IReadOnlyCollection<string> MessageLogs => _messageLogs.ToList();

    public MessageStatus Status => _status;
    
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
        _status = status;
        LastUpdated = DateTime.UtcNow;
        UpdatedBy = "";
    }
}
