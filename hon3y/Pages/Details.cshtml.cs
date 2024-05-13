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
    public class DetailsModel : PageModel
    {
        private readonly hon3y.Data.FormulierenContext _context;

        public DetailsModel(hon3y.Data.FormulierenContext context)
        {
            _context = context;
        }

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
    }
}
