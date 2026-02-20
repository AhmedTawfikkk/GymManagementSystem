using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IsessionService
    {
        public IEnumerable<SessionViewModel> GetAllSessions();
        public SessionViewModel? GetSessionById(int sessionId);
    }
}
