using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        public IActionResult Index()
        {
            var Trainers = _trainerService.GetAllTrainers(); 
            return View(Trainers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTrainerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Fields");
                return View(model);
            }
            var result = _trainerService.CreateTrainer(model);
            if(result)
            {
                TempData["Success"]="Trainer Created Successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Create! , Phone Number Or Email Already Exists";
            }
            return RedirectToAction("Index");

        }

        public IActionResult Details(int id)
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction("Index");
            }
            var trainer = _trainerService.GetTrainerById(id);
            if(trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction("index");
            }
            return View(trainer);
        }

        public IActionResult Edit(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction("Index");
            }
            var trainer = _trainerService.GetTrainerToUpdate(id);
            if (trainer == null)
            {

                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction("index");
            }
            return View(trainer);

        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id,UpdateTrainerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Fields");
                return View(model);
            }
            var result =_trainerService.UpdateTrainer(model,id);
            if (result)
            {
                TempData["Success"] = "Trainer Updated Successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Update! , Phone Number Or Email Already Exists";
            }
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {

                TempData["ErrorMessage"] = "Id Can Not Be 0 or Negative";
                return RedirectToAction("Index");
            }
            var Trainer = _trainerService.GetTrainerById(id);
            if (Trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction("Index");
            }
            ViewBag.trainerId = Trainer.Id;
            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var Trainer = _trainerService.RemoveTrainer(id);
            if (!Trainer)
            {
                TempData["ErrorMessage"] = "Trainer Failed To Be Deleted";
            }
            else
            {
                TempData["Success"] = "Trainer Deleted Successfully";
            }
            return RedirectToAction("Index");

        }
    }
}
