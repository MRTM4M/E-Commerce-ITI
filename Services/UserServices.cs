using E_commerce_iti.Models;
using E_commerce_iti.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Services
{
    public class UserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CartServices _cartServices;

        public UserServices(
            UserManager<ApplicationUser> userManager,
            CartServices cartServices)
        {
            _userManager = userManager;
            _cartServices = cartServices;
        }

        private static AccountViewModels MapToAccountViewModel(ApplicationUser user)
        {
            return new AccountViewModels
            {
                Fname = user.FName,
                Lname = user.LName,
                EmailAddress = user.Email,
                Addresses = user.Addresses,
                IsActive = user.IsActive
            };
        }

        // Get active user by ID
        public async Task<AccountViewModels?> GetUser(int userId)
        {
            var user = await _userManager.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Id == userId && u.IsActive);

            if (user == null)
                return null;

            return MapToAccountViewModel(user);
        }

        // Get active user by Email
        public async Task<AccountViewModels?> GetUser(string email)
        {
            var user = await _userManager.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);

            if (user == null)
                return null;

            return MapToAccountViewModel(user);
        }

        // Admin: Get ALL users, active and inactive
        public async Task<ICollection<AccountViewModels>> GetAllUsers()
        {
            var users = await _userManager.Users
                .Include(u => u.Addresses)
                .ToListAsync();

            return users
                .Select(MapToAccountViewModel)
                .ToList();
        }

        // Active users only
        public async Task<ICollection<AccountViewModels>> GetAllActiveUsers()
        {
            var users = await _userManager.Users
                .Include(u => u.Addresses)
                .Where(u => u.IsActive == true)
                .ToListAsync();

            return users
                .Select(MapToAccountViewModel)
                .ToList();
        }
        public async Task<ICollection<AccountViewModels>> GetAllInactiveUsers()
        {
            var users = await _userManager.Users
                .Include(u => u.Addresses)
                .Where(u => u.IsActive == false)
                .ToListAsync();

            return users
                .Select(MapToAccountViewModel)
                .ToList();
        }

        public async Task<bool> CreateUserAsync(AccountViewModels model)
        {
            var user = new ApplicationUser
            {
                FName = model.Fname,
                LName = model.Lname,
                Email = model.EmailAddress,
                UserName = model.EmailAddress,
                CreatedAt = DateTime.UtcNow,
                IsActive = model.IsActive
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _cartServices.CreateCartAsync(user.Id);
            if (!result.Succeeded)
                return false;

            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
            
            return roleResult.Succeeded;
        }

        public async Task<bool> UpdateUserAsync(int userId, AccountViewModels model)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            user.FName = model.Fname;
            user.LName = model.Lname;

            var userNameResult =
                await _userManager.SetUserNameAsync(user, model.EmailAddress);

            if (!userNameResult.Succeeded)
                return false;

            var emailResult =
                await _userManager.SetEmailAsync(user, model.EmailAddress);

            if (!emailResult.Succeeded)
                return false;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        // Deactivate user instead of deleting
        public async Task<bool> DeactivateUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        // Activate user again
        public async Task<bool> ActivateUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            user.IsActive = true;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }
    }
}