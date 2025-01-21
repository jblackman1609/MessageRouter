using System;

namespace Router.Domain.TemplateAggregate;

public interface ITemplateRepository
{
    Task<Template> GetTemplateAsync(decimal id);
    Task UpdateTemplateAsync(Template template);
}
