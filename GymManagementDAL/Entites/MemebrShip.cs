using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entites
{
    public class MemebrShip:BaseEntity
    {
        //startDate from baseEntity
        public DateTime EndDate { get; set; }
        public string status 
        { 
        
            get
            {
                if(EndDate > DateTime.Now)
                {
                    return "Expired";
                }
                else
                {
                    return "Active";
                }
            }
        
        
        }
        public int MemberId { get; set; }   
        public Member Member { get; set; } = null!;

        public int Planid { get; set; }  
        public Plan Plan { get; set; } = null!;
    }
}
