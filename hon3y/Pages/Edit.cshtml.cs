using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hon3y.Data;
using hon3y.Models;

namespace hon3y.Pages
{
    public class EditModel : PageModel
    {
        private readonly hon3y.Data.FormulierenContext _context;

        public EditModel(hon3y.Data.FormulierenContext context)
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

            var afspraak =  await _context.Afspraken.FirstOrDefaultAsync(m => m.AfspraakId == id);
            if (afspraak == null)
            {
                return NotFound();
            }
            Afspraak = afspraak;
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

            _context.Attach(Afspraak).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AfspraakExists(Afspraak.AfspraakId))
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

        private bool AfspraakExists(int id)
        {
            return _context.Afspraken.Any(e => e.AfspraakId == id);
        }
    }
}
