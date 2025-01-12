using System;
using System.Data.Common;

namespace Router.Domain.Exceptions;

public class NotFoundException<TModel> : Exception where TModel : class
{
    public NotFoundException(decimal id) : base($"{nameof(TModel)} with {id} not found.") { }
    public NotFoundException(string text) : base($"{nameof(TModel)} with {text} not found.") { }
}
