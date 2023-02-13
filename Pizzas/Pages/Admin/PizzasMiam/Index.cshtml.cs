using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pizzas.Data;
using Pizzas.Models;

namespace Pizzas.Pages.Admin.PizzasMiam
{
    public class IndexModel : PageModel
    {
        private readonly Pizzas.Data.DataContext _context;

        public IndexModel(Pizzas.Data.DataContext context)
        {
            _context = context;
        }

        //le modèle créé diretement une liste de pizzas, grâce à la méthode ci-dessous OnGetAsync ou il récupère la liste de pizza
        //de la base de donnée grâce à _context et il stocke la réponse dans Pizza
        //a chaque fois qu'on appelle la page il vient récupérer les données
        public IList<Pizza> Pizza { get;set; }

        public async Task OnGetAsync()
        {
            Pizza = await _context.Pizzas.ToListAsync();
        }
    }
}
