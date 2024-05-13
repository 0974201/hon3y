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
    public class IndexModelDB : PageModel
    {
        private readonly hon3y.Data.FormulierenContext _context;

        public IndexModelDB(hon3y.Data.FormulierenContext context)
        {
            _context = context;
        }

        public IList<Afspraak> Afspraak { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Afspraak = await _context.Afspraken.ToListAsync();
        }
    }
}
