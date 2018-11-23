using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Medical_Shop_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Medical_Shop_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace Medical_Shop_MVC.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class APISearchController : ControllerBase
    {
        private readonly MEDContext _context;

        public APISearchController(MEDContext context) {
            _context = context;
        }
        
        [HttpGet]
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
                            listOfMed = _context.Medicine.Where(x => x.Name.Contains(SearchValue) || x.Description.Contains(SearchValue)).ToList();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("{0} Is not found a Medicine:" + SearchValue);
                        }
                        //var tt = JsonConvert.SerializeObject(listOfMed);

                        return new JsonResult(listOfMed);
                    }
                    else if (SearchBy == "Doctor")
                    {
                        List<Doctors> listOfDoctors = new List<Doctors>();
                        try
                        {
                            listOfDoctors = _context.Doctors
                                .Include(e => e.doctorType)
                                .Include(d => d.DoctorEnterprise)
                                .Where(x => x.DoctorName.Contains(SearchValue) || x.DoctorSurname.Contains(SearchValue) || x.doctorType.SpecName.Contains(SearchValue) || x.doctorType.SpecDescription.Contains(SearchValue)).ToList();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("{0} Is not found a Doctor:" + SearchValue);
                        }
                        return new JsonResult(listOfDoctors);
                    } else if (SearchBy == "Pharmacy") {
                        List<Pharmacy> listOfPharmacy = new List<Pharmacy>();
                        try
                        {
                            listOfPharmacy = _context.Pharmacy.Where(x => x.PharmName.Contains(SearchValue) || x.PharmAddress.Contains(SearchValue) || x.PharmPhone.Contains(SearchValue)).ToList();
                            
                        }
                        catch (FormatException) {
                            Console.WriteLine("{0} Is not found a Pharmacy:" + SearchValue);
                        }
                        return new JsonResult(listOfPharmacy);
                    }
                    else if (SearchBy == "MedEnterprise")
                    {
                        List<Medical_Enterprise> listOfMedEnt = new List<Medical_Enterprise>();
                        try
                        {
                            listOfMedEnt = _context.Medical_Enterprise.Where(x => x.MedName.Contains(SearchValue) || x.MedAddress.Contains(SearchValue) || x.MedDescription.Contains(SearchValue)).ToList();

                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("{0} Is not found a MedEnterprise:" + SearchValue);
                        }
                        return new JsonResult(listOfMedEnt);
                    }else
                    {
                        return new JsonResult("Not Found");
                    }
                }
                else
                {
                    return new JsonResult("");
                }
            }
            else
            {
                return new JsonResult("");
            }
        }





    }
}