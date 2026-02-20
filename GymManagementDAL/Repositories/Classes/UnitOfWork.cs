using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDbContext _dbcontext;

        public UnitOfWork(GymDbContext dbcontext, IsessionRepository sessions)
        {
            _dbcontext = dbcontext;
            SessionRepository = sessions;
        }

        public IsessionRepository SessionRepository { get; } 

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType= typeof(TEntity);
            if(_repositories.TryGetValue(EntityType,out var repo))
            {
                return (IGenericRepository<TEntity>)repo;
            }
            var NewRepo= new GenericRepository<TEntity>(_dbcontext);
            _repositories[EntityType] = NewRepo;
            return NewRepo;
            
        }

        public int SaveChanges()
        {
            return _dbcontext.SaveChanges();
        }
    }
}
