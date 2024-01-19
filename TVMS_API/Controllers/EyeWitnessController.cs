using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Threading;
using System.Globalization;

namespace TVMS_API.Controllers
{

    public class EyeWitness_Det
    {
        public string EyeWitness_Options { get; set; }
        public string EyeWitness_VdoFileName { get; set; }
        public string EyeWitness_ImgfileName { get; set; }
        public string EyeWitness_Comments { get; set; }
        public string App_Flag { get; set; }
        public string Location { get; set; }
        public int CreatedBy { get; set; }

    }

    public class EyeWitnessController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();


        [System.Web.Http.HttpGet]
        public JObject EyeWitnessList(string val)    /* EyeWitness Options */
        {
            SqlConnection cn1 = new SqlConnection(db.Connection.ConnectionString.ToString());
            DataTable table = new DataTable();
            {

                using (SqlCommand cmd = new SqlCommand("select Q_Id,Q_Description from tblEyeWitness where Status=1 and (DisplayStatus='B' or DisplayStatus='"+val+"')", cn1))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }
            if (table.Rows.Count > 0)
            {

                string x = Convert.ToString(JObject.Parse(@"{""EyeWitnessOptions"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.Indented)) + "}"));
                return JObject.Parse(@"{""EyeWitnessOptions"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");
            }
            else
            {
                return JObject.Parse(@"{""EyeWitnessOptions"" :" + (JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None)) + "}");

            }
        }


        [System.Web.Http.HttpPost]
        public JObject InsertEyewitnessDetails(EyeWitness_Det EyeWitness_Det)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                //S
                string RefNo = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                if (EyeWitness_Det.App_Flag == "C")
                    RefNo = "EW" + RefNo;
                else
                    RefNo = "DR" + RefNo;

                if (EyeWitness_Det.EyeWitness_Options.Contains(",2,"))
                {
                    EyeWitness_Det.EyeWitness_Options = "2," + EyeWitness_Det.EyeWitness_Options.Replace(",2,", ",");
                }

                SqlCommand cmd = new SqlCommand("ADM_INS_EyeWitness", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EyeWitness_Options", EyeWitness_Det.EyeWitness_Options);
                cmd.Parameters.AddWithValue("@EyeWitness_VdoFileName", EyeWitness_Det.EyeWitness_VdoFileName);
                cmd.Parameters.AddWithValue("@EyeWitness_ImgfileName", EyeWitness_Det.EyeWitness_ImgfileName);
                cmd.Parameters.AddWithValue("@EyeWitness_Comments", EyeWitness_Det.EyeWitness_Comments);
                cmd.Parameters.AddWithValue("@App_Flag", EyeWitness_Det.App_Flag);
                cmd.Parameters.AddWithValue("@Location", EyeWitness_Det.Location);
                cmd.Parameters.AddWithValue("@EyeWitness_RefNo", RefNo);
                cmd.Parameters.AddWithValue("@CreatedBy", EyeWitness_Det.CreatedBy);


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
