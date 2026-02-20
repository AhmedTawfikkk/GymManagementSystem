using GymManagementDAL.Entites.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entites
{
   public abstract class GymUser:BaseEntity
    {
        public string name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender gender { get; set; }
        public Address address { get; set; } = null!;

    }
    [Owned]
    public class Address
    {
        public int BuildingNumber { get; set; }
        public string street { get; set; }= null!;
        public string City { get; set; } = null!;

    
    }

}
