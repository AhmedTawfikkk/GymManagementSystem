using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AccounViewModels;
using GymManagementDAL.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public  async Task < ApplicationUser?> ValidateUser(AccountViewModel account)
        {
            var User= await _userManager.FindByEmailAsync(account.Email);
            if (User == null) return null;
            var PasswordValidated = await _userManager.CheckPasswordAsync(User, account.Password);
            if (!PasswordValidated) return null;
            return User;

        }
    }
}
