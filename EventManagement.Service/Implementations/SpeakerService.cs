using EventManagement.Data.Entities;
using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper;
using EventManagement.Data.Helper.Enums;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EventManagement.Service.Implementations
{
	public class SpeakerService : ISpeakerService
	{
		#region Fields
		private readonly ISpeakerRepository _speakerRepository;
		private readonly UserManager<User> _userManager;
		#endregion
		#region Constructors
		public SpeakerService(ISpeakerRepository speakerRepository,
			UserManager<User> userManager)
		{
			_speakerRepository = speakerRepository;
			_userManager = userManager;
		}


		#endregion

		#region Actions
		public async Task<List<Speaker>> GetSpeakersListAsync()
		{
			return await _speakerRepository.GetSpeakersListAsync();
		}

		public async Task<Speaker?> GetByIdAsync(int id) => await _speakerRepository.GetByIdAsync(id);


		public async Task<Result> AddAsync(Speaker speaker)
		{
			using var transaction = await _speakerRepository.BeginTransactionAsync();

			try
			{
				// Add speaker
				var newSpeaker = await _speakerRepository.AddAsync(speaker);
				if (newSpeaker == null)
					return Result.Failure("Failed to add the speaker.");

				// Update user role
				var user = await _userManager.FindByIdAsync(speaker.UserId.ToString());
				if (user == null)
					return Result.Failure("User not found.");

				user.Role = UserRoleEnum.Speaker;
				var updateUserRoleResult = await _userManager.UpdateAsync(user);
				if (!updateUserRoleResult.Succeeded)
				{
					// Rollback speaker addition if role update fails
					await _speakerRepository.DeleteAsync(newSpeaker);
					return Result.Failure("Failed to update the user's role.");
				}

				// Add "Speaker" role to user's roles
				var addToSpeakerRoleResult = await _userManager.AddToRoleAsync(user, "Speaker");
				if (!addToSpeakerRoleResult.Succeeded)
				{
					// Rollback speaker addition if role assignment fails
					await _speakerRepository.DeleteAsync(newSpeaker);
					return Result.Failure("Failed to assign the Speaker role.");
				}

				await transaction.CommitAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				Log.Error($"Error in Speaker AddAsync: {ex.Message}");
				return Result.Failure("An unexpected error occurred.");
			}
		}
		public async Task<Speaker> UpdateAsyc(Speaker speaker) => await _speakerRepository.UpdateAsync(speaker);
		public async Task<bool> DeleteAsync(Speaker speaker) => await _speakerRepository.DeleteAsync(speaker);

		public async Task<bool> IsUserExist(int userId)
		{
			var result = await _speakerRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.UserId.Equals(userId));
			return result != null;
		}




		#endregion
	}
}
