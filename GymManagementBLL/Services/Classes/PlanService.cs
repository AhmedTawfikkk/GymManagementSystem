using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if(plans is null || !plans.Any())
            {
                return Enumerable.Empty<PlanViewModel>();
            }
            return plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name=p.name,
                Description=p.Description,
                DurationDays=p.DurationDays,
                Price=p.Price,
                IsActive=p.IsActive,
            }
            ).ToList();
        }

        public PlanViewModel? GetPlanById(int id)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if(Plan == null)
            {
                return null;
            }
            return new PlanViewModel()
            {

                Id = Plan.Id,
                Name = Plan.name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Price,
                IsActive =Plan.IsActive,

            };

        }

        public UpdatePlanViewModel? GetPlanToUpdate(int id)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (Plan == null || !HasActiveMemberShip(id) || Plan.IsActive==false)
            {
                return null;
            }
            return new UpdatePlanViewModel()
            {

                PlanName = Plan.name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Price,

            };


        }
        public bool UpdatePlan(UpdatePlanViewModel updatedPlan, int Planid)
        {
           
                var Plan = _unitOfWork.GetRepository<Plan>().GetById(Planid);
                if (Plan == null || HasActiveMemberShip(Planid)) return false;   //sure that the id not injected
            try
            {
                (Plan.Description, Plan.DurationDays, Plan.Price, Plan.Updated_At) = (updatedPlan.Description, updatedPlan.DurationDays, updatedPlan.Price, DateTime.Now);
                _unitOfWork.GetRepository<Plan>().Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool ToggleStatus(int Planid)
        {
            var Repo = _unitOfWork.GetRepository<Plan>();
           var Plan = Repo.GetById(Planid);
           if (Plan is null || HasActiveMemberShip(Planid))
            { return false; }
            Plan.IsActive = Plan.IsActive == true ? false : true;
            Plan.Updated_At = DateTime.Now;
            try
            {
                Repo.Update(Plan);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch
            {
                return false;
            }

        }


        #region Helpers
        private bool HasActiveMemberShip(int id)
        {
            return _unitOfWork.GetRepository<MemebrShip>().GetAll(m => m.Planid == id && m.status == "Active").Any();
        }
        #endregion
    }
}
