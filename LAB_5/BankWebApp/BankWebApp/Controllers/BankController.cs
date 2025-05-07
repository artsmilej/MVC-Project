using Microsoft.AspNetCore.Mvc;
using BankWebApp.Models;

namespace BankWebApp.Controllers
{
    public class BankController : Controller
    {
        private readonly BankRepository _repo;

        public BankController(IConfiguration config)
        {
            _repo = new BankRepository(config);
        }

        public IActionResult Index()
        {
            var list = _repo.GetAll();
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Bank bank)
        {
            _repo.Add(bank);
            return RedirectToAction("Index");
        }

        public IActionResult Search(string term)
        {
            var result = _repo.Search(term);
            return View(result);
        }

        [HttpPost]
        public IActionResult Delete(int kod)
        {
            _repo.Delete(kod);
            return RedirectToAction("Index");
        }
    }
}