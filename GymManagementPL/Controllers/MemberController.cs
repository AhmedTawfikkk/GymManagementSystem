using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entites;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            var viewmodel = _memberService.GetAllMembers();
            return View(viewmodel);
        }

        public IActionResult MemberDetail(int id)
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Can Not Be 0 or Negative";
                return RedirectToAction("Index");
            }
            var member = _memberService.GetMemberDetails(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                RedirectToAction("index");
            }
            return View(member);
        }

        public IActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Can Not Be 0 or Negative";
                return RedirectToAction("Index");
            }
            var HealthRecord = _memberService.GetMemberHealthRecordDetails(id);
            if (HealthRecord == null)
            {
                TempData["ErrorMessage"] = "Health Record Not Found";
                return RedirectToAction("Index");
            }
            return View(HealthRecord);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMember(CreateMemberViewModel createdMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Data Is Invalid", "Check For Missing Fields");
                return View("Create", createdMember);
            }
            bool Result = _memberService.CreateMember(createdMember);
            if (Result)
            {
                TempData["Succeed"] = "Member Created Successfully";   // temp data to be valid for the next request when we redirect to index
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Failed"] = "Member Failed To Be Created";
                return RedirectToAction("Index");
            }
        }

        public IActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Can Not Be 0 or Negative";
                return RedirectToAction("Index");
            }
            var MemberToEdit = _memberService.GetMemberToUpdate(id);
            if (MemberToEdit == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction("Index");
            }
            return View(MemberToEdit);


        }
        [HttpPost]
        // In Edit actions, avoid putting the ID in the ViewModel or as a hidden field in the view 
        // to prevent user tampering via 'Inspect Element'. 
        // Always use [FromRoute] in the controller action to ensure the ID is pulled strictly from the URL, 
        // as the default model binder searches form data first.
        public IActionResult MemberEdit([FromRoute] int id, MemberUpdateViewModel updatedMember)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedMember);
            }
            bool result = _memberService.UpdateMember(id, updatedMember);
            if (result)
            {
                TempData["Succeed"] = "Member Updated Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Be Updated";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {

                TempData["ErrorMessage"] = "Id Can Not Be 0 or Negative";
                return RedirectToAction("Index");
            }
            var member = _memberService.GetMemberDetails(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction("Index");
            }
            ViewBag.id= id;
            return View();
        }

        public IActionResult DeleteConfirmed(int id)
        {
            var member= _memberService.DeleteMember(id);
            if(!member)
            {
                TempData["ErrorMessage"] = "Member Failed To Be Deleted";
            }
            else
            {
                TempData["Succes Message"] = "Member Deleted Successfully";
            }
                return RedirectToAction("Index");

        }
    }
}
