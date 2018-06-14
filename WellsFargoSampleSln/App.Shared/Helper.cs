using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Data;
using System.ComponentModel;

namespace App.Shared
{
    public static class Helper
    {


        private static string _SQLConnectionstring = "SQLConnectionString";

        public static string SQLConnectionstring
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[_SQLConnectionstring].ConnectionString;
            }
        }
    }
}
