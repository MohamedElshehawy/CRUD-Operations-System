using Demo.DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace Demo.Pl.Utility
{
	public static class MailSettings
	{
		public static void SendEmail(Email email)
		{
			// Mail Servie Gmail
			// Client => send Email
			//
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mohamed2590545@gmail.com", "xxhzynddbgzxpnuu");
			// 2 step  varification => in gmail
			//App Password

			// Send email
			client.Send("mohamed2590545@gmail.com",email.Recipient,email.Subject,email.Body);

		}

	}
}
