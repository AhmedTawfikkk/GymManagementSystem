using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entites
{
    public class Plan:BaseEntity
    {
        public string name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationDays{ get; set; } 
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public ICollection<MemebrShip> memebrPlans { get; set; } = null!;

    }
}
