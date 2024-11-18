using Microsoft.AspNetCore.Http;

namespace EventManagement.Service.Abstracts
{
	/// <summary>
	/// Interface defining file management operations, such as image uploads.
	/// </summary>
	public interface IFileService
	{
		/// <summary>
		/// Uploads an image file to the specified folder asynchronously, optionally replacing an old file.
		/// </summary>
		/// <param name="folderName">The name of the folder where the image will be uploaded.</param>
		/// <param name="file">The <see cref="IFormFile"/> representing the image file to upload.</param>
		/// <param name="oldFile">The name of the old file to replace, if applicable. Can be <see langword="null"/>.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. The task result contains the path to the uploaded file as a <see cref="string"/>.
		/// </returns>

		public Task<string> UploadImageAsync(string folderName, IFormFile file, string? oldFile);
	}
}
