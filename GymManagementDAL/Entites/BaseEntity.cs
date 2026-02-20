using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entites
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get;set; }
        
    }
}
