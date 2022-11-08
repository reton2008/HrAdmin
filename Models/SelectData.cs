using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;

namespace HRAdmin.Models
{
    public class SelectData
    {

        //private OleDbConnection dbConn;
        private OracleConnection conn;
        private OracleCommand cmd;
        private OracleDataAdapter oda;
        private DataSet ds;
        string strComm;

        public DataSet select_data_c7(string package, string call_name, string sel1, string sel2)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = package;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c2", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c3", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c4", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c5", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c6", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c7", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c8", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("call_name", OracleType.VarChar).Value = call_name;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;

            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            oda.Fill(ds);
            conn.Close();
            return ds;

        }

        public DataSet get_pro_data(string package, string call_name, string sel1, string sel2)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = package;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c2", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("call_name", OracleType.VarChar).Value = call_name;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            oda.Fill(ds);
            conn.Close();
            return ds;

        }

        public DataSet select_data_c3(string package, string call_name, string sel1, string sel2)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = package;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c2", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c3", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("call_name", OracleType.VarChar).Value = call_name;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            oda.Fill(ds);
            conn.Close();
            return ds;

        }

        public DataSet select_data_c4(string package, string call_name, string sel1, string sel2)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = package;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c2", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c3", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("cur_c4", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("call_name", OracleType.VarChar).Value = call_name;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            oda.Fill(ds);
            conn.Close();
            return ds;

        }
    }
}