using BizlandWeb.DAL;
using BizlandWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizlandWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
            //if(!ModelState.IsValid) { return View(); }
            if (service == null) { return View(); }
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Service? service = await _context.Services.FirstOrDefaultAsync(p => p.Id == id);
            if (service == null) { return View(); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Service service)
        {
            if (service == null) { return View(); }
            Service? exists = await _context.Services.FirstOrDefaultAsync(p => p.Id == service.Id);
            if (exists == null) { return View(); }
            exists.Name = service.Name;
            exists.Description = service.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Service? service = await _context.Services.FirstOrDefaultAsync(p => p.Id == id);
            if (service == null) { return View(); }
            _context.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
