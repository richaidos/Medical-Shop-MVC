using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Medical_Shop_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Medical_Shop_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Medical_Shop_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MEDContext _context2;

        public HomeController(ApplicationDbContext context, MEDContext context2)
        {
            _context = context;
            _context2 = context2;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("Testsession", "This information from session");

            return View();
        }
        public JsonResult GetSearchingData(string SearchBy, string SearchValue)
        {
            if (SearchValue != null)
            {
                if (SearchValue.Length > 0)
                {
                    if (SearchBy == "Medecine")
                    {
                        List<Medicine> listOfMed = new List<Medicine>();
                        try
                        {
                            listOfMed = _context2.Medicine.Where(x => x.Name.Contains(SearchValue) || x.Description.Contains(SearchValue)).ToList();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("{0} Is not found a Medicine:" + SearchValue);
                        }
                        //var tt = JsonConvert.SerializeObject(listOfMed);

                        return Json(listOfMed);
                    }
                    else if (SearchBy == "Doctor")
                    {
                        List<Doctors> listOfDoctors = new List<Doctors>();
                        try
                        {
                            listOfDoctors = _context2.Doctors
                                .Include(e => e.doctorType)
                                .Include(d => d.DoctorEnterprise)
                                .Where(x => x.DoctorName.Contains(SearchValue) || x.DoctorSurname.Contains(SearchValue) || x.doctorType.SpecName.Contains(SearchValue) || x.doctorType.SpecDescription.Contains(SearchValue)).ToList();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("{0} Is not found a Doctor:" + SearchValue);
                        }
                        return Json(listOfDoctors);
                    }
                    else if (SearchBy == "Pharmacy")
                    {
                        List<Pharmacy> listOfPharmacy = new List<Pharmacy>();
                        try
                        {
                            listOfPharmacy = _context2.Pharmacy.Where(x => x.PharmName.Contains(SearchValue) || x.PharmAddress.Contains(SearchValue) || x.PharmPhone.Contains(SearchValue)).ToList();

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("{0} Is not found a Pharmacy:" + SearchValue);
                        }
                        return Json(listOfPharmacy);
                    }
                    else if (SearchBy == "MedEnterprise")
                    {
                        List<Medical_Enterprise> listOfMedEnt = new List<Medical_Enterprise>();
                        try
                        {
                            listOfMedEnt = _context2.Medical_Enterprise.Where(x => x.MedName.Contains(SearchValue) || x.MedAddress.Contains(SearchValue) || x.MedDescription.Contains(SearchValue)).ToList();

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("{0} Is not found a MedEnterprise:" + SearchValue);
                        }
                        return Json(listOfMedEnt);
                    }
                    else
                    {
                        return Json("Not Found");
                    }
                }
                else
                {
                    return Json("");
                }
            }
            else
            {
                return Json("");
            }
        }

        public IActionResult About()
        {
            var usls = _context.Users.ToList();
            String names = "";

            foreach (var ff in usls)
            {
                names += " " + ff.UserName;
            }
            ViewBag.userlist = names;


            ViewBag.sessionv = HttpContext.Session.GetString("Testsession");
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Test()
        {
            ViewData["Message"] = "This is a test message.";

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
