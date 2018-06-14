using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace App.BusinessLayer
{
    public class States
    {
        public string StateName { get; set; }
        public string country { get; set; }
        public string StateAbbreviation { get; set; }
        public string LargestCity { get; set; }
        public string Capital { get; set; }
    }
}
