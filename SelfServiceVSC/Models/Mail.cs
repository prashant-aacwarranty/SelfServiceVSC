using AAC.Libraries;
using MailKit.Security;
using MimeKit;

namespace AAC.SelfServiceVSC.Models
{
	public static class Mail
	{
		public static void Send(
			String to,
			String toName,
			String subject,
			String body,
			Stream[] attachmentStreams,
			String[] attachmentNames)
		{
			var senderEmail = new MailboxAddress("AAC Warranty", "programming@aacwarranty.com");
			var receiverEmail = new MailboxAddress(toName, to);

			var message = new MimeMessage();
			message.From.Add(senderEmail);
			message.To.Add(receiverEmail);
			message.Subject = subject;
			var builder = new BodyBuilder();
			builder.TextBody = body;
			for (Int32 i = 0; i < attachmentStreams.Length; i++)
			{
				builder.Attachments.Add(new MimePart()
				{
					Content = new MimeContent(attachmentStreams[i]),
					ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
					ContentTransferEncoding = ContentEncoding.Base64,
					FileName = attachmentNames[i]
				});
			}
			message.Body = builder.ToMessageBody();
			var smtp = new MailKit.Net.Smtp.SmtpClient();
			smtp.Connect(SelfServiceVSC.AppSettings.MailServer, SelfServiceVSC.AppSettings.MailPort, SecureSocketOptions.SslOnConnect);
			smtp.Authenticate(SelfServiceVSC.AppSettings.MailUsername, SelfServiceVSC.AppSettings.MailPassword);
			try
			{
				smtp.Send(message);
				smtp.Disconnect(true);
			}
			catch (Exception ex)
			{
				Logger.WriteError(ex);
			}
		}
	}
}
