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
    public class BankITEX
    {
        public string TicketNumber { get; set; }
        public string Amount { get; set; }
        public string TerminalId { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string PAN { get; set; }
        public string RRN { get; set; }
        public string STAN { get; set; }
        public string AuthCode { get; set; }
        public DateTime TransactionTime { get; set; }
        public string Reversal { get; set; }
        public string MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string MerchantAddress { get; set; }
        public string App_Bank { get; set; }

    }

    public class PayStackController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();


        [System.Web.Http.HttpPost]
        public JObject getPayee_ETRANZACT(string payeeid, string token, string paymenttype)
        {
            if (token == "cohgGE9G_kBK1h79yc95NVh0bfyW2a4KSJGAObucjUIGUxsgoqNzAAB1yPNfNWLez4zoiOF4EhHO43ZPQgqTxRZIaqBYM0xSndOCW8OXiZKOkpJOhzsYxiiTt8ESkyfkj_SFBmC8HKWG5xFzgxaJvixQLC8VVXUQOMaALIvzV22wglL7rtY2EEY3KF9gAXNOvjar2iB-rIxGg_WEVEAMpDtChV6QOcM-umuuhpmxuw_HO02dBF6Ontw20Ok3Fzv7cRUN6VAGe4DkJQFOMYc4kLAl2C99p-gjgeYFJun9on64MaHHzwYK1x-eq_RAFsIh")
            {
                string p_type = "";
                if (paymenttype == "VIS")
                    p_type = "Compliance";
                else if (paymenttype == "LASTMA")
                    p_type = "Violation";

                SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
                DataTable table = new DataTable();
                {
                    string qry = "";
                    if (payeeid.ToString().Substring(0, 2) == "VT")
                        qry = "SELECT PrintedTicketNo as TicketNo, VehicleNo, Veh_No_Customer_Detail, ChesisNo, RegisteredName, RegisteredAddress, Model, MobileNo, PenaltyTxnNo, sum(OffencePenalty) as OffencePenalty, sum(RevisedPenalty) as RevisedPenalty, Type, PrintedTicketNo  FROM vw_TicketDet_ForBank  "+
" where (TicketNo='" + payeeid + "' or PrintedTicketNo='" + payeeid + "' or PenaltyTxnNo='" + payeeid + "') and Type='" + p_type + "' "+
" group by VehicleNo, Veh_No_Customer_Detail, ChesisNo, RegisteredName, RegisteredAddress, Model, MobileNo, PenaltyTxnNo,Type, PrintedTicketNo ";
                    else
                        qry = "select * FROM vw_TicketDet_ForBank where (TicketNo='" + payeeid + "' or PrintedTicketNo='" + payeeid + "' or PenaltyTxnNo='" + payeeid + "') and Type='" + p_type + "'";

                   // using (SqlCommand cmd = new SqlCommand("select * FROM vw_TicketDet_ForBank where (TicketNo='" + payeeid + "' or PrintedTicketNo='" + payeeid + "' or PenaltyTxnNo='"+payeeid+"') and Type='" + p_type + "'", cn1))

                    using (SqlCommand cmd = new SqlCommand(qry, cn1))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }



                {

                    return JObject.Parse(@"{""ViolationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
                }
            }
            else
                return JObject.Parse(@"{""ViolationDetails"" :""No Record Found""}");

        }

      //  [Authorize(Roles = "user, user, User")]

       // [Authorize]
        [System.Web.Http.HttpPost]  //ITEX
        public JObject PaymentIntegration(string payeeid, string token)
        {
            if (token == "cohgGE9G_kBK1h79yc95NVh0bfyW2a4KSJGAObucjUIGUxsgoqNzAAB1yPNfNWLez4zoiOF4EhHO43ZPQgqTxRZIaqBYM0xSndOCW8OXiZKOkpJOhzsYxiiTt8ESkyfkj_SFBmC8HKWG5xFzgxaJvixQLC8VVXUQOMaALIvzV22wglL7rtY2EEY3KF9gAXNOvjar2iB-rIxGg_WEVEAMpDtChV6QOcM-umuuhpmxuw_HO02dBF6Ontw20Ok3Fzv7cRUN6VAGe4DkJQFOMYc4kLAl2C99p-gjgeYFJun9on64MaHHzwYK1x-eq_RAFsIh")
            {
                SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
                DataTable table = new DataTable();
                {
                    using (SqlCommand cmd = new SqlCommand("select * FROM vw_TicketDet_ForBank where TicketNo='" + payeeid + "' or PrintedTicketNo='" + payeeid + "' or PenaltyTxnNo='" + payeeid + "'", cn1))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }



                {

                    return JObject.Parse(@"{""ViolationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
                }
            }
            else
                return JObject.Parse(@"{""ViolationDetails"" :""No Record Found""}");

        }


        [System.Web.Http.HttpPost]
        public JObject BankPaymentITEX(BankITEX bITEX)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_BankDetailsITEX", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TicketNumber", bITEX.TicketNumber);
                cmd.Parameters.AddWithValue("@Amount", bITEX.Amount);
                cmd.Parameters.AddWithValue("@TerminalId", bITEX.TerminalId);
                cmd.Parameters.AddWithValue("@ResponseCode", bITEX.ResponseCode);
                cmd.Parameters.AddWithValue("@ResponseDescription", bITEX.ResponseDescription);
                cmd.Parameters.AddWithValue("@PAN", bITEX.PAN);
                cmd.Parameters.AddWithValue("@RRN", bITEX.RRN);
                cmd.Parameters.AddWithValue("@STAN", bITEX.STAN);
                cmd.Parameters.AddWithValue("@AuthCode", bITEX.AuthCode);
                cmd.Parameters.AddWithValue("@TransactionTime", bITEX.TransactionTime);                
                cmd.Parameters.AddWithValue("@Reversal", bITEX.Reversal);
                cmd.Parameters.AddWithValue("@MerchantId", bITEX.MerchantId);
                cmd.Parameters.AddWithValue("@MerchantName", bITEX.MerchantName);
                cmd.Parameters.AddWithValue("@MerchantAddress", bITEX.MerchantAddress);
                cmd.Parameters.AddWithValue("@App_Bank", bITEX.App_Bank);

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
    }
}
