using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entites
{
    public class Session:BaseEntity
    {
        public string Description { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int categoryId { get; set; }
        public Category category { get; set; } = null!;

        public int TrainerId { get; set; } 
        public Trainer trainer { get; set; } =null!;

        public ICollection<MemberSession> memberSessions { get; set; } = null!;
    }
}
