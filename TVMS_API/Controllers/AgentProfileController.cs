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
    public class AgentProfile_det
    {
        public string UserId { get; set; }
        public string ph_no { get; set; }
        public string Email { get; set; }

    }

    public class AgentProfileController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();
        

        [System.Web.Http.HttpGet]
        public JObject TotalVoilation_Processed(string UserId)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {

                using (SqlCommand cmd = new SqlCommand("select sum(1) as TotalVoilations, case when IsProcessed=1 then sum(isnull(cast(Isprocessed as int),0)) end as TotalProcessed FROM vw_ClientSearch where UserId='"+UserId+"' group by IsProcessed", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {                
                string x = Convert.ToString(JObject.Parse(@"{""TotalvoilationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""TotalvoilationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""TotalvoilationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }




        [System.Web.Http.HttpGet]
        public JObject TotalVoilation_Details(string UserId,int month, int year)   //Processed, ChallengeAccepted, TotalVoilations
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
               
                 //   using (SqlCommand cmd = new SqlCommand("select sum(1) as TotalVoilations, case when IsProcessed=1 then sum(isnull(cast(Isprocessed as int),0)) end as TotalProcessed FROM vw_ClientSearch where UserId='" + UserId + "' group by IsProcessed", cn1))

                string qry = "";
                if (month == 0 && year == 0)
                {
                    qry = "select * FROM vw_VoilationDetails_MyProfile where UserId='" + UserId + "'";

                    //changes on 22/10/2023
                    qry = "select sum(Total_Captured) as Total_Captured, sum(Total_Processed) as Total_Processed,sum(Total_Discard) as Total_Discard,sum(TotalChallengedSuccess) as TotalChallengedSuccess, sum(isnull(Total_Discarded,0)) as Total_Discarded,sum(isnull(PreviousUserId,0)) as PreviousUserId,sum(DamageCount) as DamageCount from vw_VoilationDetails_MyProfile where UserId='" + UserId + "'";
                }
                //changes on 22/10/2023
                else if (UserId=="0")
                {
                    
                    qry = "select sum(Total_Captured) as Total_Captured, sum(Total_Processed) as Total_Processed,sum(Total_Discard) as Total_Discard,sum(TotalChallengedSuccess) as TotalChallengedSuccess, sum(isnull(Total_Discarded,0)) as Total_Discarded,sum(isnull(PreviousUserId,0)) as PreviousUserId,sum(DamageCount) as DamageCount FROM vw_VoilationDetails_MyProfile where Month=" + month + " and Year=" + year + "";
                }
                else
                {
                    qry = "select * FROM vw_VoilationDetails_MyProfile where UserId='" + UserId + "' and Month=" + month + " and Year=" + year + "";

                    //changes on 22/10/2023
                    qry = "select sum(Total_Captured) as Total_Captured, sum(Total_Processed) as Total_Processed,sum(Total_Discard) as Total_Discard,sum(TotalChallengedSuccess) as TotalChallengedSuccess, sum(isnull(Total_Discarded,0)) as Total_Discarded,sum(isnull(PreviousUserId,0)) as PreviousUserId,sum(DamageCount) as DamageCount FROM vw_VoilationDetails_MyProfile where UserId='" + UserId + "' and Month=" + month + " and Year=" + year + "";
                }
                using (SqlCommand cmd = new SqlCommand(qry, cn1))

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                
               
            }
            //if (table.Rows.Count > 0)
            //{

            //    string x = Convert.ToString(JObject.Parse(@"{""TotalvoilationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
            //    return JObject.Parse(@"{""TotalvoilationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            //}
            //else
            //{
            //    return JObject.Parse(@"{""TotalvoilationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            //}
            string main_ret = "";
            if (table.Rows.Count > 0)
            {
                main_ret = "{\"Status\":1,\"Successfully Processed Violations Target\":\"3000\",\"Ticket Issued Target\":\"500\",\"Processed Violations\":\"" + table.Rows[0]["Total_Processed"].ToString() + "\",\"Ticket Issued\":\"" + table.Rows[0]["Total_Processed"].ToString() + "\",\"Ticket Captured Violations\":\"" + table.Rows[0]["Total_Captured"].ToString() + "\",\"Ticket Discarded Violations\":\"" + table.Rows[0]["Total_Discard"].ToString() + "\",\"Ticket Challenged Violations\":\"" + table.Rows[0]["TotalChallengedSuccess"].ToString() + "\",\"Damaged Devices\":\"" + table.Rows[0]["DamageCount"].ToString() + "\"}";
            }
            //return JObject.Parse(@"{""Penalty Paid Status"" :""Paid"",""Ticket No."":" + table.Rows[0]["TicketNumber"].ToString() + ",\""Fine Value"":" + table.Rows[0]["TicketNumber"].ToString() + "}"); 

            else
            {
                main_ret = "{\"Status\":0,\"Successfully Processed Violations Target\":\"3000\",\"Ticket Issued Target\":\"500\",\"Processed Violations\":\"0\",\"Ticket Issued\":\"0\",\"Ticket Captured Violations\":\"0\",\"Ticket Discarded Violations\":\"0\",\"Ticket Challenged Violations\":\"0\",\"Damaged Devices\":\"0\"}";

            }
            return JObject.Parse(main_ret);
        }



        [System.Web.Http.HttpGet]
        public JObject CalcWorkinghours_MonthYearWise(string UserId)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {

                using (SqlCommand cmd = new SqlCommand("select session_agent_id, Month,Year,sum(TotalHours) as TotalWorkingHours from " +
                "(select session_agent_id,MONTH(session_start_time) as Month, YEAR(session_start_time) as Year,datediff(HH, session_start_time , session_end_time) as TotalHours  from tblLoginSessions where session_agent_id=" + UserId + " and session_end_time is not null) a " +
                "group by session_agent_id, Month,Year", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {

                string x = Convert.ToString(JObject.Parse(@"{""TotalWorkingHours"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""TotalWorkingHours"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""TotalWorkingHours"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }



        [System.Web.Http.HttpGet]
        public JObject UpcomingShifts(string UserId)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {

                using (SqlCommand cmd = new SqlCommand("select UserId,Days,FromTime,ToTime,'' as Location from vw_UpcomingShifts where USERID="+UserId+"", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {

                string x = Convert.ToString(JObject.Parse(@"{""UpcomingShifts"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""UpcomingShifts"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""UpcomingShifts"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }



        [System.Web.Http.HttpPost]
        public JObject UpdateAgentProfile([FromBody] AgentProfile_det AgentProfileDet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_Upd_AgentUserMaster", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", AgentProfileDet.UserId);
                cmd.Parameters.AddWithValue("@ph_no", AgentProfileDet.ph_no);
                cmd.Parameters.AddWithValue("@email", AgentProfileDet.Email);



                SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);


                cn.Open();
                cmd.ExecuteNonQuery();

                a = output.Value.ToString();
                cn.Close();
                if (a == "1")
                    main_ret = main_ret + "{\"Result\":\"Success\"}";

                else if(a=="3")
                    main_ret = main_ret + "{\"Result\":\"LoginId Not Exist\"}";

                else
                    main_ret = main_ret + "{\"Result\":\"Failed\"}";

                return JObject.Parse(main_ret);
            }

            catch (Exception e)
            {
                cn.Close();
                return JObject.Parse(@"{""Server Error""}");
            }
        }

    
        [System.Web.Http.HttpGet]
        public string EncryptStringDeviceUser(string password)
        {
            string salt = "ContecS";
            bool compressText = false;
            byte[] baPwd = Encoding.UTF8.GetBytes(salt);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Encoding.UTF8.GetBytes(password);

            if (compressText)
                baText = Compress(baText);

            byte[] baSalt = GetRandomBytes();
            byte[] baEncrypted = new byte[baSalt.Length + baText.Length];

            // Combine Salt + Text
            for (int i = 0; i < baSalt.Length; i++)
                baEncrypted[i] = baSalt[i];
            for (int i = 0; i < baText.Length; i++)
                baEncrypted[i + baSalt.Length] = baText[i];

            baEncrypted = AES_Encrypt(baEncrypted, baPwdHash);

            string result = Convert.ToBase64String(baEncrypted);
            string newresult;
            if (result.Contains("+"))
            {
                newresult = EncryptStringDeviceUser(password);
                return newresult;
            }
            else
            return result;
        }

         [System.Web.Http.HttpGet]
        public string DecryptStringDeviceUser(string password)
        {
            string salt = "ContecS";
            bool decompressText = false;
            byte[] baPwd = Encoding.UTF8.GetBytes(salt);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Convert.FromBase64String(password);

            byte[] baDecrypted = AES_Decrypt(baText, baPwdHash);

            // Remove salt
            int saltLength = GetSaltLength();
            byte[] baResult = new byte[baDecrypted.Length - saltLength];
            for (int i = 0; i < baResult.Length; i++)
                baResult[i] = baDecrypted[i + saltLength];

            if (decompressText)
                baResult = Decompress(baResult);

            string result = Encoding.UTF8.GetString(baResult);
            return result;
        }
         
        // [System.Web.Http.HttpGet]
         public static string EncryptString(string password)
         {
             string salt = "ContecS";
             bool compressText = false;
             byte[] baPwd = Encoding.UTF8.GetBytes(salt);

             // Hash the password with SHA256
             byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

             byte[] baText = Encoding.UTF8.GetBytes(password);

             if (compressText)
                 baText = Compress(baText);

             byte[] baSalt = GetRandomBytes();
             byte[] baEncrypted = new byte[baSalt.Length + baText.Length];

             // Combine Salt + Text
             for (int i = 0; i < baSalt.Length; i++)
                 baEncrypted[i] = baSalt[i];
             for (int i = 0; i < baText.Length; i++)
                 baEncrypted[i + baSalt.Length] = baText[i];

             baEncrypted = AES_Encrypt(baEncrypted, baPwdHash);

             string result = Convert.ToBase64String(baEncrypted);
             string newresult;
             if (result.Contains("+"))
             {
                 newresult = EncryptString(password);
                 return newresult;
             }
             else
                 return result;
         }

        // [System.Web.Http.HttpGet]
         public static string DecryptString(string password)
         {
             string salt = "ContecS";
             bool decompressText = false;
             byte[] baPwd = Encoding.UTF8.GetBytes(salt);

             // Hash the password with SHA256
             byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

             byte[] baText = Convert.FromBase64String(password);

             byte[] baDecrypted = AES_Decrypt(baText, baPwdHash);

             // Remove salt
             int saltLength = GetSaltLength();
             byte[] baResult = new byte[baDecrypted.Length - saltLength];
             for (int i = 0; i < baResult.Length; i++)
                 baResult[i] = baDecrypted[i + saltLength];

             if (decompressText)
                 baResult = Decompress(baResult);

             string result = Encoding.UTF8.GetString(baResult);
             return result;
         }

         public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
         {
             byte[] encryptedBytes = null;

             // Set your salt here, change it to meet your flavor:
             // The salt bytes must be at least 8 bytes.
             byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

             using (MemoryStream ms = new MemoryStream())
             {
                 using (RijndaelManaged AES = new RijndaelManaged())
                 {
                     AES.KeySize = 256;
                     AES.BlockSize = 128;

                     var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                     AES.Key = key.GetBytes(AES.KeySize / 8);
                     AES.IV = key.GetBytes(AES.BlockSize / 8);

                     AES.Mode = CipherMode.CBC;

                     using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                     {
                         cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                         cs.Close();
                     }
                     encryptedBytes = ms.ToArray();
                 }
             }

             return encryptedBytes;
         }

         public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
         {
             byte[] decryptedBytes = null;

             // Set your salt here, change it to meet your flavor:
             // The salt bytes must be at least 8 bytes.
             byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

             using (MemoryStream ms = new MemoryStream())
             {
                 using (RijndaelManaged AES = new RijndaelManaged())
                 {
                     AES.KeySize = 256;
                     AES.BlockSize = 128;

                     var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                     AES.Key = key.GetBytes(AES.KeySize / 8);
                     AES.IV = key.GetBytes(AES.BlockSize / 8);

                     AES.Mode = CipherMode.CBC;

                     using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                     {
                         cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                         cs.Close();
                     }
                     decryptedBytes = ms.ToArray();
                 }
             }

             return decryptedBytes;
         }

         public static byte[] GetRandomBytes()
         {
             int saltLength = GetSaltLength();
             byte[] ba = new byte[saltLength];
             RNGCryptoServiceProvider.Create().GetBytes(ba);
             return ba;
         }

         public static int GetSaltLength()
         {
             return 8;
         }

         public static byte[] Compress(byte[] data)
         {
             MemoryStream output = new MemoryStream();
             using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
             {
                 dstream.Write(data, 0, data.Length);
             }
             return output.ToArray();
         }

         public static byte[] Decompress(byte[] data)
         {
             MemoryStream input = new MemoryStream(data);
             MemoryStream output = new MemoryStream();
             using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
             {
                 dstream.CopyTo(output);
             }
             return output.ToArray();
         }
    }
}
