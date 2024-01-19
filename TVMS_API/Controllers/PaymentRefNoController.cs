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
    public class PaymentRefNo
    {
        public string PaymentRefNumber { get; set; }       
    }

    public class PaymentRefNoController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();


        [System.Web.Http.HttpGet]
        public JObject getPaymentRefNo()
        {
           
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("select convert(varchar(4),convert(int,substring(CONVERT(VARCHAR(4),GETDATE(),112),1,2))+convert(int,substring(CONVERT(VARCHAR(4),GETDATE(),112),3,2)))+"+
//"CONVERT(VARCHAR(2),GETDATE(),113)+CONVERT(VARCHAR(2),DATEPART(DAY,GETDATE()))+CONVERT(VARCHAR,DATEPART(hh,GETDATE()))+"+
                    "CONVERT(VARCHAR(2),DATEPART(MONTH,GETDATE()))+CONVERT(VARCHAR(2),DATEPART(DAY,GETDATE()))+CONVERT(VARCHAR,DATEPART(hh,GETDATE()))+" +
"CONVERT(VARCHAR,DATEPART(MINUTE,GETDATE()))+CONVERT(VARCHAR,DATEPART(S,GETDATE()))+CONVERT(VARCHAR,DATEPART(MS,GETDATE()))", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            PaymentRefNo paymentRefNo = new PaymentRefNo();
            paymentRefNo.PaymentRefNumber = table.Rows[0][0].ToString();
            //   if (table.Rows.Count > 0)
            {
                
               // return JObject.Parse(@"{""PaymentRefNo"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

                return JObject.Parse(@"{""PaymentRefNo"" :" + paymentRefNo.PaymentRefNumber + "}");
            }

        }



        [System.Web.Http.HttpPost]
        public JObject update_ViolationPayement(string ticket_no, string txn_id)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_UPD_ViolationPayment", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TicketNumber", ticket_no);
                cmd.Parameters.AddWithValue("@PenaltyTxnNo", txn_id);

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

                main_ret = main_ret + "{\"Result\":\"Server Error\"}";
                return JObject.Parse(main_ret);
            }
        }


        [System.Web.Http.HttpPost]
        public JObject update_AllPayements(string ticket_no, string txn_id, string module_flag, float transaction_charge, float penalty, string userid, string email)  // Voilation=V, Compliance=C, Tolling=T, Parking=P 
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                string result = "";
                string url = "";

                //using (var client = new WebClient())
                //{                    
                //    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                //  //  client.Headers[HttpRequestHeader.Authorization] = "Bearer sk_test_7756d2fb3bb694bdcfd6434b23ffe0878b9d3817";
                //    client.Headers.Add("Authorization", "Bearer sk_test_7756d2fb3bb694bdcfd6434b23ffe0878b9d3817");
                //    result = client.DownloadString("http://api.paystack.co/transaction/verify/" + txn_id);
                //}

                //result = JsonConvert.DeserializeObject(result).ToString();

                result = paystack_verify(txn_id, module_flag);

                if (result == "0")
                {
                   // main_ret = main_ret + "{\"Status\":0,\"Result\":\"Verification Unsuccessful\"}";
                    main_ret = main_ret + "{\"Status\":0,\"Result\":\"Your payment was Unsuccessful. Please try again.\"}";
                    return JObject.Parse(main_ret); 
                }

                SqlCommand cmd = new SqlCommand("ADM_UPD_AllPayments", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TicketNumber", ticket_no);
                cmd.Parameters.AddWithValue("@PenaltyTxnNo", txn_id);
                cmd.Parameters.AddWithValue("@module_flag", module_flag);
                cmd.Parameters.AddWithValue("@TransactionCharge", transaction_charge);

                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@NotificationText", "Your payment of NGN " + (transaction_charge+penalty) + " was successful. Payment receipt sent to your registered mailid.");

                SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                SqlParameter PaymentRecietNo = new SqlParameter("@Artezia_tik_no", SqlDbType.VarChar, 200);                
                PaymentRecietNo.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(PaymentRecietNo);

                //  cmd.Parameters["@SuccessID"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add(new SqlParameter("@SuccessID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);
                cn.Open();
                cmd.ExecuteNonQuery();

                a = output.Value.ToString();
                cn.Close();

                if (a == "1")
                {
                   // main_ret = main_ret + "{\"Status\":1,\"Result\":\"Verification Successful and Updated\"}";

                  //  main_ret = main_ret + "{\"Status\":1,\"Result\":\"Your payment was successful. Transaction reference number is " + txn_id + " and Payment Id is " + PaymentRecietNo.Value.ToString() + "\"}";

                    main_ret = main_ret + "{\"Status\":1,\"Result\":\"Your payment was successful. Transaction reference number is " + txn_id + " and Payment Id is " + PaymentRecietNo.Value.ToString() + "\",\"PaymentId\":\"" + PaymentRecietNo.Value.ToString() + "\"}";

                    SendPaymentNotification(userid, transaction_charge + penalty);
                    SendMail_Payment(email, userid, "", transaction_charge + penalty, ticket_no, txn_id,PaymentRecietNo.Value.ToString());
                }
                else if (a == "2")
                    main_ret = main_ret + "{\"Status\":0,\"Result\":\"Verification Successful and Ticket No. not exist\"}";
                else
                    main_ret = main_ret + "{\"Status\":0,\"Result\":\"Verification Successful and Failed\"}";

                return JObject.Parse(main_ret);
            }

            catch (Exception ex)
            {
                cn.Close();

                //main_ret = main_ret + "{\"Result\":\"Server Error\"}";
                main_ret = main_ret + "{\"Status\":0,\"Result\":\"Something Wrong\"}";

               // main_ret = main_ret + "{\"Status\":0,\"Result\":\""+ex.Message+"\"}";
                return JObject.Parse(main_ret);
            }
        }

        [System.Web.Http.HttpGet]
        public string paystack_verify(string txn_id, string mod_name)
        {
            string main_ret = "";
            string result = "";
            string url = "";
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
            using (var client = new WebClient())
            {
               // client.UseDefaultCredentials = true;
                //client.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                try
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    // client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    // client.UseDefaultCredentials = false;

                    if(mod_name=="V")
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer sk_test_a46b9db6d8addb1028ea2e6b4ed184eb745892bd";
                        
                    else
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer sk_test_7756d2fb3bb694bdcfd6434b23ffe0878b9d3817";
                    // client.Headers.Add("Authorization", "Bearer sk_test_7756d2fb3bb694bdcfd6434b23ffe0878b9d3817");
                    //  client.Headers.Add("Cookie", "__cfduid=d1c8906536587de8fbf230f6d60ba04801597844193; sails.sid=s%3AhrNopoQSorO5_D9H5-1iMjf0py9Yyazr.32BEd6wLVW7lZCe%2Bfy7dMI4bLPpJz5fTSSQOcbPtUDc");
                    //  client.Credentials = new NetworkCredential("brentwood.developer@artsol.com.ng","Bcontecg1$","test");

                    result = client.DownloadString("https://api.paystack.co/transaction/verify/" + txn_id);
                    if (result.Contains("Verification successful") == true)
                        return "1";
                    else
                        return "0";
                }

                catch (Exception e)
                {
                    main_ret = main_ret + "{\"Status\":\"False\",\"Result\":\"Not Successful\"}";
                    //return JObject.Parse(main_ret);
                    return "0";
                }
            }
            
            
           
        }


        [System.Web.Http.HttpPost]
        public void SendPaymentNotification(string userid,float pay_value)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("SELECT FirebaseId, FirstName FROM tblClientUserMaster WHERE (UserId = '" + userid + "')", cn1))

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
               // var value = message;
                WebRequest tRequest;
                //   tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");

                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                // tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                // string postData = "to:dVsL5Mx1kYg:APA91bG7NYehUnLcDydqK5WLHJUCz-EgBC_YeoUEvW2dxwCvW65GCoT1isA_hV8zXU8iIpuV_bQi1X8OWprVzkXKpPxxuEJuHUHlbLXDSo0KOo1zfj7TeZe5b4gfqBehBW6GMTCs6s_B,collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";

                // string postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"A compliance check  with number " + message + " has just been carried out on your vehicle - " + VehNo + "\",\r\n     \"title\": \"Compliance\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Compliance Notification\",\r\n     \"title\": \"Collapsing A\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}";

                string postData = "{\r\n \"to\" : \"" + SENDER_ID + "\",\r\n \"collapse_key\" : \"type_a\",\r\n \"notification\" : {\r\n     \"body\" : \"Your payment of NGN" + pay_value + " was successful. Payment receipt sent to your registered email.\",\r\n     \"title\": \"Payment\"\r\n },\r\n \"data\" : {\r\n     \"body\" : \"Payment\",\r\n     \"title\": \"Collapsing A\",\r\n     \"key_1\" : \"Data for key one\",\r\n     \"key_2\" : \"Saurabh\"\r\n }}";
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


        [System.Web.Http.HttpGet]
        public JObject SendMail_Payment(string mailid, string UserId, string FirstName,float total_amt, string tik_no,string trans_ref,string payment_reciept_no)
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

            //mm.Body = "<span style='font-family:Georgia'><I> Dear " + FirstName + ",<br/><br/> Welcome to TMS.<br/<br/> <br/<br/> Kindly see your login details below: <br/><br/> <b>UserName:</b> " + mailid + " <br/> <b>E-mail verification Password:</b> 123456 <br/><br/> <b>Note:</b> Please use this OTP for your first time log in after sign up. <br/><br/> For support and enquiries please call 07000-TRAFFIC or email - tmssupport@lagosstate.gov.ng <br/><br/> Best Regards, <br/> TMS Team </I></span>";

            string logo_path = "http://tms.lagosstate.gov.ng/images/LASGLogo.jpeg";
            mm.Body = "<div style='width:40% !important;margin-left:30%;' align='center' id='divreceipt'>" +
                      "<table><tr><td align='center'><img src="+logo_path+" width=60 height=60 />" +
                     "</td></tr><tr><td><div style='align-self:center; align-content:center;'>" +
                     "<div id='divmsg' runat='server'>" +
                     "<label ID='lbl_msg' runat='server' for='name-3' Font-Size='15px'>Your payment of NGN "+total_amt+" was successfull.</label>" +
    "</div></div></td></tr><tr><td><div id='divdeatils' runat='server' style='border: solid 1px;border-radius: 5px;padding: 5px 5px;'>" +
    "<div><table style='width:100%;'><tr><td colspan='2' align='center'>" +
    "<div><div align='center'><label><strong>Payment Receipt</strong></label></div></div>" +
    "</td></tr>"+

    "<tr><td><div><label>Receipt No.:</label></div></td><td><div>" + payment_reciept_no + "</div></td></tr>" +


    "<tr><td><div><label>Transaction Ref.:</label></div></td>" +
    "<td><div>" + trans_ref + "</div></td></tr>" +
    
    "<tr><td><div><label>Payment Id:</label></div></td><td><div>"+tik_no+"</div></td></tr>"+
"<tr><td><div><label for='name-3' class='field-label-2'>Amount Paid:</label></div></td><td><div class='text-block-7'>"+
"NGN&nbsp;"+total_amt+"</div></td> </tr><tr><td><div class='w-col w-col-8'>"+
"<label for='name-3' class='field-label-2'>Date:</label></div></td><td> <div class='text-block-7'>" + DateTime.Now.ToString("dd MMMM yyyy") +
"</div></td></tr>"+
"<tr><td><div><label>Time:</label></div></td><td><div>" + DateTime.Now.ToString("h:mm tt") + "</div></td></tr>" +
"<tr><td colspan='2'><div>This reciept is subject to final verification.</div></td></tr>"+
    "</table>" +
    "</div></div> <br/><div><div><label>If you have any issues with this payment, kindly send an email to tmssupport@lagosstate.gov.ng or call 07000-TRAFFIC</label></div>" +
   "</div></td></tr></table></div>";

            mm.IsBodyHtml = true;
            mm.Subject = "Payment Verification Receipt";
            SmtpClient smcl = new SmtpClient();


            smcl.Host = "smtp.gmail.com";
           // smcl.Host = "mail.lagosstate.gov.ng";
           // smcl.Port = 25;  //587
             smcl.Port = 587;  
             smcl.Credentials = new NetworkCredential("tvms.donotreply@gmail.com", "$TMS@SB2019");
            //  smcl.Credentials = new NetworkCredential("tms-noreply@lagosstate.gov.ng", "Artsolng");

           // smcl.Credentials = new NetworkCredential("test.user@lagosstate.gov.ng", "Winte@m234");

            // smcl.Credentials = new NetworkCredential("zas.donotreply@gmail.com", "$$test1234$$");
            //smcl.Credentials = new NetworkCredential("ajit.nair@cg-infotech.com", "ganpati@450");

           // smcl.UseDefaultCredentials = false;
            smcl.EnableSsl = true;
            smcl.Send(mm);

            main_ret = main_ret + "{\"Result\":\"Please check your mail for onetime password.\"}";
            return JObject.Parse(main_ret);
        }

    }
}
