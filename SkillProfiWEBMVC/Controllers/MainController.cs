using Microsoft.AspNetCore.Mvc;
using SkillProfiRequestsToAPI;
using SkillProfiWPF;

namespace SkillProfiWEBMVC.Controllers
{
    public class MainController : Controller
    {
        private readonly SkillProfiWebClient _spClient = new(AppState.ReadServerUrl);

        public IActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public async Task<RedirectResult> AddConsultationAsync(string email, string name, string description) {

            await _spClient.Consultations.AddAsync(new() {EMail = email, Name = name, Description = description});
            return RedirectPermanent("~/");
        }
    }
}
