using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hon3y.Data;
using hon3y.Models;

namespace hon3y.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly hon3y.Data.FormulierenContext _context;

        public DeleteModel(hon3y.Data.FormulierenContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Afspraak Afspraak { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afspraak = await _context.Afspraken.FirstOrDefaultAsync(m => m.AfspraakId == id);

            if (afspraak == null)
            {
                return NotFound();
            }
            else
            {
                Afspraak = afspraak;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afspraak = await _context.Afspraken.FindAsync(id);
            if (afspraak != null)
            {
                Afspraak = afspraak;
                _context.Afspraken.Remove(Afspraak);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
