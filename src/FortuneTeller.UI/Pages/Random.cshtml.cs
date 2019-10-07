using FortuneTeller.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Threading.Tasks;

namespace FortuneTeller.UI.Pages
{
    [Authorize(Policy = "fortunes.read")]
    public class RandomModel : PageModel
    {
        public string Message { get; private set; } = "Hello from FortuneTellerUI!";
        private FortuneServiceCommand _fortunes;
        public RandomModel(FortuneServiceCommand fortuneService)
        {
            _fortunes = fortuneService;
        }

        public async Task OnGetAsync()
        {
            var fortune = await _fortunes.RandomFortuneAsync();
            Message = fortune.Text;
            HttpContext.Session.Set("MyFortune", Encoding.ASCII.GetBytes(Message));
        }
    }
}