using BizlandWeb.DAL;
using BizlandWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizlandWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProfessionController : Controller
    {
        private const string V = "Index";
        private readonly AppDbContext _context;

        public ProfessionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Professions.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Profession profession)
        {
            //if(!ModelState.IsValid) { return View(); }
            if (profession == null) { return View(); }
            await _context.Professions.AddAsync(profession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Profession? profession = await _context.Professions.FirstOrDefaultAsync(p => p.Id ==id);
            if (profession == null) { return View(); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Profession profession)
        {
            if (profession == null) { return View(); }
            Profession? exists = await _context.Professions.FirstOrDefaultAsync(p => p.Id == profession.Id);
            if (exists == null) { return View(); }  
            exists.Name = profession.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet] 
        public async Task<IActionResult> Delete(int id)
        {
            Profession? profession = await _context.Professions.FirstOrDefaultAsync(p => p.Id == id);
            if (profession == null) { return View(); }
            _context.Remove(profession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }

    }
}
