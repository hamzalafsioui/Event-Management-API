using Microsoft.AspNetCore.Http;

namespace EventManagement.Service.Abstracts
{
	public interface IFileService
	{
		public Task<string> UploadImageAsync(string location, IFormFile file);
	}
}
