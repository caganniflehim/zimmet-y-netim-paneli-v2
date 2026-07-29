using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using envanter.Models;
using Microsoft.AspNetCore.Authorization;


namespace envanter.Controllers
{
    [Authorize]
    public class CihazlarController : Controller
    {
        private readonly EnvanterDbContext _context;

        public CihazlarController(EnvanterDbContext context)
        {
            _context = context;
        }

        // LİSTELEME
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cihazlars.ToListAsync());
        }

        // EKLEME SAYFASINI AÇMA
        public IActionResult Create()
        {
            return View();
        }

        // EKLEME İŞLEMİNİ GERÇEKLEŞTİRME
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cihazlar cihaz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cihaz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                // Debug yaparken buraya bir breakpoint koy veya hatayı yazdır
            }
            return View(cihaz);
        }
    }
}