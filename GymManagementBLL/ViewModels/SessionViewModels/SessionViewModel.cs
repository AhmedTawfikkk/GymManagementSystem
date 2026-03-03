using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.SessionViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string TraineeName  { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AvailableSlots { get; set; } 

        #region Computed
        public string DateDisplay=> $"{StartDate.ToString("MMM dd, yyyy",CultureInfo.InvariantCulture)}";
        public string TimeRaneeDisplay => $"{StartDate.ToString("hh:mm tt",CultureInfo.InvariantCulture)} - {EndDate.ToString("hh:mm tt",CultureInfo.InvariantCulture)}";
        public TimeSpan duration => EndDate - StartDate;    
        public string Status
        {
            get
            {
                if(StartDate<=DateTime.Now && EndDate>=DateTime.Now)
                {
                    return "Ongoing";
                }
                else if (StartDate>DateTime.Now)
                {
                    return "Upcoming";
                }
                else
                {
                    return "Completed";
                }
            }
        }

            
            #endregion

    }
}
