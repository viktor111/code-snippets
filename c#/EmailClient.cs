public static class EmailClient
{
    public static async Task Send(string emailAddress, string subject, string body)
    {
        var fromAddress = new MailAddress("myeuroins@euroins.bg", "euroins.bg");
        var toAddress = new MailAddress(emailAddress, emailAddress);
        const string fromPassword = "Sofia2022!";

        await GetSmtp(fromAddress, fromPassword).SendMailAsync(GetMessage(body, fromAddress, toAddress, subject), CancellationToken.None);
    }
    
    private static MailMessage GetMessage(string body, MailAddress fromAddress, MailAddress toAddress, string subject)
    {
        return new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };
    }
    
    private static SmtpClient GetSmtp(MailAddress mailAddress, string password)
    {
        return new SmtpClient
        {
            Host = "Mail.profonika.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(mailAddress.Address, password)
        };
    }
}