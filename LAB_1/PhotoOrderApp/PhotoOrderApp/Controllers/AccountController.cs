using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            TempData["UserEmail"] = user.Email;
            return RedirectToAction("Create", "Order");
        }

        return View(user);
    }
}