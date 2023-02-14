using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzas.Data;
using Pizzas.Models;

namespace Pizzas.Pages.Admin.PizzasMiam
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly Pizzas.Data.DataContext _context;

        public EditModel(Pizzas.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]//le bind property va permettre d'utiliser l'objet pizza dans la vue
        public Pizza Pizza { get; set; }

        //le ? signifie que l'id peut etre nulll
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //chercher la première valeur que tu trouve ou l'id dans la base de donnée correspond à l'id en paramètre
            Pizza = await _context.Pizzas.FirstOrDefaultAsync(m => m.Id == id);

            if (Pizza == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            //metre à jour l'objet pizza dans la base de donnée
            _context.Update(Pizza);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(Pizza.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.Id == id);
        }
    }
}
