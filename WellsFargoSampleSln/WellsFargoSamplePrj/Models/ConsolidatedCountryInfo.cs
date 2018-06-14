using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.BusinessLayer;

namespace WellsFargoSamplePrj.Models
{
    public class ConsolidatedCountryInfo
    {
        public Country Country { get; set; }
        public string  StateList { get; set; }

    }
}