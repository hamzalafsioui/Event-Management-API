using EventManagement.Data.DTOs.Roles;
using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Requests;
using EventManagement.Data.Responses;

namespace EventManagement.Service.Abstracts
{
	/// <summary>
	/// Interface defining operations related to roles and claims for authorization management.
	/// </summary>
	public interface IAuthorizationService
	{
		#region Roles
		/// <summary>
		/// Adds a new role with the specified <paramref name="roleName"/> asynchronously.
		/// </summary>
		/// <param name="roleName">The name of the role to add.</param>
		/// <returns>
		/// A <see cref="string"/> result:
		/// <list type="bullet">
		/// <item><description><c>"Success"</c>: The role was added successfully.</description></item>
		/// <item><description><c>"Failed"</c>: The role creation failed due to validation or other errors.</description></item>
		/// </list>
		/// </returns>
		public Task<string> AddRoleAsync(string roleName);

		/// <summary>
		/// Updates the role with the specified <paramref name="Id"/> and sets its name to <paramref name="roleName"/> asynchronously.
		/// </summary>
		/// <param name="Id">The unique identifier of the role to update.</param>
		/// <param name="roleName">The new name for the role.</param>
		/// <returns>
		/// A <see cref="string"/> result:
		/// <list type="bullet">
		/// <item><description><c>"Success"</c>: The role was updated successfully.</description></item>
		/// <item><description><c>"NotFound"</c>: The role was not found.</description></item>
		/// <item><description><c>"Failed"</c>: The role update failed due to validation or other errors.</description></item>
		/// </list>
		/// </returns>
		public Task<string> EditRoleAsync(int Id, string Name);

		/// <summary>
		/// Deletes the role with the specified <paramref name="Id"/> asynchronously.
		/// </summary>
		/// <param name="Id">The unique identifier of the role to delete.</param>
		/// <returns>
		/// A <see cref="string"/> result:
		/// <list type="bullet">
		/// <item><description><c>"Success"</c>: The role was deleted successfully.</description></item>
		/// <item><description><c>"NotFound"</c>: The role was not found.</description></item>
		/// <item><description><c>"Used"</c>: The role is assigned to users and cannot be deleted.</description></item>
		/// <item><description><c>"Failed"</c>: The role deletion failed due to other errors.</description></item>
		/// </list>
		/// </returns>
		public Task<string> DeleteRoleAsync(int Id);

		/// <summary>
		/// Checks if a role with the specified <paramref name="roleName"/> exists asynchronously.
		/// </summary>
		/// <param name="roleName">The name of the role to check.</param>
		/// <returns><c>true</c> if the role exists; otherwise, <c>false</c>.</returns>
		public Task<bool> IsRoleExistAsync(string roleName);

		/// <summary>
		/// Checks if a role with the specified <paramref name="Id"/> exists asynchronously.
		/// </summary>
		/// <param name="Id">The unique identifier of the role to check.</param>
		/// <returns><c>true</c> if the role exists; otherwise, <c>false</c>.</returns>
		public Task<bool> IsRoleExistByIdAsync(int Id);

		/// <summary>
		/// Retrieves a list of all roles asynchronously.
		/// </summary>
		/// <returns>A list of <see cref="Role"/> entities.</returns>
		public Task<List<Role>> GetRolesListAsync();

		/// <summary>
		/// Retrieves the role with the specified <paramref name="id"/> asynchronously.
		/// </summary>
		/// <param name="id">The unique identifier of the role.</param>
		/// <returns>The <see cref="Role"/> entity if found; otherwise, <c>null</c>.</returns>
		public Task<Role> GetRoleByIdAsync(int id);

		/// <summary>
		/// Retrieves role-related data for the specified <paramref name="user"/> asynchronously.
		/// </summary>
		/// <param name="user">The <see cref="User"/> entity for which to retrieve role data.</param>
		/// <returns>A <see cref="ManageUserRolesResponse"/> containing role-related data.</returns>
		public Task<ManageUserRolesResponse> ManageUserRolesDate(User user);

		/// <summary>
		/// Updates the roles for a user based on the specified <paramref name="request"/> asynchronously.
		/// </summary>
		/// <param name="request">An <see cref="UpdateUserRolesRequest"/> object containing the role update details.</param>
		/// <returns>
		/// A <see cref="string"/> result:
		/// <list type="bullet">
		/// <item><description><c>"Success"</c>: The roles were updated successfully.</description></item>
		/// <item><description><c>"UserNotFound"</c>: The specified user was not found.</description></item>
		/// <item><description><c>"FailedToRemoveOldRoles"</c>: Failed to remove the user's old roles.</description></item>
		/// <item><description><c>"FailedToAddNewRoles"</c>: Failed to add the new roles.</description></item>
		/// <item><description><c>"FailedToUpdateUserRoles"</c>: A general failure during the update Roles process.</description></item>
		/// <item><description><c>"FailedToUpdateUserRole"</c>: A general failure during the update Role process.</description></item>
		/// </list>
		/// </returns>
		public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);

		#endregion

		#region Claims
		/// <summary>
		/// Retrieves claim-related data for the specified <paramref name="user"/> asynchronously.
		/// </summary>
		/// <param name="user">The <see cref="User"/> entity for which to retrieve claim data.</param>
		/// <returns>A <see cref="ManageUserClaimsResponse"/> containing claim-related data.</returns>
		public Task<ManageUserClaimsResponse> ManageUserClaimsData(User user);

		/// <summary>
		/// Updates the claims for a user based on the specified <paramref name="request"/> asynchronously.
		/// </summary>
		/// <param name="request">An <see cref="UpdateUserClaimsRequest"/> object containing the claim update details.</param>
		/// <returns>
		/// A <see cref="string"/> result:
		/// <list type="bullet">
		/// <item><description><c>"Success"</c>: The claims were updated successfully.</description></item>
		/// <item><description><c>"UserNotFound"</c>: The specified user was not found.</description></item>
		/// <item><description><c>"FailedToRemoveOldClaims"</c>: Failed to remove the user's old claims.</description></item>
		/// <item><description><c>"FailedToAddNewClaims"</c>: Failed to add the new claims.</description></item>
		/// <item><description><c>"FailedToUpdateUserClaims"</c>: A general failure during the update process.</description></item>
		/// </list>
		/// </returns>
		public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);

		#endregion

	}
}
