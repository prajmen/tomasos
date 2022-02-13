using Microsoft.AspNetCore.Identity;
using PizzaREAL.Models;
using PizzaREAL.ModelsIdentity;
using PizzaREAL.ViewModels;

namespace PizzaREAL.Services
{
    public interface IAccountService
    {
        AccountViewModel GetAccountViewModel();
        ApplicationUser GetById(string id);

        Customer GetCustomerByUserID(string id);
        Task<bool> CreateAsync(UserRegistrationRequest request);
        Task<bool> UpdateAsync(UserUpdateRequest model, ApplicationUser user);
        Task<bool> DeleteAsync(ApplicationUser user);
        Task<bool> UpdateRoleAsync(string userId, string newRoleId);
        UserUpdateRequest GetUserUpdateRequest(ApplicationUser user);

    }
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PizzaDbContext _context;

        public AccountService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, PizzaDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> CreateAsync(UserRegistrationRequest model)
        {
            var id = Guid.NewGuid().ToString();

            ApplicationUser newUser = new()
            {
                Id = id,
                Name = model.Name,
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                PostCode = model.PostCode,
                City = model.City,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            await _userManager.AddToRoleAsync(newUser, "Standard");

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);

                var customer = new Customer()
                {
                    AspNetUserId = id
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public ApplicationUser GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByUserID(string id)
        {
            return _context.Customers.Where(c => c.AspNetUserId == id).FirstOrDefault();
        }

        public UserUpdateRequest GetUserUpdateRequest(ApplicationUser user)
        {
            UserUpdateRequest request = new UserUpdateRequest()
            {
                Name = user.Name,
                Username = user.UserName,
                Address = user.Address,
                PostCode = user.PostCode,
                City = user.City,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return request;

        }

        public AccountViewModel GetAccountViewModel()
        {
            AccountViewModel model = new AccountViewModel()
            {
                StandardUsers = _userManager.GetUsersInRoleAsync("Standard").Result.ToList(),
                PremiumUsers = _userManager.GetUsersInRoleAsync("Premium").Result.ToList(),
                AdminUsers = _userManager.GetUsersInRoleAsync("Admin").Result.ToList(),
            };

            return model;

        }

        public async Task<bool> UpdateAsync(UserUpdateRequest model, ApplicationUser user)
        {    
            user.Name = model.Name;
            user.UserName = model.Username;
            user.Address = model.Address;
            user.PostCode = model.PostCode;
            user.City = model.City;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var resultPasswordChange = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (resultPasswordChange.Succeeded)
            {
                var resultUpdated = await _userManager.UpdateAsync(user);

                if (resultUpdated.Succeeded)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UpdateRoleAsync(string userId, string newRoleId)
        {
            var user = _userManager.FindByIdAsync(userId);
            var oldRole = _userManager.GetRolesAsync(await user).Result.FirstOrDefault();

            await _userManager.RemoveFromRoleAsync(await user, oldRole);

            var result = await _userManager.AddToRoleAsync(await user, newRoleId);

            if (result.Succeeded)
            {
                return true;
            }


            return true;
        }
    }
}
