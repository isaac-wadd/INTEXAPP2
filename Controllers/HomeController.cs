using INTEXAPP2.Models;

using Microsoft.AspNetCore.Authorization;

using INTEXAPP2.Models.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

using Nancy.Json;


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



        public IActionResult UnsupervisedAnalysis()

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

        [HttpGet]
        public IActionResult SupervisedAnalysis(Prediction? d)
        {
            ViewBag.j = d.PredictedValue;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SupervisedAnalysisAsync(SupervisedView m)
        {
            float? headE = 0;
            float? headW = 0;
            float? sexF = 0;
            float? sexM = 0;
            float? ew_E = 0;
            float? ew_w = 0;
            float? fb_N = 0;
            float? fb_Y = 0;
            float? age_A = 0;
            float? age_C = 0;
            float? age_I = 0;
            float? age_N = 0;
            //float? thickC = 0;
            //float? thickF = 0;
            //float? thickFM = 0;
            //float? thickM = 0;
            //float? thickVF = 0;
            //float? mLinen = 0;
            //float? mWool = 0;
            //float? plyM = 0;
            //float? plyS = null;
            //float? dirS = null;
            //float? dirST = null;
            //float? dirSZ = null;
            //float? dirSZT = null;
            //float? dirZ = null;
            //float? dirZT = null;



            if (m.head == "")
            {
                headE = null;
                headW = null;
            }


            float? sNorthSouth = m.squarenorthsouth;
            float? depth = m.depth;
            float? sEastWest = m.squareeastwest;
            float? length = m.length;
            

            //float? count = m.count;
            if (m.sex == "")
            {
                 sexF = null;
                 sexM = null;
            }
            if (m.eastwest == "")
            {
                 ew_E = null;
                 ew_w = null;
            }
            if (m.face == "")
            {
                 fb_N = null;
                 fb_Y = null;
            }
            if (m.age == "")
            {
                 age_A = null;
                 age_C = null;
                 age_I = null;
                 age_N = null;
            }
            //if (m.thickness == "")
            //{
            //    float? thickC = null;
            //    float? thickF = null;
            //    float? thickFM = null;
            //    float? thickM = null;
            //    float? thickVF = null;
            //}
            //if (m.material == "")
            //{
            //    float? mLinen = null;
            //    float? mWool = null;
            //}
            //if (m.ply == "")
            //{
            //    float? plyM = null;
            //    float? plyS = null;
            //}
            //if (m.direction == "")
            //{
            //    float? dirS = null;
            //    float? dirST = null;
            //    float? dirSZ = null;
            //    float? dirSZT = null;
            //    float? dirZ = null;
            //    float? dirZT = null;
            //}






            if (m.head == "E")
            {
                 headE = 1;
                 headW = 0;
            }


            if (m.sex == "M")
            {
                 sexF = 0;
                 sexM = 1;
            }
            else if (m.sex == "F")
            {
                headE = 0;
                headE = 1;
            }

            if (m.eastwest == "E")
            {
                headE = 0;
                 ew_E = 1;
            }
            else if (m.eastwest == "W")
            {
                 ew_w = 0;
                headE = 1;
            }


            if (m.face == "Y")
            {
                headE = 1;
                 fb_N = 0;
            }
            else if (m.face == "N")
            {
                 fb_Y = 0;
                 fb_N = 1;
            }


            if (m.age == "A")
            {
                 age_A = 1;
                 age_C = 0;
                 age_I = 0;
                 age_N = 0;
            }
            else if (m.age == "C")
            {
                 age_A = 0;
                 age_C = 1;
                 age_I = 0;
                 age_N = 0;
            }
            else if (m.age == "I")
            {
                ew_w = 0;
                headE = 0;
                headE = 1;
                headE = 0;
            }
            else if (m.age == "N")
            {
                headE = 0;
                headE = 0;
                headE = 0;
                headE = 1;
            }


            //if (m.thickness == "Coarse")
            //{
            //    float thickC = 1;
            //    float thickF = 0;
            //    float thickFM = 0;
            //    float thickM = 0;
            //    float thickVF = 0;
            //}
            //else if (m.thickness == "Fine")
            //{
            //    float thickC = 0;
            //    float thickF = 1;
            //    float thickFM = 0;
            //    float thickM = 0;
            //    float thickVF = 0;
            //}
            //else if (m.thickness == "FineMedium")
            //{
            //    float thickC = 0;
            //    float thickF = 0;
            //    float thickFM = 1;
            //    float thickM = 0;
            //    float thickVF = 0;
            //}
            //else if (m.thickness == "Medium")
            //{
            //    float thickC = 0;
            //    float thickF = 0;
            //    float thickFM = 0;
            //    float thickM = 1;
            //    float thickVF = 0;
            //}
            //else if (m.thickness == "VeryFine")
            //{
            //    float thickC = 0;
            //    float thickF = 0;
            //    float thickFM = 0;
            //    float thickM = 0;
            //    float thickVF = 1;
            //}


            //if (m.material == "Linen")
            //{
            //    float mLinen = 1;
            //    float mWool = 0;
            //}
            //else if (m.material == "Wool")
            //{
            //    float mLinen = 0;
            //    float mWool = 1;
            //}


            //if (m.ply == "Single")
            //{
            //    float plyS = 1;
            //    float plyM = 0;
            //}
            //else if (m.ply == "Multiple")
            //{
            //    float plyS = 0;
            //    float plyM = 1;
            //}


            //if (m.direction == "S")
            //{
            //    float dirS = 1;
            //    float dirST = 0;
            //    float dirSZ = 0;
            //    float dirSZT = 0;
            //    float dirZ = 0;
            //    float dirZT = 0;
            //}
            //else if (m.direction == "S-twist")
            //{
            //    float dirS = 0;
            //    float dirST = 1;
            //    float dirSZ = 0;
            //    float dirSZT = 0;
            //    float dirZ = 0;
            //    float dirZT = 0;
            //}
            //else if (m.direction == "S/Z")
            //{
            //    float dirS = 0;
            //    float dirST = 0;
            //    float dirSZ = 1;
            //    float dirSZT = 0;
            //    float dirZ = 0;
            //    float dirZT = 0;
            //}
            //else if (m.direction == "S/Z-twist")
            //{
            //    float dirS = 0;
            //    float dirST = 0;
            //    float dirSZ = 0;
            //    float dirSZT = 1;
            //    float dirZ = 0;
            //    float dirZT = 0;
            //}
            //else if (m.direction == "Z")
            //{
            //    float dirS = 0;
            //    float dirST = 0;
            //    float dirSZ = 0;
            //    float dirSZT = 0;
            //    float dirZ = 1;
            //    float dirZT = 0;
            //}
            //else if (m.direction == "Z-twist")
            //{
            //    float dirS = 0;
            //    float dirST = 0;
            //    float dirSZ = 0;
            //    float dirSZT = 0;
            //    float dirZ = 0;
            //    float dirZT = 1;
            //}

            Dictionary<string, float?> context = new Dictionary<string, float?>();

            context.Add("squarenorthsouth", sNorthSouth);
            context.Add("depth", depth);
            context.Add("squareeastwest", sEastWest);
            context.Add("length", length);
            context.Add("headdirection_E", headE);
            context.Add("headdirection_W", headW);
            context.Add("sex_F", sexM);
            context.Add("sex_M", sexF);
            context.Add("eastwest_E", ew_E);
            context.Add("eastwest_W", ew_w);
            context.Add("facebundles_N", fb_N);
            context.Add("facebundles_Y", fb_Y);
            context.Add("ageatdeath_A", age_A);
            context.Add("ageatdeath_C", age_C);
            context.Add("ageatdeath_I", age_I);
            context.Add("ageatdeath_N", age_N);

            //string js = JsonConvert.SerializeObject(context);

            //using var client = new HttpClient();
            ////client.BaseAddress = new Uri("https://localhost:4000/score");
            //// Add an Accept header for JSON format.
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //   new MediaTypeWithQualityHeaderValue("application/json"));
            //// Get data response
            //var response = client.PostAsync("https://localhost:4000/score",new StringContent( new JavaScriptSerializer().Serialize(context), Encoding.UTF8, "application/json")).Result;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:4000/score");
            var content = new StringContent($"{{\r\n        \"squarenorthsouth\" : {sNorthSouth},\r\n        \"depth\" : {depth},\r\n        \"squareeastwest\" : {sEastWest},\r\n        \"length\" : {length},\r\n        \"headdirection_E\" : {headE},\r\n        \"headdirection_W\" : {headW}, \r\n        \"sex_F\" : {sexF},\r\n        \"sex_M\" : {sexM},\r\n        \"eastwest_E\" : {ew_E},\r\n        \"eastwest_W\" : {ew_w},\r\n        \"facebundles_N\" : {fb_N},\r\n        \"facebundles_Y\" : {fb_Y},\r\n        \"ageatdeath_A\" : {age_A},\r\n        \"ageatdeath_C\" : {age_C},\r\n        \"ageatdeath_I\" : {age_I},\r\n        \"aeatdeath_N\" : {age_N}\r\n\r\n}}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                //Parse the response body
                var dataObjects = await response.Content.ReadFromJsonAsync<IEnumerable<Prediction>>();
                
                //GetJson<Prediction>(dataObjects);
                //foreach (var d in dataObjects)
                //{
                //    System.Diagnostics.Debug.WriteLine(GetJson<Prediction>(d).PredictedValue);
                //}
                //var prediction = JsonConvert.DeserializeObject<response>
                return RedirectToAction("SupervisedAnalysis", dataObjects.FirstOrDefault());
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode,
                              response.ReasonPhrase);
                return View();
            }

            
        }

        //static T GetJson<T>(IEnumerable<> s)
        //{
        //    return JsonSerializer.Deserialize<T>(s);
        //}
    }

    public class Prediction
    {
        public string? PredictedValue { get; set; }
    }

}