using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entites
{
    public class Member:GymUser
    {
        public string? photo { get; set; }
        public HealthRecord HealthRecord { get; set; } = null!;

        public ICollection<MemebrShip> memebrPlans { get; set; } = null!;

        public ICollection<MemberSession> memberSessions { get; set; } = null!;
    }
}
