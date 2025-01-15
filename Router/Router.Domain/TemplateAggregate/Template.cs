using System;

namespace Router.Domain.TemplateAggregate;

public class Template : AggregateRoot
{
    public string? Text { get; private set; }
    public TemplateType Type { get; private set; }
    public TemplateStatus Status { get; private set; }
}
