using System;

namespace Router.Domain.MessageAggregate;

public interface ITemplateRepository
{
    Task<Template> GetTemplateAsync(decimal id);
    Task UpdateTemplateAsync(Template template);
}
