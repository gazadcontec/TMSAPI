using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Linq.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
//using System.Web.Script.Serialization;
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
using System.Security.Cryptography;
using System.IO.Compression;

namespace TVMS_API.Controllers
{
    public class ServiceCenterDeviceResponse
    {
        public string requestId { get; set; }
        public string AttendedBy { get; set; }
        public string ServiceCenterResponse { get; set; }
        public string flag_web_app { get; set; }

    }

    public class ServiceCenterController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();

        [System.Web.Http.HttpPost]
        public JObject ServiceCenterDeviceResponse(ServiceCenterDeviceResponse ServiceCenterDeviceResponse)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_Upd_ServiceCenterDeviceResponse", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@requestId", ServiceCenterDeviceResponse.requestId);
                cmd.Parameters.AddWithValue("@AttendedBy", ServiceCenterDeviceResponse.AttendedBy);
                cmd.Parameters.AddWithValue("@ServiceCenterResponse", ServiceCenterDeviceResponse.ServiceCenterResponse);
                cmd.Parameters.AddWithValue("@flag_web_app", ServiceCenterDeviceResponse.flag_web_app);



                SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);


                //  cmd.Parameters["@SuccessID"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add(new SqlParameter("@SuccessID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);
                cn.Open();
                cmd.ExecuteNonQuery();

                a = output.Value.ToString();
                cn.Close();

                if (a == "1")
                    main_ret = main_ret + "{\"Result\":\"Success\"}";

                else if (a == "5" && ServiceCenterDeviceResponse.flag_web_app == "A")
                    main_ret = main_ret + "{\"Result\":\"Different Response from Web\"}";

                else if (a == "5" && ServiceCenterDeviceResponse.flag_web_app == "W")
                    main_ret = main_ret + "{\"Result\":\"Different Response from App\"}";

                else

                    main_ret = main_ret + "{\"Result\":\"Failed\"}";

                return JObject.Parse(main_ret);
            }

            catch (Exception ex)
            {
                cn.Close();
                return JObject.Parse(@"{""Server Error""}");
            }
        }

    }
}
