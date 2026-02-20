using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, IsessionRepository
    {
        private readonly GymDbContext _dbcontext;

        public SessionRepository(GymDbContext dbcontext):base(dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
           return  _dbcontext.sessions.Include(s => s.trainer).Include(s => s.category).ToList();

        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _dbcontext.memberSessions.Count(sb => sb.SessionId == sessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _dbcontext.sessions
                .Include(s => s.trainer)
                .Include(s => s.category)
                .FirstOrDefault(s => s.Id == sessionId);
        }
    }
}
