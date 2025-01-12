using System;

namespace Router.Contracts.Models;

public class MLResponse<TMLResponseData> where TMLResponseData : class
{
    public TMLResponseData? Data { get; set; }
}
