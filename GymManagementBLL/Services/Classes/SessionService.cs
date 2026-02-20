using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class SessionService:IsessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
           _mapper = mapper;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (sessions == null || !sessions.Any())
            {
                return Enumerable.Empty<SessionViewModel>();
            }
            var mappedSessions = _mapper.Map<IEnumerable< Session>,IEnumerable<SessionViewModel>>(sessions);
            return mappedSessions;
        }

        public SessionViewModel? GetSessionById(int sessionId)
        {
            var Session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(sessionId);
            if (Session == null)
            {
                return null;
            }
            var mappedSession = _mapper.Map<Session, SessionViewModel>(Session);
           mappedSession.AvailableSlots= Session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(Session.Id);
            return mappedSession;


        }
    }
}
