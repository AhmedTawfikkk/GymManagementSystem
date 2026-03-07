using GymManagementBLL.ViewModels.AccounViewModels;
using GymManagementDAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IAccountService
    {
         Task<ApplicationUser?> ValidateUser(AccountViewModel account);
    }
}
