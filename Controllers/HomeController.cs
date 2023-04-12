using INTEXAPP2.Models;

using Microsoft.AspNetCore.Authorization;

using INTEXAPP2.Models.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

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


        [Authorize(Roles ="Admin")]

        public IActionResult BurialSummary(int pageNum = 1)
        {

            if (HttpContext.Request.Cookies["filters"] == null)
            {
                var cookieOptions = new CookieOptions { };
                HttpContext.Response.Cookies.Append("filters", "hello,this,is,my,stuff", cookieOptions);
            }
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