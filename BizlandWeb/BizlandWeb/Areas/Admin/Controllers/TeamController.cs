using BizlandWeb.DAL;
using BizlandWeb.Models;
using BizlandWeb.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizlandWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext? _context;
        private readonly IWebHostEnvironment _environment;



        public TeamController(AppDbContext? context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.Teams.Include(p => p.Profession).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            //if(!ModelState.IsValid) { return View(); }
            if (team == null) { return View(); }

            if (!team.ImageFile.CheckFileType("image")) { return View(); } 
            if(team.ImageFile.CheckFileSize(2000)) { return View(); }

            string filename = await team.ImageFile.SaveFileAsync(_environment.WebRootPath, "team");
            team.Image = filename;


            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            Team? team = await _context.Teams.FirstOrDefaultAsync(p => p.Id == id);
            if (team == null) { return View(); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Team team)
        {
            ViewBag.Profession = await _context.Professions.ToListAsync();
            if (team == null) { return View(); }
            Team? exists = await _context.Teams.FirstOrDefaultAsync(p => p.Id == team.Id);
            if (exists == null) { return View(); }
            exists.Name = team.Name;
            exists.ProfessionId = team.ProfessionId;

            if (team.ImageFile != null)
            {
                if (!team.ImageFile.CheckFileType("image")) { return View(); }
              if(team.ImageFile.CheckFileSize(2000)) { return View(); }

                exists.ImageFile.DeleteFile(_environment.WebRootPath, "team", exists.Image);
                string filename = await team.ImageFile.SaveFileAsync(_environment.WebRootPath, "team");
                exists.Image = filename;

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Team? team = await _context.Teams.FirstOrDefaultAsync(p => p.Id == id);
            if (team == null) { return View(); }
            _context.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
