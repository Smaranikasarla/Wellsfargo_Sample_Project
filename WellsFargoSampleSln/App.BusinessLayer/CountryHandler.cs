using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using App.Shared;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System.ServiceModel;

namespace App.BusinessLayer
{
    public class CountryHandler
    {
        
        public string GetStatesListByCountry(string countryName)
        {

            try
            {
                var jsonResult = String.Empty;
                var url = Constants.COUNTRY_WEB_SERVICE_URL;
                var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url + "/" + countryName + "/all");

                using (var response = webrequest.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    jsonResult = reader.ReadToEnd();

                }

                return jsonResult;
            }
            catch (EndpointNotFoundException ex)
            {
                dynamic errorResponse;
                errorResponse = new { ReturnStatus = false, Result = "Not able to connect to Service." };
                return errorResponse;
            }
            catch (CommunicationException ex)
            {
                dynamic errorResponse;
                errorResponse = new { ReturnStatus = false, Result = "Communication Error occurred" };
                return errorResponse;
            }
            catch (Exception ex)
            {
                dynamic errorResponse;
                errorResponse = new { ReturnStatus = false, Result = "Unkown Error occurred. " + ex.Message };
                return errorResponse;
            }

        }

        public Country GetCountryInfo(string CountryName)
        {
            var Country = new Country();

            /* Since this is a prototype, I will hardcode the values for Test Call
             I will include Database transactions in another method, just for usablity.             
             */

            Country.Name = CountryName;
            Country.population = 10000000;
            Country.Continent = "ASIA";
            
            return Country;
        }


        
        public Country GetCountryInfoFromDB(string CountryName)
        {

            try
            {
                var _country = new Country();
                #region Parameters
                var paramList = new List<SqlParameter>
                {
                    new SqlParameter("@Country", CountryName),

                };
                string spName = "FetchCountryInfo";

                SQLConnection _sqlConnection = new SQLConnection(Helper.SQLConnectionstring);
                #endregion
                DataSet countryInfoData = _sqlConnection.ExecuteStoredProc(spName, paramList);


                if (countryInfoData != null && countryInfoData.Tables != null && countryInfoData.Tables[0].Rows.Count > 0)
                {
                    if (countryInfoData.Tables[0].Rows[0] != null)
                    {
                        DataRow row = countryInfoData.Tables[0].Rows[0];

                        if (!Convert.IsDBNull(row["Name"]))
                            _country.Name = row["Name"].ToString();
                        else
                            _country.Name = "N/A";

                        if (!Convert.IsDBNull(row["Population"]))
                            _country.population = Convert.ToInt32(row["Population"].ToString());
                        else
                            _country.population = Convert.ToInt32(0);

                        if (!Convert.IsDBNull(row["Continent"]))
                            _country.Continent = row["COntinent"].ToString();
                        else
                            _country.Continent = "";

                    }
                }

                return _country;
            }
            catch(SqlException ex)
            {
                dynamic errorResponse;
                errorResponse = new { ReturnStatus = false, Result = "SQl Exception occurred. " + ex.Message };
                return errorResponse;
            }
            catch (Exception ex)
            {
                dynamic errorResponse;
                errorResponse = new { ReturnStatus = false, Result = "Unkown Error occurred. " + ex.Message };
                return errorResponse;
            }
            

        }


    }
}
