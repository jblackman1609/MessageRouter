using System;
using Microsoft.ML.Data;

namespace Router.Core.Models;

public class PIIData
{
    [LoadColumn(1)]
    public bool Label { get; set; }

    [LoadColumn(2)]
    public string? BodyText { get; set; }
}
