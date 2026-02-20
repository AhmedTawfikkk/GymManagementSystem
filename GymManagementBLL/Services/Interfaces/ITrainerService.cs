using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createdTrainer);
        TrainerViewModel? GetTrainerById(int TrainerId);
        UpdateTrainerViewModel? GetTrainerToUpdate(int TrainerId);
        bool UpdateTrainer(UpdateTrainerViewModel updatedTrainer,int TrainerId);
        bool RemoveTrainer(int TrainerId);

    }
}
