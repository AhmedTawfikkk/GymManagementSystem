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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbContext _DbContext;
        public GenericRepository(GymDbContext gymDbContext)
        {
            _DbContext = gymDbContext;
        }
        public void Add(TEntity entity) => _DbContext.Add(entity);

        public TEntity? GetById(int Id) => _DbContext.Set<TEntity>().Find(Id);
        

        public void Update(TEntity entity)=> _DbContext.Update(entity);
       
        public void Delete(TEntity entity) => _DbContext.Remove(entity);


        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? Condition)
        {
            if (Condition == null) return _DbContext.Set<TEntity>().AsNoTracking();
            return _DbContext.Set<TEntity>().AsNoTracking().Where(Condition);
        }

       
    }

}
