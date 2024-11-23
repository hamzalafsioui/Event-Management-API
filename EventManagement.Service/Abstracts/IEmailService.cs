namespace EventManagement.Service.Abstracts
{
	/// <summary>
	/// Interface defining email-related service operations.
	/// </summary>
	public interface IEmailService
	{
		/// <summary>
		/// Sends an email asynchronously to the specified <paramref name="email"/> with the provided <paramref name="message"/> and <paramref name="subject"/>.
		/// </summary>
		/// <param name="email">The recipient's email address.</param>
		/// <param name="message">The content of the email message in HTML format.</param>
		/// <param name="subject">The subject of the email.</param>
		/// <returns>
		/// A <see cref="string"/> result indicating the outcome of the operation:
		/// <list type="bullet">
		/// <item><description><c>"Success"</c>: If the email was sent successfully.</description></item>
		/// <item><description><c>"Failed"</c>: If an error occurred during the email sending process.</description></item>
		/// </list>
		/// </returns>
		public Task<string> SendEmailAsync(string email, string message, string subject);
	}
}
