using GymManagementDAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IsessionRepository SessionRepository { get; }
        IGenericRepository<TEntity> GetRepository<TEntity>()where TEntity : BaseEntity, new();
        int SaveChanges();
    }
}
