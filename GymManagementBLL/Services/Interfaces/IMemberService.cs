using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMemberService
    {
         IEnumerable<MemberViewModel> GetAllMembers();
         bool CreateMember(CreateMemberViewModel member); 

         MemberViewModel? GetMemberDetails(int id);
        HealthRecordViewModel? GetMemberHealthRecordDetails(int id);

        MemberUpdateViewModel? GetMemberToUpdate(int id);

        bool UpdateMember(int id,MemberUpdateViewModel updateMember);

        bool DeleteMember(int id);
    }
}
