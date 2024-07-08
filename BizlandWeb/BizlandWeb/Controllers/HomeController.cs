using BizlandWeb.DAL;
using BizlandWeb.Models;
using BizlandWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizlandWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

      

        public async Task<IActionResult> Index()
        {
            HomeVM vM = new HomeVM()
            {
                Teams = await _context.Teams.Include(x => x.Profession).Take(4).ToListAsync(),
                Services = await _context.Services.Take(6).ToListAsync(),
            };


            return View(vM);
            
        }

    


    }
}
