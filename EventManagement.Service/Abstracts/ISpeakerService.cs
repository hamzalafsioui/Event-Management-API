using EventManagement.Data.Entities;
using EventManagement.Data.Helper;

namespace EventManagement.Service.Abstracts
{
	public interface ISpeakerService
	{
		/// <summary>
		/// Retrieves a list of all <see cref="Speaker"/> entities asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> of <see cref="Speaker"/> entities.</returns>
		public Task<List<Speaker>> GetSpeakersListAsync();

		/// <summary>
		/// Retrieves a <see cref="Speaker"/> by its <paramref name="id"/> asynchronously.
		/// </summary>
		/// <param name="id">The unique identifier of the <see cref="Speaker"/>.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Speaker"/> entity, or <see langword="null"/> if not found.</returns>
		public Task<Speaker?> GetByIdAsync(int id);


		/// <summary>
		/// Adds a new speaker to the speakers.
		/// </summary>
		/// <param name="attendee">The speaker entity to be added.</param>
		/// <returns>
		/// A task representing the asynchronous operation, with a result of the added <see cref="Speaker"/>.
		/// </returns>
		Task<Result> AddAsync(Speaker speaker);

		/// <summary>
		/// Updates an existing speaker's details.
		/// </summary>
		/// <param name="speaker">The speaker entity with updated information.</param>
		/// <returns>
		/// A task representing the asynchronous operation, with a Result of the updated <see cref="Speaker"/>.
		/// </returns>
		Task<Speaker> UpdateAsyc(Speaker speaker);

		/// <summary>
		/// Deletes the specified speaker from the speakers.
		/// </summary>
		/// <param name="speaker">The speaker entity to be deleted.</param>
		/// <returns>
		/// A task representing the asynchronous operation, with a Result indicating whether the deletion was successful.
		/// </returns>
		Task<Result> DeleteAsync(Speaker speaker);


		/// <summary>
		/// Checks if a <paramref name="userId"/> exists in the speakers.
		/// </summary>
		/// <param name="userId">The <paramref name="userId"/> to check.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the user exists by userId; otherwise, <see langword="false"/>.</returns>
		Task<bool> IsUserExist(int userId);
	}
}
