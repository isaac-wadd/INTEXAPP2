using INTEXAPP2.Models;

using Microsoft.AspNetCore.Authorization;

using INTEXAPP2.Models.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Crypto;

namespace INTEXAPP2.Controllers
{
    public class HomeController : Controller
    {

        private mummiesContext Context { get; set; }
        private mummiesContext Context1 { get; set; }
        private mummiesContext Context2 { get; set; }
        private mummiesContext Context3 { get; set; }
        private IHttpContextAccessor httpContextAccessor { get; set; }

        public HomeController(ILogger<HomeController> logger, mummiesContext mc, mummiesContext mc1, mummiesContext mc2, mummiesContext mc3, IHttpContextAccessor _httpContextAccessor)
        {

            Context = mc;
            Context1 = mc1;
            Context2 = mc2;
            Context3 = mc3;
            httpContextAccessor = _httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult BurialSummary(int pageNum = 1)
        {

            if(httpContextAccessor.HttpContext.Request.Cookies["filters"] == null)
            {
                var cookieOptions = new CookieOptions();
                httpContextAccessor.HttpContext.Response.Cookies.Append("filters", "hello,this,is,my,stuff", cookieOptions);
            }
            // Set page length
            int pageLen = 10;
            List<SummaryView> summaryViews = new List<SummaryView>();
            IQueryable<Burialmain> burialmains = Context.Burialmains.Skip((pageNum - 1) * pageLen).Take(pageLen);
            
            foreach(var b in burialmains)
            {
                SummaryView summary = new SummaryView
                {
                    Id = b.Id,
                    sex = b.Sex,
                    depth = b.Depth,
                    //stature = Context1.Burialdetails.Where(x => x.Id == b.Id).FirstOrDefault().EstimateStature,
                    age = b.Ageatdeath,
                    headdirection = b.Headdirection,
                    //haircolor = Context2.Burialdetails.Where(x => x.Id == b.Id).FirstOrDefault().RightHairColor,
                    //TextileList = Context3.Textiledetails.Where(x => x.MainBurialmainid == b.Id).ToList(),
                };

                summaryViews.Add(summary);

            }

            foreach(var s in summaryViews)
            {
                s.stature = Context1.Burialdetails.Where(x => x.Id == s.Id).FirstOrDefault().EstimateStature;
                s.haircolor = Context2.Burialdetails.Where(x => x.Id == s.Id).FirstOrDefault().RightHairColor;
                s.TextileList = Context3.Textiledetails.Where(x => x.MainBurialmainid == s.Id).ToList();
            }

            //Create view Model
            var x = new BurialViewModel
            {
                //Get books using repo
                Burials = summaryViews,

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
        [HttpGet]
        public IActionResult Edit(long id)
        {
            Burialmain b = Context.Burialmains.Where(x => x.Id == id).First();
            SummaryView s = new SummaryView
            {
                Id = b.Id,
                sex = b.Sex,
                depth = b.Depth,
                //stature = Context1.Burialdetails.Where(x => x.Id == b.Id).FirstOrDefault().EstimateStature,
                age = b.Ageatdeath,
                headdirection = b.Headdirection,
                //haircolor = Context2.Burialdetails.Where(x => x.Id == b.Id).FirstOrDefault().RightHairColor,
                //TextileList = Context3.Textiledetails.Where(x => x.MainBurialmainid == b.Id).ToList(),
            };
            s.stature = Context1.Burialdetails.Where(x => x.Id == s.Id).FirstOrDefault().EstimateStature;
            s.haircolor = Context2.Burialdetails.Where(x => x.Id == s.Id).FirstOrDefault().RightHairColor;
            s.TextileList = Context3.Textiledetails.Where(x => x.MainBurialmainid == s.Id).ToList();

            return View(s);
        }

        [HttpPost]
        public IActionResult Edit(SummaryView s) {
            if(ModelState.IsValid)
            {
                Burialmain b = Context.Burialmains.Where(x => x.Id == s.Id).First();
                b.Sex = s.sex;
                b.Depth = s.depth;
                b.Ageatdeath = s.age;
                b.Headdirection = s.headdirection;
                Context.Update(b);
                Context.SaveChanges();
                return RedirectToAction("BurialSummary");
            }
            else
            {
                return View(s);
            }
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