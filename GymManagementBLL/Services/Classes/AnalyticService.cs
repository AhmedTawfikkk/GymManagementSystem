using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.Analytics;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class AnalyticService : IAnalyticService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var sessions = _unitOfWork.SessionRepository.GetAll();
            return new AnalyticsViewModel()
            { 
                ActiveMembers=_unitOfWork.GetRepository<MemebrShip>().GetAll(m=>m.status=="Active").Count(),
                TotalMembers=_unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers=_unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions=sessions.Count(s=>s.StartDate>DateTime.Now),
                OngoingSessions=sessions.Count(s=>s.StartDate<=DateTime.Now && s.EndDate>=DateTime.Now),
                CompletedSessions=sessions.Count(s=>s.EndDate<DateTime.Now)

            };

        }
    }
}
