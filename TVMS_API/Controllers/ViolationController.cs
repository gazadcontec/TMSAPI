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
    public class ViolationController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();

        [System.Web.Http.HttpGet]
        public JObject ViolationOffenceList()
        {

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("select * FROM tblOffenceMaster", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }


            {

                return JObject.Parse(@"{""ViolationOffenceList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }

        }

        [System.Web.Http.HttpGet]
        public JObject EnforcementType(string ModName)
        {

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("select * FROM tbl_EnforcementType where EnforcementType_Module='" + ModName + "'", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }


            {

                return JObject.Parse(@"{""EnforcementTypeList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }

        }

        [System.Web.Http.HttpGet]
        public JObject ChallengeReason()
        {

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("select * FROM tbl_ChallengeReason", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }


            {

                return JObject.Parse(@"{""ChallengeReasonList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }

        }

        [System.Web.Http.HttpGet]
        public JObject ViolationTxnRefNo(string txn_no)   //"ALL" for to get all txn no. else condition basis 
        {
            string qry = "";
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                if (txn_no == "ALL")
                    qry = "select * FROM tblVoilationTicketGen";
                else
                {
                 //   qry = "select TicketNumber,sum(TotalPenalty) as FineValue,PenaltyTxnNo FROM tblVoilationTicketGen where PenaltyTxnNo!='' and PenaltyTxnNo='" + txn_no + "' group by PenaltyTxnNo,TicketNumber";

                    qry = "select TicketNumber,sum(TotalPenalty) as FineValue,PenaltyTxnNo,CONVERT(varchar, VideoDateTime, 103) as VideoDateTime FROM tblVoilationTicketGen a inner join tblVoilations b on a.TempTicketNo=b.TempTicketNo where PenaltyTxnNo!='' and PenaltyTxnNo='" + txn_no + "' group by PenaltyTxnNo,TicketNumber,CONVERT(varchar, VideoDateTime, 103)";
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
                main_ret = "{\"Status\":1,\"Penalty Paid Status\":\"Paid\",\"Ticket No.\":\"" + table.Rows[0]["TicketNumber"].ToString() + "\",\"Fine Value\":\"" + table.Rows[0]["FineValue"].ToString() + "\",\"Date\":\"" + table.Rows[0]["VideoDateTime"].ToString() + "\"}";
            }
            //return JObject.Parse(@"{""Penalty Paid Status"" :""Paid"",""Ticket No."":" + table.Rows[0]["TicketNumber"].ToString() + ",\""Fine Value"":" + table.Rows[0]["TicketNumber"].ToString() + "}"); 

            else
            {
                main_ret = "{\"Status\":0,\"Penalty Paid Status\":\"Not Paid\",\"Ticket No.\":\"\",\"Fine Value\":\"\",\"Date\":\"\"}";

            }
            return JObject.Parse(main_ret);
        }
    }
}
