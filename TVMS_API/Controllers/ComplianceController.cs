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

    /*************** Complaince ******************/

    public class Complaince_det
    {
        public string VehicleNo { get; set; }
        public string DrivingLicNo { get; set; }
        public string DrivingLicImage { get; set; }
        public string ComplainceAuditId { get; set; }
        public string OffenceVdoImgName { get; set; }
        public string NotifiedBy { get; set; }
        public string DeviceIMEI { get; set; }

        public string locationname { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string WorkingStatus { get; set; }

        public string DriverType { get; set; }

        public string Penalty { get; set; }
        public string PaymentRefNo { get; set; }

        public int IsSpotCheck { get; set; }

    }

    public class IncompleteComplaince_det
    {
        public string VehicleNo { get; set; }
        public string DrivingLicNo { get; set; }
        public string DrivingLicImage { get; set; }
        public string ComplainceAuditId { get; set; }
        public string OffenceVdoImgName { get; set; }
        public string NotifiedBy { get; set; }
        public string DeviceIMEI { get; set; }

        public string locationname { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string WorkingStatus { get; set; }

        public string DriverType { get; set; }

        public string Penalty { get; set; }
        public string PaymentRefNo { get; set; }

        public int IsSpotCheck { get; set; }

    }

    public class Complaince_Enforced_violation_det
    {
        public string TicketNo { get; set; }
        public string vehicleNumber { get; set; }
        public string EnforcedImage { get; set; }
        public string EnforcedBy { get; set; }
        public string ImpoundYardId { get; set; }
        public int EnforcementType { get; set; }

    }

    public class Complaince_Enforced_violation_det_UpdatedFlow
    {
        // public string ImpoundId { get; set; }
        public string vehicleNumber { get; set; }
        public string EnforcedImage { get; set; }
        public string EnforcedBy { get; set; }
        public string ImpoundYardId { get; set; }
        public string ImpoundReason { get; set; }

        public string DeviceIMEI { get; set; }

        public string locationname { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string Penalty { get; set; }

        public int EnforcementType { get; set; }
    }


    public class ComplianceController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();


        [System.Web.Http.HttpGet]
        public JObject ComplainceAuditList(string Veh_No)    /* Questions List */
        {
            Veh_No = Veh_No.Replace("'", "''");
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                // using (SqlCommand cmd = new SqlCommand("select * FROM tbl_Compliance_AuditMaster where Status=1 and '" + Veh_No + "' not in (select top(1) VehicleNo from tbl_ComplianceOffence where VehicleNo='" + Veh_No + "' and WorkingStatus='Not Ok' order by NotifiedOn desc)", cn1))

                // using (SqlCommand cmd = new SqlCommand("select * FROM tbl_Compliance_AuditMaster where Status=1 and '" + Veh_No + "' in (select top(1) VehicleNo from tbl_ComplianceOffence where VehicleNo='" + Veh_No + "' and GETDATE()>DATEADD(d, 7, NotifiedOn) order by NotifiedOn desc)", cn1))

                //                select * FROM tbl_Compliance_AuditMaster where Status=1 and '4349348' in  (select VehicleNo from (select top(1) VehicleNo,DATEADD(d, 7, NotifiedOn) as d,NotifiedOn from tbl_ComplianceOffence where VehicleNo='4349348' order by NotifiedOn desc) a11 where GETDATE()>DATEADD(d, 7, NotifiedOn))
                //and QuestionCode not in (select QuestionCode from (select top(1) VehicleNo,DATEADD(d, 7, NotifiedOn) as d,NotifiedOn from tbl_ComplianceOffence where VehicleNo='4349348' order by NotifiedOn desc) a11 where GETDATE()>DATEADD(d, 7, NotifiedOn))


                // using (SqlCommand cmd = new SqlCommand("select * from (select top(1) VehicleNo,DATEADD(d, 7, NotifiedOn) as d,NotifiedOn from tbl_ComplianceOffence where VehicleNo='" + Veh_No + "' order by NotifiedOn desc) a11 where GETDATE()>DATEADD(d, 7, NotifiedOn)", cn1))

                using (SqlCommand cmd = new SqlCommand("select VehicleNo,DATEADD(d, 8, NotifiedOn) as d,NotifiedOn,DATEDIFF(DAY,GETDATE(), DATEADD(d, 8, NotifiedOn)) AS DaysLeft from tbl_ComplianceOffence where VehicleNo='" + Veh_No + "' order by NotifiedOn desc", cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            string main_ret = "";
            DataTable table_Ques_List = new DataTable();
            if (table.Rows.Count > 0)
            {
                if (Convert.ToInt32(table.Rows[0]["DaysLeft"].ToString()) < 0)
                {
                    //  using (SqlCommand cmd = new SqlCommand("select Q_id,Question,Type_vdo_photo,SubSequentPenalty as Penalty,Currency,QuestionCode from tbl_Compliance_AuditMaster", cn1))


                    // using (SqlCommand cmd = new SqlCommand("select  Q_id,Question,Type_vdo_photo, (case when IsApplyPenalty='P' then Penalty else 0 end) as Penalty,Currency,QuestionCode,PenaltyPoints" +
                    // ",IsApplyPenalty,Comments from  (select  Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints" +
                    //",IsApplyPenalty,Comments from vw_ComplianceViolation where VehicleNo='"+Veh_No+"' union  select Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints" +
                    //",IsApplyPenalty,Comments from tbl_Compliance_AuditMaster where Q_id not in (select Q_id from vw_ComplianceViolation where VehicleNo='"+Veh_No+"')) a", cn1)) 

                    using (SqlCommand cmd = new SqlCommand("select  Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints" +
                     ",ApplyPenaltyOn,Comments,YESConditions,NOConditions,OffenceName from  (select  Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints" +
                     ",ApplyPenaltyOn,Comments,YESConditions,NOConditions,OffenceName,Status from vw_ComplianceViolation where VehicleNo='" + Veh_No + "'" +
                    "union  select Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints" +
                    " ,ApplyPenaltyOn,Comments,YESConditions,NOConditions,OffenceName,Status from tbl_Compliance_AuditMaster where Q_id not in (select Q_id from vw_ComplianceViolation where VehicleNo='" + Veh_No + "')) a where Status=1", cn1))

                    //   using (SqlCommand cmd = new SqlCommand("select distinct Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode from vw_ComplianceViolation where VehicleNo='" + Veh_No + "' or VehicleNo is null", cn1))

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table_Ques_List);
                    }

                    return JObject.Parse(@"{""ComplianceAuditDetails"" :" + (JsonConvert.SerializeObject(table_Ques_List, Newtonsoft.Json.Formatting.None)) + "}");
                }

                else
                {

                    // main_ret = "{\"ComplianceAuditDetails\":\"Not Authorize for Compliance Now.\"}";

                    // return JObject.Parse(main_ret);

                    return JObject.Parse(@"{""ComplianceAuditDetails"" :" + (JsonConvert.SerializeObject(table_Ques_List, Newtonsoft.Json.Formatting.None)) + "}");
                }
            }

            else
            {
                using (SqlCommand cmd = new SqlCommand("select Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints,ApplyPenaltyOn,Comments,YESConditions,NOConditions, OffenceName from tbl_Compliance_AuditMaster where Status=1", cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table_Ques_List);
                }
                return JObject.Parse(@"{""ComplianceAuditDetails"" :" + (JsonConvert.SerializeObject(table_Ques_List, Newtonsoft.Json.Formatting.None)) + "}");
            }

        }


        [System.Web.Http.HttpGet]
        public JObject ComplainceAudit_Check_Paid_NotPaid(string Veh_No, string flag)
        {
            Veh_No = Veh_No.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("select * FROM vw_Check_Paid_Compliance where VehicleNo='" + Veh_No + "' ", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            DataTable table_check_enforced = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("select * FROM vw_Check_Paid_Compliance where VehicleNo='" + Veh_No + "' and Isnull(PenaltyPaid,0)=0 and Isnull(IsEnforced,0)=1 ", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table_check_enforced);
                }
            }

            if (table.Rows.Count > 0)
            {
                if (flag == "All")
                {

                    table.Columns.Remove("ComplianceOffenceId");
                    table.Columns.Remove("Question");

                    table.Columns.Remove("PenaltyPaid");
                    table.Columns.Remove("PenaltyPaidOn");
                    table.Columns.Remove("Latitude");

                    table.Columns.Remove("Longitude");
                    table.Columns.Remove("WorkingStatus");

                    table.Columns.Remove("IsEnforced");
                    //  table.Columns.Remove("EnforcementFlag");

                }
                else
                {
                    table.Columns.Remove("DrivingLicNumber");
                    table.Columns.Remove("ComplianceOffenceId");
                    table.Columns.Remove("Question");
                    table.Columns.Remove("QuestionCode");
                    table.Columns.Remove("PenaltyPaidOn");
                    table.Columns.Remove("Latitude");

                    table.Columns.Remove("Longitude");
                    table.Columns.Remove("LocationName");
                    table.Columns.Remove("WorkingStatus");

                }

                string x = Convert.ToString(JObject.Parse(@"{""ComplianceAudit_Paid_NotPaid_Details"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

                if (table_check_enforced.Rows.Count > 0)
                {
                    // return JObject.Parse("{\"Result\":\"Already Enforced So that Not Searched.\" }{\"ComplianceAudit_Paid_NotPaid_Details\" : [] }");
                    return JObject.Parse("{\"Result\":\"Vehicle Already Enforced.\", \"ComplianceAudit_Paid_NotPaid_Details\":\"[]\"}");
                    //  return JObject.Parse(@"{""ComplianceAudit_Paid_NotPaid_Details"" : [] }");
                }
                else if (flag == "All")
                    return JObject.Parse(@"{""ComplianceAudit_Paid_NotPaid_Details"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
                else
                    return JObject.Parse(@"{""PenaltyPaid"" :" + (JsonConvert.SerializeObject(table.Rows[0]["PenaltyPaid"], Newtonsoft.Json.Formatting.None)) + "}");

            }
            else
            {
                return JObject.Parse(@"{""ComplianceAudit_Paid_NotPaid_Details"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }



        [System.Web.Http.HttpGet]
        public JObject ComplainceAudit_lastPaidUnPaid(string Veh_No, string flag_VehNo_RefNo)    //- Deviceapp
        {
            Veh_No = Veh_No.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand(" select top 1 ComplianceTicketNo,VehicleNo,LocationName, sum(isnull(OffencePenalty,0)) as TotalPenalty, (case when isnull(PenaltyPaid,0)=0 " +
 " then 'UnPaid' else 'Paid' end) as Status, Convert(varchar(10),MAX(CONVERT(date, NotifiedOn)),103) as ComplianceDate , datediff(DAY,MAX(CONVERT(date, NotifiedOn)),getdate()) as Days from " +
 " tbl_ComplianceOffence where VehicleNo='" + Veh_No + "' or PaymentRefNo='" + Veh_No + "' group by ComplianceTicketNo,PenaltyPaid,VehicleNo,LocationName order by MAX(CONVERT(date, NotifiedOn)) desc", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            DataTable table_check_enforced = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("select * FROM vw_Check_Paid_Compliance where VehicleNo='" + Veh_No + "' and Isnull(PenaltyPaid,0)=0 and Isnull(IsEnforced,0)=1 ", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table_check_enforced);
                }
            }

            string x = Convert.ToString(JObject.Parse(@"{""LastCompliance_Details"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));

            if (table_check_enforced.Rows.Count > 0 && flag_VehNo_RefNo == "V")  // V== Vehicle
            {
                // return JObject.Parse("{\"Result\":\"Already Enforced So that Not Searched.\" }{\"ComplianceAudit_Paid_NotPaid_Details\" : [] }");
                return JObject.Parse("{\"Result\":\"Vehicle Already Enforced.\", \"ComplianceAudit_Paid_NotPaid_Details\":\"[]\"}");
                //  return JObject.Parse(@"{""ComplianceAudit_Paid_NotPaid_Details"" : [] }");
            }

            else
            {
                //  return JObject.Parse(@"{""ComplianceAudit_Paid_NotPaid_Details"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
                return JObject.Parse(x);
            }
        }




        [System.Web.Http.HttpPost]
        public JObject update_Complaince_Enforced_voilation_details([FromBody] Complaince_Enforced_violation_det Complaince_Enforced_violationdet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_Upd_Complaince_Enforced_VoilationDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TicketNumber", Complaince_Enforced_violationdet.TicketNo);
                cmd.Parameters.AddWithValue("@VehicleNumber", Complaince_Enforced_violationdet.vehicleNumber);
                cmd.Parameters.AddWithValue("@EnforcedBy", Complaince_Enforced_violationdet.EnforcedBy);
                cmd.Parameters.AddWithValue("@EnforcedImage", Complaince_Enforced_violationdet.EnforcedImage);
                cmd.Parameters.AddWithValue("@ImpoundYardId", Complaince_Enforced_violationdet.ImpoundYardId);
                cmd.Parameters.AddWithValue("@EnforcementType", Complaince_Enforced_violationdet.EnforcementType);

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

                    main_ret = main_ret + "{\"Result\":\"Already Enforced\"}";
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
        public JObject update_Complaince_Enforced_voilation_details_UpdatedFlow([FromBody] Complaince_Enforced_violation_det_UpdatedFlow Complaince_Enforced_violationdet_updatedFlow)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());
            SqlParameter ImpoundId;
            try
            {
                SqlCommand cmd = new SqlCommand("ADM_Ins_Complaince_Enforced_VoilationDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // cmd.Parameters.AddWithValue("@TicketNumber", Complaince_Enforced_violationdet.TicketNo);
                cmd.Parameters.AddWithValue("@VehicleNumber", Complaince_Enforced_violationdet_updatedFlow.vehicleNumber);
                cmd.Parameters.AddWithValue("@EnforcedBy", Complaince_Enforced_violationdet_updatedFlow.EnforcedBy);
                cmd.Parameters.AddWithValue("@EnforcedImage", Complaince_Enforced_violationdet_updatedFlow.EnforcedImage);
                cmd.Parameters.AddWithValue("@ImpoundYardId", Complaince_Enforced_violationdet_updatedFlow.ImpoundYardId);
                cmd.Parameters.AddWithValue("@ImpoundReason", Complaince_Enforced_violationdet_updatedFlow.ImpoundReason);
                cmd.Parameters.AddWithValue("@DeviceIMEI", Complaince_Enforced_violationdet_updatedFlow.DeviceIMEI);
                cmd.Parameters.AddWithValue("@Latitude", Complaince_Enforced_violationdet_updatedFlow.latitude);
                cmd.Parameters.AddWithValue("@Longitude", Complaince_Enforced_violationdet_updatedFlow.longitude);
                cmd.Parameters.AddWithValue("@LocationName", Complaince_Enforced_violationdet_updatedFlow.locationname);
                cmd.Parameters.AddWithValue("@Penalty", Complaince_Enforced_violationdet_updatedFlow.Penalty);
                cmd.Parameters.AddWithValue("@EnforcementType", Complaince_Enforced_violationdet_updatedFlow.EnforcementType);

                ImpoundId = new SqlParameter("@ImpoundId", SqlDbType.VarChar, 500);

                ImpoundId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ImpoundId);

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
                    //   main_ret = main_ret + "{\"Result\":\"Success\"}";
                    main_ret = "{\"Result\":\"Success\", \"ImpoundId\": \"" + ImpoundId.Value.ToString() + "\"}";
                }
                else if (a == "2")

                    main_ret = main_ret + "{\"Result\":\"Already Enforced\"}";
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


        [System.Web.Http.HttpGet]
        public JObject ComplainceList(string Veh_No)
        {
            Veh_No = Veh_No.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("select * FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' or ComplianceTicketNo='" + Veh_No + "'", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }


            //   if (table.Rows.Count > 0)
            {
                // return JObject.Parse("{\"Result\":\"Already Enforced So that Not Searched.\" }{\"ComplianceAudit_Paid_NotPaid_Details\" : [] }");
                //     return JObject.Parse("{\"Result\":\"Vehicle Already Enforced.\", \"ComplianceAudit_Paid_NotPaid_Details\":\"[]\"}");

                return JObject.Parse(@"{""ComplianceList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }

        }

        [System.Web.Http.HttpGet]
        public JObject ComplainceYardList()    /* Yard List (Impound Location) */
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {

                using (SqlCommand cmd = new SqlCommand("select * FROM tbl_ImpoundYard where Status=1 ", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {

                string x = Convert.ToString(JObject.Parse(@"{""ComplianceImpoundDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""ComplianceImpoundDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""ComplianceImpoundDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }

        [System.Web.Http.HttpPost]
        public JObject insert_Complaince_details([FromBody] Complaince_det[] complainceDet)
        {
            string NotificationFlag = "0";
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlParameter ComplianceTicketNo, Payment_RefNo;
                for (int i = 0; i < complainceDet.Length; i++)
                {

                    SqlCommand cmd = new SqlCommand("ADM_INS_ComplainceOffenceDetails", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehicleNo", complainceDet[i].VehicleNo);
                    cmd.Parameters.AddWithValue("@DrivingLicNo", complainceDet[i].DrivingLicNo);
                    cmd.Parameters.AddWithValue("@DrivingLicImgName", complainceDet[i].DrivingLicImage);
                    cmd.Parameters.AddWithValue("@ComplainceAuditId", complainceDet[i].ComplainceAuditId);
                    cmd.Parameters.AddWithValue("@OffenceVdoImgName", complainceDet[i].OffenceVdoImgName);

                    cmd.Parameters.AddWithValue("@NotifiedBy", complainceDet[i].NotifiedBy);
                    cmd.Parameters.AddWithValue("@DeviceIMEI", complainceDet[i].DeviceIMEI);

                    cmd.Parameters.AddWithValue("@Latitude", complainceDet[i].latitude);

                    cmd.Parameters.AddWithValue("@Longitude", complainceDet[i].longitude);
                    cmd.Parameters.AddWithValue("@Locationname", complainceDet[i].locationname);
                    cmd.Parameters.AddWithValue("@WorkingStatus", complainceDet[i].WorkingStatus);
                    cmd.Parameters.AddWithValue("@DriverType", complainceDet[i].DriverType);
                    cmd.Parameters.AddWithValue("@Penalty", complainceDet[i].Penalty);
                    cmd.Parameters.AddWithValue("@PaymentRefNo", complainceDet[i].PaymentRefNo);
                    cmd.Parameters.AddWithValue("@IsSpotcheck", complainceDet[i].IsSpotCheck);

                    SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);


                    ComplianceTicketNo = new SqlParameter("@ComplianceTicketNo", SqlDbType.VarChar, 500);

                    ComplianceTicketNo.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ComplianceTicketNo);


                    //  cmd.Parameters["@SuccessID"].Direction = ParameterDirection.Output;

                    // cmd.Parameters.Add(new SqlParameter("@SuccessID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);
                    cn.Open();
                    cmd.ExecuteNonQuery();

                    a = output.Value.ToString();
                    cn.Close();


                    if (a == "1")
                    {
                        // if (i == 1)
                        if (complainceDet[i].WorkingStatus != "Ok" && NotificationFlag == "0")
                        {
                            NotificationFlag = "1";
                            NotificationController Notification_controller = new NotificationController();
                            Notification_controller.SendComplainceNotification(complainceDet[i].VehicleNo, ComplianceTicketNo.Value.ToString());
                        }
                        main_ret = "{\"Result\":\"Success\", \"ComplianceTicketNo\": \"" + ComplianceTicketNo.Value.ToString() + "\", \"Payment_RefNo\": \"" + complainceDet[i].PaymentRefNo.ToString() + "\"}";
                    }
                    else
                        main_ret = "{\"Result\":\"Failed\"}";

                }

                return JObject.Parse(main_ret);
            }

            catch (Exception ex)
            {
                cn.Close();
                return JObject.Parse("{\"Result\":\"Server Error\"}");
            }
        }


        [System.Web.Http.HttpPost]
        public JObject insert_IncompleteComplaince_details([FromBody] IncompleteComplaince_det[] IncompletecomplainceDet)
        {
            string NotificationFlag = "0";
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlParameter ComplianceTicketNo, Payment_RefNo;
                for (int i = 0; i < IncompletecomplainceDet.Length; i++)
                {

                    SqlCommand cmd = new SqlCommand("ADM_INS_Incomplete_ComplainceOffenceDetails", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VehicleNo", IncompletecomplainceDet[i].VehicleNo);
                    cmd.Parameters.AddWithValue("@DrivingLicNo", IncompletecomplainceDet[i].DrivingLicNo);
                    cmd.Parameters.AddWithValue("@DrivingLicImgName", IncompletecomplainceDet[i].DrivingLicImage);
                    cmd.Parameters.AddWithValue("@ComplainceAuditId", IncompletecomplainceDet[i].ComplainceAuditId);
                    cmd.Parameters.AddWithValue("@OffenceVdoImgName", IncompletecomplainceDet[i].OffenceVdoImgName);

                    cmd.Parameters.AddWithValue("@NotifiedBy", IncompletecomplainceDet[i].NotifiedBy);
                    cmd.Parameters.AddWithValue("@DeviceIMEI", IncompletecomplainceDet[i].DeviceIMEI);

                    cmd.Parameters.AddWithValue("@Latitude", IncompletecomplainceDet[i].latitude);

                    cmd.Parameters.AddWithValue("@Longitude", IncompletecomplainceDet[i].longitude);
                    cmd.Parameters.AddWithValue("@Locationname", IncompletecomplainceDet[i].locationname);
                    cmd.Parameters.AddWithValue("@WorkingStatus", IncompletecomplainceDet[i].WorkingStatus);
                    cmd.Parameters.AddWithValue("@DriverType", IncompletecomplainceDet[i].DriverType);
                    cmd.Parameters.AddWithValue("@Penalty", IncompletecomplainceDet[i].Penalty);
                    cmd.Parameters.AddWithValue("@PaymentRefNo", IncompletecomplainceDet[i].PaymentRefNo);
                    cmd.Parameters.AddWithValue("@IsSpotcheck", IncompletecomplainceDet[i].IsSpotCheck);

                    SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);


                    ComplianceTicketNo = new SqlParameter("@ComplianceTicketNo", SqlDbType.VarChar, 500);

                    ComplianceTicketNo.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ComplianceTicketNo);


                    //  cmd.Parameters["@SuccessID"].Direction = ParameterDirection.Output;

                    // cmd.Parameters.Add(new SqlParameter("@SuccessID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);
                    cn.Open();
                    cmd.ExecuteNonQuery();

                    a = output.Value.ToString();
                    cn.Close();


                    if (a == "1")
                    {

                        //if (IncompletecomplainceDet[i].WorkingStatus != "Ok" && NotificationFlag == "0")
                        //{
                        //    NotificationFlag = "1";
                        //    NotificationController Notification_controller = new NotificationController();
                        //    Notification_controller.SendComplainceNotification(IncompletecomplainceDet[i].VehicleNo, ComplianceTicketNo.Value.ToString());
                        //}
                        main_ret = "{\"Result\":\"Success\", \"ComplianceTicketNo\": \"" + ComplianceTicketNo.Value.ToString() + "\", \"Payment_RefNo\": \"" + IncompletecomplainceDet[i].PaymentRefNo.ToString() + "\"}";
                    }
                    else
                        main_ret = "{\"Result\":\"Failed\"}";

                }

                return JObject.Parse(main_ret);
            }

            catch (Exception ex)
            {
                cn.Close();
                return JObject.Parse("{\"Result\":\"Server Error\"}");
            }
        }


        [System.Web.Http.HttpGet]
        public JObject ComplainceList_web(string Veh_No, string Chessis_No)
        {
            Veh_No = Veh_No.Replace("'", "''");

            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                using (SqlCommand cmd = new SqlCommand("select distinct VehicleNo,ComplianceTicketNo,LocationName FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' and '" + Chessis_No + "' = (select ChesisNo from tbl_Vehicle_Details_Artezia where VehicleNo='" + Veh_No + "') and WorkingStatus='Not Ok'", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }


            //   if (table.Rows.Count > 0)
            {
                // return JObject.Parse("{\"Result\":\"Already Enforced So that Not Searched.\" }{\"ComplianceAudit_Paid_NotPaid_Details\" : [] }");
                //     return JObject.Parse("{\"Result\":\"Vehicle Already Enforced.\", \"ComplianceAudit_Paid_NotPaid_Details\":\"[]\"}");

                return JObject.Parse(@"{""ComplianceList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }

        }




        //[System.Web.Http.HttpGet]
        //public JObject ComplainceList_Unregistered(string Veh_No, int UserId)
        //{
        //    Veh_No = Veh_No.Replace("'", "''");
        //    string query = "";

        //    if (Veh_No == "0")  //Registered
        //    {
        //        query = "select * FROM vw_display_Compliance where VehicleNo in (select VehicleNo from tblVehicleDetails where UserId=" + UserId + ")";
        //    }
        //    else
        //    {
        //        query = "select * FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' and VehicleNo not in (select VehicleNo from tblVehicleDetails where UserId=" + UserId + ")";
        //    }
        //    SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
        //    DataTable table = new DataTable();
        //    {
        //       // using (SqlCommand cmd = new SqlCommand("select * FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' and VehicleNo not in (select VehicleNo from tblVehicleDetails where UserId="+UserId+")", cn1))

        //        using (SqlCommand cmd = new SqlCommand(query, cn1))
        //        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        //        {
        //            adapter.Fill(table);
        //        }
        //    }


        //    //   if (table.Rows.Count > 0)
        //    {
        //        // return JObject.Parse("{\"Result\":\"Already Enforced So that Not Searched.\" }{\"ComplianceAudit_Paid_NotPaid_Details\" : [] }");
        //        //     return JObject.Parse("{\"Result\":\"Vehicle Already Enforced.\", \"ComplianceAudit_Paid_NotPaid_Details\":\"[]\"}");

        //        return JObject.Parse(@"{""ComplianceList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
        //    }

        //}


        [System.Web.Http.HttpGet]
        public JObject ComplainceAuditList_Updated(string Veh_No, string condition)    /* Questions List */
        {
            if (condition == "Private")
            {
                condition = "Comments!='Commercial' and Comments!='TTT' and Comments!='Company'";
            }

            if (condition == "Commercial")
            {
                condition = "Comments!='Private' and Comments!='TTT' and Comments!='Company'";
            }

            if (condition == "TTT")
            {
                condition = "Comments!='Private' and Comments!='Commercial' and Comments!='Company'";
            }
            if (condition == "Company")
            {
                condition = "Comments!='Private' and Comments!='Commercial' and Comments!='TTT'";
            }

            Veh_No = Veh_No.Replace("'", "''");
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                // using (SqlCommand cmd = new SqlCommand("select * FROM tbl_Compliance_AuditMaster where Status=1 and '" + Veh_No + "' not in (select top(1) VehicleNo from tbl_ComplianceOffence where VehicleNo='" + Veh_No + "' and WorkingStatus='Not Ok' order by NotifiedOn desc)", cn1))

                // using (SqlCommand cmd = new SqlCommand("select * FROM tbl_Compliance_AuditMaster where Status=1 and '" + Veh_No + "' in (select top(1) VehicleNo from tbl_ComplianceOffence where VehicleNo='" + Veh_No + "' and GETDATE()>DATEADD(d, 7, NotifiedOn) order by NotifiedOn desc)", cn1))

                //                select * FROM tbl_Compliance_AuditMaster where Status=1 and '4349348' in  (select VehicleNo from (select top(1) VehicleNo,DATEADD(d, 7, NotifiedOn) as d,NotifiedOn from tbl_ComplianceOffence where VehicleNo='4349348' order by NotifiedOn desc) a11 where GETDATE()>DATEADD(d, 7, NotifiedOn))
                //and QuestionCode not in (select QuestionCode from (select top(1) VehicleNo,DATEADD(d, 7, NotifiedOn) as d,NotifiedOn from tbl_ComplianceOffence where VehicleNo='4349348' order by NotifiedOn desc) a11 where GETDATE()>DATEADD(d, 7, NotifiedOn))


                // using (SqlCommand cmd = new SqlCommand("select * from (select top(1) VehicleNo,DATEADD(d, 7, NotifiedOn) as d,NotifiedOn from tbl_ComplianceOffence where VehicleNo='" + Veh_No + "' order by NotifiedOn desc) a11 where GETDATE()>DATEADD(d, 7, NotifiedOn)", cn1))

                using (SqlCommand cmd = new SqlCommand("select VehicleNo,DATEADD(d, 8, NotifiedOn) as d,NotifiedOn,DATEDIFF(DAY,GETDATE(), DATEADD(d, 8, NotifiedOn)) AS DaysLeft from tbl_ComplianceOffence where VehicleNo='" + Veh_No + "' order by NotifiedOn desc", cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            string main_ret = "";
            DataTable table_Ques_List = new DataTable();
            if (table.Rows.Count > 0)
            {
                if (Convert.ToInt32(table.Rows[0]["DaysLeft"].ToString()) < 0)
                {
                    //  using (SqlCommand cmd = new SqlCommand("select Q_id,Question,Type_vdo_photo,SubSequentPenalty as Penalty,Currency,QuestionCode from tbl_Compliance_AuditMaster", cn1))


                    // using (SqlCommand cmd = new SqlCommand("select  Q_id,Question,Type_vdo_photo, (case when IsApplyPenalty='P' then Penalty else 0 end) as Penalty,Currency,QuestionCode,PenaltyPoints" +
                    // ",IsApplyPenalty,Comments from  (select  Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints" +
                    //",IsApplyPenalty,Comments from vw_ComplianceViolation where VehicleNo='"+Veh_No+"' union  select Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints" +
                    //",IsApplyPenalty,Comments from tbl_Compliance_AuditMaster where Q_id not in (select Q_id from vw_ComplianceViolation where VehicleNo='"+Veh_No+"')) a", cn1)) 

                    using (SqlCommand cmd = new SqlCommand("select  Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints" +
                     ",ApplyPenaltyOn,Comments,YESConditions,NOConditions,OffenceName from  (select  Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints" +
                     ",ApplyPenaltyOn,Comments,YESConditions,NOConditions,OffenceName,Status from vw_ComplianceViolation where VehicleNo='" + Veh_No + "'" +
                    "union  select Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints" +
                    " ,ApplyPenaltyOn,Comments,YESConditions,NOConditions,OffenceName,Status from tbl_Compliance_AuditMaster where Q_id not in (select Q_id from vw_ComplianceViolation where VehicleNo='" + Veh_No + "')) a where Status=1 and " + condition, cn1))

                    //   using (SqlCommand cmd = new SqlCommand("select distinct Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode from vw_ComplianceViolation where VehicleNo='" + Veh_No + "' or VehicleNo is null", cn1))

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table_Ques_List);
                    }

                    return JObject.Parse(@"{""ComplianceAuditDetails"" :" + (JsonConvert.SerializeObject(table_Ques_List, Newtonsoft.Json.Formatting.None)) + "}");
                }

                else
                {

                    // main_ret = "{\"ComplianceAuditDetails\":\"Not Authorize for Compliance Now.\"}";

                    // return JObject.Parse(main_ret);

                    return JObject.Parse(@"{""ComplianceAuditDetails"" :" + (JsonConvert.SerializeObject(table_Ques_List, Newtonsoft.Json.Formatting.None)) + "}");
                }
            }

            else
            {
                using (SqlCommand cmd = new SqlCommand("select Q_id,Question,Type_vdo_photo, Penalty,Currency,QuestionCode,PenaltyPoints,ApplyPenaltyOn,Comments,YESConditions,NOConditions, OffenceName from tbl_Compliance_AuditMaster where Status=1 and " + condition, cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table_Ques_List);
                }
                return JObject.Parse(@"{""ComplianceAuditDetails"" :" + (JsonConvert.SerializeObject(table_Ques_List, Newtonsoft.Json.Formatting.None)) + "}");
            }

        }



        [System.Web.Http.HttpGet]
        public JObject ComplainceList_Registered_Unregistered(string Veh_No, int UserId, string flag_Reg)
        {
            Veh_No = Veh_No.Replace("'", "''");
            string query = "";

            if (flag_Reg == "CT")  //ComplianceTicketNo
            {
                query = "select * FROM vw_display_Compliance where ComplianceTicketNo ='" + Veh_No + "' order by NotifiedOn desc";
            }

            else if (flag_Reg == "V")  //PrintedTicketNo (Traffic Violation)
            {
                query = "select TicketNumber,PrintedTicketNo,VehicleNumber,TotalPenalty FROM tblVoilationTicketGen where PrintedTicketNo ='" + Veh_No + "'";
            }

            else if (flag_Reg == "R")
            {
                if (Veh_No == "0")  //Registered userId basis
                {
                    query = "select * FROM vw_display_Compliance where VehicleNo in (select VehicleNo from tblVehicleDetails where UserId=" + UserId + ")";
                }
                else
                { // registered UserId & VehicleNo basis
                    query = "select * FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' and VehicleNo in (select VehicleNo from tblVehicleDetails where UserId=" + UserId + ")";
                }
            }
            else if (flag_Reg == "VEH")  //Vehicle No. (Compliance--- DeviceApp) 
            {
                query = "select * FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' order by NotifiedOn desc";
            }
            else
            {
                query = "select * FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' and VehicleNo not in (select VehicleNo from tblVehicleDetails where UserId=" + UserId + ") order by NotifiedOn desc";
            }
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                // using (SqlCommand cmd = new SqlCommand("select * FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' and VehicleNo not in (select VehicleNo from tblVehicleDetails where UserId="+UserId+")", cn1))

                using (SqlCommand cmd = new SqlCommand(query, cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }


            //   if (table.Rows.Count > 0)
            {
                // return JObject.Parse("{\"Result\":\"Already Enforced So that Not Searched.\" }{\"ComplianceAudit_Paid_NotPaid_Details\" : [] }");
                //     return JObject.Parse("{\"Result\":\"Vehicle Already Enforced.\", \"ComplianceAudit_Paid_NotPaid_Details\":\"[]\"}");

                return JObject.Parse(@"{""ComplianceList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }

        }

        [System.Web.Http.HttpGet]
        public JObject ComplainceList_NoPenalty(string Veh_No, string Chesis_No, string UserId, string flag_Reg)
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            Veh_No = Veh_No.Replace("'", "''");
            string query = "";
            string if_query = "";

            if (flag_Reg == "CT")  //ComplianceTicketNo
            {
                if_query = "select count(*) FROM vw_display_Compliance where ComplianceTicketNo ='" + Veh_No + "' and ActualPenalty!='No Penalty'";
                SqlCommand cmd = new SqlCommand(if_query, cn1);
                cn1.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count == 0)
                {
                    return JObject.Parse(@"{""ComplianceNoPenalty"" :""False"",""ComplianceNoPenalty_Msg"" :""No record found.""}");
                    //  return JObject.Parse(@"{""ComplianceNoPenalty"" :""False"",""ComplianceNoPenalty_Msg"" :""Compliance Ticket No. Not Exist.""}");
                }
                query = "select * FROM vw_display_Compliance where ComplianceTicketNo ='" + Veh_No + "'";
            }
            else if (flag_Reg == "R")
            {
                if_query = "(SELECT (CASE WHEN (select count(*) from tbl_Vehicle_Details_Artezia where VehicleNo='" + Veh_No + "') > 0 THEN 1 ELSE 0 END))";
                SqlCommand cmd = new SqlCommand(if_query, cn1);
                cn1.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count == 0)
                    return JObject.Parse(@"{""ComplianceNoPenalty"" :""False"",""ComplianceNoPenalty_Msg"" :""Wrong VehicleNo""}");

                query = "select * FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' and VehicleNo in (select VehicleNo from tblVehicleDetails where UserId=" + UserId + ") and ActualPenalty!='No Penalty'";
            }
            else
            {
                if_query = "(SELECT (CASE WHEN (select count(*) from tbl_Vehicle_Details_Artezia where VehicleNo='" + Veh_No + "' and ChesisNo='" + Chesis_No + "') > 0 THEN 1 ELSE 0 END))";
                SqlCommand cmd = new SqlCommand(if_query, cn1);
                cn1.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count == 0)
                    return JObject.Parse(@"{""ComplianceNoPenalty"" :""False"",""ComplianceNoPenalty_Msg"" :""Wrong Vehicle Number or Chassis No""}");
                // if ("1"=="(SELECT (CASE WHEN (select count(*) from tbl_Vehicle_Details_Artezia where VehicleNo='" + Veh_No + "' and ChesisNo='" + Chesis_No + "') > 0 THEN 1 ELSE 0 END))")
                query = "select * FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' and 1=(SELECT (CASE WHEN (select count(*) from tbl_Vehicle_Details_Artezia where VehicleNo='" + Veh_No + "' and ChesisNo='" + Chesis_No + "') > 0 THEN 1 ELSE 0 END)) and VehicleNo not in (select VehicleNo from tblVehicleDetails where UserId=" + UserId + ") and ActualPenalty!='No Penalty'";

            }

            DataTable table = new DataTable();
            {
                // using (SqlCommand cmd = new SqlCommand("select * FROM vw_display_Compliance where VehicleNo='" + Veh_No + "' and VehicleNo not in (select VehicleNo from tblVehicleDetails where UserId="+UserId+")", cn1))

                using (SqlCommand cmd = new SqlCommand(query, cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            if (table.Rows.Count > 0)
                return JObject.Parse(@"{""ComplianceNoPenalty"" :""True"",""ComplianceNoPenalty_Msg"" :""Exist""}");
            else
                return JObject.Parse(@"{""ComplianceNoPenalty"" :""False"",""ComplianceNoPenalty_Msg"" :""No record found""}");
        }




        [System.Web.Http.HttpGet]
        public JObject ComplianceStastics(string AgentId, int month, int year)  
        {
            string qry = "";
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                //qry = "select 2000 as Target, sum(Actual) as Actual,sum(Canceled) as Canceled,NotifiedBy from (select sum(1) as Actual, 0 as Canceled,NotifiedBy from (select count(1) as Actual, 0 as Canceled,NotifiedBy,ComplianceTicketNo from tbl_ComplianceOffence group by NotifiedBy,ComplianceTicketNo) a group by  NotifiedBy "+
                //      " union select 0 as Actual, sum(1) as Canceled,NotifiedBy from (select 0 as Actual ,COUNT(1) as Canceled,NotifiedBy,ComplianceTicketNo from tbl_ComplianceOffence_Incomplete group by NotifiedBy,ComplianceTicketNo) b group by  NotifiedBy) c where NotifiedBy="+AgentId+" group by NotifiedBy";

                qry = "select 600 as Target, sum(Actual) as Actual,sum(Canceled) as Canceled,NotifiedBy,ComplianceMonth,ComplianceYear from (select sum(1) as Actual, " +
                      " 0 as Canceled,NotifiedBy,ComplianceMonth,ComplianceYear from (select count(1) as Actual, 0 as Canceled,NotifiedBy,ComplianceTicketNo,month(NotifiedOn) as ComplianceMonth," +
                      " year(NotifiedOn) as ComplianceYear from tbl_ComplianceOffence group by NotifiedBy,ComplianceTicketNo,month(NotifiedOn),year(NotifiedOn)) a group by  NotifiedBy,ComplianceMonth,ComplianceYear" +
                      " union" +
                      " select 0 as Actual, sum(1) as Canceled,NotifiedBy,ComplianceMonth,ComplianceYear from (select 0 as Actual ,COUNT(1) as Canceled,NotifiedBy,ComplianceTicketNo,month(NotifiedOn) as ComplianceMonth,year(NotifiedOn) as ComplianceYear from tbl_ComplianceOffence_Incomplete group by NotifiedBy,ComplianceTicketNo,month(NotifiedOn),year(NotifiedOn)) b group by  NotifiedBy,ComplianceMonth,ComplianceYear) as a" +
                      " where NotifiedBy='" + AgentId + "'  and ComplianceMonth=" + month + " and ComplianceYear=" + year + " group by NotifiedBy,ComplianceMonth,ComplianceYear";

                using (SqlCommand cmd = new SqlCommand(qry, cn1))

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            string main_ret = "";

            if (table.Rows.Count > 0)
            {
                main_ret = "{\"Status\":1,\"Target\":\"" + table.Rows[0]["Target"].ToString() + "\",\"Actual\":\"" + table.Rows[0]["Actual"].ToString() + "\",\"Canceled\":\"" + table.Rows[0]["Canceled"].ToString() + "\",\"Damaged Devices\":\"0\"}";
            }
            //return JObject.Parse(@"{""Penalty Paid Status"" :""Paid"",""Ticket No."":" + table.Rows[0]["TicketNumber"].ToString() + ",\""Fine Value"":" + table.Rows[0]["TicketNumber"].ToString() + "}"); 

            else
            {
                main_ret = "{\"Status\":0,\"Target\":\"600\",\"Actual\":\"0\",\"Canceled\":\"0\",\"Damaged Devices\":\"0\"}";

            }
            return JObject.Parse(main_ret);
        }
    }
}
