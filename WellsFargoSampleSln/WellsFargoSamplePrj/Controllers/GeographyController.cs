using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.BusinessLayer;
using WellsFargoSamplePrj.Models;

namespace WellsFargoSamplePrj.Controllers
{
    public class GeographyController : Controller
    {
        // GET: Geography
        public ActionResult StateInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StateInfo(string CountryCode)
        {
            ConsolidatedCountryInfo _consolidatedData = new ConsolidatedCountryInfo();
            var countryHandler = new CountryHandler();
            var states = countryHandler.GetStatesListByCountry(CountryCode);
            states = states.Replace("\n", "");           
            Country countryInfo = countryHandler.GetCountryInfo(CountryCode);

            _consolidatedData.Country = countryInfo;
            _consolidatedData.StateList = states;
            return View(_consolidatedData);

           
        }
    }
}