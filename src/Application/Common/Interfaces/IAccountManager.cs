using AspNetCoreAngularTemplate.Application.Common.Models;

namespace AspNetCoreAngularTemplate.Application.Common.Interfaces
{
    public interface IAccountManager
    {
        Task<(Result Result, string UserId)> CreateUserAsync(string email, string password);

        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task<Result> ConfirmEmailAsync(string userId, string confirmationCode);

        Task<string?> FindUserId(string userEmail);
        Task<bool> IsEmailConfirmedAsync(string userId);
        Task<string> GeneratePasswordResetTokenAsync(string userId);
        Task<Result> ResetPasswordAsync(string userId, string token, string newPassword);

        // Task<ApplicationUser> GetUserByIdAsync(string userId);


        // Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        // Task<(bool Succeeded, string[] Errors)> CreateRoleAsync(ApplicationRole role, IEnumerable<string> claims);
     
        // Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(ApplicationRole role);
        // Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(string roleName);
        // Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(ApplicationUser user);
        // Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(string userId);
        // Task<ApplicationRole> GetRoleByIdAsync(string roleId);
        // Task<ApplicationRole> GetRoleByNameAsync(string roleName);
        // Task<ApplicationRole> GetRoleLoadRelatedAsync(string roleName);
        // Task<List<ApplicationRole>> GetRolesLoadRelatedAsync(int page, int pageSize);
        // Task<(ApplicationUser User, string[] Roles)?> GetUserAndRolesAsync(string userId);
        // Task<ApplicationUser> GetUserByEmailAsync(string email);
        // Task<ApplicationUser> GetUserByIdAsync(string userId);
        // Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        // Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        // Task<List<(ApplicationUser User, string[] Roles)>> GetUsersAndRolesAsync(int page, int pageSize);
        // Task<bool> GetUserHasPasswordAsync(ApplicationUser user);
        // Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(ApplicationUser user, string newPassword);
        // Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        // Task<bool> TestCanDeleteRoleAsync(string roleId);
        // Task<bool> TestCanDeleteUserAsync(string userId);
        // Task<(bool Succeeded, string[] Errors)> UpdatePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        // Task<(bool Succeeded, string[] Errors)> UpdateRoleAsync(ApplicationRole role, IEnumerable<string> claims);
        // Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(ApplicationUser user);
        // Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(ApplicationUser user, IEnumerable<string> roles);
        
    }
}
