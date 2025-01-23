using System;
using Router.Domain.MessageAggregate;

namespace Router.Core.Models;

public class HandlerResponse
{
    public bool IsSuccess { get; set; }
    private IReadOnlyList<MessageStatus> _statuses = [];
    private decimal _transmittedStatusCount { get; set; }

    public IReadOnlyList<MessageStatus> Statuses => _statuses.ToList();
    public decimal TransmittedStatusCount => _transmittedStatusCount;

    public void AddStatus(MessageStatus status)
    {
        _statuses.ToList().Add(status);
    }
    
    public void CountTransmitted()
    {
        _transmittedStatusCount = _statuses
            .Where(s => s == MessageStatus.Transmitted)
            .Count();
    }
}
