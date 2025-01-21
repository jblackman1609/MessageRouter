using System;

namespace Router.Domain.MessageAggregate;

public enum MessageStatus
{
    Accepted,
    Rejected,
    Approved,
    Denied,
    PendingReview,
    DataLocked,
    Transmitted,
    Failed
}
