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
    public class VersionIns
    {
        public string version { get; set; }
        public string forceversion { get; set; }
        public string latestVersion { get; set; }
        public string link { get; set; }
        public string CreatedBy { get; set; }
        
    }

    public class ContactUs
    {
        // public string Name { get; set; }
        public string EmailId { get; set; }
        // public string Phone { get; set; }
        public string Complain { get; set; }

    }

    public class VersionController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();


        [System.Web.Http.HttpGet]
        public JObject getVersion(string version)    /* Version List */
        {
           
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                string qry = "";

                qry = "select Version,ForceVersion,LatestVersion,Link FROM tbl_apk_version where Version='" + version + "'";
                
              
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


                string x = Convert.ToString(JObject.Parse(@"{""VersionList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""VersionList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)).Replace("[","").Replace("]","") + "}");
            }
            else
            {
                return JObject.Parse(@"{""VersionList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)).Replace("[", "").Replace("]", "") + "}");

            }
        }



        [System.Web.Http.HttpPost]
        public JObject VersionUpdate(VersionIns versionUpd)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_APK_Version", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@version", versionUpd.version);
                cmd.Parameters.AddWithValue("@forceversion", versionUpd.forceversion);
                cmd.Parameters.AddWithValue("@latestVersion", versionUpd.latestVersion);
                cmd.Parameters.AddWithValue("@link", versionUpd.link);
                cmd.Parameters.AddWithValue("@CreatedBy", versionUpd.CreatedBy);



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
                    main_ret = main_ret + "{\"Result\":\"Version Already Exist.\"}";

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
        public JObject ContactUs(ContactUs ContactUs)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_Complain", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", "App");
                cmd.Parameters.AddWithValue("@Phone", "");
                cmd.Parameters.AddWithValue("@emailid", ContactUs.EmailId);
                cmd.Parameters.AddWithValue("@complain", ContactUs.Complain);

                SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cn.Open();
                cmd.ExecuteNonQuery();

                a = output.Value.ToString();
                cn.Close();

                if (a == "1")
                {
                    sendemail_contactUs(ContactUs.EmailId, "tmssupport@lagosstate.gov.ng", "Contact Us", ContactUs.Complain);
                    // sendemail_contactUs(ContactUs.EmailId, "saurabh.bansal@contecglobal.com", "Contact Us", ContactUs.Complain);
                   // sendemail_contactUs(ContactUs.EmailId, "anair@contecglobal.com", "Contact Us", ContactUs.Complain);
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

        
        public static void sendemail_contactUs(string sendermail, string receiveremail, string subject, string body)
        {
            byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(receiveremail);
            string encodedMailId = System.Convert.ToBase64String(mybyte);
            MailMessage mm = new MailMessage();
            mm.To.Add(new MailAddress(receiveremail, subject));
            mm.From = new MailAddress(sendermail, sendermail);

            string main_body = body + "<br><br>" + sendermail;
            mm.Body = body;
            mm.IsBodyHtml = true;
            mm.Subject = subject;
            SmtpClient smcl = new SmtpClient();
            smcl.Host = "smtp.gmail.com";
            smcl.Port = 587;
            //smcl.Credentials = new networkcredential("zas.donotreply@gmail.com", "$$test1234$$");
            // smcl.Credentials = new NetworkCredential("tvms.donotreply@gmail.com", "$TMS@SB2019");

            smcl.Credentials = new NetworkCredential("tvms.donotreply@gmail.com", "$TMS@SB2019");
            smcl.EnableSsl = true;
            smcl.Send(mm);
        }
    }
}
