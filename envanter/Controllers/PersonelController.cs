using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using envanter.Models;

[Authorize]
public class PersonelController : Controller
{
    private readonly EnvanterDbContext _context;

    public PersonelController(EnvanterDbContext context)
    {
        _context = context;
    }

    // GET: PERSONELS
    public async Task<IActionResult> Index()    
    {
        var personelListesi = await _context.Personels.Include(p => p.Zimmets).ThenInclude(z => z.Cihaz).ToListAsync();
        return View(personelListesi);
    }

    // GET: PERSONELS/Details/5
    public async Task<IActionResult> Details(int? personelid)
    {
        if (personelid == null)
        {
            return NotFound();
        }

        var personel = await _context.Personels
            .FirstOrDefaultAsync(m => m.PersonelId == personelid);
        if (personel == null)
        {
            return NotFound();
        }

        return View(personel);
    }

    // GET: PERSONELS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PERSONELS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PersonelId,AdSoyad,Departman,Email,KayitTarihi,Durum,Zimmets")] Personel personel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(personel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(personel);
    }

    // GET: PERSONELS/Edit/5
    public async Task<IActionResult> Edit(int? personelid)
    {
        if (personelid == null)
        {
            return NotFound();
        }

        var personel = await _context.Personels.FindAsync(personelid);
        if (personel == null)
        {
            return NotFound();
        }
        return View(personel);
    }

    // POST: PERSONELS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? personelid, [Bind("PersonelId,AdSoyad,Departman,Email,KayitTarihi,Durum,Zimmets")] Personel personel)
    {
        if (personelid != personel.PersonelId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(personel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonelExists(personel.PersonelId))
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
        return View(personel);
    }

    // GET: PERSONELS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var personel = await _context.Personels
            .FirstOrDefaultAsync(m => m.PersonelId == id);

        if (personel == null)
        {
            return NotFound();
        }

        return View(personel);
    }

    // POST: PERSONELS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var personel = await _context.Personels.FindAsync(id);
        if (personel != null)
        {
            _context.Personels.Remove(personel);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool PersonelExists(int? personelid)
    {
        return _context.Personels.Any(e => e.PersonelId == personelid);
    }
}
