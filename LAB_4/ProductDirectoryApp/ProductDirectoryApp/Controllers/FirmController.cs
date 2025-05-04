using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductDirectoryApp.Data;
using ProductDirectoryApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace ProductDirectoryApp.Controllers
{
    public class FirmController : Controller
    {
        private readonly AppDbContext _context;

        public FirmController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Firms.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var firm = await _context.Firms.FindAsync(id);
            if (firm == null) return NotFound();
            return View(firm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Firm firm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(firm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(firm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var firm = await _context.Firms.FindAsync(id);
            if (firm == null) return NotFound();
            return View(firm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Firm firm)
        {
            if (ModelState.IsValid)
            {
                _context.Update(firm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(firm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var firm = await _context.Firms.FindAsync(id);
            if (firm == null) return NotFound();
            return View(firm);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var firm = await _context.Firms.FindAsync(id);
            if (firm != null)
            {
                _context.Firms.Remove(firm);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}