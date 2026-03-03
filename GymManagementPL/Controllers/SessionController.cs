using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly IsessionService _sessionService;

        public SessionController(IsessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction("Index");
            }
            var session = _sessionService.GetSessionById(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                RedirectToAction("Index");
            }
            return View(session);
        }

        public IActionResult Create()
        {
            GetDropDowns();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateSessionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                GetDropDowns();
                return View(model);
            }
            var result = _sessionService.CreateSession(model);
            if(result)
            {
                TempData["success"] = "Session Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Create Session";
            }
            return RedirectToAction("Index");

        }
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction("Index");
            }
            var session = _sessionService.GetSessionForUpdate(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session Can Not Be Updated";
                return RedirectToAction("Index");
            }
            GetDropDowns();
            return View(session);

        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdateSessionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                GetDropDowns();
                return View(model);
            }
            var result = _sessionService.UpdateSession(model,id);
            if (result)
            {
                TempData["success"] = "Session Updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To update Session";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id";
                return RedirectToAction("Index");
            }
            var session = _sessionService.GetSessionById(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                RedirectToAction("Index");
            }
            TempData["sessionId"]=session!.Id;
            return View();
        }


        public IActionResult DeleteConfirmed(int id)
        {
            var Session = _sessionService.DeleteSession(id);
            if (!Session)
            {
                TempData["ErrorMessage"] = "Session Failed To Be Deleted";
            }
            else
            {
                TempData["Success"] = "Session Deleted Successfully";
            }
            return RedirectToAction("Index");

        }
        #region Helpers
        private void GetDropDowns()
        {
            var trainers = _sessionService.GetTrainerDropDownList();
            ViewBag.trainers = new SelectList(trainers, "id", "name");
            var categories = _sessionService.GetCategoryDropDownList();
            ViewBag.categories = new SelectList(categories, "id", "name");

        }
        #endregion

    }
}