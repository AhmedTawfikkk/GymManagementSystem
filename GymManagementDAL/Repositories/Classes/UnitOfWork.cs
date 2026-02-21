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
    // in unit of work we cant make injection for the repos here as every repo will have a new instance of dbcontext in injection and the idea of unit of work to make them all one transaction
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();  // store the object that is generated to not makr a new object every usage in the same request
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
            var NewRepo= new GenericRepository<TEntity>(_dbcontext); // the generic repo here not injected automatically then its constructor needs the dbcontext to be passed manually
            _repositories[EntityType] = NewRepo;
            return NewRepo;
            
        }

        public int SaveChanges()
        {
            return _dbcontext.SaveChanges();
        }
    }
}
