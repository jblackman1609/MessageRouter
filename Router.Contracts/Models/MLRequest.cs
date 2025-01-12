using System;

namespace Router.Contracts.Models;

public class MLRequest<TMLData> where TMLData : class
{
    public TMLData? Data { get; set; }
}
