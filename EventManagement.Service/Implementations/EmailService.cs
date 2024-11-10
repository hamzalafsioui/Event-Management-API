using EventManagement.Data.Helper.Email;
using EventManagement.Service.Abstracts;
using MailKit.Net.Smtp;
using MimeKit;

namespace EventManagement.Service.Implementations
{
	public class EmailService : IEmailService
	{
		private readonly SmtpSettings _smtpSettings;
		#region Fields

		#endregion
		#region Constructors
		public EmailService(SmtpSettings smtpSettings)
		{
			this._smtpSettings = smtpSettings;
		}
		#endregion
		#region Handle Functions 
		public async Task<string> SendEmailAsync(string toEmail, string message)
		{
			try
			{

				using (var client = new SmtpClient())
				{
					await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, _smtpSettings.EnableSsl);
					client.Authenticate(_smtpSettings.Username, _smtpSettings.Password);

					var SMS = new MimeMessage();
					SMS.Body = new TextPart("html")
					{
						Text = message
					};
					SMS.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
					SMS.To.Add(MailboxAddress.Parse(toEmail));
					SMS.Subject = "Confirm Email";
					await client.SendAsync(SMS);
					await client.DisconnectAsync(true);
				}
				return "Success";
			}
			catch (Exception ex)
			{
				return "Failed";
			}
		}
		#endregion

	}
}
