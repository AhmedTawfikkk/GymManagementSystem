using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.Entites;

namespace GymManagementBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateMember(CreateMemberViewModel member)
        {
            try
            {
               
                if (IsEmailExists(member.Email) || IsPhoneExists(member.Phone)) return false;

                var Member = new Member()
                {
                    name = member.Name,
                    Email = member.Email,
                    Phone = member.Phone,
                    gender = member.Gender,
                    DateOfBirth = member.DateOfBirth,
                    address = new Address()
                    {
                        BuildingNumber = member.BuildingNumber,
                        street = member.Street,
                        City = member.City,

                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = member.HealthRecordViewModel.Height,
                        Weight = member.HealthRecordViewModel.Weight,
                        bloodtype = member.HealthRecordViewModel.BloodType,
                        Note = member.HealthRecordViewModel.Note,

                    }
                };
                _unitOfWork.GetRepository<Member>().Add(Member);
                return _unitOfWork.SaveChanges()>0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMember(int id)
        {  
            var memberRepo = _unitOfWork.GetRepository<Member>();  // no need for that as the dictionary will give me the same object along the request but only to not repeat code
            var memberShip = _unitOfWork.GetRepository<MemebrShip>(); 

            var memberToDelete = memberRepo.GetById(id);
            if (memberToDelete is null) return false;
            var ActiveMemeberSession = _unitOfWork.GetRepository<MemberSession>().GetAll(m=>m.MemeberId == id && m.Session.StartDate>DateTime.Now).Any();
            if (ActiveMemeberSession) return false;
            var MemberShips = memberShip.GetAll(m => m.MemberId == id);
            try
            {
                if (MemberShips.Any())
                {
                    foreach (var MemebrShip in MemberShips)   // cascading in my app before going to db
                    {
                        memberShip.Delete(MemebrShip);
                    }

                 }
                memberRepo.Delete(memberToDelete);
                return _unitOfWork.SaveChanges()>0;

                
            }
            catch
            {
                return false;
            }

        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Memebers = _unitOfWork.GetRepository<Member>().GetAll();
            if(Memebers is null || !Memebers.Any())
            {
                return Enumerable.Empty<MemberViewModel>();
            }
            //var MemberViewModels = new List<MemberViewModel>();
            //foreach(var Member in  Memebers)
            //{
            //    var MemberViewModel = new MemberViewModel()
            //    {
            //        Id = Member.Id,
            //        Email = Member.Email,
            //        Phone = Member.Phone,
            //        Gender = Member.gender.ToString(),
            //        Photo = Member.photo,
            //        Name = Member.name,
            //    };
            //    MemberViewModels.Add(MemberViewModel);
            //}


            var MemberViewModels = Memebers.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Email = m.Email,
                Phone = m.Phone,
                Gender = m.gender.ToString(),
                Photo = m.photo,
                Name = m.name,
            });

            return MemberViewModels;

        }

        public MemberViewModel? GetMemberDetails(int id)
        {
             var MemberDetails = _unitOfWork.GetRepository<Member>().GetById(id);
            if(MemberDetails is null)
            {
                return null;
            }

            var ViewModel = new MemberViewModel()
            {
                Name = MemberDetails.name,
                Email = MemberDetails.Email,
                Phone = MemberDetails.Phone,
                Gender = MemberDetails.gender.ToString(),
                Photo = MemberDetails.photo,
                DateOfBirth = MemberDetails.DateOfBirth.ToString(),
                Address = $"{MemberDetails.address.BuildingNumber}-{MemberDetails.address.street}-{MemberDetails.address.City}",
            };
            var ActivememebrShip = _unitOfWork.GetRepository<MemebrShip>().GetAll(p => p.MemberId == id && p.status == "Active").FirstOrDefault();
            if(ActivememebrShip is not null)
            {
                ViewModel.MemberShipStartDate = ActivememebrShip.Created_At.ToShortDateString();
                ViewModel.MemberShipEndDate = ActivememebrShip.EndDate.ToShortDateString() ;
                var Plan =  _unitOfWork.GetRepository<Plan>().GetById(ActivememebrShip.Planid);
                ViewModel.PlanName = Plan?.name;
            }

            return ViewModel;
            

        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int id)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(id);
            if(MemberHealthRecord is null)
            {  return null; }
            var viewModel = new HealthRecordViewModel()
            { 
                Height=MemberHealthRecord.Height,
                Weight=MemberHealthRecord.Weight,
                BloodType=MemberHealthRecord.bloodtype,
                Note=MemberHealthRecord.Note
            
            };
            return viewModel;

        }

        public MemberUpdateViewModel? GetMemberToUpdate(int id)
        {
            var memberToUpdate= _unitOfWork.GetRepository<Member>().GetById(id);
            if (memberToUpdate is null)
            { return null; }
            var viewModel = new MemberUpdateViewModel()
            {
                Email = memberToUpdate.Email,
                Phone = memberToUpdate.Phone,
                Name = memberToUpdate.name,
                BuildingNumber = memberToUpdate.address.BuildingNumber,
                City = memberToUpdate.address.City,
                Street = memberToUpdate.address.street
            };
            return viewModel;
        }
        public bool UpdateMember(int id, MemberUpdateViewModel updateMember)
        {
            try
            {
                if (IsEmailExists(updateMember.Email) || IsPhoneExists(updateMember.Phone)) return false;
                var memberToUpdate = _unitOfWork.GetRepository<Member>().GetById(id);
                if(memberToUpdate is null)  return false;
                memberToUpdate.Email = updateMember.Email;
                memberToUpdate.Phone=updateMember.Phone;
                memberToUpdate.address.BuildingNumber = updateMember.BuildingNumber;
                memberToUpdate.address.street=updateMember.Street;
                memberToUpdate.address.City=updateMember.City;
                memberToUpdate.Updated_At = DateTime.Now;

                _unitOfWork.GetRepository<Member>().Update(memberToUpdate);
              return  _unitOfWork.SaveChanges()>0;
                
                
            }
            catch
            {
                return false;
            }
            
        }

        #region Helpers
        bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(m => m.Email == email).Any();
        }
        bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(m => m.Phone == phone).Any();
        }
        #endregion

    }
}
