using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TVMS_API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Globalization;

namespace TVMS_API.Controllers
{
    public class SoftAllianceController : ApiController
    {
        //
        // GET: /SoftAlliance/
        TVMS_dataDataContext db = new TVMS_dataDataContext();

        [System.Web.Http.HttpGet]
        public JObject getSearch_SoftAlliance(string VehNo_tikNo, string key)
        {
            string main_ret="";
            try
            {
                if (key == "69fa29879bj298")
                {
                    VehNo_tikNo = VehNo_tikNo.Replace("'", "''");
                    key = "69fa29879bj298";

                    SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
                    SqlCommand cmd = new SqlCommand("select *,link+VideoFileName as imgLink,case when ISNULL(penaltypaid,0)=0 then 'Unpaid' else 'Paid' end as Status FROM vw_ClientSearch where VehicleNumber='" + VehNo_tikNo + "'", cn1);
                    cmd.CommandType = CommandType.Text;

                    DataTable table = new DataTable();
                    {
                        string qry = "";
                        string qryCondition = "";

                        //  using (SqlCommand cmd = new SqlCommand(qry, cn1))


                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(table);
                        }

                        DataTable dt_vehicleData = new DataTable();
                        dt_vehicleData.Columns.AddRange(new DataColumn[7] { new DataColumn("Status", typeof(string)), new DataColumn("bill_number", typeof(string)), new DataColumn("offense_code", typeof(string)), new DataColumn("offense_name", typeof(string)), new DataColumn("offense_date", typeof(string)), new DataColumn("image_url", typeof(string)), new DataColumn("bill_amount", typeof(string)) });


                        //  return JObject.Parse(table);
                        if (table.Rows.Count > 0)
                        {
                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                dt_vehicleData.Rows.Add(table.Rows[i]["Status"].ToString(), table.Rows[i]["TicketNo"].ToString(), table.Rows[i]["OffenceId"].ToString(), table.Rows[i]["OffenceName"].ToString(), table.Rows[i]["VideoDateTime"].ToString(), table.Rows[i]["imgLink"].ToString(), table.Rows[i]["Penalty"].ToString());
                            }

                            if (dt_vehicleData.Rows[0]["bill_amount"].ToString() != "")
                            {
                                // return JObject.Parse(@"{""VehicleDetails"" :" + (JsonConvert.SerializeObject(dt_vehicleData, Newtonsoft.Json.Formatting.None)) + "}");
                                main_ret = "{\"header\":{\"has_bills\": true, \"total_bills\": " + dt_vehicleData.Rows.Count + " }, \"lines\":" + JsonConvert.SerializeObject(dt_vehicleData, Newtonsoft.Json.Formatting.None) + "}";


                                return JObject.Parse(main_ret);
                            }
                            else
                            {
                                main_ret = "{\"header\":{\"has_bills\": false, \"total_bills\": 0 }, \"lines\":[] }";
                                return JObject.Parse(main_ret);
                            }
                        }
                        else
                        {
                            main_ret = main_ret + "{\"code\":\"400\",\"message\":\"Invalid plate number\"}";
                            return JObject.Parse(main_ret);
                        }

                    }

                }
                else
                {
                    main_ret = main_ret + "{\"code\":\"400\",\"message\":\"Invalid plate number\"}";
                    return JObject.Parse(main_ret);
                }
            }
            catch (Exception ex)
            {
                main_ret = main_ret + "{\"code\":\"400\",\"message\":\"Invalid plate number\"}";
                return JObject.Parse(main_ret);
            }
        }
    }
}
