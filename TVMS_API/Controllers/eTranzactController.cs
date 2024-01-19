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
    public class Bank_eTranzact
    {
        public string Receipt_No{ get; set; }
        public string Payment_Code { get; set; }
        public string Merchant_Code { get; set; }
        public string Trans_Amount { get; set; }
        public string Trans_Date { get; set; }
        public string Trans_Descr { get; set; }
        public string Customer_Id { get; set; }
        public string Bank_Code { get; set; }

        public string Branch_Code { get; set; }
        public string Service_Id { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Address { get; set; }
        public string Teller_Id { get; set; }
        public string Bank_Name { get; set; }
        public string Branch_Name { get; set; }

        public string Channel_Name { get; set; }
        public string Payment_Method_Name { get; set; }
        public string Payment_Currency { get; set; }
        public string Trans_Type { get; set; }
        public string Trans_Fee { get; set; }
        public string Type_Name { get; set; }
        public string Lead_Bank_Code { get; set; }

         public string Lead_Bank_Name { get; set; }
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }
        public string Col5 { get; set; }
        public string Col6 { get; set; }

        public string Col7 { get; set; }
        public string Col8 { get; set; }
        public string Col9 { get; set; }
        public string Col10 { get; set; }
        public string Col11 { get; set; }
        public string Col12 { get; set; }
        public string Col13 { get; set; }

    }

    public class eTranzactController : ApiController
    {
        TVMS_dataDataContext db = new TVMS_dataDataContext();

        [System.Web.Http.HttpPost]

        public JObject BankPayment_eTranzact(string Receipt_No, string Payment_Code, string Merchant_Code, string Trans_Amount, string Trans_Date, string Trans_Descr, string Customer_Id, string Bank_Code, string Branch_Code, string Service_Id, string Customer_Name, string Customer_Address, string Teller_Id, string Bank_Name, string Branch_Name, string Channel_Name, string Payment_Method_Name, string Payment_Currency, string Trans_Type, string Trans_Fee, string Type_Name, string Lead_Bank_Code, string Lead_Bank_Name, string username, string password)
        {
            string a;
            string main_ret = "";
            SqlConnection cn = new SqlConnection(db.Connection.ConnectionString.ToString());

            try
            {
                float test;
                bool Trans_Amt_Check= float.TryParse(Trans_Amount, out test);

                bool Trans_Fee_Check = float.TryParse(Trans_Fee, out test);

                if (Trans_Amt_Check == false || Trans_Fee_Check == false)
                {
                    main_ret = main_ret + "{\"Result\":\"False 4\"}";
                    return JObject.Parse(main_ret);
                }

                if (username == "B_eTran" && password == "p_eTran")
                {
                    if (Receipt_No == "" || Receipt_No == null || Payment_Code == "" || Payment_Code == null || Merchant_Code == "" || Merchant_Code == null || Trans_Amount == "" || Trans_Amount == null || Trans_Date == "" || Trans_Date == null || Trans_Descr == "" || Trans_Descr == null || Customer_Id == "" || Customer_Id == null || Bank_Code == "" || Bank_Code == null || Branch_Code == "" || Branch_Code == null || Service_Id == "" || Service_Id == null || Customer_Address == "" || Customer_Address == null || Teller_Id == "" || Teller_Id == null || Bank_Name == "" || Bank_Name == null || Branch_Name == "" || Branch_Name == null || Channel_Name == "" || Channel_Name == null || Payment_Method_Name == "" || Payment_Method_Name == null || Payment_Currency == "" || Payment_Currency == null || Trans_Type == "" || Trans_Type == null || Trans_Fee == "" || Trans_Fee == null || Type_Name == "" || Type_Name == null || Lead_Bank_Code == "" || Lead_Bank_Code == null || Lead_Bank_Name == "" || Lead_Bank_Name == null || username == "" || username == null || password == "" || password == null)
                    {
                        main_ret = main_ret + "{\"Result\":\"False 3\"}";
                        return JObject.Parse(main_ret);
                    }

                    if (Customer_Name == null)
                        Customer_Name = "";

                    SqlCommand cmd = new SqlCommand("ADM_INS_BankDetails_eTranzact", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Receipt_No", Receipt_No);
                    cmd.Parameters.AddWithValue("@Payment_Code", Payment_Code);
                    cmd.Parameters.AddWithValue("@Merchant_Code", Merchant_Code);
                    cmd.Parameters.AddWithValue("@Trans_Amount", Trans_Amount);
                    cmd.Parameters.AddWithValue("@Trans_Date", Trans_Date);
                    cmd.Parameters.AddWithValue("@Trans_Descr", Trans_Descr);
                    cmd.Parameters.AddWithValue("@Customer_Id", Customer_Id);
                    cmd.Parameters.AddWithValue("@Bank_Code", Bank_Code);
                    cmd.Parameters.AddWithValue("@Branch_Code", Branch_Code);
                    cmd.Parameters.AddWithValue("@Service_Id", Service_Id);
                    cmd.Parameters.AddWithValue("@Customer_Name", Customer_Name);
                    cmd.Parameters.AddWithValue("@Customer_Address", Customer_Address);
                    cmd.Parameters.AddWithValue("@Teller_Id", Teller_Id);
                    cmd.Parameters.AddWithValue("@Bank_Name", Bank_Name);
                    cmd.Parameters.AddWithValue("@Branch_Name", Branch_Name);

                    cmd.Parameters.AddWithValue("@Channel_Name", Channel_Name);
                    cmd.Parameters.AddWithValue("@Payment_Method_Name", Payment_Method_Name);
                    cmd.Parameters.AddWithValue("@Payment_Currency", Payment_Currency);
                    cmd.Parameters.AddWithValue("@Trans_Type", Trans_Type);
                    cmd.Parameters.AddWithValue("@Trans_Fee", Trans_Fee);
                    cmd.Parameters.AddWithValue("@Type_Name", Type_Name);

                    cmd.Parameters.AddWithValue("@Lead_Bank_Code", Lead_Bank_Code);
                    cmd.Parameters.AddWithValue("@Lead_Bank_Name", Lead_Bank_Name);
                    //cmd.Parameters.AddWithValue("@Col1", b_eTranzact.Col1);
                    //cmd.Parameters.AddWithValue("@Col2", b_eTranzact.Col2);
                    //cmd.Parameters.AddWithValue("@Col3", b_eTranzact.Col3);
                    //cmd.Parameters.AddWithValue("@Col4", b_eTranzact.Col4);
                    //cmd.Parameters.AddWithValue("@Col5", b_eTranzact.Col5);
                    //cmd.Parameters.AddWithValue("@Col6", b_eTranzact.Col6);
                    //cmd.Parameters.AddWithValue("@Col7", b_eTranzact.Col7);
                    //cmd.Parameters.AddWithValue("@Col8", b_eTranzact.Col8);
                    //cmd.Parameters.AddWithValue("@Col9", b_eTranzact.Col9);
                    //cmd.Parameters.AddWithValue("@Col10", b_eTranzact.Col10);
                    //cmd.Parameters.AddWithValue("@Col11", b_eTranzact.Col11);
                    //cmd.Parameters.AddWithValue("@Col12", b_eTranzact.Col12);
                    //cmd.Parameters.AddWithValue("@Col13", b_eTranzact.Col13);

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
                        main_ret = main_ret + "{\"Transaction Status\":\"True\"}";

                    else if (a == "2")
                        main_ret = main_ret + "{\"Transaction Status\":\"False 1\"}";

                    else
                        main_ret = main_ret + "{\"Transaction Status\":\"False\"}";

                    return JObject.Parse(main_ret);
                }

                else
                {
                    main_ret = main_ret + "{\"Result\":\"Try Again\"}";
                    return JObject.Parse(main_ret);
                }
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
