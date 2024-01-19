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

    /************* Parking *******************/

    public class Parking_Public_det
    {
        public string ParkingId { get; set; }
        public string vehicleNumber { get; set; }
        public string SlotNumber { get; set; }
        public string Duration { get; set; }
        public string Amount { get; set; }
        public string IsPaid { get; set; }
        public DateTime StartOn { get; set; }
        public string CreatedBy { get; set; }
        public string ParkingTicketNo { get; set; }
       // public string EndOn { get; set; }
        public string InsUpdFlag { get; set; }
        public string PhoneNumber { get; set; }
         

    }

    public class Parking_LogOut
    {
        public string UserId { get; set; }
        public string VehicleNo { get; set; }
    }

    public class Parking_Offence_det
    {
        public string OffenceTicketNumber { get; set; }
        public string VehicleNumber { get; set; }
        public int OffenceId { get; set; }
        public string OffenceDetail { get; set; }
        public string TotalPenalty { get; set; }
        public string Img_Vdo_Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ParkingLocationId { get; set; }
        public string SlotNumber { get; set; }
        public string flag_Y { get; set; }

        public string CreatedBy { get; set; }
        

    }

    public class ParkingController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();
        /*************** Parking ********************/

        [System.Web.Http.HttpGet]
        public JObject ParkingSlotList(string parking_id)    /* Slot List */
        {
            parking_id = parking_id.Replace("'", "''");
           
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                string qry = "";
                if (parking_id == "A")
                {
                    //qry = "select * FROM tblParkingSlotMaster where Status=1";
                    qry = "select * FROM vw_Public_Parking_LocSlot";
                }
                else
                {
                    //  qry = "select * FROM tblParkingSlotMaster where Status=1 and ParkingId='" + parking_id + "'";
                    qry = "select * FROM vw_Public_Parking_LocSlot where ParkingLocationId='" + parking_id + "'";
                }
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


                string x = Convert.ToString(JObject.Parse(@"{""ParkingSlotList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""ParkingSlotList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""ParkingSlotList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }



        [System.Web.Http.HttpGet]
        public JObject ParkingSlotList_vacant_Nonvacant(string parking_id)    /* Slot List */
        {
            parking_id = parking_id.Replace("'", "''");
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                string qry = "";
                if (parking_id == "A")
                    qry = "select * FROM vw_ParkingSlotSituation where Status=1 order by slot asc";
                else
                    qry = "select * FROM vw_ParkingSlotSituation where Status=1 and ParkingId='" + parking_id + "' order by slot asc";

                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {
                //  table.Columns.Remove("CreatedBy");
                table.Columns.Remove("Status");
                //  table.Columns.Remove("CreatedOn");

                table.Columns.Remove("Situation");
                table.Columns.Remove("Expr1");


                string x = Convert.ToString(JObject.Parse(@"{""ParkingSlotList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""ParkingSlotList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""ParkingSlotList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }


        [System.Web.Http.HttpGet]
        public JObject ParkingLocationList()    /* Parking Location List */
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                string qry = "";

                qry = "select * FROM tblParkingSlotMaster where Status=1";


                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {
                table.Columns.Remove("CreatedBy");
                table.Columns.Remove("Status");
                table.Columns.Remove("CreatedOn");

                string x = Convert.ToString(JObject.Parse(@"{""ParkingLocationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""ParkingLocationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""ParkingLocationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }

        [System.Web.Http.HttpPost]
        public JObject PublicParkingAllocation([FromBody] Parking_Public_det ParkingPublicDet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_PublicParking_Allocation", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ParkingId", ParkingPublicDet.ParkingId);
                cmd.Parameters.AddWithValue("@VehicleNo", ParkingPublicDet.vehicleNumber);
                cmd.Parameters.AddWithValue("@SlotNumber", ParkingPublicDet.SlotNumber);
                cmd.Parameters.AddWithValue("@Duration", ParkingPublicDet.Duration);
                cmd.Parameters.AddWithValue("@Amount", ParkingPublicDet.Amount);
                cmd.Parameters.AddWithValue("@IsPaid", ParkingPublicDet.IsPaid);
                cmd.Parameters.AddWithValue("@StartOn",ParkingPublicDet.StartOn);
                cmd.Parameters.AddWithValue("@CreatedBy", ParkingPublicDet.CreatedBy);
                cmd.Parameters.AddWithValue("@PhoneNumber", ParkingPublicDet.PhoneNumber);

               // cmd.Parameters.AddWithValue("@EndOn", ParkingPublicDet.EndOn);
                cmd.Parameters.AddWithValue("@InsUpdFlag", ParkingPublicDet.InsUpdFlag);
                cmd.Parameters.AddWithValue("@ParkingTicketNo", ParkingPublicDet.ParkingTicketNo);


                SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                
                SqlParameter StartDate;
                SqlParameter EndDate;

                StartDate = new SqlParameter("@StartTime", SqlDbType.VarChar, 500);

                StartDate.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(StartDate);

                EndDate = new SqlParameter("@EndTime", SqlDbType.VarChar, 500);

                EndDate.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(EndDate);
                //  cmd.Parameters["@SuccessID"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add(new SqlParameter("@SuccessID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);
                cn.Open();
                cmd.ExecuteNonQuery();

                a = output.Value.ToString();
                cn.Close();
                if (a == "1")
                    main_ret = main_ret + "{\"Result\":\"Success\", \"StartTime\": \"" + StartDate.Value.ToString() + "\", \"EndTime\": \"" + EndDate.Value.ToString() + "\"}";

                else if(a=="2")
                    main_ret = main_ret + "{\"Result\":\"Success\", \"StartTime\": \"" + StartDate.Value.ToString() + "\", \"EndTime\": \"" + EndDate.Value.ToString() + "\"}";
                else if(a=="5")
                    main_ret = main_ret + "{\"Result\":\"This Vehicle Already Parked.\"}";

                else if (a == "6")
                    main_ret = main_ret + "{\"Result\":\"This Slot Already Booked.\"}";

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


        /************************* Parking Allocation in Minutes ***************************/

        [System.Web.Http.HttpPost]
        public JObject PublicParkingAllocation_Minutes([FromBody] Parking_Public_det ParkingPublicDet)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                SqlCommand cmd = new SqlCommand("ADM_INS_PublicParking_Allocation_minutes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ParkingId", ParkingPublicDet.ParkingId);
                cmd.Parameters.AddWithValue("@VehicleNo", ParkingPublicDet.vehicleNumber);
                cmd.Parameters.AddWithValue("@SlotNumber", ParkingPublicDet.SlotNumber);
                cmd.Parameters.AddWithValue("@Duration", ParkingPublicDet.Duration);
                cmd.Parameters.AddWithValue("@Amount", ParkingPublicDet.Amount);
                cmd.Parameters.AddWithValue("@IsPaid", ParkingPublicDet.IsPaid);
                cmd.Parameters.AddWithValue("@StartOn", ParkingPublicDet.StartOn);
                cmd.Parameters.AddWithValue("@CreatedBy", ParkingPublicDet.CreatedBy);
                cmd.Parameters.AddWithValue("@PhoneNumber", ParkingPublicDet.PhoneNumber);

                // cmd.Parameters.AddWithValue("@EndOn", ParkingPublicDet.EndOn);
                cmd.Parameters.AddWithValue("@InsUpdFlag", ParkingPublicDet.InsUpdFlag);
                cmd.Parameters.AddWithValue("@ParkingTicketNo", ParkingPublicDet.ParkingTicketNo);


                SqlParameter output = new SqlParameter("@SuccessID", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                SqlParameter StartDate;
                SqlParameter EndDate;

                StartDate = new SqlParameter("@StartTime", SqlDbType.VarChar, 500);

                StartDate.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(StartDate);

                EndDate = new SqlParameter("@EndTime", SqlDbType.VarChar, 500);

                EndDate.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(EndDate);
                //  cmd.Parameters["@SuccessID"].Direction = ParameterDirection.Output;

                // cmd.Parameters.Add(new SqlParameter("@SuccessID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output);
                cn.Open();
                cmd.ExecuteNonQuery();

                a = output.Value.ToString();
                cn.Close();
                if (a == "1")
                {
                    main_ret = main_ret + "{\"Result\":\"Success\", \"StartTime\": \"" + StartDate.Value.ToString() + "\", \"EndTime\": \"" + EndDate.Value.ToString() + "\"}";
                    NotificationController Notification_controller = new NotificationController();
                    Notification_controller.SendNotification("tikGen", ParkingPublicDet.ParkingTicketNo);
                }
                else if (a == "2")
                    main_ret = main_ret + "{\"Result\":\"Success\", \"StartTime\": \"" + StartDate.Value.ToString() + "\", \"EndTime\": \"" + EndDate.Value.ToString() + "\"}";
                else if (a == "5")
                    main_ret = main_ret + "{\"Result\":\"This Vehicle Already Parked.\"}";

                else if (a == "6")
                    main_ret = main_ret + "{\"Result\":\"This Slot Already Booked.\"}";

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

        /***********************************************************************/





        [System.Web.Http.HttpPost]
        public JObject PublicParkingLogOut(string veh_no, string userId, string type_Client_Officer, string ParkingId)
        {
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            string a;
            string main_ret = "";
            try
            {
                //ADM_Upd_ParkingLogOut

                SqlCommand cmd = new SqlCommand("ADM_Upd_ParkingLogOut", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleNumber", veh_no);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@type_Client_Officer", type_Client_Officer);
                cmd.Parameters.AddWithValue("@parking_id", ParkingId);

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



        /*********************** Parking Offence *******************************/
        [System.Web.Http.HttpGet]

        public JObject ParkingOffenceList()    /* Parking Offence List */
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {
                string qry = "";

                qry = "select * FROM tblParkingOffenceMaster where Status=1";


                using (SqlCommand cmd = new SqlCommand(qry, cn1))


                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {
                table.Columns.Remove("CreatedBy");
                table.Columns.Remove("Status");
                table.Columns.Remove("CreatedOn");

                string x = Convert.ToString(JObject.Parse(@"{""ParkingOffenceList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""ParkingOffenceList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""ParkingLocationList"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }

        [System.Web.Http.HttpPost]
        public JObject InsertParkingOffence([FromBody] Parking_Offence_det[] ParkingPublicOffenceDet)    /* Insert Parking Offence */
        {
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            string a;
            string main_ret = "";
            try
            {

                for (int i = 0; i < ParkingPublicOffenceDet.Length; i++)
                {
                    SqlCommand cmd = new SqlCommand("ADM_INS_ParkingOffenceDetails", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OffenceTicketNumber", ParkingPublicOffenceDet[i].OffenceTicketNumber);
                    cmd.Parameters.AddWithValue("@VehicleNumber", ParkingPublicOffenceDet[i].VehicleNumber);
                    cmd.Parameters.AddWithValue("@OffenceId", ParkingPublicOffenceDet[i].OffenceId);

                    cmd.Parameters.AddWithValue("@OffenceDetail", ParkingPublicOffenceDet[i].OffenceDetail);
                    cmd.Parameters.AddWithValue("@TotalPenalty", ParkingPublicOffenceDet[i].TotalPenalty);
                    cmd.Parameters.AddWithValue("@OffenceVdoImgName", ParkingPublicOffenceDet[i].Img_Vdo_Name);

                    cmd.Parameters.AddWithValue("@Latitude", ParkingPublicOffenceDet[i].Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", ParkingPublicOffenceDet[i].Longitude);
                    cmd.Parameters.AddWithValue("@ParkingLocationId", ParkingPublicOffenceDet[i].ParkingLocationId);
                    cmd.Parameters.AddWithValue("@CreatedBy", ParkingPublicOffenceDet[i].CreatedBy);
                    cmd.Parameters.AddWithValue("@SlotNumber", ParkingPublicOffenceDet[i].SlotNumber);
                    cmd.Parameters.AddWithValue("@flag_Y", ParkingPublicOffenceDet[i].flag_Y);

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
                }
                return JObject.Parse(main_ret);
            }

            catch (Exception ex)
            {
                cn.Close();
                return JObject.Parse(@"{""Server Error""}");
            }
        }



        /****************** Parking Voilation Search *********************/

        [System.Web.Http.HttpGet]
        public JObject ParkingVoilation_veh(string veh_no)   
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {

                using (SqlCommand cmd = new SqlCommand("select OffenceTicketNumber,sum(TotalPenalty) as TotalPenalty FROM tblParkingOffenceDetails where IsPaid<>1 and VehicleNumber='"+veh_no+"' group by OffenceTicketNumber", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {

                string x = Convert.ToString(JObject.Parse(@"{""ParkingvoilationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""ParkingvoilationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""ParkingvoilationDetails"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }

        /****************************************************************/

    }
}
