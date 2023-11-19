using Microsoft.AspNetCore.Mvc;
using Resunet.BL.Auth;
using Resunet.ViewMapper;
using Resunet.ViewModels;
using System.Net;

namespace Resunet.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthBL authBl;

        public LoginController(IAuthBL authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        public async Task <IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (Resunet.BL.AuthorizationException ex)
                {
                    ModelState.AddModelError("Email", "Имя или Email неверные");
                }
            }

            return View("Index", model);
        }
    }
}
