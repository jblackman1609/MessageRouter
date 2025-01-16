using System;
using System.Reflection;
using System.Text;

namespace Router.Domain.MessageAggregate;

public class Template : AggregateRoot
{
    private TemplateStatus _status;
    public string Title { get; private set; }
    public string Text { get; private set; }
    public TemplateStatus Status => _status;
    public TemplateType Type { get; private set; }
    private IReadOnlyList<KeyValuePair<string, string>> _keywords = [];
    private IReadOnlyList<Message> _messages = [];

    public Template(decimal id, string title, string text) :
        base(id)
    {
        Title = title;
        Text = text;
        _status = TemplateStatus.Pending;
    }

    public IReadOnlyList<KeyValuePair<string, string>> Keywords => _keywords;
    public IReadOnlyList<Message> Messages => _messages;

    public void AddKeyword(string key, string value)
    {
        _keywords.ToList().Add(new KeyValuePair<string, string>(key, value));
    }

    public void UpdateStatus(TemplateStatus status)
    {
        _status = status;
    }

    private string BuildMessageBody(string text, List<KeyValuePair<string, string>> keywords)
    {
        string body = $"";
        foreach (KeyValuePair<string, string> kvp in keywords)
        {
            body.Concat(kvp.Value);
        }

        return body;        
    }

    public void AddMessage(string sub, string body, string from, string to)
    {
        if (_status != TemplateStatus.Approved)
        {
            throw new ArgumentException($"Template must be approved for use.");
        }

        Message message = new Message(sub, body, from, to);

        if (!_messages.Contains(message))
        {
            _messages.ToList().Add(message);
        }
        
        throw new ArgumentException($"Message {message.Subject} already exists.");
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
