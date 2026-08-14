using Microsoft.AspNetCore.Authorization;
using E_commerce_iti.Models;
using E_commerce_iti.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_iti.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
