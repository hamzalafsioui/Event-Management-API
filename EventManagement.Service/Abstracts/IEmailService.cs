namespace EventManagement.Service.Abstracts
{
	public interface IEmailService
	{
		public Task<string> SendEmailAsync(string email, string message, string subject);
	}
}
