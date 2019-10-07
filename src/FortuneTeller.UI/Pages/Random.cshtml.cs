using FortuneTeller.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Threading.Tasks;

namespace FortuneTeller.UI.Pages
{
    public class RandomModel : PageModel
    {
        public string Message { get; private set; } = "Hello from FortuneTellerUI!";
        private IFortuneService _fortunes;
        public RandomModel(IFortuneService fortuneService)
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