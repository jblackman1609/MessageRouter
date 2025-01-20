using System;
using Router.Domain.MessageAggregate;
using Router.Domain.TenantRecipientAggregate;
using Router.External.DataModels;

namespace Router.External;

public static class Mappers
{
    public static MessageModel Map(this Message message)
    {
        return new MessageModel();
    }

    public static Template Map(this TemplateModel template)
    {
        return new Template(template.Id, 
            template.Title!, 
            (TemplateType)Enum.Parse(typeof(TemplateType), template.Type!));
    }

    public static TemplateModel Map(this Template template)
    {
        return new TemplateModel();
    }

    public static TenantRecipientModel Map(this TenantRecipient tenantRecipient)
    {
        return new TenantRecipientModel();
    }

    public static TenantRecipient Map(this TenantRecipientModel tenantRecipient)
    {
        return new TenantRecipient(tenantRecipient.Id, 
            tenantRecipient.TenantId, 
            tenantRecipient.RecipientId);
    }

    public static Tenant Map(this TenantModel tenant)
    {
        return new Tenant(tenant.Phone!);
    }
}
