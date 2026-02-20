using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanById(int id);
        UpdatePlanViewModel? GetPlanToUpdate(int id);
        bool UpdatePlan(UpdatePlanViewModel updatedPlan,int Planid);

        bool ToggleStatus(int Planid);
    }
}
