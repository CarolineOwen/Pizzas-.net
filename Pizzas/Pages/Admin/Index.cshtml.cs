using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Pizzas.Pages.Admin
{
    public class IndexModel : PageModel
    {
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
            if (username == "admin")
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
            return Page();

        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin");
        }
        
    }
}
