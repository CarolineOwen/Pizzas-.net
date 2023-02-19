using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pizzas.Data;
using Pizzas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzas.Pages
{
    public class MenuPizzasModel : PageModel
    {

        private readonly Pizzas.Data.DataContext _context;

        public MenuPizzasModel(DataContext context)
        {
            _context = context;
        }
        public IList<Pizza> Pizza { get; set;}

        public async Task OnGetAsync()
        {
            Pizza = await _context.Pizzas.ToListAsync();
            //trier par prix
            Pizza = Pizza.OrderBy(p=>p.Id).ToList();    
        }
    }
}
