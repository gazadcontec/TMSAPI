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
using System.Web.Script.Serialization;
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
using System.Threading.Tasks;
using Microsoft;
using System.Timers;
//using System.Threading;


namespace TVMS_API.Controllers
{

    // timer to call MyMethod() every minutes 


    public class NotificationController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();
        //
        // GET: /Notification/

        // System.Timers.Timer timer = new System.Timers.Timer(1000);


        //  [STAThread]

        static void Main(string[] args)
        {

            Timer t = new Timer(60000); // 1 sec = 1000, 60 sec = 60000

            t.AutoReset = true;

            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);

            t.Start();

        }



        private static void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            // do stuff every minute



        }






        //[System.Web.Http.HttpPost]
        //public JObject UpdateFirebase_Client([FromBody] ClientFirebase_det ClientFirebaseDet)
        //{
        //    string a;
        //    string main_ret = "";
        //    SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("ADM_Upd_Client_Firebase", cn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@loginid", ClientFirebaseDet.LoginId);
        //        cmd.Parameters.AddWithValue("@firebaseId", ClientFirebaseDet.FirebaseCode);



        //        SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
        //        output.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(output);


        //        cn.Open();
        //        cmd.ExecuteNonQuery();

        //        a = output.Value.ToString();
        //        cn.Close();
        //        if (a == "1")
        //            main_ret = main_ret + "{\"Result\":\"Success\"}";


        //        else
        //            main_ret = main_ret + "{\"Result\":\"Failed\"}";

        //        return JObject.Parse(main_ret);
        //    }

        //    catch (Exception e)
        //    {
        //        cn.Close();
        //        return JObject.Parse(@"{""Server Error""}");
        //    }
        //}


       


        [System.Web.Http.HttpPost]
        public void SendNotification(string deviceId, string message)    // expires in 15,10,5 min
        {

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                string qry = "";
                if (deviceId == "tikGen")
                    qry = "select * from vw_RegVehicle_ParkingNotification where ParkingTicketNo='" + message + "'";
                else
                {
                 //   qry = "select * from vw_RegVehicle_ParkingNotification where SlotSituation='Orange'";
                    qry = "select * from vw_RegVehicle_ParkingNotification where SlotSituation='Orange' or (SlotSituation='Red' and (TimeLeft<=0 and TimeLeft>-3))";

                }
                using (SqlCommand cmd = new SqlCommand(qry, cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }


            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //string SERVER_API_KEY = "AAAAFJS8O-Q:APA91bG3bOond6FfpMGnVvv-jX60Y6744O0qVCOTtxBJVa2cU9xhscNzONSxl-c-RlN7f5Kd1h-6dMAS_n612lFZRprkGwbycqtoJjPQisDbO2Hf8xmC6_-zKWfKNNu4z4BVhxFUErIe";
                    string SERVER_API_KEY = "AAAA3vXIgCA:APA91bESiVxOnjKc_1H20Q9JstdRwlSBRnCL1n8r7IzmqU6ZZYb-O_SSSiCsgoWevfQOyxt-X_gStoKiLwcOot-YOosb_uQInCKILV9mZdFwg8m_RgJTwis-a3jlRCc9RuLe4ogfaUCP";

                    var SENDER_ID = table.Rows[i]["FirebaseId"].ToString();

                    message = "aaaa";
                    var value = message;
                    WebRequest tRequest;
                    //   tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");

                    tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "post";
                    tRequest.ContentType = "application/json";
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                    // tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                    // string postData = "to:dVsL5Mx1kYg:APA91bG7NYehUnLcDydqK5WLHJUCz-EgBC_YeoUEvW2dxwCvW65GCoT1isA_hV8zXU8iIpuV_bQi1X8OWprVzkXKpPxxuEJuHUHlbLXDSo0KOo1zfj7TeZe5b4gfqBehBW6GMTCs6s_B,collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";

                    string postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"This is to inform you that the parking permit for your vehicle " + table.Rows[i]["VehicleNo"].ToString() + " valid for " + table.Rows[i]["Duration"].ToString() + " hours from " + table.Rows[i]["StartOn"].ToString() + " to " + table.Rows[i]["EndOn"].ToString() + " to expires in " + table.Rows[i]["TimeLeft"].ToString() + "minutes. Kindly effect the soonest renewal of the parking permit should you choose to do so, or risk it’s reversal to public parking space subject to hourly parking fees or its allocation as exclusive permanent parking spaces to another organization. Thank You... \",\r\n     \"title\": \"Collapsing A\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Parking Notification\",\r\n     \"title\": \"Parking\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}";

                    if (deviceId == "tikGen")
                    {
                        postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"You have successfully booked parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " for " + table.Rows[i]["Duration"].ToString() + " minutes. Your permit " + table.Rows[i]["ParkingTicketNo"].ToString() + " expires at " + table.Rows[i]["EndOn"].ToString() + ". \",\r\n     \"title\": \"Parking\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Parking Notification\",\r\n     \"title\": \"Collapsing A\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}"; //Parking Allocation
                    }
                    else
                    {
                        if (table.Rows[i]["SlotSituation"].ToString() == "Red")
                        {
                            postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"Your permit for parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " has now expired. Kindly ensure your vehicle is no longer parked in this slot to avoid being fined for an expired parking permit.\",\r\n     \"title\": \"Parking\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Parking Notification\",\r\n     \"title\": \"Collapsing A\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}"; // if expires

                            WebClient client = new WebClient();
                            string URI1 = "https://zm4j6.api.infobip.com/sms/1/text/query?username=Brentwood&password=Bmg_123@&to=+234" + table.Rows[i]["MobileNo"].ToString() + "&text=Your permit for parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " has now expired. Kindly ensure your vehicle is no longer parked in this slot to avoid being fined for an expired parking permit.";
                            var res = client.UploadString(URI1, "");
                        }
                        else
                        {
                            postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"Your permit " + table.Rows[i]["ParkingTicketNo"].ToString() + " for parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " will expire in " + table.Rows[i]["TimeLeft"].ToString() + " minutes.To end or extend your parking session Click here.\",\r\n     \"title\": \"Parking\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Parking Notification\",\r\n     \"title\": \"Collapsing A\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}"; // expires in 15,10,5 min


                            WebClient client = new WebClient();
                            string URI1 = "https://zm4j6.api.infobip.com/sms/1/text/query?username=Brentwood&password=Bmg_123@&to=+234" + table.Rows[i]["MobileNo"].ToString() + "&text=Your permit " + table.Rows[i]["ParkingTicketNo"].ToString() + " for parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " will expire in " + table.Rows[i]["TimeLeft"].ToString() + " minutes. To end or extend your parking session login in mobile app or contact to agent.";
                            var res = client.UploadString(URI1, "");
                        }
                    }
                    Console.WriteLine(postData);
                    Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    tRequest.ContentLength = byteArray.Length;

                    Stream dataStream = tRequest.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    WebResponse tResponse = tRequest.GetResponse();

                    dataStream = tResponse.GetResponseStream();

                    StreamReader tReader = new StreamReader(dataStream);

                    String sResponseFromServer = tReader.ReadToEnd();


                    tReader.Close();
                    dataStream.Close();
                    tResponse.Close();


                    /********** Insert In Database ***************/

                    string a;
                    string main_ret = "";
                    SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

                    try
                    {
                        SqlCommand cmd = new SqlCommand("ADM_INS_Notifications", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                       // cmd.Parameters.AddWithValue("@NotificationText", "This is to inform you that the parking permit for your vehicle " + table.Rows[i]["VehicleNo"].ToString() + " valid for " + table.Rows[i]["Duration"].ToString() + " hours from " + table.Rows[i]["StartOn"].ToString() + " to " + table.Rows[i]["EndOn"].ToString() + " to expires in " + table.Rows[i]["TimeLeft"].ToString() + "minutes. Kindly effect the soonest renewal of the parking permit should you choose to do so, or risk it’s reversal to public parking space subject to hourly parking fees or its allocation as exclusive permanent parking spaces to another organization. Thank You...");

                        if (deviceId == "tikGen")
                        {
                          //  cmd.Parameters.AddWithValue("@NotificationText", "You have successfully booked parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " for " + table.Rows[i]["TimeLeft"].ToString() + " minutes. Your Permit " + table.Rows[i]["ParkingTicketNo"].ToString() + " expires at " + table.Rows[i]["EndOn"].ToString() + ". To end or extend your parking session visit www.tms.gov.ng/parking, the TMS Mobile App,or the nearest Parking Officer.");

                            cmd.Parameters.AddWithValue("@NotificationText", "You have successfully booked parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " for " + table.Rows[i]["Duration"].ToString() + " minutes. Your permit " + table.Rows[i]["ParkingTicketNo"].ToString() + " expires at " + table.Rows[i]["EndOn"].ToString() + ".");
                            //You have successfully booked parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " for " + table.Rows[i]["Duration"].ToString() + " minutes. Your permit " + table.Rows[i]["ParkingTicketNo"].ToString() + " expires at " + table.Rows[i]["EndOn"].ToString() + ".
                        }
                        else
                        {
                            if (table.Rows[i]["SlotSituation"].ToString() == "Red")
                            {
                                cmd.Parameters.AddWithValue("@NotificationText", "Your Permit for parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " has now expired. Kindly ensure your vehicle is no longer parked in this slot to avoid being fined for an expired parking permit.");
                            }

                            else
                            {
                                cmd.Parameters.AddWithValue("@NotificationText", "Your Permit " + table.Rows[i]["ParkingTicketNo"].ToString() + " for parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " will expire in " + table.Rows[i]["TimeLeft"].ToString() + " minutes.To end or extend your parking session click here.");
                            }

                            //"Your permit " + table.Rows[i]["ParkingTicketNo"].ToString() + " for parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " will expire in " + table.Rows[i]["TimeLeft"].ToString() + " minutes.To end or extend your parking session visit www.tms.gov.ng/parking, the TMS Mobile App,or the nearest Parking Officer.
                           // "Your permit for parking slot " + table.Rows[i]["Slot"].ToString() + " on " + table.Rows[i]["ParkingName"].ToString() + " has now expired. Kindly ensure your vehicle is no longer parked in this slot to avoid being fined for an expired parking permit.
                        }
                            cmd.Parameters.AddWithValue("@ModuleName", "Parking");
                        cmd.Parameters.AddWithValue("@UserId", table.Rows[i]["UserId"].ToString());
                        


                        SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                        output.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(output);


                        //  cmd.Parameters["@SuccessID"].Direction = ParameterDirection.Output;

                        // cmd.Parameters.Add(new SqlParameter("@SuccessID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);
                        cn.Open();
                        cmd.ExecuteNonQuery();

                        a = output.Value.ToString();
                        cn.Close();

                        //if (a == "1")
                        //    main_ret = main_ret + "{\"Result\":\"Success\"}";

                        //else if (a == "2")
                        //    main_ret = main_ret + "{\"Result\":\"Version Already Exist.\"}";

                        //else

                        //    main_ret = main_ret + "{\"Result\":\"Failed\"}";

                       
                    }

                    catch (Exception ex)
                    {
                        cn.Close();
                      
                    }

                }
            }


            // return sResponseFromServer;

        }


        [System.Web.Http.HttpPost]
        public void SendComplainceNotification(string VehNo, string message)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("SELECT FirebaseId, VehicleNo FROM tblVehicleDetails INNER JOIN tblClientUserMaster ON tblVehicleDetails.UserId = dbo.tblClientUserMaster.UserId WHERE (tblVehicleDetails.VehicleNo = '"+VehNo+"')", cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {
               // string SERVER_API_KEY = "AAAAFJS8O-Q:APA91bG3bOond6FfpMGnVvv-jX60Y6744O0qVCOTtxBJVa2cU9xhscNzONSxl-c-RlN7f5Kd1h-6dMAS_n612lFZRprkGwbycqtoJjPQisDbO2Hf8xmC6_-zKWfKNNu4z4BVhxFUErIe";
               
                string SERVER_API_KEY = "AAAA3vXIgCA:APA91bESiVxOnjKc_1H20Q9JstdRwlSBRnCL1n8r7IzmqU6ZZYb-O_SSSiCsgoWevfQOyxt-X_gStoKiLwcOot-YOosb_uQInCKILV9mZdFwg8m_RgJTwis-a3jlRCc9RuLe4ogfaUCP";
                var SENDER_ID = table.Rows[0]["FirebaseId"].ToString();

               // message = "aaaa";
                var value = message;
                WebRequest tRequest;
                //   tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");

                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                // tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                // string postData = "to:dVsL5Mx1kYg:APA91bG7NYehUnLcDydqK5WLHJUCz-EgBC_YeoUEvW2dxwCvW65GCoT1isA_hV8zXU8iIpuV_bQi1X8OWprVzkXKpPxxuEJuHUHlbLXDSo0KOo1zfj7TeZe5b4gfqBehBW6GMTCs6s_B,collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";

               // string postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"A compliance check  with number " + message + " has just been carried out on your vehicle - " + VehNo + "\",\r\n     \"title\": \"Compliance\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Compliance Notification\",\r\n     \"title\": \"Collapsing A\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}";

                string postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"A compliance check  with number " + message + " has just been processed against your vehicle " + VehNo + ". Click here to view.\",\r\n     \"title\": \"Compliance\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Compliance Notification\",\r\n     \"title\": \"Collapsing A\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}";
                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();


                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
        }


        [System.Web.Http.HttpPost]
        public void SendChallengeNotification(string VehNo, string message)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("SELECT FirebaseId, VehicleNo FROM tblVehicleDetails INNER JOIN tblClientUserMaster ON tblVehicleDetails.UserId = dbo.tblClientUserMaster.UserId WHERE (tblVehicleDetails.VehicleNo = '" + VehNo + "')", cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {
                string SERVER_API_KEY = "AAAA3vXIgCA:APA91bESiVxOnjKc_1H20Q9JstdRwlSBRnCL1n8r7IzmqU6ZZYb-O_SSSiCsgoWevfQOyxt-X_gStoKiLwcOot-YOosb_uQInCKILV9mZdFwg8m_RgJTwis-a3jlRCc9RuLe4ogfaUCP";
                // var SENDER_ID = "cfSNWCaPcik:APA91bFLDt-Zjfa0DiKJDani5AqGGhhHtYcNFiHxKRlFwCfZoPikMtAFi_TB3icE26HnkQc30ykbmDwituleSsWN6j5ivM10GZh5x3_09OYiLN21jA7s-EvnI9rNFawrOn0AjiKttvOY";
                var SENDER_ID = table.Rows[0]["FirebaseId"].ToString();

              //  message = "aaaa";
                var value = message;
                WebRequest tRequest;
                //   tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");

                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                // tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                // string postData = "to:dVsL5Mx1kYg:APA91bG7NYehUnLcDydqK5WLHJUCz-EgBC_YeoUEvW2dxwCvW65GCoT1isA_hV8zXU8iIpuV_bQi1X8OWprVzkXKpPxxuEJuHUHlbLXDSo0KOo1zfj7TeZe5b4gfqBehBW6GMTCs6s_B,collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";

                //string postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"A challenge of the violation with number " + message + " has just been registered and sent for preliminary review.\",\r\n     \"title\": \"Challenge\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Challenge Notification\",\r\n     \"title\": \"Collapsing A\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}";

                string postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"Your challenge of the violation with number  " + message + " has just been registered and sent for preliminary review.<br/>We will respond shortly with next steps.<br/>\",\r\n     \"title\": \"Challenge\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Challenge Notification\",\r\n     \"title\": \"Collapsing A\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}";
                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();


                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
        }


        [System.Web.Http.HttpPost]
        public void TrafficViolationNotification(string FireBaseId,string message)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            //DataTable table = new DataTable();
            //{
            //    using (SqlCommand cmd = new SqlCommand("SELECT FirebaseId, VehicleNo FROM tblVehicleDetails INNER JOIN tblClientUserMaster ON tblVehicleDetails.UserId = dbo.tblClientUserMaster.UserId WHERE (tblVehicleDetails.VehicleNo = '" + VehNo + "')", cn1))

            //    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            //    {
            //        adapter.Fill(table);
            //    }
            //}
            //if (table.Rows.Count > 0)
            {
                string SERVER_API_KEY = "AAAA3vXIgCA:APA91bESiVxOnjKc_1H20Q9JstdRwlSBRnCL1n8r7IzmqU6ZZYb-O_SSSiCsgoWevfQOyxt-X_gStoKiLwcOot-YOosb_uQInCKILV9mZdFwg8m_RgJTwis-a3jlRCc9RuLe4ogfaUCP";
               // string SERVER_API_KEY = "AAAAFJS8O-Q:APA91bG3bOond6FfpMGnVvv-jX60Y6744O0qVCOTtxBJVa2cU9xhscNzONSxl-c-RlN7f5Kd1h-6dMAS_n612lFZRprkGwbycqtoJjPQisDbO2Hf8xmC6_-zKWfKNNu4z4BVhxFUErIe";

              //For IOS Key//  SERVER_API_KEY = "AAAAjbciR5c:APA91bEixB1BmF_DZOePwuQYKACP80aVA3HD6ioNevLpiHp_MgWicqiv7xTNUuvg6RvXGKybd_QG08RvTC8o4vOFhbtmoLMmE1eYHO9JTQvsyatYB1NdHNk520E8wODrFURP-jJiVavh";
                
                // var SENDER_ID = "cfSNWCaPcik:APA91bFLDt-Zjfa0DiKJDani5AqGGhhHtYcNFiHxKRlFwCfZoPikMtAFi_TB3icE26HnkQc30ykbmDwituleSsWN6j5ivM10GZh5x3_09OYiLN21jA7s-EvnI9rNFawrOn0AjiKttvOY";
                var SENDER_ID = FireBaseId;

               
                WebRequest tRequest;
                //   tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");

                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                // tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                // string postData = "to:dVsL5Mx1kYg:APA91bG7NYehUnLcDydqK5WLHJUCz-EgBC_YeoUEvW2dxwCvW65GCoT1isA_hV8zXU8iIpuV_bQi1X8OWprVzkXKpPxxuEJuHUHlbLXDSo0KOo1zfj7TeZe5b4gfqBehBW6GMTCs6s_B,collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";

                string postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"" + message + "\",\r\n     \"title\": \"Traffic Violation\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Traffic Violation Notification\",\r\n     \"title\": \"Collapsing A\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}";
                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();


                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
        }




        public class FCMPushNotification
        {
            public FCMPushNotification()
            {
                // TODO: Add constructor logic here
            }

            public bool Successful
            {
                get;
                set;
            }

            public string Response
            {
                get;
                set;
            }
            public Exception Error
            {
                get;
                set;
            }


            public FCMPushNotification SendNotification(string _title, string _message, string _topic)
            {

                FCMPushNotification result = new FCMPushNotification();
                try
                {
                    result.Successful = true;
                    result.Error = null;
                    // var value = message;
                    var requestUri = "https://fcm.googleapis.com/fcm/send";

                    WebRequest webRequest = WebRequest.Create(requestUri);
                    webRequest.Method = "POST";
                    webRequest.Headers.Add(string.Format("Authorization: key={0}", "key=AAAAFJS8O-Q:APA91bG3bOond6FfpMGnVvv-jX60Y6744O0qVCOTtxBJVa2cU9xhscNzONSxl-c-RlN7f5Kd1h-6dMAS_n612lFZRprkGwbycqtoJjPQisDbO2Hf8xmC6_-zKWfKNNu4z4BVhxFUErIe"));
                    webRequest.Headers.Add(string.Format("Sender: id={0}", "cfSNWCaPcik:APA91bFLDt-Zjfa0DiKJDani5AqGGhhHtYcNFiHxKRlFwCfZoPikMtAFi_TB3icE26HnkQc30ykbmDwituleSsWN6j5ivM10GZh5x3_09OYiLN21jA7s-EvnI9rNFawrOn0AjiKttvOY"));
                    webRequest.ContentType = "application/json";

                    var data = new
                    {
                        // to = YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
                        to = "/topics/" + _topic, // this is for topic 
                        notification = new
                        {
                            title = _title,
                            body = _message,
                            //icon="myicon"
                        }
                    };
                    var serializer = new JavaScriptSerializer();
                    var json = serializer.Serialize(data);

                    Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                    webRequest.ContentLength = byteArray.Length;
                    using (Stream dataStream = webRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);

                        using (WebResponse webResponse = webRequest.GetResponse())
                        {
                            using (Stream dataStreamResponse = webResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    result.Response = sResponseFromServer;
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Successful = false;
                    result.Response = null;
                    result.Error = ex;
                }
                return result;
            }



            public FCMPushNotification SendComplainceNotification(string _title, string _message, string _topic)
            {

                FCMPushNotification result = new FCMPushNotification();
                try
                {
                    result.Successful = true;
                    result.Error = null;
                    // var value = message;
                    var requestUri = "https://fcm.googleapis.com/fcm/send";

                    WebRequest webRequest = WebRequest.Create(requestUri);
                    webRequest.Method = "POST";
                    webRequest.Headers.Add(string.Format("Authorization: key={0}", "key=AAAAFJS8O-Q:APA91bG3bOond6FfpMGnVvv-jX60Y6744O0qVCOTtxBJVa2cU9xhscNzONSxl-c-RlN7f5Kd1h-6dMAS_n612lFZRprkGwbycqtoJjPQisDbO2Hf8xmC6_-zKWfKNNu4z4BVhxFUErIe"));
                    webRequest.Headers.Add(string.Format("Sender: id={0}", "dVsL5Mx1kYg:APA91bG7NYehUnLcDydqK5WLHJUCz-EgBC_YeoUEvW2dxwCvW65GCoT1isA_hV8zXU8iIpuV_bQi1X8OWprVzkXKpPxxuEJuHUHlbLXDSo0KOo1zfj7TeZe5b4gfqBehBW6GMTCs6s_B"));
                    webRequest.ContentType = "application/json";

                    var data = new
                    {
                        // to = YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
                        to = "/topics/" + _topic, // this is for topic 
                        notification = new
                        {
                            title = _title,
                            body = _message,
                            //icon="myicon"
                        }
                    };
                    var serializer = new JavaScriptSerializer();
                    var json = serializer.Serialize(data);

                    Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                    webRequest.ContentLength = byteArray.Length;
                    using (Stream dataStream = webRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);

                        using (WebResponse webResponse = webRequest.GetResponse())
                        {
                            using (Stream dataStreamResponse = webResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    result.Response = sResponseFromServer;
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Successful = false;
                    result.Response = null;
                    result.Error = ex;
                }
                return result;
            }
        }

      //  TVMS_dataDataContext db = new TVMS_dataDataContext();

        [System.Web.Http.HttpGet]
        public JObject getNotifications(string userId, string StartPage, string EndPage, string flag)    /* Notification List */
        {

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                string qry = "";

                if (flag == "A")
                    qry = "SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY NotificationId) AS RowNum FROM tbl_Notifications) AS tbl_Notifications_DerivedTable WHERE UserId=" + userId + " and Status=0 order by Notification_DateTime desc";
                    
                else
                    qry = "SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY NotificationId) AS RowNum FROM tbl_Notifications) AS tbl_Notifications_DerivedTable WHERE (tbl_Notifications_DerivedTable.RowNum BETWEEN " + StartPage + " AND " + EndPage + ") and UserId=" + userId + " and Status=0 order by Notification_DateTime desc";


                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {
                //table.Columns.Remove("CreatedBy");
                //table.Columns.Remove("Status");
                //table.Columns.Remove("CreatedOn");


                string x = Convert.ToString(JObject.Parse(@"{""NotificationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                return JObject.Parse(@"{""NotificationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}");
               // return JObject.Parse(@"{""NotificationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)).Replace("[", "").Replace("]", "") + "}");
            }
            else
            {
                return JObject.Parse(@"{""NotificationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}");
               // return JObject.Parse(@"{""NotificationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)).Replace("[", "").Replace("]", "") + "}");

            }
        }


        [System.Web.Http.HttpPost]
        public JObject NotificationUpdate(int NotificationId)  // Read
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_UPD_Notifications", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NotificationId", NotificationId);

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


        [System.Web.Http.HttpGet]
        public JObject get_All_Notifications(string userId, string StartPage, string EndPage, string flag)    /* Notification List */
        {

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                string qry = "";

                if (flag == "A")
                    qry = "SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY NotificationId) AS RowNum FROM tbl_Notifications) AS tbl_Notifications_DerivedTable WHERE UserId=" + userId + " and status!=2  order by Notification_DateTime desc";

                else
                    qry = "SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY NotificationId) AS RowNum FROM tbl_Notifications) AS tbl_Notifications_DerivedTable WHERE (tbl_Notifications_DerivedTable.RowNum BETWEEN " + StartPage + " AND " + EndPage + ") and UserId=" + userId + " and status!=2 order by Notification_DateTime desc";


                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {
                //table.Columns.Remove("CreatedBy");
                //table.Columns.Remove("Status");
                //table.Columns.Remove("CreatedOn");


                string x = Convert.ToString(JObject.Parse(@"{""NotificationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                return JObject.Parse(@"{""NotificationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}");
                // return JObject.Parse(@"{""NotificationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)).Replace("[", "").Replace("]", "") + "}");
            }
            else
            {
                return JObject.Parse(@"{""NotificationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}");
                // return JObject.Parse(@"{""NotificationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)).Replace("[", "").Replace("]", "") + "}");

            }
        }

        [System.Web.Http.HttpPost]
        public JObject NotificationDelete(int NotificationId)  // Delete 
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_DEL_Notifications", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NotificationId", NotificationId);

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
