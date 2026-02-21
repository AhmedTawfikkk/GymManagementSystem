using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GymManagementBLL.Services.Classes
{
    public class SessionService : IsessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel createdsession)
        {
            try
            {
                if (!TrinerExists(createdsession.TrainerId)) return false;    // DropDown reduce error but server validation is required

                if (!CategoryExists(createdsession.CategoryId)) return false;

                if (!IsDateValid(createdsession.StartDate, createdsession.EndDate)) return false;

                if (createdsession.Capacity > 25 || createdsession.Capacity < 0) return false;   // we made data annotation for this and will be validated in model state in controller but (Controller protects the endpoint and Service protects the business), the controller nt always called and for ex unit test

                var sessionEntity = _mapper.Map<Session>(createdsession);
                _unitOfWork.GetRepository<Session>().Add(sessionEntity);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create Session Failed {ex}");
                return false;
            }

        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (sessions == null || !sessions.Any())
            {
                return Enumerable.Empty<SessionViewModel>();
            }
            var mappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
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
            mappedSession.AvailableSlots = Session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(Session.Id);
            return mappedSession;


        }
        public UpdateSessionViewModel? GetSessionForUpdate(int SessionId)
        {
            var Session = _unitOfWork.SessionRepository.GetById(SessionId);
            if (!IsSessionAvailableForUpdating(Session!))
            {
                return null;

            }
            return _mapper.Map<UpdateSessionViewModel>(Session);
        }
        public bool UpdateSession(UpdateSessionViewModel updatedsession, int sessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(sessionId);
                if (!IsSessionAvailableForUpdating(Session!))    // ti avoid the injection of a new id in request (hacking)
                {
                    return false;
                }

                if (!TrinerExists(updatedsession.TrainerId)) return false;    // DropDown reduce error but server validation is required (make inspect and change the value)


                if (!IsDateValid(updatedsession.StartDate, updatedsession.EndDate)) return false;

                var SessionEntity = _mapper.Map(updatedsession, Session);
                SessionEntity!.Updated_At = DateTime.Now;
                _unitOfWork.SessionRepository.Update(SessionEntity);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"update failed {ex}");
                return false;
            }
        }

        public bool DeleteSession(int sessionId)
        {
            try
            {
                var Session = _unitOfWork.SessionRepository.GetById(sessionId);
                if (!IsSessionAvailableForDeleting(Session!))
                {
                    return false;
                }
                _unitOfWork.SessionRepository.Delete(Session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remove Session Failed {ex}");
                return false;
            }
            

        }





        #region Helpers
        private bool TrinerExists(int TrainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(TrainerId) is not null;
        }
        private bool CategoryExists(int CategoryId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(CategoryId) is not null;
        }
        private bool IsDateValid(DateTime StartDate, DateTime EndDate)
        {
            return StartDate < EndDate;
        }
        private bool IsSessionAvailableForUpdating(Session session)
        {
            if (session == null) return false;
            if (session.EndDate < DateTime.Now) return false;
            if (session.StartDate <= DateTime.Now) return false;
            var hasactivebooking = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (hasactivebooking) return false;
            return true;
        }
        private bool IsSessionAvailableForDeleting(Session session)
        {
            if (session == null) return false;
            //started
            if (session.StartDate <= DateTime.Now && session.EndDate>DateTime.Now) return false;
            //upcoming
            if(session.StartDate >= DateTime.Now) return false;
            var hasactivebooking = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (hasactivebooking) return false;
            return true;
        }

    }
    #endregion

}



