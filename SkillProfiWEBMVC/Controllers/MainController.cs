using Microsoft.AspNetCore.Mvc;
using SkillProfiRequestsToAPI.Consultations;

namespace SkillProfiWEBMVC.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public async Task<RedirectResult> AddConsultationAsync(string email, string name, string description) {

            await ConsultationsRequests.AddConsultationAsync(new() {EMail = email, Name = name, Description = description});
            return RedirectPermanent("~/");
        }
    }
}
