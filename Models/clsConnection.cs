using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;



namespace HRAdmin.Models
{
    public class clsConnection
    {
        public OracleConnection oConn;

        public clsConnection()
        {

        }

        public static string ConnectionSave
        {
            get
            {
                string oradb = "Data Source=HR; User Id=TFLHR; Password =FCN";
                return oradb;

            }
        }
    }
}