using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entites
{
    public class MemberSession:BaseEntity
    {
        //startDate from baseEntity

        public bool IsAttended { get; set; }
        public int MemeberId { get; set; }  
        public Member Member { get; set; } = null!;
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
    }
}
