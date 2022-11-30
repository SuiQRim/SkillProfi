using Microsoft.AspNetCore.Mvc;
using SkillProfi;
using SkillProfiRequestsToAPI;
using SkillProfiWPF;

namespace SkillProfiWEBMVC.Controllers
{
    public class MainController : Controller
    {
        private readonly SkillProfiWebClient _spClient = new(AppState.ReadServerUrl);

        public async Task<IActionResult> Face()
        {
            Face face = await _spClient.Face.GetAsync();
            return View(face);
        }

        [HttpPost]
        public IActionResult GoWriteConsultation() 
        {
            return Redirect("/Consultation/Write");
        }
    }
}
