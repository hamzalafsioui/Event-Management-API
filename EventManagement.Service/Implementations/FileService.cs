using EventManagement.Service.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EventManagement.Service.Implementations
{
	public class FileService : IFileService
	{
		#region Fields
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;
		#endregion
		#region Constructors
		public FileService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_webHostEnvironment = webHostEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}
		#endregion
		#region Actions
		public async Task<string> UploadImageAsync(string folderName, IFormFile file)
		{
			if (file == null || file.Length == 0)
				throw new ArgumentException("File cannot be empty", nameof(file));

			var context = _httpContextAccessor?.HttpContext?.Request;
			var baseURL = context?.Scheme + "://" + context?.Host;

			// Ensure Folder Path
			var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			// Generate unique file name
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
			var fullPath = Path.Combine(folderPath, fileName);
			using (var fileStream = new FileStream(fullPath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			return $"{baseURL}/{folderName}/{fileName}";

		}
		#endregion

	}
}
