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


namespace TVMS_API.Controllers
{
    public class login_det
    {
        public string status { get; set; }
        public string userId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string Phone { get; set; }
        public string ftpURL { get; set; }
        public string ftpPORT { get; set; }
        public string ftpUser { get; set; }

        public string ftpPassword { get; set; }

        public string UserTypeCode { get; set; }
        public string WebApp { get; set; }

        public string firstTimeLogin { get; set; }
        public string emailId { get; set; }

    }

    public class violation_det
    {
        public string UserId { get; set; }
        public string videoFilename { get; set; }
        public string picturesFilenames { get; set; }
        public string locationname { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string deviceIMEI { get; set; }
        public DateTime vdo_datetime { get; set; }
        public string OffenceId { get; set; }

    }

    public class Notified_violation_det
    {
        public string vehicleNumber { get; set; }
        public string NotifiedImage { get; set; }
        public DateTime NotifiedOn { get; set; }
        public string NotifiedBy { get; set; }
    }

    public class Enforced_Notified_violation_det
    {
        public string vehicleNumber { get; set; }
        public string NotifiedImage { get; set; }
        public string NotifiedOn { get; set; }
        public string NotifiedBy { get; set; }
        public string PrintedTicketNo { get; set; }
        public string EnforcementLocation { get; set; }
        public int EnforcementType { get; set; }
        public string PaymentRefNo { get; set; }
        public string flag { get; set; }
    }

    public class handover_det
    {
        public string deviceIMEI { get; set; }
        public string DeviceResponse { get; set; }
        public string DeviceAction { get; set; }
        public string ActionDateTime { get; set; }
        public string UserId { get; set; }
    }

    public class search_violation
    {
        public string status { get; set; }
        public string officerId { get; set; }
        public string officerFname { get; set; }
        public string officerLname { get; set; }
        public string officerLoginId { get; set; }


        public string videoFilename { get; set; }
        public string picturesFilenames { get; set; }
        public string locationname { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string deviceIMEI { get; set; }
        public string vdo_datetime { get; set; }

        public string vehicleNumber { get; set; }
        public string OffenceDetails { get; set; }
        public string Penalty { get; set; }
        public string TicketNo { get; set; }
        public string IsNotified { get;set;}

    }

    public class getSearchClient
    {
        public string status { get; set; }
        public string officerId { get; set; }
        public string officerFname { get; set; }
        public string officerLname { get; set; }
        public string officerLoginId { get; set; }


        public string videoFilename { get; set; }
        public string picturesFilenames { get; set; }
        public string locationname { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string deviceIMEI { get; set; }
        public string vdo_datetime { get; set; }

        public string vehicleNumber { get; set; }
        public string OffenceDetails { get; set; }
        public string Penalty { get; set; }
        public string TicketNo { get; set; }
        public string IsNotified { get; set; }
        public string IsPaid { get; set; }
        public string PenaltyPaidOn { get; set; }
        public string PenaltyTxnNo { get; set; }

        public string diff_flag { get; set; }

        public string PrintedTicketNo { get; set; }

        public string ChallangedOn { get; set; }

        public string ChallangedExpiryDiff { get; set; }
    }


    public class getSearchClient_App
    {
        public string status { get; set; }
        public string officerId { get; set; }
        public string officerFname { get; set; }
        public string officerLname { get; set; }
        public string officerLoginId { get; set; }


        public string videoFilename { get; set; }
        public string picturesFilenames { get; set; }
        public string locationname { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string deviceIMEI { get; set; }
        public DateTime vdo_datetime { get; set; }

        public string vehicleNumber { get; set; }
        public string OffenceDetails { get; set; }
        public string Penalty { get; set; }
        public string TicketNo { get; set; }
        public string IsNotified { get; set; }
        public string IsPaid { get; set; }
        public DateTime PenaltyPaidOn { get; set; }
        public string PenaltyTxnNo { get; set; }

        public string diff_flag { get; set; }

        public string PrintedTicketNo { get; set; }

        public DateTime ChallangedOn { get; set; }

        public string ChallangedExpiryDiff { get; set; }
    }

    public class Clientlogin_Register
    {        
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string LicNo { get; set; }

    }

    public class Clientlogin_det
    {
        public string status { get; set; }
        public string LoginId { get; set; }
        public string userId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string DrivingLicNo { get; set; }
        public byte[] ImagePath { get; set; }

        public string IsFirstTimeLogin { get; set; }

    }

    public class ClientFirebase_det
    {
        public string LoginId { get; set; }
        public string FirebaseCode { get; set; }
    }

    public class Delete_Vehicle
    {
        public string VehicleNumber { get; set; }
        public string UserId { get; set; }
    }

    public class Add_Vehicle_det
    {
        public string VehicleNo { get; set; }
        public string ChesisNo { get; set; }
        public string RegisteredName { get; set; }
        public string RegisteredAddress { get; set; }
        public string RegistrationDate { get; set; }
        public string Model { get; set; }
        public string Class { get; set; }
        public string VehicleColor { get; set; }
        public string UserId { get; set; }
        public string DrivingLicNo { get; set; }

    }

    public class Challenged_violation_det
    {
        public string TicketNo { get; set; }
        public string OffenceId { get; set; }

        public string ChallengeByEmail { get; set; }
        public string ChallengeByMobNo { get; set; }

        public string ChallengeReason { get; set; }

    }

  

   

   


    /************ Tolling *****************/

    public class tolling_det
    {
        public string UserId { get; set; }        
        public string picturename { get; set; }
        public string locationname { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string deviceIMEI { get; set; }
        public DateTime tolling_datetime { get; set; }
    }

    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };
        }
        TVMS_dataDataContext db = new TVMS_dataDataContext();
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

     //   [Authorize]
        [System.Web.Http.HttpGet]
        public JObject getLogin(string u, string p, string deviceIMEI)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            SqlCommand cmd1 = new SqlCommand("select * FROM vw_User_n_FTP_det where LoginId=@u and password=@p", cn1);

            cmd1.Parameters.AddWithValue("@u", u);
            cmd1.Parameters.AddWithValue("@p", p);

            cmd1.CommandType = CommandType.Text;
            //cmd1.ExecuteNonQuery();

            DataTable table = new DataTable();

            {
               // using (SqlCommand cmd = new SqlCommand("select * FROM vw_User_n_FTP_det where LoginId='" + u + "' and password='" + p + "'", cn1))

                using (SqlCommand cmd = new SqlCommand("select * FROM vw_User_n_FTP_det where LoginId='" + u + "'", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }


            string password = "";
            if (table.Rows.Count > 0)
                password = AgentProfileController.DecryptString(table.Rows[0]["Password"].ToString());

            if (table.Rows.Count > 0 && p == password)         
            {
                /****************************************/

                SqlCommand cmd = new SqlCommand("ADM_INS_Session_App", cn1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(table.Rows[0]["userId"].ToString()));
                cmd.Parameters.AddWithValue("@CurrentDevice", deviceIMEI);

                cn1.Open();
                cmd.ExecuteNonQuery();
                cn1.Close();
                /***************************************/

               // if (table.Rows[0]["IsLogin"].ToString() == "1" && table.Rows[0]["CurrentDevice"].ToString() != deviceIMEI)
                if (table.Rows[0]["CurrentDevice"].ToString() != deviceIMEI)
                    return JObject.Parse(@"{""LoginDetails"" :{status:""3""}}");

                login_det login = new login_det
                {
                    status = "1",
                    userId = table.Rows[0]["UserId"].ToString(),
                    fname = table.Rows[0]["FirstName"].ToString(),
                    lname = table.Rows[0]["LastName"].ToString(),
                    Phone = table.Rows[0]["PhoneNumber"].ToString(),
                    ftpURL = table.Rows[0]["FtpURL"].ToString(),
                    ftpPORT = table.Rows[0]["FtpPort"].ToString(),
                    ftpUser = table.Rows[0]["FtpUsername"].ToString(),
                    ftpPassword = table.Rows[0]["FtpPassword"].ToString(),
                    UserTypeCode = table.Rows[0]["UserTypeCode"].ToString(),
                    WebApp = table.Rows[0]["WebApp"].ToString(),
                    firstTimeLogin = table.Rows[0]["FirstTimeLogin"].ToString(),
                    emailId = table.Rows[0]["EmailId"].ToString()

                };
                // StringBuilder x= new StringBuilder();
                string x = Convert.ToString(JObject.Parse(@"{""LoginDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                return JObject.Parse(@"{""LoginDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(login) + "}");


            }
            else
            {
                return JObject.Parse(@"{""LoginDetails"" :{status:""0""}}");
                //  return (@"{LoginDetails : {Status:0}}");
            }
        }


        [System.Web.Http.HttpPost]
        public JObject forgotChangePassword(string u, string np, string Agent_client)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();

            {
                string qry = "";
                if (Agent_client == "A")
                {

                    qry = "select * FROM vw_User_n_FTP_det where LoginId=@u";
                }
                else
                {
                    qry = "select * FROM tblClientUserMaster where LoginId=@u";
                    np = AgentProfileController.EncryptString(np);
                }

                SqlCommand cmd = new SqlCommand(qry, cn1);
                 cmd.Parameters.AddWithValue("@u", u);
               

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

          

            string a = "";
            string main_ret = "";
            if (table.Rows.Count > 0)
            {
                SqlCommand cmd = new SqlCommand("ADM_forgot_Change_Password", cn1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginId", u);
                cmd.Parameters.AddWithValue("@NewPassword", np);
                cmd.Parameters.AddWithValue("@Agent_client", Agent_client);


                SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);


                //  cmd.Parameters["@SuccessID"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add(new SqlParameter("@SuccessID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);
                cn1.Open();
                cmd.ExecuteNonQuery();

                a = output.Value.ToString();
                cn1.Close();
                if (a == "1")
                    main_ret = main_ret + "{\"Result\":\"Success\"}";


                else
                    main_ret = main_ret + "{\"Result\":\"Failed\"}";

                // return JObject.Parse(main_ret);

            }
            else
            {
                main_ret = main_ret + "{\"Result\":\"Wrong LoginId or Password\"}";
            }



            return JObject.Parse(main_ret);


        }


        [System.Web.Http.HttpPost]
        public JObject ChangePassword(string u, string op, string np,string Agent_client)
        {
            u = u.Replace("'", "''");
            op = op.Replace("'", "''");
            np = np.Replace("'", "''");
            Agent_client = Agent_client.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();


            {
                string qry = "";
                if (Agent_client == "A")
                {

                   // qry = "select * FROM vw_User_n_FTP_det where LoginId='" + u + "' and password='" + op + "'";
                    qry = "select * FROM vw_User_n_FTP_det where LoginId='" + u + "'";
                }
                else
                {
                   // qry = "select * FROM tblClientUserMaster where LoginId='" + u + "' and password='" + op + "'";
                    qry = "select * FROM tblClientUserMaster where LoginId='" + u + "'";
                }

                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            string old_password="";

            //if (Agent_client != "A")
            //    old_password = "";
            //else
                old_password = op;

            if (table.Rows.Count > 0)
            {
                old_password = AgentProfileController.DecryptString(table.Rows[0]["Password"].ToString());
                np = AgentProfileController.EncryptString(np);
            }
            string a = "";
            string main_ret = "";
            if (table.Rows.Count > 0 && op==old_password)
            {
                SqlCommand cmd = new SqlCommand("ADM_Change_Password", cn1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoginId", u);
                cmd.Parameters.AddWithValue("@OldPassword", op);
                cmd.Parameters.AddWithValue("@NewPassword", np);
                cmd.Parameters.AddWithValue("@Agent_client", Agent_client);


                SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);


                //  cmd.Parameters["@SuccessID"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add(new SqlParameter("@SuccessID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);
                cn1.Open();
                cmd.ExecuteNonQuery();

                a = output.Value.ToString();
                cn1.Close();
                if (a == "1")
                    main_ret = main_ret + "{\"Result\":\"Success\"}";


                else
                    main_ret = main_ret + "{\"Result\":\"Failed\"}";

                // return JObject.Parse(main_ret);

            }
            else
            {
                main_ret = main_ret + "{\"Result\":\"Wrong LoginId or Password\"}";
            }

           

            return JObject.Parse(main_ret);
        }


        [System.Web.Http.HttpPost]
        public JObject ForgotPassword(string username, string Reg_FP)
        {
            username = username.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            string qry = "";

            {
                qry = "select * FROM tblClientUserMaster where LoginId='" + username + "'";
            }

            using (SqlCommand cmd = new SqlCommand(qry, cn1))


            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(table);
            }
            string a = "";
            string main_ret = "";
            if (table.Rows.Count > 0 && Reg_FP == "F")
            {
                string ph_no = "";
                ph_no = table.Rows[0]["PhoneNumber"].ToString();
                int otp = GenerateRandomNo();
                WebClient client = new WebClient();

                //  string URI1 = "https://zm4j6.api.infobip.com/sms/1/text/query?username=Brentwood&password=Bmg_123@&to=+234" + ph_no + "&text=" + otp;
                //var res = client.UploadString(URI1, "");

                //if (SMS_CALL == "C")
                //{
                //    OTP_OnCall(ph_no, Convert.ToString(otp));
                //}
                //else
                {
                    ArteziaController art_c = new ArteziaController();
                    art_c.VansoArtizia_SMS(ph_no, Convert.ToString(otp), "N", "Forget Password", "Login", "Artezia", ph_no);
                }

                cn1.Close();
                // if (a == "1")
                main_ret = main_ret + "{\"Result\":\"" + otp + "\"}";


                //  else
                //    main_ret = main_ret + "{\"Result\":\"Failed\"}";






                return JObject.Parse(main_ret);
            }

            else if (Reg_FP == "R")
            {
                string ph_no = "";
                ph_no = username;
                int otp = GenerateRandomNo();

                // otp = 1211;   // hardcode testing purpose

                WebClient client = new WebClient();
                // client.BaseAddress = "github.bmg.ng:2121";


                //  string URI1 = "https://zm4j6.api.infobip.com/sms/1/text/query?username=Brentwood&password=Bmg_123@&to=+234" + ph_no.Substring(ph_no.Length - 10) + "&text=" + otp;
                //  var res = client.UploadString(URI1, "");

                //if (SMS_CALL == "C")
                //{
                //    OTP_OnCall(ph_no, Convert.ToString(otp));
                //}
                //else
                {
                    ArteziaController art_c = new ArteziaController();
                    art_c.VansoArtizia_SMS(ph_no, Convert.ToString(otp), "N", "New Registration", "Login", "Artezia", ph_no);
                }

                cn1.Close();
                // if (a == "1")
                main_ret = main_ret + "{\"Result\":\"" + otp + "\"}";

                return JObject.Parse(main_ret);
            }

            else
            {
                main_ret = main_ret + "{\"Result\":\"-1\"}";
                return JObject.Parse(main_ret);
            }
        }


        [System.Web.Http.HttpPost]
        public JObject ForgotPassword(string username, string Reg_FP, string SMS_CALL)
        {
            username = username.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            string qry = "";

            {
                qry = "select * FROM tblClientUserMaster where LoginId='" + username + "'";
            }

            using (SqlCommand cmd = new SqlCommand(qry, cn1))


            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(table);
            }
            string a = "";
            string main_ret = "";
            if (table.Rows.Count > 0 && Reg_FP=="F")
            {
                string ph_no = "";
                ph_no = table.Rows[0]["PhoneNumber"].ToString();
                int otp = GenerateRandomNo();
                WebClient client = new WebClient();

              //  string URI1 = "https://zm4j6.api.infobip.com/sms/1/text/query?username=Brentwood&password=Bmg_123@&to=+234" + ph_no + "&text=" + otp;
                //var res = client.UploadString(URI1, "");

                if (SMS_CALL == "C")
                {
                    OTP_OnCall(ph_no, Convert.ToString(otp));
                }
                else
                {
                    ArteziaController art_c = new ArteziaController();
                    art_c.VansoArtizia_SMS(ph_no, Convert.ToString(otp), "N", "Forget Password", "Login", "Artezia", ph_no);
                }

                cn1.Close();
                // if (a == "1")
                main_ret = main_ret + "{\"Result\":\"" + otp + "\"}";


             

                return JObject.Parse(main_ret);
            }

            else if (Reg_FP == "R")
            {
                string ph_no = "";
                ph_no = username;
                int otp = GenerateRandomNo();

               // otp = 1211;   // hardcode testing purpose

                WebClient client = new WebClient();
               // client.BaseAddress = "github.bmg.ng:2121";


              //  string URI1 = "https://zm4j6.api.infobip.com/sms/1/text/query?username=Brentwood&password=Bmg_123@&to=+234" + ph_no.Substring(ph_no.Length - 10) + "&text=" + otp;
              //  var res = client.UploadString(URI1, "");

                if (SMS_CALL == "C")
                {
                    OTP_OnCall(ph_no, Convert.ToString(otp));
                }
                else
                {
                    ArteziaController art_c = new ArteziaController();
                    art_c.VansoArtizia_SMS(ph_no, Convert.ToString(otp), "N", "New Registration", "Login", "Artezia", ph_no);
                }

                cn1.Close();
                // if (a == "1")
                main_ret = main_ret + "{\"Result\":\"" + otp + "\"}";

                return JObject.Parse(main_ret);
            }

            else
            {
                main_ret = main_ret + "{\"Result\":\"-1\"}";
                return JObject.Parse(main_ret);
            }
        }


        [System.Web.Http.HttpPost]
        public JObject ForgotPassword(string username, string Reg_FP, string SMS_CALL, int old_otp)
        {
            username = username.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            string qry = "";

            {
                qry = "select * FROM tblClientUserMaster where LoginId='" + username + "'";
            }

            using (SqlCommand cmd = new SqlCommand(qry, cn1))


            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(table);
            }
            string a = "";
            string main_ret = "";
            if (table.Rows.Count > 0 && Reg_FP == "F")
            {
                string ph_no = "";
                ph_no = table.Rows[0]["PhoneNumber"].ToString();
                int otp = GenerateRandomNo();
                WebClient client = new WebClient();

                //  string URI1 = "https://zm4j6.api.infobip.com/sms/1/text/query?username=Brentwood&password=Bmg_123@&to=+234" + ph_no + "&text=" + otp;
                //var res = client.UploadString(URI1, "");
                if (old_otp != 0)
                    otp = old_otp;

                if (SMS_CALL == "C")
                {
                    OTP_OnCall(ph_no, Convert.ToString(otp));
                }
                else
                {
                    ArteziaController art_c = new ArteziaController();
                    art_c.VansoArtizia_SMS(ph_no, Convert.ToString(otp), "N", "Forget Password", "Login", "Artezia", ph_no);
                }

                cn1.Close();
                // if (a == "1")
                main_ret = main_ret + "{\"Result\":\"" + otp + "\"}";




                return JObject.Parse(main_ret);
            }

            else if (Reg_FP == "R")
            {
                string ph_no = "";
                ph_no = username;
                int otp = GenerateRandomNo();

                // otp = 1211;   // hardcode testing purpose

                if (old_otp != 0)
                    otp = old_otp;

                WebClient client = new WebClient();
                // client.BaseAddress = "github.bmg.ng:2121";


                //  string URI1 = "https://zm4j6.api.infobip.com/sms/1/text/query?username=Brentwood&password=Bmg_123@&to=+234" + ph_no.Substring(ph_no.Length - 10) + "&text=" + otp;
                //  var res = client.UploadString(URI1, "");

                if (SMS_CALL == "C")
                {
                    OTP_OnCall(ph_no, Convert.ToString(otp));
                }
                else
                {
                    ArteziaController art_c = new ArteziaController();
                    art_c.VansoArtizia_SMS(ph_no, Convert.ToString(otp), "N", "New Registration", "Login", "Artezia", ph_no);
                }

                cn1.Close();
                // if (a == "1")
                main_ret = main_ret + "{\"Result\":\"" + otp + "\"}";

                return JObject.Parse(main_ret);
            }

            else
            {
                main_ret = main_ret + "{\"Result\":\"-1\"}";
                return JObject.Parse(main_ret);
            }
        }

        //Generate RandomNo
        public int GenerateRandomNo()
        {
            int _min = 000000;
            int _max = 999999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }



        [System.Web.Http.HttpGet]
        public JObject getViolationSearch_web(string Veh_No, string userId)
        {
            Veh_No = Veh_No.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            SqlCommand cmd1 = new SqlCommand("select * FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "'", cn1);
            cmd1.CommandType = CommandType.Text;
            //cmd1.ExecuteNonQuery();

            DataTable table = new DataTable();


            {
                string qry = "";

                // qry = "select * FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "' and isnull(IsNotified,0)!=1 and isnull(IsEnforced,0)!=1";
                qry = "select * FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "' and isnull(IsProcessed,0)=1 and isnull(IsEnforced,0)!=1";


               // qry = "select LoginId,TicketNo,VehicleNumber,SUM(Penalty)as Penalty  FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "' and isnull(IsProcessed,0)=1 and isnull(IsEnforced,0)!=1 group by TicketNo,VehicleNumber,LoginId order by TicketNo";
                qry = "SELECT LoginId,TicketNo,VehicleNumber, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty FROM vw_ClientSearch AS t GROUP BY t.TicketNo,LoginId,VehicleNumber";

                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
                Notified_violation_det n = new Notified_violation_det();
                n.vehicleNumber = Veh_No;
                n.NotifiedBy = userId;
                n.NotifiedImage = "";
                n.NotifiedOn = DateTime.Now.Date;
                update_Notified_voilation_details(n);
            }


            DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[11] {new DataColumn("officerLoginId", typeof(string)),new DataColumn("officerFname", typeof(string)), new DataColumn("officerLname", typeof(string)), new DataColumn("vehicleNumber", typeof(string)), new DataColumn("TicketNo", typeof(string)),  new DataColumn("OffenceDetails", typeof(string)),  new DataColumn("Penalty", typeof(string)),                        
            //                new DataColumn("deviceIMEI",typeof(string)), new DataColumn("vdo_datetime",typeof(string)), new DataColumn("daysLeft",typeof(int)), new DataColumn("IsNotified",typeof(int))});

            dt.Columns.AddRange(new DataColumn[5] {new DataColumn("officerLoginId", typeof(string)), new DataColumn("vehicleNumber", typeof(string)), new DataColumn("TicketNo", typeof(string)), new DataColumn("OffenceDetails", typeof(string)), new DataColumn("Penalty", typeof(string))});

            if (table.Rows.Count > 0)
            {
                search_violation SearchViolationDetails = new search_violation
                {
                    status = "1",
                    //officerId = table.Rows[0]["UserId"].ToString(),
                    //officerFname = table.Rows[0]["FirstName"].ToString(),
                    //officerLname = table.Rows[0]["LastName"].ToString(),
                    officerLoginId = table.Rows[0]["LoginId"].ToString(),

                    vehicleNumber = table.Rows[0]["VehicleNumber"].ToString(),
                    OffenceDetails = table.Rows[0]["OffenceName"].ToString(),
                    Penalty = table.Rows[0]["Penalty"].ToString(),
                    TicketNo = table.Rows[0]["TicketNo"].ToString()

                    //videoFilename = table.Rows[0]["VideoFileName"].ToString(),
                    //picturesFilenames = table.Rows[0]["PicFileName"].ToString(),
                    //locationname = table.Rows[0]["LocationName"].ToString(),
                    //latitude = table.Rows[0]["Longitude"].ToString(),
                    //longitude = table.Rows[0]["Latitude"].ToString(),
                    //vdo_datetime = table.Rows[0]["videodatetime"].ToString(),
                    //deviceIMEI = table.Rows[0]["DeviceIMEI"].ToString(),
                    //IsNotified = table.Rows[0]["IsNotified"].ToString()

                };

                for (int i = 0; i < table.Rows.Count; i++)
                {
                   // dt.Rows.Add(table.Rows[i]["LoginId"].ToString(), table.Rows[i]["FirstName"].ToString(), table.Rows[i]["LastName"].ToString(), table.Rows[i]["VehicleNumber"].ToString(), table.Rows[i]["TicketNo"].ToString(), table.Rows[i]["OffenceName"].ToString(), table.Rows[i]["Penalty"].ToString(), table.Rows[i]["DeviceIMEI"].ToString(), table.Rows[i]["videodatetime"].ToString(), table.Rows[i]["DaysLeft"].ToString(), table.Rows[i]["IsNotified"].ToString());

                    dt.Rows.Add(table.Rows[i]["LoginId"].ToString(), table.Rows[i]["VehicleNumber"].ToString(), table.Rows[i]["TicketNo"].ToString(),table.Rows[i]["OffenceName"].ToString(), table.Rows[i]["Penalty"].ToString());
                }
                // StringBuilder x= new StringBuilder();
                string x = Convert.ToString(JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                // return JObject.Parse(@"{""SearchDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(SearchViolationDetails) + "}");
                return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");

            }
            else
            {
                // return JObject.Parse(@"{""SearchDetails"" :{status:""0""}}");

                return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }



        [System.Web.Http.HttpGet]
        public JObject getViolationSearch(string Veh_No, string userId)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            SqlCommand cmd1 = new SqlCommand("select * FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "'", cn1);
            cmd1.CommandType = CommandType.Text;
            //cmd1.ExecuteNonQuery();

            DataTable table = new DataTable();


            {
                string qry = "";

               // qry = "select * FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "' and isnull(IsNotified,0)!=1 and isnull(IsEnforced,0)!=1";
                qry = "select * FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "' and isnull(IsProcessed,0)=1 and isnull(IsEnforced,0)!=1";
               

                //select STRING_AGG(OffenceName, ', ')  FROM vw_SearchViolations where VehicleNumber='DL998899' and isnull(IsProcessed,0)=1 and isnull(IsEnforced,0)!=1 group by TicketNo


                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
                Notified_violation_det n = new Notified_violation_det();
                n.vehicleNumber = Veh_No;
                n.NotifiedBy = userId;
                n.NotifiedImage = "";
                n.NotifiedOn = DateTime.Now.Date;
                update_Notified_voilation_details(n);
            }


            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[11] {new DataColumn("officerLoginId", typeof(string)),new DataColumn("officerFname", typeof(string)), new DataColumn("officerLname", typeof(string)), new DataColumn("vehicleNumber", typeof(string)), new DataColumn("TicketNo", typeof(string)),  new DataColumn("OffenceDetails", typeof(string)),  new DataColumn("Penalty", typeof(string)),                        
                            new DataColumn("deviceIMEI",typeof(string)), new DataColumn("vdo_datetime",typeof(string)), new DataColumn("daysLeft",typeof(int)), new DataColumn("IsNotified",typeof(int))});


            if (table.Rows.Count > 0)
            {
                search_violation SearchViolationDetails = new search_violation
                {
                    status = "1",
                    officerId = table.Rows[0]["UserId"].ToString(),
                    officerFname = table.Rows[0]["FirstName"].ToString(),
                    officerLname = table.Rows[0]["LastName"].ToString(),
                    officerLoginId = table.Rows[0]["LoginId"].ToString(),

                    vehicleNumber = table.Rows[0]["VehicleNumber"].ToString(),
                    OffenceDetails = table.Rows[0]["OffenceName"].ToString(),
                    Penalty = table.Rows[0]["Penalty"].ToString(),
                    TicketNo = table.Rows[0]["TicketNo"].ToString(),

                    videoFilename = table.Rows[0]["VideoFileName"].ToString(),
                    picturesFilenames = table.Rows[0]["PicFileName"].ToString(),
                    locationname = table.Rows[0]["LocationName"].ToString(),
                    latitude = table.Rows[0]["Longitude"].ToString(),
                    longitude = table.Rows[0]["Latitude"].ToString(),
                    vdo_datetime = table.Rows[0]["videodatetime"].ToString(),
                    deviceIMEI = table.Rows[0]["DeviceIMEI"].ToString(),
                    IsNotified = table.Rows[0]["IsNotified"].ToString()

                };

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dt.Rows.Add(table.Rows[i]["LoginId"].ToString(), table.Rows[i]["FirstName"].ToString(), table.Rows[i]["LastName"].ToString(), table.Rows[i]["VehicleNumber"].ToString(), table.Rows[i]["TicketNo"].ToString(), table.Rows[i]["OffenceName"].ToString(), table.Rows[i]["Penalty"].ToString(), table.Rows[i]["DeviceIMEI"].ToString(), table.Rows[i]["videodatetime"].ToString(), table.Rows[i]["DaysLeft"].ToString(), table.Rows[i]["IsNotified"].ToString());
                }
                // StringBuilder x= new StringBuilder();
                string x = Convert.ToString(JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

               // return JObject.Parse(@"{""SearchDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(SearchViolationDetails) + "}");
                return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");

            }
            else
            {
               // return JObject.Parse(@"{""SearchDetails"" :{status:""0""}}");

                return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");
               
            }
        }


        [System.Web.Http.HttpPost]
        public JObject insert_voilation_details([FromBody] violation_det violationdet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_VoilationDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@UserId", violationdet.UserId);
                cmd.Parameters.AddWithValue("@deviceIMEI", violationdet.deviceIMEI);
                cmd.Parameters.AddWithValue("@videoFilename", violationdet.videoFilename);
                cmd.Parameters.AddWithValue("@picturesFilenames", violationdet.picturesFilenames);
                cmd.Parameters.AddWithValue("@latitude", violationdet.latitude);
                cmd.Parameters.AddWithValue("@longitude", violationdet.longitude);
                cmd.Parameters.AddWithValue("@locationname", violationdet.locationname);
                cmd.Parameters.AddWithValue("@vdo_datetime", violationdet.vdo_datetime);
                cmd.Parameters.AddWithValue("@OffenceId", violationdet.OffenceId);
                

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
                {
                    main_ret = main_ret + "{\"Result\":\"Success\"}";

                }

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

        [System.Web.Http.HttpPost]
        public JObject insert_handshake_details([FromBody] handover_det handshake_det)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_HandshakeDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", handshake_det.UserId);
                cmd.Parameters.AddWithValue("@deviceIMEI", handshake_det.deviceIMEI);
                cmd.Parameters.AddWithValue("@deviceResponse", handshake_det.DeviceResponse);
                cmd.Parameters.AddWithValue("@deviceAction", handshake_det.DeviceAction);
                cmd.Parameters.AddWithValue("@ActionDateTime", handshake_det.ActionDateTime);



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


        [System.Web.Http.HttpPost]
        public JObject update_Notified_voilation_details([FromBody] Notified_violation_det Notified_violationdet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_Upd_Notified_VoilationDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleNumber", Notified_violationdet.vehicleNumber);
                cmd.Parameters.AddWithValue("@NotifiedOn", Notified_violationdet.NotifiedOn);
                cmd.Parameters.AddWithValue("@NotifiedBy", Notified_violationdet.NotifiedBy);
                cmd.Parameters.AddWithValue("@NotifiedImage", Notified_violationdet.NotifiedImage);
                


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


        [System.Web.Http.HttpPost]
        public JObject update_Notified_Enforced_voilation_details([FromBody] Enforced_Notified_violation_det Enforced_Notified_violationdet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_Upd_Notified_Enforced_VoilationDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleNumber", Enforced_Notified_violationdet.vehicleNumber);
                cmd.Parameters.AddWithValue("@NotifiedOn", Enforced_Notified_violationdet.NotifiedOn);
                cmd.Parameters.AddWithValue("@NotifiedBy", Enforced_Notified_violationdet.NotifiedBy);
                cmd.Parameters.AddWithValue("@NotifiedImage", Enforced_Notified_violationdet.NotifiedImage);
                cmd.Parameters.AddWithValue("@PrintedTicketNo", Enforced_Notified_violationdet.PrintedTicketNo);  // PrintedTicketNo else EnforcementTicketNo
                cmd.Parameters.AddWithValue("@EnforcementLocation", Enforced_Notified_violationdet.EnforcementLocation);
                cmd.Parameters.AddWithValue("@EnforcementType", Enforced_Notified_violationdet.EnforcementType);
                cmd.Parameters.AddWithValue("@PaymentRefNo", Enforced_Notified_violationdet.PaymentRefNo);
                cmd.Parameters.AddWithValue("@flag", Enforced_Notified_violationdet.flag);



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
        public JObject getSearch_Client_web(string VehNo_tikNo, string chesis_No, string flag, string u)
        {
            try
            {
                VehNo_tikNo = VehNo_tikNo.Replace("'", "''");
                chesis_No = chesis_No.Replace("'", "''");

                SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
                SqlCommand cmd1 = new SqlCommand("select * FROM vw_ClientSearch where VehicleNumber='" + VehNo_tikNo + "'", cn1);
                cmd1.CommandType = CommandType.Text;
                //cmd1.ExecuteNonQuery();

                DataTable table = new DataTable();

                {
                    string qry = "";
                    string qryCondition = "";


                    if (VehNo_tikNo == "b" && chesis_No == "b")
                    {
                        qryCondition = "UserId='" + u + "'";
                    }
                    else if (chesis_No == "b" && u == "b")
                    {
                        qryCondition = "(VehicleNumber='" + VehNo_tikNo + "' or TicketNo='" + VehNo_tikNo + "')";
                    }
                    else if (chesis_No == "b")
                    {
                        qryCondition = "(VehicleNumber='" + VehNo_tikNo + "' or TicketNo='" + VehNo_tikNo + "') and UserId='" + u + "'";
                    }
                    else if (u == "b")
                    {
                        qryCondition = "((VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "') or TicketNo='" + VehNo_tikNo + "')";
                    }
                    else
                    {
                        qryCondition = "VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "' and UserId='" + u + "'";
                    }
                    // qry = "select * FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "' and isnull(IsNotified,0)!=1 and isnull(IsEnforced,0)!=1";
                    if (flag == "Pd")
                        qry = "SELECT LoginId,TicketNo,VehicleNumber, LocationName, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,CONVERT(varchar, VideoDateTime, 103)+' '+ convert(char(8), VideoDateTime, 108) as VdoDate,PrintedTicketNo FROM vw_ClientSearch AS t where " + qryCondition + "  and PenaltyPaid=1 GROUP BY t.TicketNo,LoginId,VehicleNumber,LocationName,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,PrintedTicketNo,OffenceName  ";
                    else if (flag == "Pr")
                        qry = "SELECT LoginId,TicketNo,VehicleNumber,LocationName, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,CONVERT(varchar, VideoDateTime, 103)+' '+ convert(char(8), VideoDateTime, 108) as VdoDate,PrintedTicketNo FROM vw_ClientSearch AS t where " + qryCondition + "  and IsProcessed=1 and isnull(PenaltyPaid,0)<>1 and isnull(IsChallenged,0)<>1 GROUP BY t.TicketNo,LoginId,VehicleNumber,LocationName,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,PrintedTicketNo";
                    else if (flag == "Ch")
                        qry = "SELECT LoginId,TicketNo,VehicleNumber,LocationName, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,CONVERT(varchar, VideoDateTime, 103)+' '+ convert(char(8), VideoDateTime, 108) as VdoDate,PrintedTicketNo FROM vw_ClientSearch AS t where " + qryCondition + " and IsChallenged=1 GROUP BY t.TicketNo,LoginId,VehicleNumber,LocationName,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,PrintedTicketNo";
                    else if (flag == "b")
                        qry = "SELECT LoginId,TicketNo,VehicleNumber,LocationName, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,CONVERT(varchar, VideoDateTime, 103)+' '+ convert(char(8), VideoDateTime, 108) as VdoDate,PrintedTicketNo FROM vw_ClientSearch AS t where " + qryCondition + " GROUP BY t.TicketNo,LoginId,VehicleNumber,LocationName,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,PrintedTicketNo";
                    else
                        qry = "SELECT LoginId,TicketNo,VehicleNumber,LocationName, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,CONVERT(varchar, VideoDateTime, 103)+' '+ convert(char(8), VideoDateTime, 108) as VdoDate,PrintedTicketNo FROM vw_ClientSearch AS t where (VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "') or TicketNo='" + VehNo_tikNo + "' GROUP BY t.TicketNo,LoginId,VehicleNumber,LocationName,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,PrintedTicketNo";


                    using (SqlCommand cmd = new SqlCommand(qry, cn1))


                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }


                }


                DataTable dt = new DataTable();
                //dt.Columns.AddRange(new DataColumn[19] {new DataColumn("officerLoginId", typeof(string)),new DataColumn("officerFname", typeof(string)), new DataColumn("officerLname", typeof(string)), new DataColumn("vehicleNumber", typeof(string)), new DataColumn("TicketNo", typeof(string)),  new DataColumn("OffenceDetails", typeof(string)),  new DataColumn("Penalty", typeof(string)),                        
                //                new DataColumn("deviceIMEI",typeof(string)), new DataColumn("vdo_datetime",typeof(string)), new DataColumn("daysLeft",typeof(int)), new DataColumn("IsNotified",typeof(int)), new DataColumn("IsPaid",typeof(int)), new DataColumn("PenaltyPaidOn",typeof(string)), new DataColumn("PeneltyTxnNo",typeof(string)),new DataColumn("OffenceId",typeof(string)),new DataColumn("VideoFileName",typeof(string)),new DataColumn("PicFileName",typeof(string)),new DataColumn("link",typeof(string)),new DataColumn("LocationName",typeof(string))});


                dt.Columns.AddRange(new DataColumn[12] { new DataColumn("officerLoginId", typeof(string)), new DataColumn("vehicleNumber", typeof(string)), new DataColumn("TicketNo", typeof(string)), new DataColumn("OffenceDetails", typeof(string)), new DataColumn("Penalty", typeof(string)), new DataColumn("LocationName", typeof(string)), new DataColumn("VideoFileName", typeof(string)), new DataColumn("PicFileName", typeof(string)), new DataColumn("Diff_Flag", typeof(string)), new DataColumn("chassis_no", typeof(string)), new DataColumn("vdo_datetime", typeof(string)), new DataColumn("PrintedTicketNo", typeof(string)) });

                if (table.Rows.Count == 0)
                {
                    table = getSearch_Client_web_Artizia(VehNo_tikNo, chesis_No, flag, u);
                }

                if (table.Rows.Count > 0)
                {
                    getSearchClient SearchViolationDetails = new getSearchClient
                    {
                        status = "1",
                        // officerId = table.Rows[0]["UserId"].ToString(),
                        // officerFname = table.Rows[0]["FirstName"].ToString(),
                        // officerLname = table.Rows[0]["LastName"].ToString(),
                        officerLoginId = table.Rows[0]["LoginId"].ToString(),

                        vehicleNumber = table.Rows[0]["VehicleNumber"].ToString(),
                        OffenceDetails = table.Rows[0]["OffenceName"].ToString(),
                        Penalty = table.Rows[0]["Penalty"].ToString(),
                        TicketNo = table.Rows[0]["TicketNo"].ToString(),

                        videoFilename = table.Rows[0]["VideoFileName"].ToString(),
                        picturesFilenames = table.Rows[0]["PicFileName"].ToString(),
                        locationname = table.Rows[0]["LocationName"].ToString(),
                        diff_flag = table.Rows[0]["diffflag"].ToString()
                        //latitude = table.Rows[0]["Longitude"].ToString(),
                        //longitude = table.Rows[0]["Latitude"].ToString(),
                        //vdo_datetime = table.Rows[0]["videodatetime"].ToString(),
                        //deviceIMEI = table.Rows[0]["DeviceIMEI"].ToString(),
                        //IsNotified = table.Rows[0]["IsNotified"].ToString(),
                        //IsPaid = table.Rows[0]["PenaltyPaid"].ToString(),
                        //PenaltyPaidOn = table.Rows[0]["PenaltyPaidOn"].ToString(),
                        //PenaltyTxnNo = table.Rows[0]["PenaltyTxnNo"].ToString()

                    };

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        //  dt.Rows.Add(table.Rows[i]["LoginId"].ToString(), table.Rows[i]["FirstName"].ToString(), table.Rows[i]["LastName"].ToString(), table.Rows[i]["VehicleNumber"].ToString(), table.Rows[i]["TicketNo"].ToString(), table.Rows[i]["OffenceName"].ToString(), table.Rows[i]["Penalty"].ToString(), table.Rows[i]["DeviceIMEI"].ToString(), table.Rows[i]["videodatetime"].ToString(), table.Rows[i]["DaysLeft"].ToString(), string.IsNullOrEmpty(table.Rows[i]["IsNotified"].ToString()) ? "0" : table.Rows[i]["IsNotified"].ToString(), string.IsNullOrEmpty(table.Rows[i]["PenaltyPaid"].ToString()) ? "0" : table.Rows[i]["PenaltyPaid"].ToString(), string.IsNullOrEmpty(table.Rows[i]["PenaltyPaidOn"].ToString()) ? "" : table.Rows[i]["PenaltyPaidOn"].ToString(), table.Rows[i]["PenaltyTxnNo"].ToString(), table.Rows[i]["OffenceId"].ToString(), table.Rows[i]["VideoFileName"].ToString(), table.Rows[i]["PicFileName"].ToString(), table.Rows[i]["link"].ToString(), table.Rows[i]["LocationName"].ToString());

                        dt.Rows.Add(table.Rows[i]["LoginId"].ToString(), table.Rows[i]["VehicleNumber"].ToString(), table.Rows[i]["TicketNo"].ToString(), table.Rows[i]["OffenceName"].ToString(), table.Rows[i]["Penalty"].ToString(), table.Rows[i]["LocationName"].ToString(), table.Rows[i]["VideoFileName"].ToString(), table.Rows[i]["PicFileName"].ToString(), table.Rows[i]["diffflag"].ToString(), table.Rows[i]["ChesisNo"].ToString(), table.Rows[i]["VdoDate"].ToString(), table.Rows[i]["PrintedTicketNo"].ToString());
                    }
                    // StringBuilder x= new StringBuilder();
                    string x = Convert.ToString(JObject.Parse(@"{""LoginDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                    // return JObject.Parse(@"{""SearchDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(SearchViolationDetails) + "}");
                    return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");

                }
                else
                {
                    // return JObject.Parse(@"{""SearchDetails"" :{status:""0""}}");

                    return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");

                }
            }
            catch (Exception ex)
            {
                return JObject.Parse(@"{""SearchDetails"" :[]}");
            }
        }



        //[System.Web.Http.HttpGet]
        public DataTable getSearch_Client_web_Artizia(string VehNo_tikNo, string chesis_No, string flag, string u)
        {
            VehNo_tikNo = VehNo_tikNo.Replace("'", "''");
            chesis_No = chesis_No.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            SqlCommand cmd1 = new SqlCommand("select * FROM vw_ClientSearch_Artizia where VehicleNumber='" + VehNo_tikNo + "'", cn1);
            cmd1.CommandType = CommandType.Text;
            //cmd1.ExecuteNonQuery();

            DataTable table = new DataTable();

            {
                string qry = "";
                string qryCondition = "";


                if (VehNo_tikNo == "b" && chesis_No == "b")
                {
                    qryCondition = "UserId='" + u + "'";
                }
                else if (chesis_No == "b" && u == "b")
                {
                    qryCondition = "(VehicleNumber='" + VehNo_tikNo + "' or TicketNo='" + VehNo_tikNo + "')";
                }
                else if (chesis_No == "b")
                {
                    qryCondition = "(VehicleNumber='" + VehNo_tikNo + "' or TicketNo='" + VehNo_tikNo + "') and UserId='" + u + "'";
                }
                else if (u == "b")
                {
                    qryCondition = "((VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "') or TicketNo='" + VehNo_tikNo + "')";
                }
                else
                {
                    qryCondition = "VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "' and UserId='" + u + "'";
                }
                // qry = "select * FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "' and isnull(IsNotified,0)!=1 and isnull(IsEnforced,0)!=1";
                if (flag == "Pd")
                    qry = "SELECT LoginId,TicketNo,VehicleNumber, LocationName, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch_Artizia s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,CONVERT(varchar, VideoDateTime, 103)+' '+ convert(char(8), VideoDateTime, 108) as VdoDate,PrintedTicketNo FROM vw_ClientSearch_Artizia AS t where " + qryCondition + "  and PenaltyPaid=1 GROUP BY t.TicketNo,LoginId,VehicleNumber,LocationName,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,PrintedTicketNo,OffenceName  ";
                else if (flag == "Pr")
                    qry = "SELECT LoginId,TicketNo,VehicleNumber,LocationName, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch_Artizia s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,CONVERT(varchar, VideoDateTime, 103)+' '+ convert(char(8), VideoDateTime, 108) as VdoDate,PrintedTicketNo FROM vw_ClientSearch_Artizia AS t where " + qryCondition + "  and IsProcessed=1 and isnull(PenaltyPaid,0)<>1 and isnull(IsChallenged,0)<>1 GROUP BY t.TicketNo,LoginId,VehicleNumber,LocationName,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,PrintedTicketNo";
                else if (flag == "Ch")
                    qry = "SELECT LoginId,TicketNo,VehicleNumber,LocationName, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch_Artizia s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,CONVERT(varchar, VideoDateTime, 103)+' '+ convert(char(8), VideoDateTime, 108) as VdoDate,PrintedTicketNo FROM vw_ClientSearch_Artizia AS t where " + qryCondition + " and IsChallenged=1 GROUP BY t.TicketNo,LoginId,VehicleNumber,LocationName,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,PrintedTicketNo";
                else if (flag == "b")
                    qry = "SELECT LoginId,TicketNo,VehicleNumber,LocationName, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch_Artizia s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,CONVERT(varchar, VideoDateTime, 103)+' '+ convert(char(8), VideoDateTime, 108) as VdoDate,PrintedTicketNo FROM vw_ClientSearch_Artizia AS t where " + qryCondition + " GROUP BY t.TicketNo,LoginId,VehicleNumber,LocationName,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,PrintedTicketNo";
                else
                    qry = "SELECT LoginId,TicketNo,VehicleNumber,LocationName, STUFF((SELECT ',' + s.OffenceName FROM vw_ClientSearch_Artizia s WHERE s.TicketNo = t.TicketNo FOR XML PATH('')),1,1,'') AS OffenceName,SUM(Penalty)as Penalty,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,CONVERT(varchar, VideoDateTime, 103)+' '+ convert(char(8), VideoDateTime, 108) as VdoDate,PrintedTicketNo FROM vw_ClientSearch_Artizia AS t where (VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "') or TicketNo='" + VehNo_tikNo + "' GROUP BY t.TicketNo,LoginId,VehicleNumber,LocationName,PicFileName,VideofileName,diffflag,ChesisNo,VideoDateTime,PrintedTicketNo";


                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }


            }


            DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[19] {new DataColumn("officerLoginId", typeof(string)),new DataColumn("officerFname", typeof(string)), new DataColumn("officerLname", typeof(string)), new DataColumn("vehicleNumber", typeof(string)), new DataColumn("TicketNo", typeof(string)),  new DataColumn("OffenceDetails", typeof(string)),  new DataColumn("Penalty", typeof(string)),                        
            //                new DataColumn("deviceIMEI",typeof(string)), new DataColumn("vdo_datetime",typeof(string)), new DataColumn("daysLeft",typeof(int)), new DataColumn("IsNotified",typeof(int)), new DataColumn("IsPaid",typeof(int)), new DataColumn("PenaltyPaidOn",typeof(string)), new DataColumn("PeneltyTxnNo",typeof(string)),new DataColumn("OffenceId",typeof(string)),new DataColumn("VideoFileName",typeof(string)),new DataColumn("PicFileName",typeof(string)),new DataColumn("link",typeof(string)),new DataColumn("LocationName",typeof(string))});


            dt.Columns.AddRange(new DataColumn[12] { new DataColumn("officerLoginId", typeof(string)), new DataColumn("vehicleNumber", typeof(string)), new DataColumn("TicketNo", typeof(string)), new DataColumn("OffenceDetails", typeof(string)), new DataColumn("Penalty", typeof(string)), new DataColumn("LocationName", typeof(string)), new DataColumn("VideoFileName", typeof(string)), new DataColumn("PicFileName", typeof(string)), new DataColumn("Diff_Flag", typeof(string)), new DataColumn("chassis_no", typeof(string)), new DataColumn("vdo_datetime", typeof(string)), new DataColumn("PrintedTicketNo", typeof(string)) });


            if (table.Rows.Count > 0)
            {
                getSearchClient SearchViolationDetails = new getSearchClient
                {
                    status = "1",
                    // officerId = table.Rows[0]["UserId"].ToString(),
                    // officerFname = table.Rows[0]["FirstName"].ToString(),
                    // officerLname = table.Rows[0]["LastName"].ToString(),
                    officerLoginId = table.Rows[0]["LoginId"].ToString(),

                    vehicleNumber = table.Rows[0]["VehicleNumber"].ToString(),
                    OffenceDetails = table.Rows[0]["OffenceName"].ToString(),
                    Penalty = table.Rows[0]["Penalty"].ToString(),
                    TicketNo = table.Rows[0]["TicketNo"].ToString(),

                    videoFilename = table.Rows[0]["VideoFileName"].ToString(),
                    picturesFilenames = table.Rows[0]["PicFileName"].ToString(),
                    locationname = table.Rows[0]["LocationName"].ToString(),
                    diff_flag = table.Rows[0]["diffflag"].ToString()
                    //latitude = table.Rows[0]["Longitude"].ToString(),
                    //longitude = table.Rows[0]["Latitude"].ToString(),
                    //vdo_datetime = table.Rows[0]["videodatetime"].ToString(),
                    //deviceIMEI = table.Rows[0]["DeviceIMEI"].ToString(),
                    //IsNotified = table.Rows[0]["IsNotified"].ToString(),
                    //IsPaid = table.Rows[0]["PenaltyPaid"].ToString(),
                    //PenaltyPaidOn = table.Rows[0]["PenaltyPaidOn"].ToString(),
                    //PenaltyTxnNo = table.Rows[0]["PenaltyTxnNo"].ToString()

                };

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //  dt.Rows.Add(table.Rows[i]["LoginId"].ToString(), table.Rows[i]["FirstName"].ToString(), table.Rows[i]["LastName"].ToString(), table.Rows[i]["VehicleNumber"].ToString(), table.Rows[i]["TicketNo"].ToString(), table.Rows[i]["OffenceName"].ToString(), table.Rows[i]["Penalty"].ToString(), table.Rows[i]["DeviceIMEI"].ToString(), table.Rows[i]["videodatetime"].ToString(), table.Rows[i]["DaysLeft"].ToString(), string.IsNullOrEmpty(table.Rows[i]["IsNotified"].ToString()) ? "0" : table.Rows[i]["IsNotified"].ToString(), string.IsNullOrEmpty(table.Rows[i]["PenaltyPaid"].ToString()) ? "0" : table.Rows[i]["PenaltyPaid"].ToString(), string.IsNullOrEmpty(table.Rows[i]["PenaltyPaidOn"].ToString()) ? "" : table.Rows[i]["PenaltyPaidOn"].ToString(), table.Rows[i]["PenaltyTxnNo"].ToString(), table.Rows[i]["OffenceId"].ToString(), table.Rows[i]["VideoFileName"].ToString(), table.Rows[i]["PicFileName"].ToString(), table.Rows[i]["link"].ToString(), table.Rows[i]["LocationName"].ToString());

                    dt.Rows.Add(table.Rows[i]["LoginId"].ToString(), table.Rows[i]["VehicleNumber"].ToString(), table.Rows[i]["TicketNo"].ToString(), table.Rows[i]["OffenceName"].ToString(), table.Rows[i]["Penalty"].ToString(), table.Rows[i]["LocationName"].ToString(), table.Rows[i]["VideoFileName"].ToString(), table.Rows[i]["PicFileName"].ToString(), table.Rows[i]["diffflag"].ToString(), table.Rows[i]["ChesisNo"].ToString(), table.Rows[i]["VdoDate"].ToString(), table.Rows[i]["PrintedTicketNo"].ToString());
                }
                // StringBuilder x= new StringBuilder();
                string x = Convert.ToString(JObject.Parse(@"{""LoginDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                // return JObject.Parse(@"{""SearchDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(SearchViolationDetails) + "}");
               // return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");
                return table;
            }
            else
            {
                return table;
            }
        }





        [System.Web.Http.HttpGet]
        public JObject getSearch_Client(string VehNo_tikNo, string chesis_No, string flag, string u)
        {
            VehNo_tikNo = VehNo_tikNo.Replace("'", "''");
            chesis_No = chesis_No.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            SqlCommand cmd1 = new SqlCommand("select * FROM vw_ClientSearch where VehicleNumber='" + VehNo_tikNo + "'", cn1);
            cmd1.CommandType = CommandType.Text;
            //cmd1.ExecuteNonQuery();

            DataTable table = new DataTable();

            {
                string qry = "";
                string qryCondition = "";


                if (VehNo_tikNo=="b" && chesis_No == "b")
                {
                    qryCondition = "UserId='" + u + "'";
                }
                else if (chesis_No == "b")
                {
                    qryCondition = "VehicleNumber='" + VehNo_tikNo + "' and UserId='" + u + "'";
                }
               
                else
                {
                    qryCondition = "VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "' and UserId='" + u + "'";
                }
                // qry = "select * FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "' and isnull(IsNotified,0)!=1 and isnull(IsEnforced,0)!=1";
                if (flag == "Pd")
                    qry = "select * FROM vw_ClientSearch where " + qryCondition + "  and PenaltyPaid=1 ";
                else if (flag == "Pr")
                    qry = "select * FROM vw_ClientSearch where " + qryCondition + " and IsProcessed=1 and isnull(PenaltyPaid,0)<>1 and isnull(IsChallenged,0)<>1";
                else if (flag == "Ch")
                    qry = "select * FROM vw_ClientSearch where " + qryCondition + " and IsChallenged=1";
                else if (flag == "b")
                    qry = "select * FROM vw_ClientSearch where " + qryCondition + "";
                else
                {

                    if (chesis_No == "b")
                    {
                        if (flag == "PTN") // Printed Ticket No.
                            qry = "select * FROM vw_ClientSearch where PrintedTicketNo='" + VehNo_tikNo + "'";
                        else if (flag == "VN") // Violation No.
                            qry = "select * FROM vw_ClientSearch where TicketNo='" + VehNo_tikNo + "'";
                        else
                            qry = "select * FROM vw_ClientSearch where VehicleNumber='" + VehNo_tikNo + "' or TicketNo='" + VehNo_tikNo + "'";
                    }
                    else
                    {
                        qry = "select * FROM vw_ClientSearch where (VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "')";
                        // qry = "select * FROM vw_ClientSearch where (VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "') or TicketNo='" + VehNo_tikNo + "' or PrintedTicketNo='" + VehNo_tikNo + "'";
                    }
                }

                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
                
               
            }


            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[28] {new DataColumn("officerLoginId", typeof(string)),new DataColumn("officerFname", typeof(string)), new DataColumn("officerLname", typeof(string)), new DataColumn("vehicleNumber", typeof(string)), new DataColumn("TicketNo", typeof(string)),  new DataColumn("OffenceDetails", typeof(string)),  new DataColumn("Penalty", typeof(string)),                        
                            new DataColumn("deviceIMEI",typeof(string)), new DataColumn("vdo_datetime",typeof(string)), new DataColumn("daysLeft",typeof(int)), new DataColumn("IsNotified",typeof(int)), new DataColumn("IsPaid",typeof(int)), new DataColumn("PenaltyPaidOn",typeof(string)), new DataColumn("PeneltyTxnNo",typeof(string)),new DataColumn("OffenceId",typeof(string)),new DataColumn("VideoFileName",typeof(string)),new DataColumn("PicFileName",typeof(string)),new DataColumn("link",typeof(string)),new DataColumn("LocationName",typeof(string)),new DataColumn("Diff_Flag",typeof(string)),new DataColumn("VoilationStatus",typeof(string)),new DataColumn("PrintedTicketNo",typeof(string)),new DataColumn("ChallengedOn",typeof(string)),new DataColumn("ChallengedExpiry",typeof(string)),new DataColumn("IsEnforced",typeof(string)),new DataColumn("PenaltyPoints",typeof(string)),new DataColumn("ChallengeByMobNo",typeof(string)),new DataColumn("ChallengeByExpDiff",typeof(string))});


            if (table.Rows.Count > 0)
            {
                getSearchClient SearchViolationDetails = new getSearchClient
                {
                    status = "1",
                    officerId = table.Rows[0]["UserId"].ToString(),
                    officerFname = table.Rows[0]["FirstName"].ToString(),
                    officerLname = table.Rows[0]["LastName"].ToString(),
                    officerLoginId = table.Rows[0]["LoginId"].ToString(),

                    vehicleNumber = table.Rows[0]["VehicleNumber"].ToString(),
                    OffenceDetails = table.Rows[0]["OffenceName"].ToString(),
                    Penalty = table.Rows[0]["Penalty"].ToString(),
                    TicketNo = table.Rows[0]["TicketNo"].ToString(),

                    videoFilename = table.Rows[0]["VideoFileName"].ToString(),
                    picturesFilenames = table.Rows[0]["PicFileName"].ToString(),
                    locationname = table.Rows[0]["LocationName"].ToString(),
                    latitude = table.Rows[0]["Longitude"].ToString(),
                    longitude = table.Rows[0]["Latitude"].ToString(),
                    vdo_datetime = table.Rows[0]["videodatetime"].ToString(),
                    deviceIMEI = table.Rows[0]["DeviceIMEI"].ToString(),
                    IsNotified = table.Rows[0]["IsNotified"].ToString(),
                    IsPaid = table.Rows[0]["PenaltyPaid"].ToString(),
                    PenaltyPaidOn = table.Rows[0]["PenaltyPaidOn"].ToString(),
                    PenaltyTxnNo = table.Rows[0]["PenaltyTxnNo"].ToString(),
                    PrintedTicketNo = table.Rows[0]["PrintedTicketNo"].ToString(),
                    ChallangedExpiryDiff=table.Rows[0]["ChallengedExpiryDiff"].ToString()

                };

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dt.Rows.Add(table.Rows[i]["LoginId"].ToString(), table.Rows[i]["FirstName"].ToString(), table.Rows[i]["LastName"].ToString(), table.Rows[i]["VehicleNumber"].ToString(), table.Rows[i]["TicketNo"].ToString(), table.Rows[i]["OffenceName"].ToString(), table.Rows[i]["Penalty"].ToString(), table.Rows[i]["DeviceIMEI"].ToString(), table.Rows[i]["videodatetime"].ToString(), table.Rows[i]["DaysLeft"].ToString(), string.IsNullOrEmpty(table.Rows[i]["IsNotified"].ToString()) ? "0" : table.Rows[i]["IsNotified"].ToString(), string.IsNullOrEmpty(table.Rows[i]["PenaltyPaid"].ToString()) ? "0" : table.Rows[i]["PenaltyPaid"].ToString(), string.IsNullOrEmpty(table.Rows[i]["PenaltyPaidOn"].ToString()) ? "" : table.Rows[i]["PenaltyPaidOn"].ToString(), table.Rows[i]["PenaltyTxnNo"].ToString(), table.Rows[i]["OffenceId"].ToString(), table.Rows[i]["VideoFileName"].ToString(), table.Rows[i]["PicFileName"].ToString(), table.Rows[i]["link"].ToString(), table.Rows[i]["LocationName"].ToString(), table.Rows[i]["diffflag"].ToString(), table.Rows[i]["VoilationStatus"].ToString(), table.Rows[i]["PrintedTicketNo"].ToString(), table.Rows[i]["ChallengedOn"].ToString(), table.Rows[i]["ChallengedExpiry"].ToString(), String.IsNullOrEmpty(table.Rows[i]["IsEnforced"].ToString()) ? "0" : table.Rows[i]["IsEnforced"].ToString(), table.Rows[i]["PenaltyPoints"].ToString(), table.Rows[i]["ChallengeByMobNo"].ToString(), table.Rows[i]["ChallengedExpiryDiff"].ToString());
                }
                // StringBuilder x= new StringBuilder();
                string x = Convert.ToString(JObject.Parse(@"{""LoginDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                // return JObject.Parse(@"{""SearchDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(SearchViolationDetails) + "}");
                return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");

            }
            else
            {
                // return JObject.Parse(@"{""SearchDetails"" :{status:""0""}}");

                return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }



        [System.Web.Http.HttpGet]
        public JObject getSearch_ClientApp(string VehNo_tikNo, string chesis_No, string flag, string u)
        {
            VehNo_tikNo = VehNo_tikNo.Replace("'", "''");
            chesis_No = chesis_No.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            SqlCommand cmd1 = new SqlCommand("select * FROM vw_ClientSearch_ClientApp where VehicleNumber='" + VehNo_tikNo + "' order by ChallengedOn desc", cn1);
            cmd1.CommandType = CommandType.Text;
            //cmd1.ExecuteNonQuery();

            DataTable table = new DataTable();

            {
                string qry = "";
                string qryCondition = "";


                if (VehNo_tikNo == "b" && chesis_No == "b")
                {
                    qryCondition = "UserId='" + u + "'";
                }
                else if (chesis_No == "b")
                {
                    qryCondition = "VehicleNumber='" + VehNo_tikNo + "' and UserId='" + u + "'";
                }

                else
                {
                    qryCondition = "VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "' and UserId='" + u + "'";
                }
                // qry = "select * FROM vw_SearchViolations where VehicleNumber='" + Veh_No + "' and isnull(IsNotified,0)!=1 and isnull(IsEnforced,0)!=1";
                if (flag == "Pd")
                    qry = "select * FROM vw_ClientSearch_ClientApp where " + qryCondition + "  and PenaltyPaid=1 order by VideoDateTime asc";
                else if (flag == "Pr")
                    qry = "select * FROM vw_ClientSearch_ClientApp where " + qryCondition + " and IsProcessed=1 and isnull(PenaltyPaid,0)<>1 and isnull(IsChallenged,0)<>1 order by VideoDateTime desc";
                else if (flag == "Ch")
                {
                    qry = "select * FROM vw_ClientSearch_ClientApp where " + qryCondition + " and isnull(IsChallenged,0)<>0 order by ChallengedExpiryDiff,ChallengedOn desc";
                    qry = "select * FROM vw_ClientSearch_ClientApp where " + qryCondition + " and isnull(IsChallenged,0)<>0 order by ChallengedOn desc";
                }
                else if (flag == "b")
                    qry = "select * FROM vw_ClientSearch_ClientApp where " + qryCondition + "";
                else
                {

                    if (chesis_No == "b")
                    {
                        if (flag == "PTN") // Printed Ticket No.
                            qry = "select * FROM vw_ClientSearch_ClientApp where PrintedTicketNo='" + VehNo_tikNo + "'";
                        else if (flag == "VN") // Violation No.
                            qry = "select * FROM vw_ClientSearch_ClientApp where TicketNo='" + VehNo_tikNo + "'";
                        else
                            qry = "select * FROM vw_ClientSearch_ClientApp where VehicleNumber='" + VehNo_tikNo + "' or TicketNo='" + VehNo_tikNo + "'";
                    }
                    else
                    {
                        qry = "select * FROM vw_ClientSearch_ClientApp where (VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "') order by ChallengedOn desc";
                        // qry = "select * FROM vw_ClientSearch where (VehicleNumber='" + VehNo_tikNo + "' and ChesisNo='" + chesis_No + "') or TicketNo='" + VehNo_tikNo + "' or PrintedTicketNo='" + VehNo_tikNo + "'";
                    }
                }

                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }


            }


            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[30] {new DataColumn("officerLoginId", typeof(string)),new DataColumn("officerFname", typeof(string)), new DataColumn("officerLname", typeof(string)), new DataColumn("vehicleNumber", typeof(string)), new DataColumn("TicketNo", typeof(string)),  new DataColumn("OffenceDetails", typeof(string)),  new DataColumn("Penalty", typeof(string)),                        
                            new DataColumn("deviceIMEI",typeof(string)), new DataColumn("vdo_datetime",typeof(string)), new DataColumn("daysLeft",typeof(int)), new DataColumn("IsNotified",typeof(int)), new DataColumn("IsPaid",typeof(int)), new DataColumn("PenaltyPaidOn",typeof(string)), new DataColumn("PeneltyTxnNo",typeof(string)),new DataColumn("OffenceId",typeof(string)),new DataColumn("VideoFileName",typeof(string)),new DataColumn("PicFileName",typeof(string)),new DataColumn("link",typeof(string)),new DataColumn("LocationName",typeof(string)),new DataColumn("Diff_Flag",typeof(string)),new DataColumn("VoilationStatus",typeof(string)),new DataColumn("PrintedTicketNo",typeof(string)),new DataColumn("ChallengedOn",typeof(string)),new DataColumn("ChallengedExpiry",typeof(string)),new DataColumn("IsEnforced",typeof(string)),new DataColumn("PenaltyPoints",typeof(string)),new DataColumn("ChallengeByMobNo",typeof(string)),new DataColumn("ChallengeByExpDiff",typeof(string)),new DataColumn("Status",typeof(string)),new DataColumn("TimeStatus",typeof(string))});


            if (table.Rows.Count > 0)
            {
                //getSearchClient_App SearchViolationDetails = new getSearchClient_App
                //{
                //    status = "1",
                //    officerId = table.Rows[0]["UserId"].ToString(),
                //    officerFname = table.Rows[0]["FirstName"].ToString(),
                //    officerLname = table.Rows[0]["LastName"].ToString(),
                //    officerLoginId = table.Rows[0]["LoginId"].ToString(),

                //    vehicleNumber = table.Rows[0]["VehicleNumber"].ToString(),
                //    OffenceDetails = table.Rows[0]["OffenceName"].ToString(),
                //    Penalty = table.Rows[0]["Penalty"].ToString(),
                //    TicketNo = table.Rows[0]["TicketNo"].ToString(),

                //    videoFilename = table.Rows[0]["VideoFileName"].ToString(),
                //    picturesFilenames = table.Rows[0]["PicFileName"].ToString(),
                //    locationname = table.Rows[0]["LocationName"].ToString(),
                //    latitude = table.Rows[0]["Longitude"].ToString(),
                //    longitude = table.Rows[0]["Latitude"].ToString(),
                //    vdo_datetime = Convert.ToDateTime(table.Rows[0]["videodatetime"]),
                //    deviceIMEI = table.Rows[0]["DeviceIMEI"].ToString(),
                //    IsNotified = table.Rows[0]["IsNotified"].ToString(),
                //    IsPaid = table.Rows[0]["PenaltyPaid"].ToString(),
                //    PenaltyPaidOn = Convert.ToDateTime(table.Rows[0]["PenaltyPaidOn"]),
                //    PenaltyTxnNo = table.Rows[0]["PenaltyTxnNo"].ToString(),
                //    PrintedTicketNo = table.Rows[0]["PrintedTicketNo"].ToString(),
                //    ChallangedExpiryDiff = table.Rows[0]["ChallengedExpiryDiff"].ToString()

               // };

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dt.Rows.Add(table.Rows[i]["LoginId"].ToString(), table.Rows[i]["FirstName"].ToString(), table.Rows[i]["LastName"].ToString(), table.Rows[i]["VehicleNumber"].ToString(), table.Rows[i]["TicketNo"].ToString(), table.Rows[i]["OffenceName"].ToString(), table.Rows[i]["Penalty"].ToString(), table.Rows[i]["DeviceIMEI"].ToString(), Convert.ToDateTime(table.Rows[i]["videodatetime"]).ToString("dd/MM/yyyy hh:mm:ss tt"), table.Rows[i]["DaysLeft"].ToString(), string.IsNullOrEmpty(table.Rows[i]["IsNotified"].ToString()) ? "0" : table.Rows[i]["IsNotified"].ToString(), string.IsNullOrEmpty(table.Rows[i]["PenaltyPaid"].ToString()) ? "0" : table.Rows[i]["PenaltyPaid"].ToString(), string.IsNullOrEmpty(table.Rows[i]["PenaltyPaidOn"].ToString()) ? "" : table.Rows[i]["PenaltyPaidOn"].ToString(), table.Rows[i]["PenaltyTxnNo"].ToString(), table.Rows[i]["OffenceId"].ToString(), table.Rows[i]["VideoFileName"].ToString(), table.Rows[i]["PicFileName"].ToString(), table.Rows[i]["link"].ToString(), table.Rows[i]["LocationName"].ToString(), table.Rows[i]["diffflag"].ToString(), table.Rows[i]["VoilationStatus"].ToString(), table.Rows[i]["PrintedTicketNo"].ToString(), string.IsNullOrEmpty(table.Rows[i]["ChallengedOn"].ToString()) ? table.Rows[i]["ChallengedOn"].ToString() : (Convert.ToDateTime(table.Rows[i]["ChallengedOn"]).ToString("dd/MM/yyyy hh:mm:ss tt")), table.Rows[i]["ChallengedExpiry"].ToString(), String.IsNullOrEmpty(table.Rows[i]["IsEnforced"].ToString()) ? "0" : table.Rows[i]["IsEnforced"].ToString(), table.Rows[i]["PenaltyPoints"].ToString(), table.Rows[i]["ChallengeByMobNo"].ToString(), table.Rows[i]["ChallengedExpiryDiff"].ToString(), table.Rows[i]["Status"].ToString(), table.Rows[i]["TimeStatus"].ToString());
                }
                // StringBuilder x= new StringBuilder();
                string x = Convert.ToString(JObject.Parse(@"{""LoginDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                // return JObject.Parse(@"{""SearchDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(SearchViolationDetails) + "}");
                return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");

            }
            else
            {
                // return JObject.Parse(@"{""SearchDetails"" :{status:""0""}}");

                return JObject.Parse(@"{""SearchDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }








        [System.Web.Http.HttpPost]
        public JObject Register_ClientLogin([FromBody] Clientlogin_Register ClientLoginDet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());
            
            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_ClientUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Loginid", ClientLoginDet.LoginId);
                cmd.Parameters.AddWithValue("@Password", AgentProfileController.EncryptString(ClientLoginDet.Password));
              //  cmd.Parameters.AddWithValue("@Password",ClientLoginDet.Password);
                cmd.Parameters.AddWithValue("@FirstName", ClientLoginDet.fname);
                cmd.Parameters.AddWithValue("@LastName", ClientLoginDet.lname);
                cmd.Parameters.AddWithValue("@PhoneNumber", ClientLoginDet.Phone);
                cmd.Parameters.AddWithValue("@Address", ClientLoginDet.Address);
                cmd.Parameters.AddWithValue("@DOB", ClientLoginDet.DOB);
                cmd.Parameters.AddWithValue("@Gender", ClientLoginDet.Gender);
                cmd.Parameters.AddWithValue("@DrivingLicNo", ClientLoginDet.LicNo);

                SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                SqlParameter UserId = new SqlParameter("@UserId", SqlDbType.VarChar, 200);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                UserId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(UserId);
                
                /***************************** Email Verification **********************************/

                //MailMessage mm = new MailMessage();
                //mm.To.Add(new MailAddress("yourwebsitemailchecker@gmail.com", "Request for Verification"));
                //mm.From = new MailAddress("saurabhbansal2004@gmail.com");
                //mm.Body = "click here to verify fgdfgdfgdfgdfgdfgdfgfdg";
                //mm.IsBodyHtml = true;
                //mm.Subject = "Verification";
                //SmtpClient smcl = new SmtpClient();
                //smcl.Host = "smtp.gmail.com";
                //smcl.Port = 587;
                //smcl.Credentials = new NetworkCredential("yourwebsitemailid@gmail.com", "yourmailpasswrod");
                //smcl.EnableSsl = true;
                //smcl.Send(mm);

               // mail(ClientLoginDet.LoginId);
                /***********************************************************************************/




                //  cmd.Parameters["@SuccessID"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add(new SqlParameter("@SuccessID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);
                cn.Open();
                cmd.ExecuteNonQuery();

                a = output.Value.ToString();
                cn.Close();
                if (a == "1")
                {
                  // mail(ClientLoginDet.LoginId);
                  
                    /********************* mail code ******************************/

                    //byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(ClientLoginDet.LoginId);
                    //string encodedMailId = System.Convert.ToBase64String(mybyte);

                    //MailMessage mm = new MailMessage();
                    //mm.To.Add(new MailAddress(ClientLoginDet.LoginId, "Request for Verification"));
                    //mm.From = new MailAddress("zas.donotreply@gmail.com");
                    //mm.Body = "click here to verify http://52.214.162.100:8283/ClientHome.aspx?LoginId=" + encodedMailId;
                    //mm.IsBodyHtml = true;
                    //mm.Subject = "Verification";
                    //SmtpClient smcl = new SmtpClient();
                    //smcl.Host = "smtp.gmail.com";
                    //smcl.Port = 587;

                    //smcl.Credentials = new NetworkCredential("zas.donotreply@gmail.com", "$$test1234$$");
                    //// smcl.UseDefaultCredentials = false;
                    //smcl.EnableSsl = true;
                    //smcl.SendMailAsync(mm);


                    /*******************************************************************************/
                    //Commented on 9th jan 2020
                 //   byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(ClientLoginDet.LoginId);
                 //   string encodedMailId = System.Convert.ToBase64String(mybyte);
                 //   MailMessage mm = new MailMessage();
                 //   mm.To.Add(new MailAddress(ClientLoginDet.LoginId, "Request for Verification"));
                 //   mm.From = new MailAddress("saurabhbansal2004@gmail.com");
                 ////   mm.Body = "click here to verify http://217.117.3.12:8283/ClientHome.aspx?LoginId=" + encodedMailId;
                 //   mm.IsBodyHtml = true;
                 //   mm.Subject = "Verification";
                 //   SmtpClient smcl = new SmtpClient();
                 //   smcl.Host = "smtp.gmail.com";
                 //   smcl.Port = 587;
                 //   //smcl.Credentials = new NetworkCredential("zas.donotreply@gmail.com", "$$test1234$$");
                 //   smcl.Credentials = new NetworkCredential("tvms.donotreply@gmail.com", "$TMS@SB2019");
                 //   smcl.EnableSsl = true;
                 //   smcl.Send(mm);
                    /**************************************************************/


                    main_ret = main_ret + "{\"Result\":\"Success\", \"UserId\": \"" + UserId.Value.ToString() + "\"}";
                   // main_ret = main_ret + "{\"Result\":\"Success\"}";
                }
                else if (a == "2")
                    main_ret = main_ret + "{\"Result\":\"LoginId Already Exist\"}";

                else if (a == "3")
                    main_ret = main_ret + "{\"Result\":\"Phone Already Exist\"}";

                else
                    main_ret = main_ret + "{\"Result\":\"Failed\"}";

                return JObject.Parse(main_ret);
            }

            catch (Exception ex)
            {
                cn.Close();
                main_ret = main_ret + "{\"Result\":\"Server Error\"}";
                return JObject.Parse(main_ret);

               // return JObject.Parse(@"{""Server Error""}");
            }
        }


        [System.Web.Http.HttpGet]
        public JObject getFTP_Client()
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();

            {
                using (SqlCommand cmd = new SqlCommand("select * FROM tblFtpDetails where FtpID=1", cn1))

                // using (SqlCommand cmd = new SqlCommand("select * FROM tblClientUserMaster where LoginId='" + u + "'", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            if(table.Rows.Count>0)
            {
                    string x = Convert.ToString(JObject.Parse(@"{""FTPDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                    x = x.Replace("[","").Replace("]","");
                    return JObject.Parse(x);


            }
            else
            {
                return JObject.Parse(@"{""FTPDetails"" :{status:""0""}}");
                //  return (@"{LoginDetails : {Status:0}}");
            }
        }



        [System.Web.Http.HttpGet]
        public JObject getClient_phoneNo_exist(string phno_email)
        {   
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());

            //changes on 9th jan 2020
            //SqlCommand cmd1 = new SqlCommand("select * FROM tblClientUserMaster where LoginId=@u and password=@p and Flag_email_verify='Y'", cn1);

            SqlCommand cmd1 = new SqlCommand("select * FROM tblClientUserMaster where PhoneNumber='" + phno_email + "' or PhoneNumber='0" + phno_email + "' or LoginId='" + phno_email + "'", cn1);

            cmd1.CommandType = CommandType.Text;
            //cmd1.ExecuteNonQuery();

            DataTable table = new DataTable();

            {

                using (SqlCommand cmd = new SqlCommand("select * FROM tblClientUserMaster where PhoneNumber='" + phno_email + "' or PhoneNumber='0" + phno_email + "' or LoginId='" + phno_email + "'", cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            if(table.Rows.Count>0)
            {
                return JObject.Parse(@"{""Status"" :{status:""Already Exist.""}}");
                //  return (@"{LoginDetails : {Status:0}}");
            }
             else
            {
                return JObject.Parse(@"{""Status"" :{status:""Not Exist""}}");
                //  return (@"{LoginDetails : {Status:0}}");
            }
        }


        [System.Web.Http.HttpGet]
        public JObject getClientLogin(string u, string p)
        {
            u = u.Replace("'", "''");
            p = p.Replace("'", "''");
            
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());

            //changes on 9th jan 2020
            //SqlCommand cmd1 = new SqlCommand("select * FROM tblClientUserMaster where LoginId=@u and password=@p and Flag_email_verify='Y'", cn1);

            SqlCommand cmd1 = new SqlCommand("select * FROM tblClientUserMaster where LoginId=@u and password=@p", cn1);
            
            cmd1.Parameters.AddWithValue("@u", u);
           // cmd1.Parameters.AddWithValue("@p", AgentProfileController.EncryptString(p));

            cmd1.Parameters.AddWithValue("@p", p);
            cmd1.CommandType = CommandType.Text;
            //cmd1.ExecuteNonQuery();

            DataTable table = new DataTable();

            {
                //changes on 9th jan 2020
                // using (SqlCommand cmd = new SqlCommand("select * FROM tblClientUserMaster where LoginId='" + u + "' and Flag_email_verify='Y'", cn1))

                using (SqlCommand cmd = new SqlCommand("select * FROM tblClientUserMaster where LoginId='" + u + "'", cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            string password="";
            if(table.Rows.Count>0)
             password=AgentProfileController.DecryptString(table.Rows[0]["Password"].ToString());

            byte[] imagem = new byte[0];

            if (table.Rows.Count > 0)
            {
                if ((table.Rows[0]["UserImage"].ToString() != ""))
                    imagem = (byte[])(table.Rows[0]["UserImage"]);
            }

            if (table.Rows.Count > 0 && p==password)
            {
                Clientlogin_det login = new Clientlogin_det
                {
                    status = "1",
                    LoginId = table.Rows[0]["LoginId"].ToString(),
                    userId = table.Rows[0]["UserId"].ToString(),
                    fname = table.Rows[0]["FirstName"].ToString(),
                    lname = table.Rows[0]["LastName"].ToString(),
                    Phone = table.Rows[0]["PhoneNumber"].ToString(),
                    Address = table.Rows[0]["Address"].ToString(),
                    Gender = table.Rows[0]["Gender"].ToString(),

                   // DateOfBirth = table.Rows[0]["DOB"].ToString(),
                   DateOfBirth=Convert.ToDateTime(table.Rows[0]["DOB"]).ToString("dd/MM/yyyy hh:mm:ss tt"),
                    DrivingLicNo = table.Rows[0]["DrivingLicNo"].ToString(),
                    ImagePath = imagem,
                    Email = table.Rows[0]["EmailId"].ToString(),

                    IsFirstTimeLogin = table.Rows[0]["IsFirstTimeLogin"].ToString()

                };
                // StringBuilder x= new StringBuilder();
                string x = Convert.ToString(JObject.Parse(@"{""LoginDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                return JObject.Parse(@"{""LoginDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(login) + "}");


            }
            else
            {
                return JObject.Parse(@"{""LoginDetails"" :{status:""0""}}");
                //  return (@"{LoginDetails : {Status:0}}");
            }
        }


        [System.Web.Http.HttpPost]
        public JObject Delete_Vehicle(Delete_Vehicle del_veh)
        {
              string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_Delete_Vehicle", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleNumber", del_veh.VehicleNumber);
                cmd.Parameters.AddWithValue("@UserId", del_veh.UserId);
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

                else if (a == "2")
                    main_ret = main_ret + "{\"Result\":\"Vehicle Not Exist.\"}";

                else if (a == "3")
                    main_ret = main_ret + "{\"Result\":\"Vehicle Already Parked.\"}";

                else if (a == "4")
                    main_ret = main_ret + "{\"Result\":\"Vehicle Voilation Not Paid.\"}";

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



        [System.Web.Http.HttpPost]
        public JObject Add_Vehicle([FromBody] Add_Vehicle_det AddVehicleDet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_Vehicle", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleNo", AddVehicleDet.VehicleNo);
                cmd.Parameters.AddWithValue("@ChesisNo", AddVehicleDet.ChesisNo);
                cmd.Parameters.AddWithValue("@RegName", AddVehicleDet.RegisteredName);
                cmd.Parameters.AddWithValue("@RegAddress", AddVehicleDet.RegisteredAddress);
                cmd.Parameters.AddWithValue("@RegDate", AddVehicleDet.RegistrationDate);
                cmd.Parameters.AddWithValue("@Model", AddVehicleDet.Model);
                cmd.Parameters.AddWithValue("@Class", AddVehicleDet.Class);
                cmd.Parameters.AddWithValue("@VehicleColor", AddVehicleDet.VehicleColor);
                cmd.Parameters.AddWithValue("@UserId", AddVehicleDet.UserId);
                cmd.Parameters.AddWithValue("@DrivingLicNo", AddVehicleDet.DrivingLicNo);


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

                else if (a == "2")
                    main_ret = main_ret + "{\"Result\":\"Vehicle No. Already Exist\"}";

                else if (a == "3")
                    main_ret = main_ret + "{\"Result\":\"Chesis No. Already Exist\"}";

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

        [System.Web.Http.HttpPost]
        public JObject Ins_ServiceCenter_Req(string DeviceId, string SerCenterId, string ReqBy, string PreviousUserId)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_CreateServiceReq", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DeviceId", DeviceId);
                cmd.Parameters.AddWithValue("@RequestBy", ReqBy);
                cmd.Parameters.AddWithValue("@CenterId", SerCenterId);
                cmd.Parameters.AddWithValue("@PreviousUserId", PreviousUserId);


                SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                SqlParameter ReqId = new SqlParameter("@ReqId", SqlDbType.VarChar,200);

                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                ReqId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ReqId);

                cn.Open();
                cmd.ExecuteNonQuery();

                a = output.Value.ToString();
                cn.Close();
                string req_blank = "";
                if (a == "1")
                    main_ret = main_ret + "{\"Result\":\"Success\", \"ReqId\": \"" + ReqId.Value.ToString() + "\"}";

                else if (a == "2")
                    main_ret = main_ret + "{\"Result\":\"Request Already Generated Against This Device.\", \"ReqId\": \"" + req_blank.ToString() + "\"}";

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
        public JObject getSecurityCode(string ReqNo, string securityCode)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
           
            DataTable table = new DataTable();


            {
                using (SqlCommand cmd = new SqlCommand("select * FROM tblServiceRequest where RequestId='" + ReqNo + "' and SecurityCode='" + securityCode + "'", cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            string main_ret = "";
            if (table.Rows.Count > 0)
            {
                  main_ret = main_ret + "{\"Result\":\"Success\"}";


             
            }
            else
            {
                  main_ret = main_ret + "{\"Result\":\"Failed\"}";
            }

             return JObject.Parse(main_ret);
        }


        [System.Web.Http.HttpGet]
        public JObject getDeviceStatus(string IMEI_No)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());

            DataTable table = new DataTable();


            {
               // using (SqlCommand cmd = new SqlCommand("SELECT dbo.tblServiceRequest.RequestId, dbo.tblDeviceDetails.Status, dbo.tblDeviceDetails.UpdatedOn FROM dbo.tblServiceRequest INNER JOIN dbo.tblDeviceDetails ON dbo.tblServiceRequest.DeviceId = dbo.tblDeviceDetails.DeviceId where RequestId='" + ReqNo + "' ORDER BY dbo.tblDeviceDetails.UpdatedOn desc", cn1))

                using (SqlCommand cmd = new SqlCommand("SELECT * from dbo.tblDeviceDetails where DeviceIMEI='"+IMEI_No+"'", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            string main_ret = "";
            if (table.Rows.Count > 0)
            {
                if (table.Rows[0]["Status"].ToString() == "1")
                    main_ret = main_ret + "{\"Result\":\"Device is Ok Now\"}";
                else if (table.Rows[0]["Status"].ToString() == "3")
                    main_ret = main_ret + "{\"Result\":\"Device is Under Maintenance\"}";
                else
                    main_ret = main_ret + "{\"Result\":\"Device is Inactive\"}";

            }

            else
            {
                 main_ret = main_ret + "{\"Result\":\"Result Not Found\"}"; 
                
            }
          

            return JObject.Parse(main_ret);
        }




        [System.Web.Http.HttpGet]
        public JObject getRegisteredVehicle(string u)
        {
            u = u.Replace("'", "''");
           
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            SqlCommand cmd1 = new SqlCommand("select * FROM tblVehicleDetails where UserId='" + u + "'", cn1);
            cmd1.CommandType = CommandType.Text;
            //cmd1.ExecuteNonQuery();

            DataTable table = new DataTable();


            {
                using (SqlCommand cmd = new SqlCommand("select * FROM tblVehicleDetails where UserId='" + u + "'", cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }


            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[8] {new DataColumn("VehicleNo", typeof(string)),new DataColumn("ChesisNo", typeof(string)), new DataColumn("RegisteredName", typeof(string)), new DataColumn("RegisteredAddress", typeof(string)), new DataColumn("RegistrationDate", typeof(string)),  new DataColumn("Model", typeof(string)),  new DataColumn("Class", typeof(string)),                        
                            new DataColumn("VehicleColor",typeof(string))});


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
                    dt.Rows.Add(table.Rows[i]["VehicleNo"].ToString(), table.Rows[i]["ChesisNo"].ToString(), table.Rows[i]["RegisteredName"].ToString(), table.Rows[i]["RegisteredAddress"].ToString(), table.Rows[i]["RegistrationDate"].ToString(), table.Rows[i]["Model"].ToString(), table.Rows[i]["Class"].ToString(), table.Rows[i]["VehicleColor"].ToString());
                }
                // StringBuilder x= new StringBuilder();
                string x = Convert.ToString(JObject.Parse(@"{""VehicleDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                // return JObject.Parse(@"{""SearchDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(SearchViolationDetails) + "}");
                return JObject.Parse(@"{""VehicleDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");

              

            }
            else
            {
                return JObject.Parse(@"{""VehicleDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");
                //  return (@"{LoginDetails : {Status:0}}");
            }
        }




        [System.Web.Http.HttpGet]
        public JObject getRegisteredVehicle_Web(string VehicleNo)
        {
            try
            {
                VehicleNo = VehicleNo.Replace("'", "''");
                //ChesisNo = ChesisNo.Replace("'", "''");
                VehicleNo = VehicleNo.Replace(",", "','");
                SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
               // SqlCommand cmd1 = new SqlCommand("select * FROM tblVehicleDetails where VehicleNo='" + VehicleNo + "'", cn1);
                SqlCommand cmd1 = new SqlCommand("select * FROM tbl_Vehicle_Details_Artezia where VehicleNo in ('" + VehicleNo + "')", cn1);
                
                cmd1.CommandType = CommandType.Text;
                //cmd1.ExecuteNonQuery();

                DataTable table = new DataTable();


                //{
                //  //  using (SqlCommand cmd = new SqlCommand("select * FROM tblVehicleDetails where VehicleNo='" + VehicleNo + "'", cn1))

                //    using (SqlCommand cmd = new SqlCommand("select * FROM tbl_Vehicle_Details_Artezia where VehicleNo='" + VehicleNo + "'", cn1))
                //    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                //    {
                //        adapter.Fill(table);
                //    }
                //}

                if (table.Rows.Count > 0)
                {
                    // Artezia (for me its commented) //tbl_Vehicle_Details_Artezia
                    using (SqlCommand cmd = new SqlCommand("select * FROM tbl_Vehicle_Details_Artezia where VehicleNo in ('" + VehicleNo + "')", cn1))


                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("select * FROM tbl_Vehicle_Details_Artezia where VehicleNo in ('" + VehicleNo + "')", cn1))


                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[10] {new DataColumn("VehicleNo", typeof(string)),new DataColumn("ChesisNo", typeof(string)), new DataColumn("RegisteredName", typeof(string)), new DataColumn("RegisteredAddress", typeof(string)), new DataColumn("RegistrationDate", typeof(string)),  new DataColumn("Model", typeof(string)),  new DataColumn("Class", typeof(string)),                        
                            new DataColumn("VehicleColor",typeof(string)),new DataColumn("PhoneNumber",typeof(string)),new DataColumn("Make",typeof(string))});


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
                        dt.Rows.Add(table.Rows[i]["VehicleNo"].ToString(), table.Rows[i]["ChesisNo"].ToString(), table.Rows[i]["RegisteredName"].ToString(), table.Rows[i]["RegisteredAddress"].ToString(), table.Rows[i]["RegistrationDate"].ToString(), table.Rows[i]["Model"].ToString(), table.Rows[i]["Class"].ToString(), table.Rows[i]["VehicleColor"].ToString(), table.Rows[i]["MobileNo"].ToString(), table.Rows[i]["Make"].ToString());

                        
                        //dt.Rows.Add(table.Rows[i]["VehicleNo"].ToString(), table.Rows[i]["ChesisNo"].ToString(), table.Rows[i]["RegisteredName"].ToString(), table.Rows[i]["RegisteredAddress"].ToString(), table.Rows[i]["RegistrationDate"].ToString(), table.Rows[i]["Model"].ToString(), table.Rows[i]["Class"].ToString(), table.Rows[i]["VehicleColor"].ToString(), "07088080800", table.Rows[i]["Make"].ToString());
                    }
                    // StringBuilder x= new StringBuilder();
                    string x = Convert.ToString(JObject.Parse(@"{""VehicleDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                    // return JObject.Parse(@"{""SearchDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(SearchViolationDetails) + "}");
                    return JObject.Parse(@"{""VehicleDetails"" :" + (JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None)) + "}");



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

        //[System.Web.Http.HttpPost]
        //public JObject ChallangeVoilation(string TikNo,string voilationId)
        //{

        //}

        [System.Web.Http.HttpPost]
        public JObject ChallangeVoilation([FromBody] Challenged_violation_det Challenged_violationdet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_Upd_Challenged", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TicketNumber", Challenged_violationdet.TicketNo);
                cmd.Parameters.AddWithValue("@OffenceId", Challenged_violationdet.OffenceId);
                cmd.Parameters.AddWithValue("@email", Challenged_violationdet.ChallengeByEmail);
                cmd.Parameters.AddWithValue("@ph_no", Challenged_violationdet.ChallengeByMobNo);

                cmd.Parameters.AddWithValue("@ChallengeReason", Challenged_violationdet.ChallengeReason);

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
                {
                    main_ret = main_ret + "{\"Result\":\"Success\"}";
                    NotificationController Notification_controller = new NotificationController();
                    Notification_controller.SendChallengeNotification("", Challenged_violationdet.TicketNo);

                    CommonController common = new CommonController();

                    string body = "Your challenge of the violation with number " + Challenged_violationdet.TicketNo + " has been registered and sent for preliminary review.<br/><br/>";
                    body += "We will respond shortly with next steps.<br/><br/> Thank you.<br/><br/>TMS Team";
                    common.sendemail(Challenged_violationdet.ChallengeByEmail, "Challenge Notification", body);
                }
                else if (a == "2")
                    main_ret = main_ret + "{\"Result\":\"Already Challenged not to challenged again.\"}";  // not challenged again
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
        public string getToken_payment()
        {
            //reqid = ticketNo
            // string url = "http://api.nownowpay.ng/mfs-transaction-management/authManagement/get";

            string url = "http://apidev.nownowpay.com.ng/mfs-transaction-management/authManagement/get";

            var res = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Basic YXBpQ2xpZW50OmFwaUNsaWVudFNlY3JldA==";
                string json = "{\n\"mfsCommonServiceRequest\": {\n\"mfsSourceInfo\": {\n\"channelId\": \"22\",\n\"surroundSystem\": \"1\"\n},\n\"mfsTransactionInfo\": {\n\"requestId\": \"9086544566665\",\n\"serviceType\": \"0\",\n\"timestamp\": \"1517218465350\"\n}\n}\n}";
                res = client.UploadString(url, json);

                var ew = res;

            }
            return JObject.Parse(res)["mfsResponseInfo"]["token"].ToString();
        }


        [System.Web.Http.HttpGet]
        public string payment(string req_id, string amt, string paymentModuleName)
        {
            string URL = "http://217.117.3.12:8283/PaymentResponse.aspx?paymentModuleName=" + paymentModuleName + "&";

            //if (paymentModuleName == "Tolling")
            //{
            //    URL = "http://52.214.162.100:8283/PaymentResponseTolling.aspx";
            //}

            //if (paymentModuleName == "Parking")
            //{
            //    URL = "http://52.214.162.100:8283/PaymentResponseParking.aspx";
            //}

            string token = getToken_payment();
            //reqid = ticketNo
            //string url = "http://pg.nownowpay.ng/nownowpg/getKey";
            string url_api = "http://34.254.53.229:8080/nownowpg/getKey";
            var res="";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
               
               // string json = "{\n    \"deviceType\": \"Web\",\n    \"entitySubType\": \"2\",\n    \"amount\": \"" + amt + "\",\n    \"authToken\": \"" + token + "\",\n    \"authmodel\": \"VBVSECURECODE\",\n    \"channelId\": \"23\",\n    \"country\": \"NG\",\n    \"currency\": \"NGN\",\n    \"custid\": \"81000000177\",\n    \"entityId\": \"81000000177\",\n    \"entityType\": \"81\",\n    \"flag\": \"0\",\n    \"fromEntityId\": \"81000000177\",\n    \"mpin\": \"0f1eaedb30bfa62c3445edb6cdcc7ffe\",\n    \"msisdn\": \"9053810099\",\n    \"narration\": \"NESREA PG TEST\",\n    \"pin\": \"\",\n    \"requestId\":  \"" + req_id + "\",\n    \"surroundSystem\": \"1\",\n    \"timestamp\": \"1555494630\",\n    \"toEntityId\": \"81000000177\",\n    \"projectType\": \"NGECP\",\n    \"clientCallbackURL\": \"" + URL + "\"\n}";
                string json = "{\n    \"deviceType\": \"Web\",\n    \"entitySubType\": \"2\",\n    \"amount\": \"" + amt + "\",\n    \"authToken\": \"" + token + "\",\n    \"authmodel\": \"VBVSECURECODE\",\n    \"channelId\": \"23\",\n    \"country\": \"NG\",\n    \"currency\": \"NGN\",\n    \"custid\": \"" + req_id + "\",\n    \"entityId\": \"95000000001\",\n    \"entityType\": \"95\",\n    \"flag\": \"0\",\n    \"fromEntityId\": \"95000000001\",\n    \"mpin\": \"2735c056c095cf686aa0b592cd9fb7b8\",\n    \"msisdn\": \"7088080808\",\n    \"narration\": \"TVMS test\",\n    \"pin\": \"\",\n    \"requestId\":  \"" + req_id + "\",\n    \"surroundSystem\": \"1\",\n    \"timestamp\": \"1555494630\",\n    \"toEntityId\": \"95000000001\",\n    \"projectType\": \"TVMS\",\n    \"clientCallbackURL\": \"" + URL + "\"\n}";
                res = client.UploadString(url_api, json);
               
            }

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[1] { new DataColumn("key", typeof(string)) });
           
           // Uri a = new Uri("https://pg.nownowpay.ng/nownowpg/pay?resp=" + JObject.Parse(res)["key"].ToString());
            Uri a = new Uri("http://34.254.53.229:8080/nownowpg/pay?resp=" + JObject.Parse(res)["key"].ToString());
           return a.OriginalString;


           // return ("https://pg.nownowpay.ng/nownowpg/pay?resp=" + JObject.Parse(res)["key"].ToString());
        }

        [System.Web.Http.HttpGet]
        public JObject getServiceCenterList()
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            SqlCommand cmd1 = new SqlCommand("select * FROM tblServiceCenterList", cn1);
            cmd1.CommandType = CommandType.Text;
            //cmd1.ExecuteNonQuery();

            DataTable table = new DataTable();


            {
                using (SqlCommand cmd = new SqlCommand("select * FROM tblServiceCenterList", cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }


          if(table.Rows.Count>0)
          {
                string x = Convert.ToString(JObject.Parse(@"{""ServiceCenterDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

               
                return JObject.Parse(@"{""ServiceCenterDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");



            }
            else
            {
                return JObject.Parse(@"{""ServiceCenterDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
                
            }
        }

      

        /************* Tolling Process ********************/

        [System.Web.Http.HttpPost]
        public JObject insert_tolling_details([FromBody] tolling_det tollingdet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_TollingDet", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", tollingdet.UserId);
                cmd.Parameters.AddWithValue("@deviceIMEI", tollingdet.deviceIMEI);
                cmd.Parameters.AddWithValue("@picname", tollingdet.picturename);
                cmd.Parameters.AddWithValue("@latitude", tollingdet.latitude);
                cmd.Parameters.AddWithValue("@longitude", tollingdet.longitude);
                cmd.Parameters.AddWithValue("@locationname", tollingdet.locationname);
                cmd.Parameters.AddWithValue("@tolling_datetime", tollingdet.tolling_datetime);

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
        public JObject TollingUnpaidRecords(string Veh_Id)    /* Slot List */
        {
            Veh_Id = Veh_Id.Replace("'", "''");
            
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                string qry = "";

                qry = "select * FROM tblTollingDet where VehicleNo='" + Veh_Id + "' and (isnull(FarePaid,0)<>1 or isnull(IsEscalate,0)<>1 or isnull(IsProcessed,0)=1)";

                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {
                table.Columns.Remove("IsEscalate");
                table.Columns.Remove("EscalateOn");
                table.Columns.Remove("EscalateBy");

                table.Columns.Remove("DeviceId");
                table.Columns.Remove("IsProcessed");
                table.Columns.Remove("ProcessedBy");
                table.Columns.Remove("ProcessedOn");
                table.Columns.Remove("IsLocked");
                table.Columns.Remove("LockedBy");
                table.Columns.Remove("LockedOn");


                string x = Convert.ToString(JObject.Parse(@"{""TollingUnpaidList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""TollingUnpaidList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""TollingUnpaidList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }

         [System.Web.Http.HttpGet]
        public void mail(string mailid)
        {

            byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(mailid);
            string encodedMailId = System.Convert.ToBase64String(mybyte);

            MailMessage mm = new MailMessage();
            mm.To.Add(new MailAddress(mailid, "Request for Verification"));
            mm.From = new MailAddress("zas.donotreply@gmail.com");
            mm.Body = "click here to verify http://217.117.3.12:8283/ClientHome.aspx?LoginId=" + encodedMailId;
            mm.IsBodyHtml = true;
            mm.Subject = "Verification";
            SmtpClient smcl = new SmtpClient();
            smcl.Host = "smtp.gmail.com";
            smcl.Port = 587;
            
           // smcl.Credentials = new NetworkCredential("zas.donotreply@gmail.com", "$$test1234$$");
            smcl.Credentials = new NetworkCredential("ajit.nair@cg-infotech.com", "ganpati@450");

           // smcl.UseDefaultCredentials = false;
            smcl.EnableSsl = true;
            smcl.SendMailAsync(mm);
            
        //    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        ////    smtp.EnableSsl = true;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = new NetworkCredential("zas.donotreply@gmail.com", "$$test1234$$");

        //    MailMessage msg = new MailMessage("zas.donotreply@gmail.com", "saurabhbansal2004@gmail.com");
        //    msg.Subject = "hello";
        //    msg.Body = "world";

        //    smtp.Send(msg);

           // return true;
        }

         [System.Web.Http.HttpGet]
         public JObject ClientPasswordmail(string mailid, string password, string FirstName)
         {
             string main_ret = "";
             byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(mailid);
             string encodedMailId = System.Convert.ToBase64String(mybyte);

             MailMessage mm = new MailMessage();
            // mm.To.Add(new MailAddress(mailid, "Request for Password Verification"));
             mm.To.Add(new MailAddress(mailid, mailid.Split('@')[0].ToString()));
             mm.From = new MailAddress("tmssupport@lagosstate.gov.ng", "tmssupport@lagosstate.gov.ng");
             //  mm.Body = "click here to verify http://217.117.3.12:8283/ClientHome.aspx?LoginId=" + encodedMailId;

            // mm.Body = "<span style='font-family:Georgia'><b><I> Dear " + FirstName + ",<br/><br/> Welcome to TMS.<br/<br/> <br/<br/> Kindly see your login details below: <br/><br/> UserName: " + mailid + " <br/> E-mail verification Password: " + password + " <br/><br/> For support and enquiries please call 07000-TRAFFIC or email - support@tms.lagosstate.gov.ng <br/><br/> Best Regards, <br/> TMS Team </I></b></span>";

             mm.Body = "<span style='font-family:Georgia'><I> Dear " + FirstName + ",<br/><br/> Welcome to TMS.<br/<br/> <br/<br/> Kindly see your login details below: <br/><br/> <b>UserName:</b> " + mailid + " <br/> <b>E-mail verification Password:</b> " + password + " <br/><br/> <b>Note:</b> Please use this OTP for your first time log in after sign up. <br/><br/> For support and enquiries please call 07000-TRAFFIC or email - tmssupport@lagosstate.gov.ng <br/><br/> Best Regards, <br/> TMS Team </I></span>";
             mm.IsBodyHtml = true;
             mm.Subject = "E-Mail Verification Password";
             SmtpClient smcl = new SmtpClient();


             smcl.Host = "smtp.gmail.com";
             //smcl.Host = "mail.lagosstate.gov.ng";
            // smcl.Port = 25;  //587
             smcl.Port = 587;  
             smcl.Credentials = new NetworkCredential("tvms.donotreply@gmail.com", "$TMS@SB2019");
            // smcl.Credentials = new NetworkCredential("tms-noreply@lagosstate.gov.ng", "Artsolng");
             // smcl.Credentials = new NetworkCredential("zas.donotreply@gmail.com", "$$test1234$$");
             //smcl.Credentials = new NetworkCredential("ajit.nair@cg-infotech.com", "ganpati@450");

             // smcl.UseDefaultCredentials = false;
             smcl.EnableSsl = false;
             smcl.Send(mm);

             main_ret = main_ret + "{\"Result\":\"Please check your mail for onetime password.\"}";
             return JObject.Parse(main_ret);
         }



         [System.Web.Http.HttpGet]
         public JObject ClientPassword_UpdateVerifymail(string mailid, string password, string FirstName, string flag_Email_Phone)
         {
             string main_ret = "";
             byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(mailid);
             string encodedMailId = System.Convert.ToBase64String(mybyte);
             string qry = "";
             DataTable table = new DataTable();
             SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
             if (flag_Email_Phone == "E")
                 qry = "select * FROM tblClientUserMaster where EmailId='" + mailid + "'";
             else
                 qry = "select * FROM tblClientUserMaster where PhoneNumber='" + mailid + "'";

             using (SqlCommand cmd = new SqlCommand(qry, cn1))

             using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
             {
                 adapter.Fill(table);
             }

             if (table.Rows.Count > 0)
             {
                 if (flag_Email_Phone == "E")
                 {
                     main_ret = main_ret + "{\"Result\":\"Email already exists.\",\"Status\":0}";
                 }
                 else
                 {
                     main_ret = main_ret + "{\"Result\":\"Phone number already exists.\",\"Status\":0}";
                 }
                 return JObject.Parse(main_ret);
             }

             if (flag_Email_Phone == "E")
             {
                 MailMessage mm = new MailMessage();
                 // mm.To.Add(new MailAddress(mailid, "Request for Password Verification"));
                 mm.To.Add(new MailAddress(mailid, mailid.Split('@')[0].ToString()));
                 mm.From = new MailAddress("tmssupport@lagosstate.gov.ng", "tmssupport@lagosstate.gov.ng");
                 //  mm.Body = "click here to verify http://217.117.3.12:8283/ClientHome.aspx?LoginId=" + encodedMailId;

                 // mm.Body = "<span style='font-family:Georgia'><b><I> Dear " + FirstName + ",<br/><br/> Welcome to TMS.<br/<br/> <br/<br/> Kindly see your login details below: <br/><br/> UserName: " + mailid + " <br/> E-mail verification Password: " + password + " <br/><br/> For support and enquiries please call 07000-TRAFFIC or email - support@tms.lagosstate.gov.ng <br/><br/> Best Regards, <br/> TMS Team </I></b></span>";

                 mm.Body = "<span style='font-family:Georgia'><I> Dear " + FirstName + ",<br/><br/> <b>Your E-mail verification password is:</b> " + password + " <br/><br/> <b></b> Please use this OTP to complete your profile update. <br/><br/> For support and enquiries please call 07000-TRAFFIC or email - tmssupport@lagosstate.gov.ng <br/><br/> Best Regards, <br/> TMS Team </I></span>";
                 mm.IsBodyHtml = true;
                 mm.Subject = "E-Mail Verification Password";
                 SmtpClient smcl = new SmtpClient();


                 smcl.Host = "smtp.gmail.com";
                 // smcl.Host = "smtp.mail.lagosstate.gov.ng/";
                 // smcl.Port = 25;  //587
                 smcl.Port = 587;
                 smcl.Credentials = new NetworkCredential("tvms.donotreply@gmail.com", "$TMS@SB2019");
                 // smcl.Credentials = new NetworkCredential("tms-noreply@lagosstate.gov.ng", "Artsolng");

                 // smcl.Credentials = new NetworkCredential("zas.donotreply@gmail.com", "$$test1234$$");
                 //smcl.Credentials = new NetworkCredential("ajit.nair@cg-infotech.com", "ganpati@450");

                 // smcl.UseDefaultCredentials = false;
                 smcl.EnableSsl = true;
                 smcl.Send(mm);

                 //   main_ret = main_ret + "{\"Result\":\"Your One Time Passwords have been forwarded to your updated E-mail address and Phone Number.\",\"Status\":1}";
                 main_ret = main_ret + "{\"Result\":\"Your One Time Password have been forwarded to your updated E-mail address.\",\"Status\":1}";
                 return JObject.Parse(main_ret);
             }
             else
             {
                 main_ret = main_ret + "{\"Result\":\"Phone number not exists.\",\"Status\":1}";
                 return JObject.Parse(main_ret);
             }
         }

         [System.Web.Http.HttpGet]
         public JObject OTP_OnCall(string mobile_no, string OTP)    /* OTP CALL */
         {
             string url = "http://217.117.3.14/tmsotpcall/api/values/otpCall?mobile=" + mobile_no + "&pin=" + OTP;
             string res;
             using (var client = new WebClient())
             {
                 client.Headers[HttpRequestHeader.ContentType] = "application/json";

                 res = client.DownloadString(url);

             }

             return JObject.Parse(res);
         }

    }
}