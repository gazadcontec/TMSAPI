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
using System.Threading;
using System.Globalization;
using Vanso.SXMP;


namespace TVMS_API.Controllers
{
    public class ArteziaController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();

       

        public void Email_SMS_Log(string Email_Msg_Text, string Type, string status, string comments, string ModuleName,string SentBy, string RecievedBy)
        {
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            SqlCommand cmd = new SqlCommand("ADM_INS_Email_SMS_Log", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email_Msg_Text", Email_Msg_Text);
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Comments", comments);
            cmd.Parameters.AddWithValue("@ModuleName", ModuleName);
            cmd.Parameters.AddWithValue("@SentBy", SentBy);
            cmd.Parameters.AddWithValue("@RecievedBy", RecievedBy);
           

            SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
            output.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(output);

            cn.Open();
            cmd.ExecuteNonQuery();

            string a = output.Value.ToString();
            cn.Close();
        }

        [System.Web.Http.HttpPost]
        public void VansoArtizia_SMS(string ph_no, string message, string c_code, string comments, string ModuleName, string sent_by, string rec_by)
        {
            try
            {
                string SMS_Vanso_Infobip = System.Configuration.ConfigurationManager.AppSettings["SMS_Vanso_Infobip"];

                if (SMS_Vanso_Infobip == "V")
                {
                    SubmitRequest request = new SubmitRequest();
                   // request.account = new Account("NG.106.0919", "B5RqUs97");// set the username and password shipped with the email here

                    request.account = new Account("NG.100.0120", "KYGeg2eN");// set the username and password shipped with the email here
                    request.SourceAddress = new MobileAddress(MobileAddress.Type.alphanumeric, "LASG-TMS");// Please replace VANSO with your preffered Source Address not MOT MORE THAN 11 CHARACTERS
                    if (c_code == "I")
                    {
                        request.DestinationAddress = new MobileAddress(MobileAddress.Type.international, "+91" + ph_no.Substring(ph_no.Length - 10));// destination address is the MSISDN in the international format
                    }
                    else
                    {
                        request.DestinationAddress = new MobileAddress(MobileAddress.Type.international, "+234" + ph_no.Substring(ph_no.Length - 10));// destination address is the MSISDN in the international format
                    }

                    request.SourceAddress.Address = "LASG-TMS";
                    request.Text = message;// message body
                    // var sender = new SXMPSender(@"http://sxmp.gw1.vanso.com", 80);

                    var sender = new SXMPSender(@"http://sxmp.gw1.vanso.com/api/sxmp/1.0", 80);

                    var resp = sender.Submit(request);
                    if (resp.ErrorCode == (int)SXMPErrorCode.OK)
                    {
                        System.Console.WriteLine("Message Submitted. Ticket ID = {0}", ((SubmitResponse)resp).TicketID);
                        Email_SMS_Log(message, "M", "1", comments, ModuleName, sent_by, rec_by);
                    }
                    else
                    {
                        System.Console.WriteLine("Sending Failed. Error code = {0}, Message = {1}", resp.ErrorCode, resp.ErrorMessage);
                    }
                }
                else
                {
                    WebClient client = new WebClient();
                    // client.BaseAddress = "github.bmg.ng:2121";
                    string URI1 = "";
                    if (c_code == "I")
                    {
                        URI1 = "https://zm4j6.api.infobip.com/sms/1/text/query?username=Brentwood&password=Bmg_123@&to=+91" + ph_no.Substring(ph_no.Length - 10) + "&text=" + message;
                    }
                    else
                    {
                        URI1 = "https://zm4j6.api.infobip.com/sms/1/text/query?username=Brentwood&password=Bmg_123@&to=+234" + ph_no.Substring(ph_no.Length - 10) + "&text=" + message;
                    }
                    var res = client.UploadString(URI1, "");
                }
            }
            catch (Exception ex)
            {
            }
        }

        [System.Web.Http.HttpGet]
        public JObject getRegisteredVehicle_Artezia(string VehicleNo)    // Registered & RoadWorthiness
        {
            try
            {
                VehicleNo = VehicleNo.Replace("'", "''");
                //ChesisNo = ChesisNo.Replace("'", "''");

                SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
                SqlCommand cmd1 = new SqlCommand("select * FROM tbl_Vehicle_Details_Artezia where VehicleNo='" + VehicleNo + "'", cn1);
                cmd1.CommandType = CommandType.Text;
                //cmd1.ExecuteNonQuery();

                DataTable table = new DataTable();


                {
                    using (SqlCommand cmd = new SqlCommand("select * FROM tbl_Vehicle_Details_Artezia where VehicleNo='" + VehicleNo + "'", cn1))


                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                if (table.Rows.Count > 0)
                {
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("select * FROM tbl_Vehicle_Details_Artezia where VehicleNo='" + VehicleNo + "'", cn1))


                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[13] {new DataColumn("VehicleNo", typeof(string)),new DataColumn("ChesisNo", typeof(string)), new DataColumn("RegisteredName", typeof(string)), new DataColumn("RegisteredAddress", typeof(string)), new DataColumn("RegistrationDate", typeof(string)),  new DataColumn("Model", typeof(string)),  new DataColumn("Class", typeof(string)),                        
                            new DataColumn("VehicleColor",typeof(string)),new DataColumn("PhoneNumber",typeof(string)),new DataColumn("Make",typeof(string)),new DataColumn("RwcNo",typeof(string)),new DataColumn("RwcExp",typeof(string)),new DataColumn("RwcMessage",typeof(string))});


                if (table.Rows.Count > 0)
                {
                    Add_Vehicle_det VehDet = new Add_Vehicle_det
                    {

                        VehicleNo = table.Rows[0]["VehicleNo"].ToString(),
                        ChesisNo = table.Rows[0]["ChesisNo"].ToString(),
                        RegisteredName = table.Rows[0]["RegisteredName"].ToString(),
                        RegisteredAddress = table.Rows[0]["RegisteredAddress"].ToString(),
                        RegistrationDate = table.Rows[0]["RegistrationDate"].ToString(),
                        Model = table.Rows[0]["Model"].ToString(),
                        Class = table.Rows[0]["Class"].ToString(),
                        VehicleColor = table.Rows[0]["VehicleColor"].ToString(),
                        UserId = table.Rows[0]["UserId"].ToString()


                    };

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataTable dt_RoadWorthiness = getVehicleRoadWorthinessDetails_Artezia(VehicleNo);
                        if (dt_RoadWorthiness.Rows[0]["status"].ToString() == "0")
                            dt.Rows.Add(table.Rows[i]["VehicleNo"].ToString(), table.Rows[i]["ChesisNo"].ToString(), table.Rows[i]["RegisteredName"].ToString(), table.Rows[i]["RegisteredAddress"].ToString(), table.Rows[i]["RegistrationDate"].ToString(), table.Rows[i]["Model"].ToString(), table.Rows[i]["Class"].ToString(), table.Rows[i]["VehicleColor"].ToString(), table.Rows[i]["MobileNo"].ToString(), table.Rows[i]["Make"].ToString(), "", "", dt_RoadWorthiness.Rows[0]["message"].ToString());
                        else
                            dt.Rows.Add(table.Rows[i]["VehicleNo"].ToString(), table.Rows[i]["ChesisNo"].ToString(), table.Rows[i]["RegisteredName"].ToString(), table.Rows[i]["RegisteredAddress"].ToString(), table.Rows[i]["RegistrationDate"].ToString(), table.Rows[i]["Model"].ToString(), table.Rows[i]["Class"].ToString(), table.Rows[i]["VehicleColor"].ToString(), table.Rows[i]["MobileNo"].ToString(), table.Rows[i]["Make"].ToString(), dt_RoadWorthiness.Rows[0]["RwcNo"].ToString(), dt_RoadWorthiness.Rows[0]["RwcExp"].ToString(), dt_RoadWorthiness.Rows[0]["message"].ToString());
                    }
                    // StringBuilder x= new StringBuilder();
                    string x = Convert.ToString(JObject.Parse(@"{""VehicleDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                    // return JObject.Parse(@"{""SearchDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(SearchViolationDetails) + "}");
                    // return JObject.Parse(@"{""VehicleDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}").Merge(getVehicleRoadWorthinessDetails_Artezia(VehicleNo), new JsonMergeSettings {MergeArrayHandling = MergeArrayHandling.Union}); //("{""RoadWorthinessDetails"" :"+getVehicleRoadWorthinessDetails_Artezia(VehicleNo)+"}");

                    JObject veh_det = JObject.Parse(@"{""VehicleDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");


                    return veh_det;
                    // return veh_det.Merge(Road_Worthiness_det, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });
                    // return veh_det.Merge(Road_Worthiness_det, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Concat }); ;
                }
                else
                {
                    return JObject.Parse(@"{""VehicleDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");
                    //  return (@"{LoginDetails : {Status:0}}");
                }
            }
            catch (Exception ex)
            {
                return JObject.Parse(@"{""VehicleDetails"" :[]}");
                // return JObject.Parse(@"{""Server Error""}");
            }
        }


        [System.Web.Http.HttpGet]
        public DataTable getVehicleRoadWorthinessDetails_Artezia(string VehicleNo)    // Registered & RoadWorthiness
        {
            string url = "https://www.lagroute.org/rwc_tms_api_test.php/RWC/Vehicle Registration Number Verification API";

            var res = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string data = "VehRegNo=" + VehicleNo + "&PassKey=123456";
                // string json = " {\n\"VehRegNo\": \"" + VehicleNo + "\",\n\"PassKey\": \"123456\"}";
                res = client.UploadString(url, data);

                var ew = Newtonsoft.Json.JsonConvert.SerializeObject(res);

            }

            DataTable dt = (DataTable)JsonConvert.DeserializeObject("[" + res + "]", (typeof(DataTable)));
            return dt;
        }
    }
}
