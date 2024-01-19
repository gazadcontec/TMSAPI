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

namespace TVMS_API.Controllers
{
    public class ClientUser_Update_det
    {
        public string UserId { get; set; }
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string DrivingLicNo { get; set; }
        public string Ph_No { get; set; }
        public byte[] image { get; set; }
    }

    public class ClientApiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        TVMS_dataDataContext db = new TVMS_dataDataContext();

       // [System.Web.Http.HttpGet]
        //public JObject getLogin(string u, string p)
        //{
        //    SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
        //    SqlCommand cmd1 = new SqlCommand("select * FROM vw_User_n_FTP_det where LoginId='" + u + "' and password='" + p + "'", cn1);
        //    cmd1.CommandType = CommandType.Text;
        //    //cmd1.ExecuteNonQuery();

        //    DataTable table = new DataTable();


        //    {
        //        using (SqlCommand cmd = new SqlCommand("select * FROM vw_User_n_FTP_det where LoginId='" + u + "' and password='" + p + "'", cn1))


        //        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        //        {
        //            adapter.Fill(table);
        //        }
        //    }




        //    if (table.Rows.Count > 0)
        //    {


        //        login_det login = new login_det
        //        {
        //            status = "1",
        //            userId = table.Rows[0]["UserId"].ToString(),
        //            fname = table.Rows[0]["FirstName"].ToString(),
        //            lname = table.Rows[0]["LastName"].ToString(),
        //            Phone = table.Rows[0]["PhoneNumber"].ToString(),
        //            ftpURL = table.Rows[0]["FtpURL"].ToString(),
        //            ftpPORT = table.Rows[0]["FtpPort"].ToString(),
        //            ftpUser = table.Rows[0]["FtpUsername"].ToString(),
        //            ftpPassword = table.Rows[0]["FtpPassword"].ToString(),
        //            UserTypeCode = table.Rows[0]["UserTypeCode"].ToString(),
        //            WebApp = table.Rows[0]["WebApp"].ToString(),
        //            firstTimeLogin = table.Rows[0]["FirstTimeLogin"].ToString()

        //        };
        //        // StringBuilder x= new StringBuilder();
        //        string x = Convert.ToString(JObject.Parse(@"{""LoginDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

        //        return JObject.Parse(@"{""LoginDetails"" :" + Newtonsoft.Json.JsonConvert.SerializeObject(login) + "}");


        //    }
        //    else
        //    {
        //        return JObject.Parse(@"{""LoginDetails"" :{status:""0""}}");
        //        //  return (@"{LoginDetails : {Status:0}}");
        //    }
        //}



        [System.Web.Http.HttpPost]
        public JObject update_ClientUser_details([FromBody] ClientUser_Update_det ClientUser_Update_det)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_Upd_ClientUserMaster", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", ClientUser_Update_det.UserId);
                cmd.Parameters.AddWithValue("@LoginId", ClientUser_Update_det.LoginId);
                cmd.Parameters.AddWithValue("@FirstName", ClientUser_Update_det.FirstName);
                cmd.Parameters.AddWithValue("@LastName", ClientUser_Update_det.LastName);
                cmd.Parameters.AddWithValue("@email", ClientUser_Update_det.Email);
                cmd.Parameters.AddWithValue("@DOB", ClientUser_Update_det.DOB);
                cmd.Parameters.AddWithValue("@DrivingLicNo", ClientUser_Update_det.DrivingLicNo);
                cmd.Parameters.AddWithValue("@Ph_No", ClientUser_Update_det.Ph_No);
                cmd.Parameters.AddWithValue("@Gender", ClientUser_Update_det.Gender);

                cmd.Parameters.AddWithValue("@userImage", ClientUser_Update_det.image);


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