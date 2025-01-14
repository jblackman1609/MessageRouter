using System;

namespace Router.Contracts.Models;

public class EmailModel
{
    public string Body { get; set; }
    public string Subject { get; set; }
    public string FromAddress { get; set; }
    public string[] ToAddresses { get; set; }

    public EmailModel(string body, string subject, string fromAddress, string[] toAddresses)
    {
        Body = body;
        Subject = subject;
        FromAddress = fromAddress;
        ToAddresses = toAddresses;
    }

    public void AddToAddress(string address)
    {
        ToAddresses.Append(address);
    }
}
