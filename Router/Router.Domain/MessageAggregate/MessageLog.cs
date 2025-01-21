using System;

namespace Router.Domain.MessageAggregate;

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
    public const string APPROVED_PII_OVERRIDE = "PII review complete - Confirmed message contains no PII.";
    public const string TRANSMITTED = "Message has been transmitted to the provider.";
    public const string FAILED = "Message failed to be transmitted to the provider.";
}
