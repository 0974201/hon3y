using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using hon3y.Data;
using hon3y.Models;

namespace hon3y.Pages
{
    public class CreateModel : PageModel
    {
        private readonly hon3y.Data.FormulierenContext _context;

        public CreateModel(hon3y.Data.FormulierenContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Afspraak Afspraak { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Afspraken.Add(Afspraak);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
