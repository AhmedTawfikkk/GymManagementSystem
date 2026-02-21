using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.SessionViewModels
{
    public class CreateSessionViewModel
    {
       
        [Required(ErrorMessage = "Category Is Required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Trainer Is Required")]
        [Display(Name ="Trainer")]
        public int TrainerId { get; set; }
        [Required(ErrorMessage ="Description Is Required")]
        [StringLength(500,MinimumLength =20,ErrorMessage ="Description must be between 20 and 500")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage ="Capacity Is Required")]
        [Range(0,25 ,ErrorMessage ="Capacity Must Be Between 0 and 25")]
        public int Capacity { get; set; }
        [Required(ErrorMessage ="StartDate Is Required")]
        [Display(Name ="StartDate & Time")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "EndDate Is Required")]
        [Display(Name = "StartDate & Time")]
        public DateTime EndDate { get; set; }
        


    }
}
