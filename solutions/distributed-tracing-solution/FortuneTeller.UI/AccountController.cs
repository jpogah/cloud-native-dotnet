using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FortuneTeller.UI
{
    public class AccountController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Login()
        {
            return Redirect("/random");
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            await HttpContext.Session.CommitAsync();
            return Redirect("/");
        }
    }
}
