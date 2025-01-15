using System;
using System.ComponentModel;

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
