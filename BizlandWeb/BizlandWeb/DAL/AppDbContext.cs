using BizlandWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BizlandWeb.DAL
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Service> Services { get; set; }




    }
}
