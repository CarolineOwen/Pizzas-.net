using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;

namespace Pizzas.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public bool InvalidConnection = false;
        //création d'un constructeur pour utiliser le Iconfiguration, j'ai maintenant accès à configuration que je vais utiliser dans le Post
        IConfiguration configuration;
        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult OnGet()
        {
           if(HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Pizzasmiam");
            }

            return Page();
        }

        //une fonction async retourne un Task, le IActionResult c'est pour retourner la page qu'on veut et non celle par défault sert à utiliser des actions le Redirect par exemple
        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            //pour accéder à la section Auth du appsettings.json, après le authSection s'utilise comme un dictionnaire
            var authSection = configuration.GetSection("Auth");

            string adminLogin = authSection["AdminLogin"];
            string adminPassword = authSection["AdminPassword"];

            if (username == adminLogin && password == adminPassword)
            {
                var claims = new List<Claim>
 {
 new Claim(ClaimTypes.Name, username)
 };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
               ClaimsPrincipal(claimsIdentity));
                return Redirect( "/Admin/Pizzasmiam");
            }
            InvalidConnection = true;
            return Page();

        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin");
        }
        
    }
}
