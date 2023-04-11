using INTEXAPP2.Models;
using INTEXAPP2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace INTEXAPP2.Controllers
{
    public class HomeController : Controller
    {

        private mummiesContext Context { get; set; }

        public HomeController(ILogger<HomeController> logger, mummiesContext mc)
        {

            Context = mc;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BurialSummary(int pageNum = 1)
        { 
            // Set page length
            int pageLen = 10;

            //Create view Model
            var x = new BurialViewModel
            {
                //Get books using repo
                Burials = Context.Burialmains
                .Skip((pageNum - 1) * pageLen)
                .Take(pageLen),

                //Set up page details
                PageDetails = new PageDetails
                {
                    totalBurials = Context.Burialmains.Count(),
                    BurialsPerPage = pageLen,
                    CurrentPage = pageNum
                }

            };
            return View(x);
        }

        public IActionResult SupervisedAnalysis()
        {
            return View();
        }

        public IActionResult UnsupervisedAnalysis()
        {
            return View();
        }

        public IActionResult AdministrativePages()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}