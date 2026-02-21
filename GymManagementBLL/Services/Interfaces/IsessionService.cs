using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entites;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
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
        public bool CreateSession(CreateSessionViewModel createdsession);
        public UpdateSessionViewModel? GetSessionForUpdate(int SessionId);
        public bool UpdateSession(UpdateSessionViewModel updatedSession,int sessionId);

        public bool DeleteSession(int sessionId);
    }
}
