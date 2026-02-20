using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateTrainer(CreateTrainerViewModel createdTrainer)
        {
            try
            {
                if (IsEmailExists(createdTrainer.Email) || IsPhoneExists(createdTrainer.Phone))
                {
                    return false;
                }
                var Trainee = new Trainer
                {
                    name = createdTrainer.Name,
                    Email = createdTrainer.Email,
                    Phone = createdTrainer.Phone,
                    DateOfBirth = createdTrainer.DateOfBirth,
                    Specialities = createdTrainer.Specialities,
                    gender = createdTrainer.Gender,
                    address = new Address
                    {
                        BuildingNumber = createdTrainer.BuildingNumber,
                        street = createdTrainer.Street,
                        City = createdTrainer.City,
                    }
                };
                _unitOfWork.GetRepository<Trainer>().Add(Trainee);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {   return false; }


        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers=_unitOfWork.GetRepository<Trainer>().GetAll();
            if(Trainers == null || !Trainers.Any())
            {
                return Enumerable.Empty<TrainerViewModel>();
            }
            return Trainers.Select(t => new TrainerViewModel
            {
                Id = t.Id,
                Name = t.name,
                Email = t.Email,
                Phone = t.Phone,
                Specialization = t.Specialities.ToString()

            });
        }

        public TrainerViewModel? GetTrainerById(int TrainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if(trainer == null)
            {
                return null;
            }
            return new TrainerViewModel
            {
                Id = trainer.Id,
                Name = trainer.name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialization = trainer.Specialities.ToString()
            };
        }

        public UpdateTrainerViewModel? GetTrainerToUpdate(int TrainerId)
        {
           var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if(trainer == null)
            {
                return null;
            }
            return new UpdateTrainerViewModel
            {
                Name = trainer.name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.address.BuildingNumber,
                Street = trainer.address.street,
                City = trainer.address.City,
                Specialities = trainer.Specialities
            };
        }

        public bool RemoveTrainer(int TrainerId)
        {
            var Repo = _unitOfWork.GetRepository<Trainer>();
            var trainer = Repo.GetById(TrainerId);
            if (trainer == null || HasActiveSessions(TrainerId))
            {
                return false;
            }
            Repo.Delete(trainer);
            return _unitOfWork.SaveChanges() > 0;

        }

        public bool UpdateTrainer(UpdateTrainerViewModel updatedTrainer, int TrainerId)
        {
            var TrainerToUpdate= _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (TrainerToUpdate == null || IsEmailExists(updatedTrainer.Email) || IsPhoneExists(updatedTrainer.Phone))
            {
                return false;
            }
            
            TrainerToUpdate.Email = updatedTrainer.Email;
            TrainerToUpdate.Phone = updatedTrainer.Phone;
            TrainerToUpdate.name = updatedTrainer.Name;
            TrainerToUpdate.address.BuildingNumber = updatedTrainer.BuildingNumber;
            TrainerToUpdate.address.street = updatedTrainer.Street;
            TrainerToUpdate.address.City = updatedTrainer.City;
            TrainerToUpdate.Specialities = updatedTrainer.Specialities;
            TrainerToUpdate.Updated_At = DateTime.Now;
            return _unitOfWork.SaveChanges() > 0;

        }
        #region Helpers
        bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Email == email).Any();
        }
        bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Phone == phone).Any();
        }
        bool HasActiveSessions(int trainerId)
        {
            return _unitOfWork.GetRepository<Session>().GetAll(s => s.TrainerId == trainerId && s.StartDate > DateTime.Now).Any();
        }
        #endregion

    }
}
