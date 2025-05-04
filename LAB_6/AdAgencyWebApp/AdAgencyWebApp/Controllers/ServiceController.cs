using Microsoft.AspNetCore.Mvc;
using AdAgencyWebApp.Models;
using System.Linq;
using AdAgencyWebApp.Data;

namespace AdAgencyWebApp.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        public ServiceController(AppDbContext context) => _context = context;

        public IActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var query = _context.Services.AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(s => s.Name.Contains(search));

            ViewBag.TotalPages = (int)Math.Ceiling(query.Count() / (double)pageSize);
            ViewBag.CurrentPage = page;

            var services = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(services);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        public IActionResult Edit(int id) => View(_context.Services.Find(id));

        [HttpPost]
        public IActionResult Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Update(service);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        public IActionResult Delete(int id) => View(_context.Services.Find(id));

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var service = _context.Services.Find(id);
            _context.Services.Remove(service);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}