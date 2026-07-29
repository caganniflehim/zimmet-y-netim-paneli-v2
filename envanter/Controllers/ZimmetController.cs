using envanter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class ZimmetController : Controller
{
    private readonly EnvanterDbContext _context;

    public ZimmetController(EnvanterDbContext context)
    {
        _context = context;
    }

    // GET: ZIMMETS
    public async Task<IActionResult> Index()    
    {
        var envanterDbContext = _context.Zimmets.Include(z => z.Cihaz).Include(z => z.Personel);
        return View(await envanterDbContext.ToListAsync());
    }

    // GET: ZIMMETS/Details/5
    public async Task<IActionResult> Details(int? zimmetid)
    {
        if (zimmetid == null)
        {
            return NotFound();
        }

        var zimmet = await _context.Zimmets
            .FirstOrDefaultAsync(m => m.ZimmetId == zimmetid);
        if (zimmet == null)
        {
            return NotFound();
        }

        return View(zimmet);
    }

    // GET: ZIMMETS/Create
    [HttpGet]
    public IActionResult Create()
    {
        // Personel ve Cihaz listelerini dolduruyoruz
        var personeller = _context.Personels != null ? _context.Personels.ToList() : new List<Personel>();
        ViewBag.PersonelId = new SelectList(personeller, "PersonelId", "AdSoyad");

        // Cihazlars tablosu boş mu kontrol edelim, boşsa boş liste atalım
        var cihazlar = _context.Cihazlars != null ? _context.Cihazlars.ToList() : new List<Cihazlar>();
        ViewBag.CihazId = new SelectList(cihazlar, "CihazID", "MarkaModel");

        return View();
    }
    /*public IActionResult Create()
    {
        // Personel ve Cihaz listesini Dropdown (SelectListItem) olarak View'a gönderiyoruz
        ViewBag.PersonelId = new SelectList(_context.Personels, "PersonelId", "AdSoyad");
        ViewBag.CihazId = new SelectList(_context.Cihazlars, "CihazID", "MarkaModel"); // DbSet adın Cihazlars olduğu için bu doğru!
        return View();
    }*/

    // POST: ZIMMETS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ZimmetId,CihazId,PersonelId,VerilisTarihi,IadeTarihi,Aciklama,Cihaz,Personel")] Zimmet zimmet)
    {
        if (zimmet.VerilisTarihi == default)
        {
            zimmet.VerilisTarihi = DateTime.Now;
        }

        // ModelState kontrolüne takılmadan direkt ekleyip kaydediyoruz
        _context.Zimmets.Add(zimmet);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: ZIMMETS/Edit/5
    public async Task<IActionResult> Edit(int? zimmetid)
    {
        if (zimmetid == null)
        {
            return NotFound();
        }

        var zimmet = await _context.Zimmets.FindAsync(zimmetid);
        if (zimmet == null)
        {
            return NotFound();
        }
        return View(zimmet);
    }

    // POST: ZIMMETS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? zimmetid, [Bind("ZimmetId,CihazId,PersonelId,VerilisTarihi,IadeTarihi,Aciklama,Cihaz,Personel")] Zimmet zimmet)
    {
        if (zimmetid != zimmet.ZimmetId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(zimmet);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZimmetExists(zimmet.ZimmetId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(zimmet);
    }

    // GET: ZIMMETS/Delete/5
    public async Task<IActionResult> Delete(int? zimmetid)
    {
        if (zimmetid == null)
        {
            return NotFound();
        }

        var zimmet = await _context.Zimmets
            .FirstOrDefaultAsync(m => m.ZimmetId == zimmetid);
        if (zimmet == null)
        {
            return NotFound();
        }

        return View(zimmet);
    }

    // POST: ZIMMETS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? zimmetid)
    {
        var zimmet = await _context.Zimmets.FindAsync(zimmetid);
        if (zimmet != null)
        {
            _context.Zimmets.Remove(zimmet);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ZimmetExists(int? zimmetid)
    {
        return _context.Zimmets.Any(e => e.ZimmetId == zimmetid);
    }
}
