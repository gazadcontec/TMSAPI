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

    public class CommonController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();

       [System.Web.Http.HttpGet]
        public void sendemail(string receiveremail, string subject, string body)
        {
            byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(receiveremail);
            string encodedMailId = System.Convert.ToBase64String(mybyte);
            MailMessage mm = new MailMessage();
            mm.To.Add(new MailAddress(receiveremail, subject));
            mm.From = new MailAddress("tmssupport@lagosstate.gov.ng", "tmssupport@lagosstate.gov.ng");
            mm.Body = body;
            mm.IsBodyHtml = true;
            mm.Subject = subject;
            SmtpClient smcl = new SmtpClient();
            smcl.Host = "smtp.gmail.com";
            smcl.Port = 587;
            //smcl.Credentials = new networkcredential("zas.donotreply@gmail.com", "$$test1234$$");
            smcl.Credentials = new NetworkCredential("tvms.donotreply@gmail.com", "$TMS@SB2019");
            smcl.EnableSsl = true;
            smcl.Send(mm);
        }

        [System.Web.Http.HttpGet]
        public JObject TestPasswordmail(string mailid, string password, string FirstName)
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


            //smcl.Host = "smtp.gmail.com";
            smcl.Host = "mail.lagosstate.gov.ng";
            smcl.Port = 25;  //587
            // smcl.Port = 587;  
            // smcl.Credentials = new NetworkCredential("tvms.donotreply@gmail.com", "$TMS@SB2019");
          //  smcl.Credentials = new NetworkCredential("tms-noreply@lagosstate.gov.ng", "Artsolng");

            smcl.Credentials = new NetworkCredential("test.user@lagosstate.gov.ng", "Winte@m234");

            // smcl.Credentials = new NetworkCredential("zas.donotreply@gmail.com", "$$test1234$$");
            //smcl.Credentials = new NetworkCredential("ajit.nair@cg-infotech.com", "ganpati@450");

            // smcl.UseDefaultCredentials = false;
            smcl.EnableSsl = false;
            smcl.Send(mm);

            main_ret = main_ret + "{\"Result\":\"Please check your mail for onetime password.\"}";
            return JObject.Parse(main_ret);
        }


        [System.Web.Http.HttpGet]
        public JObject get_transaction_Amt(float amount)
        {

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            float Transactioncharge = 0;
            {
                string qry = "";
                
                if (amount >= 2500)
                {
                    qry = "select * from tbl_transaction_charge_cal where AmtCondition>=2500";
                    Transactioncharge = (amount / (1 - (float)(0.014))) - amount;
                    if (Transactioncharge >= 2000)
                        Transactioncharge = 2000;
                    
                }
                else
                {
                    qry = "select * from tbl_transaction_charge_cal where AmtCondition<2500";
                   // Transactioncharge = ((amount + 100) / (1 - (float)(0.015))) - amount; updated on 12 sep 2020
                    Transactioncharge = (amount / (1 - (float)(0.014))) - amount;
                    if (Transactioncharge >= 2000)
                        Transactioncharge = 2000;
                }

                Transactioncharge = (float)System.Math.Round(Transactioncharge, 2); 

                using (SqlCommand cmd = new SqlCommand(qry, cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }

            }
            string x = "";
           // if (table.Rows.Count > 0)
            {
                //string cal_formula=table.Rows[0]["TransactionCharge_cal"].ToString();
                //cal_formula=cal_formula.Replace("{Payment Amount}",Convert.ToString(amount));
                //double total_amt = Convert.ToDouble(cal_formula);
               //  x = Convert.ToString(JObject.Parse(@"{""TotalAmt"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
               // return JObject.Parse(@"{""TotalAmt"" :" + (JsonConvert.SerializeObject(x, Newtonsoft.Json.Formatting.None)) + "}");

                return JObject.Parse(@"{""Status"":1,""TransactionCharge"" :" + Transactioncharge + "}");
            }

            //else
            //{
            //    return JObject.Parse(@"{""TotalAmt"" :" + (JsonConvert.SerializeObject(x, Newtonsoft.Json.Formatting.None)) + "}");

            //}
        }


        // search on PaymentRefNo for Violation & Compliance

        [System.Web.Http.HttpGet]
        public JObject Search_Violation_Compliance_PaymentRefNo(string payment_ref_no) 
        {
            string qry = "";
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
               
                {
                    //   qry = "select TicketNumber,sum(TotalPenalty) as FineValue,PenaltyTxnNo FROM tblVoilationTicketGen where PenaltyTxnNo!='' and PenaltyTxnNo='" + txn_no + "' group by PenaltyTxnNo,TicketNumber";

                    qry = "select * from vw_SearchByRefNo where PaymentRefNo='" + payment_ref_no + "'";
                }
                using (SqlCommand cmd = new SqlCommand(qry, cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            string main_ret = "";

            if (table.Rows.Count > 0)
            {
              //  main_ret = "{\"Status\":1,\"Penalty Paid Status\":\"Paid\",\"Ticket No.\":\"" + table.Rows[0]["TicketNumber"].ToString() + "\",\"Fine Value\":\"" + table.Rows[0]["FineValue"].ToString() + "\",\"Date\":\"" + table.Rows[0]["VideoDateTime"].ToString() + "\"}";


                return JObject.Parse(@"{""PaymentRefStatus"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
           

            else
            {
               // main_ret = "{\"Status\":0,\"Penalty Paid Status\":\"Not Paid\",\"Ticket No.\":\"\",\"Fine Value\":\"\",\"Date\":\"\"}";
                return JObject.Parse(@"{""PaymentRefStatus"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            
        }


        [System.Web.Http.HttpGet]
        public JObject Search_Violation_Compliance_TransactionNo(string txn_no)
        {
            string qry = "";
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {

                {
                    //   qry = "select TicketNumber,sum(TotalPenalty) as FineValue,PenaltyTxnNo FROM tblVoilationTicketGen where PenaltyTxnNo!='' and PenaltyTxnNo='" + txn_no + "' group by PenaltyTxnNo,TicketNumber";

                    qry = "select * from vw_SearchByRefNo where PenaltyTxnNo='" + txn_no + "'";
                }
                using (SqlCommand cmd = new SqlCommand(qry, cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            string main_ret = "";

            if (table.Rows.Count > 0)
            {
                //  main_ret = "{\"Status\":1,\"Penalty Paid Status\":\"Paid\",\"Ticket No.\":\"" + table.Rows[0]["TicketNumber"].ToString() + "\",\"Fine Value\":\"" + table.Rows[0]["FineValue"].ToString() + "\",\"Date\":\"" + table.Rows[0]["VideoDateTime"].ToString() + "\"}";

                DataTable table_payment_Id = new DataTable();
                {

                    {
                        //   qry = "select TicketNumber,sum(TotalPenalty) as FineValue,PenaltyTxnNo FROM tblVoilationTicketGen where PenaltyTxnNo!='' and PenaltyTxnNo='" + txn_no + "' group by PenaltyTxnNo,TicketNumber";

                        qry = "select distinct Artizia_payment_id from tbl_payment_details where txn_id='" + txn_no + "'";
                    }
                    using (SqlCommand cmd = new SqlCommand(qry, cn1))

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table_payment_Id);
                    }
                }

              //  table.Columns.Add("ArtiziaPaymentId", typeof(string));
                if (table_payment_Id.Rows.Count > 0)
                {
                    string strColName = "ArtiziaPaymentId";
                    DataColumn colNew = new DataColumn(strColName, typeof(string));
                    colNew.DefaultValue = table_payment_Id.Rows[0]["Artizia_payment_id"].ToString();
                    table.Columns.Add(colNew);
                   // table.Columns["ArtiziaPaymentId Type"].DefaultValue = table_payment_Id.Rows[0]["Artizia_payment_id"].ToString();
                }
                else
                {
                   // table.Columns["ArtiziaPaymentId Type"].DefaultValue = "";
                    string strColName = "ArtiziaPaymentId";
                    DataColumn colNew = new DataColumn(strColName, typeof(string));
                    colNew.DefaultValue = "";
                    table.Columns.Add(colNew);
                }
                

                return JObject.Parse(@"{""Transaction_Payment_Status"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }


            else
            {
                // main_ret = "{\"Status\":0,\"Penalty Paid Status\":\"Not Paid\",\"Ticket No.\":\"\",\"Fine Value\":\"\",\"Date\":\"\"}";
                return JObject.Parse(@"{""Transaction_Payment_Status"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }

        }
    }
}
