using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FortuneTeller.UI.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your application description page.";
        }
    }
}
