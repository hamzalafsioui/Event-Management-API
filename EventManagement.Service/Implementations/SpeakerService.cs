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

		public async Task<Speaker?> GetByIdAsync(int id) => await _speakerRepository.GetTableNoTracking().Include(x => x.User).FirstOrDefaultAsync(x => x.Id.Equals(id));


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
					return Result.Failure("Failed to update the user's role.");

				// Add "Speaker" role to user's roles
				var addToSpeakerRoleResult = await _userManager.AddToRoleAsync(user, "Speaker");
				if (!addToSpeakerRoleResult.Succeeded)
					return Result.Failure("Failed to assign the Speaker role.");
				

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
		public async Task<Result> UpdateAsyc(Speaker speaker)
		{
			var result = await _speakerRepository.UpdateAsync(speaker);
			if (result == null)
				return Result.Failure("FailedToUpdateTheSpeaker");
			return Result.Success();
		}
		public async Task<Result> DeleteAsync(Speaker speaker)
		{
			using var transaction = await _speakerRepository.BeginTransactionAsync();

			try
			{
				// Delete speaker
				var deleteResult = await _speakerRepository.DeleteAsync(speaker);
				if (!deleteResult)
					return Result.Failure("FailedToDeleteTheSpeaker");

				// Update user role
				var user = await _userManager.FindByIdAsync(speaker.UserId.ToString());
				if (user == null)
					return Result.Failure("UserNotFound");

				user.Role = UserRoleEnum.User;
				var updateUserRoleResult = await _userManager.UpdateAsync(user);
				if (!updateUserRoleResult.Succeeded)
					return Result.Failure("FailedToUpdateTheUser'sRole");

				// Remove "Speaker" role to user's roles
				var removeSpeakerRoleResult = await _userManager.RemoveFromRoleAsync(user, "Speaker");
				if (!removeSpeakerRoleResult.Succeeded)
					return Result.Failure("FailedToRemoveTheSpeakerRole");

				// commit transaction
				await transaction.CommitAsync();
				// return success
				return Result.Success();
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				Log.Error($"Error in Speaker DeleteAsync: {ex.Message}");
				return Result.Failure("AnUnexpectedErrorOccurred");
			}
		}

		public async Task<bool> IsUserExistAsync(int userId)
		{
			var result = await _speakerRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.UserId.Equals(userId));
			return result != null;
		}

		public async Task<bool> IsSpeakerExistAsync(int speakerId)
		{
			var result = await _speakerRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(speakerId));
			return result != null;
		}


		#endregion
	}
}
