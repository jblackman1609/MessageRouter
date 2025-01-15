using System;

namespace Router.Contracts.Models;

public class SmsModel
{
    public string Body { get; set; }
    public string Subject { get; set; }
    public string FromPhone { get; set; }
    public string ToPhone { get; set; }

    public SmsModel(string body, string subject, string from, string to)
    {
        Body = body;
        Subject = subject;
        FromPhone = from;
        ToPhone = to;
    }
}
