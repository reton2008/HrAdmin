using System.Data.OracleClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HRAdmin.Models
{
    public class DataAccess
    {


        //private OleDbConnection dbConn;
        private OracleConnection conn;
        private OracleCommand cmd;
        private OracleDataAdapter oda;
        private DataSet ds;
        string strComm;
        public DataSet read_data(string sql)
        {
            try
            {
                ds = new DataSet();
                conn = new OracleConnection(clsConnection.ConnectionSave);
                conn.Open();
                cmd = new OracleCommand(sql, conn);
                oda = new OracleDataAdapter(cmd);
                oda.Fill(ds);

                conn.Dispose();
                conn.Close();

                return ds;
            }
            catch
            {
                return null;
            }
        }

        public DataSet package_user(string package, string fname, string pass, string uname, string role, string iuser)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("user_name", OracleType.VarChar).Value = fname;
            cmd.Parameters.Add("user_pass", OracleType.VarChar).Value = pass;
            cmd.Parameters.Add("user_enid", OracleType.VarChar).Value = uname;
            cmd.Parameters.Add("user_type", OracleType.VarChar).Value = role;
            cmd.Parameters.Add("iuser", OracleType.VarChar).Value = iuser;
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = iuser;


            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();

            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public void update_user_pro(string oid, byte image)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "update tflhr.t_user set image = :image, user_path = :user_path where oid ='" + oid + "'";
            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("user_path", OracleType.VarChar).Value = oid;
            cmd.Parameters.Add("image", OracleType.Blob).Value = image;
            cmd.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();
        }


        public void update_menu_permission(string user_id, string menu_id, string actv, string oid, string euser)
        {
            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            if (oid == "N")
            {
                strComm = "insert into tflhr.t_umnu (oid, umnu_mnud, umnu_user, umnu_actv, iuser, euser) values "
                    + "(:oid, :umnu_mnud, :umnu_user, :umnu_actv, :iuser, :euser)";

                cmd = new OracleCommand(strComm, conn);
                cmd.Parameters.Add("oid", OracleType.VarChar).Value = "UMNU" + user_id.ToUpper() + menu_id.ToUpper();
                cmd.Parameters.Add("umnu_mnud", OracleType.VarChar).Value = menu_id;
                cmd.Parameters.Add("umnu_user", OracleType.VarChar).Value = user_id;
                cmd.Parameters.Add("umnu_actv", OracleType.VarChar).Value = actv;
                cmd.Parameters.Add("iuser", OracleType.VarChar).Value = euser;
                cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            }
            else
            {
                strComm = "update tflhr.t_umnu set umnu_actv = :umnu_actv, euser = :euser where oid ='" + oid + "'";

                cmd = new OracleCommand(strComm, conn);
                cmd.Parameters.Add("umnu_actv", OracleType.VarChar).Value = actv;
                cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            }
            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }

        public void Create_Company(string oid, string cpname, string sname, string ctype, string addr, string tphone, string fax, string email, string website, string iuser)
        {
            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "insert into tflhr.t_acmp (oid, acmp_text, acmp_name, acmp_bnam, acmp_type, acmp_addr, acmp_teln, acmp_faxn, acmp_mail, acmp_urln, acmp_actv, iuser, euser) values "
                + "(:oid, :acmp_text, :acmp_name, :acmp_bnam, :acmp_type, :acmp_addr, :acmp_teln, :acmp_faxn, :acmp_mail, :acmp_urln, :acmp_actv, :iuser, :euser)";

            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("oid", OracleType.VarChar).Value = "CCOMx" + oid.ToUpper();
            cmd.Parameters.Add("acmp_text", OracleType.VarChar).Value = oid.ToUpper();
            cmd.Parameters.Add("acmp_name", OracleType.VarChar).Value = cpname;
            cmd.Parameters.Add("acmp_bnam", OracleType.VarChar).Value = sname;
            cmd.Parameters.Add("acmp_type", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("acmp_addr", OracleType.VarChar).Value = addr;
            cmd.Parameters.Add("acmp_teln", OracleType.VarChar).Value = tphone;
            cmd.Parameters.Add("acmp_faxn", OracleType.VarChar).Value = fax;
            cmd.Parameters.Add("acmp_mail", OracleType.VarChar).Value = email;
            cmd.Parameters.Add("acmp_urln", OracleType.VarChar).Value = website;
            cmd.Parameters.Add("acmp_actv", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("iuser", OracleType.VarChar).Value = iuser;
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = iuser;

            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }

        public void Update_Company(string oid, string cpname, string sname, string ctype, string addr, string tphone, string fax, string email, string website, string iuser)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            strComm = "update tflhr.t_acmp set acmp_name = :acmp_name, acmp_bnam = :acmp_bnam, acmp_type = :acmp_type, acmp_addr = :acmp_addr, acmp_teln = :acmp_teln, acmp_faxn = :acmp_faxn, acmp_mail = :acmp_mail, acmp_urln = :acmp_urln,euser = :euser  where oid ='" + oid + "'";
            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("acmp_name", OracleType.VarChar).Value = cpname;
            cmd.Parameters.Add("acmp_bnam", OracleType.VarChar).Value = sname;
            cmd.Parameters.Add("acmp_type", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("acmp_addr", OracleType.VarChar).Value = addr;
            cmd.Parameters.Add("acmp_teln", OracleType.VarChar).Value = tphone;
            cmd.Parameters.Add("acmp_faxn", OracleType.VarChar).Value = fax;
            cmd.Parameters.Add("acmp_mail", OracleType.VarChar).Value = email;
            cmd.Parameters.Add("acmp_urln", OracleType.VarChar).Value = website;
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = iuser;

            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }

        public void Create_location(string oid, string lname, string bname, string add1, string add2, string iuser, string euser)
        {
            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "insert into tflhr.t_jloc (oid, jloc_text, jloc_name, jloc_bnam, jloc_add1,jloc_add2,jloc_actv,iuser,euser) values "
                + "(:oid, :jloc_text, :jloc_name, :jloc_bnam, :jloc_add1, :jloc_add2, :jloc_actv, :iuser, :euser)";

            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("oid", OracleType.VarChar).Value = "JLOCx" + oid.ToUpper();
            cmd.Parameters.Add("jloc_text", OracleType.VarChar).Value = oid.ToUpper();
            cmd.Parameters.Add("jloc_name", OracleType.VarChar).Value = lname;
            cmd.Parameters.Add("jloc_bnam", OracleType.VarChar).Value = bname;
            cmd.Parameters.Add("jloc_add1", OracleType.VarChar).Value = add1;
            cmd.Parameters.Add("jloc_add2", OracleType.VarChar).Value = add2;
            cmd.Parameters.Add("jloc_actv", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("iuser", OracleType.VarChar).Value = iuser;
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }

        public void Update_location(string oid, string lname, string bname, string add1, string add2, string euser)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "update tflhr.t_jloc set jloc_name = :jloc_name, jloc_bnam = :jloc_bnam, jloc_add1 = :jloc_add1, jloc_add2 = :jloc_add2, jloc_actv = :jloc_actv,euser = :euser  where oid ='" + oid + "'";
            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("jloc_name", OracleType.VarChar).Value = lname;
            cmd.Parameters.Add("jloc_bnam", OracleType.VarChar).Value = bname;
            cmd.Parameters.Add("jloc_add1", OracleType.VarChar).Value = add1;
            cmd.Parameters.Add("jloc_add2", OracleType.VarChar).Value = add2;
            cmd.Parameters.Add("jloc_actv", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }

        public void Create_dept_type(string oid, string lname, string bname, string euser)
        {
            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "insert into tflhr.t_dtyp (oid, dtyp_text, dtyp_name, dtyp_bnam, dtyp_actv, iuser, euser) values "
                + "(:oid, :dtyp_text, :dtyp_name, :dtyp_bnam, :dtyp_actv, :iuser, :euser)";

            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("oid", OracleType.VarChar).Value = "DTYPx" + oid.ToUpper();
            cmd.Parameters.Add("dtyp_text", OracleType.VarChar).Value = oid.ToUpper();
            cmd.Parameters.Add("dtyp_name", OracleType.VarChar).Value = lname;
            cmd.Parameters.Add("dtyp_bnam", OracleType.VarChar).Value = bname;
            cmd.Parameters.Add("dtyp_actv", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("iuser", OracleType.VarChar).Value = euser;
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }
        public void Update_dept_type(string oid, string dname, string bname, string euser)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "update tflhr.t_dtyp set dtyp_name = :dtyp_name, dtyp_bnam = :dtyp_bnam, dtyp_actv = :dtyp_actv, euser = :euser  where oid ='" + oid + "'";
            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("dtyp_name", OracleType.VarChar).Value = dname;
            cmd.Parameters.Add("dtyp_bnam", OracleType.VarChar).Value = bname;
            cmd.Parameters.Add("dtyp_actv", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }


        public void Create_desg_type(string oid, string lname, string bname, string euser)
        {
            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "insert into tflhr.t_desgtp (oid, desgtp_text, desgtp_name, desgtp_bnam, desgtp_actv, iuser, euser) values "
                + "(:oid, :desgtp_text, :desgtp_name, :desgtp_bnam, :desgtp_actv, :iuser, :euser)";

            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("oid", OracleType.VarChar).Value = "DESGTx" + oid.ToUpper();
            cmd.Parameters.Add("desgtp_text", OracleType.VarChar).Value = oid.ToUpper();
            cmd.Parameters.Add("desgtp_name", OracleType.VarChar).Value = lname;
            cmd.Parameters.Add("desgtp_bnam", OracleType.VarChar).Value = bname;
            cmd.Parameters.Add("desgtp_actv", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("iuser", OracleType.VarChar).Value = euser;
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }
        public void Update_desg_type(string oid, string dname, string bname, string euser)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "update tflhr.t_desgtp set desgtp_name = :desgtp_name, desgtp_bnam = :desgtp_bnam, desgtp_actv = :desgtp_actv, euser = :euser  where oid ='" + oid + "'";
            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("desgtp_name", OracleType.VarChar).Value = dname;
            cmd.Parameters.Add("desgtp_bnam", OracleType.VarChar).Value = bname;
            cmd.Parameters.Add("desgtp_actv", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }


        public void Create_desg(string oid, string dname, string dptp, string bname, string iuser, string euser)
        {
            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "insert into tflhr.t_desg (oid, desg_desgtp, desg_text, desg_name, desg_bnam, desg_actv, iuser, euser) values" +
                "(:oid, :desg_desgtp, :desg_text, :desg_name, :desg_bnam, :desg_actv, :iuser, :euser)";

            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("oid", OracleType.VarChar).Value = "DESGx" + oid.ToUpper();
            cmd.Parameters.Add("desg_desgtp", OracleType.VarChar).Value = dptp;
            cmd.Parameters.Add("desg_text", OracleType.VarChar).Value = oid.ToUpper();
            cmd.Parameters.Add("desg_name", OracleType.VarChar).Value = dname;
            cmd.Parameters.Add("desg_bnam", OracleType.VarChar).Value = bname;
            cmd.Parameters.Add("desg_actv", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("iuser", OracleType.VarChar).Value = iuser;
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }
        public void Update_desg(string oid, string dname, string dptp, string bname, string euser)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "update tflhr.t_desg set desg_desgtp = :desg_desgtp, desg_name = :desg_name, desg_bnam = :desg_bnam, desg_actv = :desg_actv, euser = :euser  where oid ='" + oid + "'";
            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("desg_desgtp", OracleType.VarChar).Value = dptp;
            cmd.Parameters.Add("desg_name", OracleType.VarChar).Value = dname;
            cmd.Parameters.Add("desg_bnam", OracleType.VarChar).Value = bname;
            cmd.Parameters.Add("desg_actv", OracleType.VarChar).Value = "1";
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            cmd.ExecuteNonQuery();

            conn.Dispose();
            conn.Close();
        }

        public DataSet package_save_data(string package, string sel1, string sel2, string sel3, string sel4, string sel5)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();

            conn.Close();
            oda.Fill(ds);
            return ds;

        }





        public DataSet package_update_data(string package, string sel1, string sel2, string sel3, string sel4)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = package;

            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_save_cycl(string package, string sel1, string sel2, string sel3, string sel4, string sel5, string sel6,
            string sel7, string sel8, string sel9)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.DateTime).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.DateTime).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            cmd.Parameters.Add("sel6", OracleType.VarChar).Value = sel6;
            cmd.Parameters.Add("sel7", OracleType.VarChar).Value = sel7;
            cmd.Parameters.Add("sel8", OracleType.VarChar).Value = sel8;
            cmd.Parameters.Add("sel9", OracleType.VarChar).Value = sel9;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();

            conn.Close();
            oda.Fill(ds);
            return ds;

        }


        public DataSet package_update_cycl(string package, string sel1, string sel2, string sel3, string sel4, string sel5, string sel6,
            string sel7, string sel8, string sel9)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = package;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.DateTime).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.DateTime).Value = sel5;
            cmd.Parameters.Add("sel6", OracleType.VarChar).Value = sel6;
            cmd.Parameters.Add("sel7", OracleType.VarChar).Value = sel7;
            cmd.Parameters.Add("sel8", OracleType.VarChar).Value = sel8;
            cmd.Parameters.Add("sel9", OracleType.VarChar).Value = sel9;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_save_Hday(string package, string sel1, string sel2, string sel3, string sel4)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.DateTime).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();

            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_update_hday(string package, string sel1, string sel2, string sel3, string sel4, string sel5)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = package;

            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.DateTime).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_save_Salary(string package, string sel1, string sel2, string sel3, string sel4, string sel5, string sel6,
            string sel7, string sel8, string sel9)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;

            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            cmd.Parameters.Add("sel6", OracleType.VarChar).Value = sel6;
            cmd.Parameters.Add("sel7", OracleType.VarChar).Value = sel7;
            cmd.Parameters.Add("sel8", OracleType.VarChar).Value = sel8;
            cmd.Parameters.Add("sel9", OracleType.VarChar).Value = sel9;

            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_update_salary(string package, string sel1, string sel2, string sel3, string sel4, string sel5, string sel6,
            string sel7, string sel8, string sel9, string sel10)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = package;

            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            cmd.Parameters.Add("sel6", OracleType.VarChar).Value = sel6;
            cmd.Parameters.Add("sel7", OracleType.VarChar).Value = sel7;
            cmd.Parameters.Add("sel8", OracleType.VarChar).Value = sel8;
            cmd.Parameters.Add("sel9", OracleType.VarChar).Value = sel9;
            cmd.Parameters.Add("sel10", OracleType.VarChar).Value = sel10;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }


        public DataSet package_save_LeaveType(string package, string sel1, string sel2, string sel3, string sel4)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;

            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;

            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }


        public DataSet Update_dept_Leavetype(string package, string sel1, string sel2, string sel3, string sel4, string sel5)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = package;

            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;

            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_save_YearlyLeave(string package, string sel0, string sel1, string sel2, string sel3, string sel4)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("sel0", OracleType.VarChar).Value = sel0;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;

            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_save_LeaveStatus(string package, string sel0, string sel1, string sel2, string sel3, string sel4)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("sel0", OracleType.VarChar).Value = sel0;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;

            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }


        public DataSet package_save_Five(string package, string sel1, string sel2, string sel3, string sel4, string sel5)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output; ;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;

            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }


        public DataSet Update_Six(string package, string sel1, string sel2, string sel3, string sel4, string sel5, string sel6)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = package;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            cmd.Parameters.Add("sel6", OracleType.VarChar).Value = sel6;

            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_save_three(string package, string sel1, string sel2, string sel3)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output; ;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;

            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }


        public DataSet package_save_four(string package, string sel1, string sel2, string sel3, string sel4)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output; ;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_Update_Four(string package, string sel1, string sel2, string sel3, string sel4)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();

            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = package;

            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;

            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }


        public DataSet package_update_five(string package, string sel1, string sel2, string sel3, string sel4, string sel5)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = package;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_save_fifteen(string package, string sel1, string sel2, string sel3, string sel4, string sel5,
            string sel6, string sel7, string sel8, string sel9, string sel10, string sel11, string sel12, string sel13, string sel14, string sel15)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output; ;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            cmd.Parameters.Add("sel6", OracleType.VarChar).Value = sel6;
            cmd.Parameters.Add("sel7", OracleType.VarChar).Value = sel7;
            cmd.Parameters.Add("sel8", OracleType.VarChar).Value = sel8;
            cmd.Parameters.Add("sel9", OracleType.VarChar).Value = sel9;
            cmd.Parameters.Add("sel10", OracleType.VarChar).Value = sel10;
            cmd.Parameters.Add("sel11", OracleType.VarChar).Value = sel11;
            cmd.Parameters.Add("sel12", OracleType.DateTime).Value = sel12;
            cmd.Parameters.Add("sel13", OracleType.DateTime).Value = sel13;
            cmd.Parameters.Add("sel14", OracleType.VarChar).Value = sel14;
            cmd.Parameters.Add("sel15", OracleType.VarChar).Value = sel15;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_insert_prof(string package, string sel1, string sel2)
        {


            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output; ;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_update_fourteen(string package, string sel1, string sel2, string sel3, string sel4, string sel5, string sel6,
            string sel7, string sel8, string sel9, string sel10, string sel11, string sel12, string sel13, string sel14)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = package;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            cmd.Parameters.Add("sel6", OracleType.VarChar).Value = sel6;
            cmd.Parameters.Add("sel7", OracleType.VarChar).Value = sel7;
            cmd.Parameters.Add("sel8", OracleType.VarChar).Value = sel8;
            cmd.Parameters.Add("sel9", OracleType.VarChar).Value = sel9;
            cmd.Parameters.Add("sel10", OracleType.VarChar).Value = sel10;
            cmd.Parameters.Add("sel11", OracleType.VarChar).Value = sel11;
            cmd.Parameters.Add("sel12", OracleType.VarChar).Value = sel12;
            cmd.Parameters.Add("sel13", OracleType.VarChar).Value = sel13;
            cmd.Parameters.Add("sel14", OracleType.VarChar).Value = sel14;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_update_sixteen(string package, string sel1, string sel2, string sel3, string sel4, string sel5, string sel6,
            string sel7, string sel8, string sel9, string sel10, string sel11, string sel12, string sel13, string sel14, string sel15, string sel16)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = package;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            cmd.Parameters.Add("sel6", OracleType.VarChar).Value = sel6;
            cmd.Parameters.Add("sel7", OracleType.VarChar).Value = sel7;
            cmd.Parameters.Add("sel8", OracleType.VarChar).Value = sel8;
            cmd.Parameters.Add("sel9", OracleType.VarChar).Value = sel9;
            cmd.Parameters.Add("sel10", OracleType.VarChar).Value = sel10;
            cmd.Parameters.Add("sel11", OracleType.VarChar).Value = sel11;
            cmd.Parameters.Add("sel12", OracleType.VarChar).Value = sel12;
            cmd.Parameters.Add("sel13", OracleType.DateTime).Value = sel13;
            cmd.Parameters.Add("sel14", OracleType.DateTime).Value = sel14;
            cmd.Parameters.Add("sel15", OracleType.VarChar).Value = sel15;
            cmd.Parameters.Add("sel16", OracleType.VarChar).Value = sel16;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_insert_eight(string package, string sel1, string sel2, string sel3, string sel4, string sel5, string sel6,
            string sel7, string sel8)
        {
            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cur_c1", OracleType.Cursor).Direction = ParameterDirection.Output; ;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            cmd.Parameters.Add("sel6", OracleType.DateTime).Value = sel6;
            cmd.Parameters.Add("sel7", OracleType.DateTime).Value = sel7;
            cmd.Parameters.Add("sel8", OracleType.VarChar).Value = sel8;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;

        }

        public DataSet package_update_leave_details(string package, string sel1, string sel2, string sel3, string sel4, string sel5, string sel6, string sel7, string sel8, string sel9)
        {
            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            cmd = new OracleCommand(package, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = package;
            cmd.Parameters.Add("sel1", OracleType.VarChar).Value = sel1;
            cmd.Parameters.Add("sel2", OracleType.VarChar).Value = sel2;
            cmd.Parameters.Add("sel3", OracleType.VarChar).Value = sel3;
            cmd.Parameters.Add("sel4", OracleType.VarChar).Value = sel4;
            cmd.Parameters.Add("sel5", OracleType.VarChar).Value = sel5;
            cmd.Parameters.Add("sel6", OracleType.VarChar).Value = sel6;
            cmd.Parameters.Add("sel7", OracleType.DateTime).Value = sel7;
            cmd.Parameters.Add("sel8", OracleType.DateTime).Value = sel8;
            cmd.Parameters.Add("sel9", OracleType.VarChar).Value = sel9;
            oda = new OracleDataAdapter(cmd);
            ds = new DataSet();
            conn.Close();
            oda.Fill(ds);
            return ds;
        }

        public void save_salary_deduction(string oid, string empl, string acyl, string hshd, string fdat, string amnt, string note, string euser)
        {
            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "insert into tflhr.t_esam (oid, esam_empl, esam_acyl, esam_hshd, esam_date, esam_amnt, esam_note, iuser, euser) values "
                + "(:oid, :esam_empl, :esam_acyl, :esam_hshd, :esam_date, :esam_amnt, :esam_note, :iuser, :euser)";

            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("oid", OracleType.VarChar).Value = oid.ToUpper();
            cmd.Parameters.Add("esam_empl", OracleType.VarChar).Value = empl;
            cmd.Parameters.Add("esam_acyl", OracleType.VarChar).Value = acyl;
            cmd.Parameters.Add("esam_hshd", OracleType.VarChar).Value = hshd;
            cmd.Parameters.Add("esam_date", OracleType.DateTime).Value = fdat;
            cmd.Parameters.Add("esam_amnt", OracleType.VarChar).Value = amnt;
            cmd.Parameters.Add("esam_note", OracleType.VarChar).Value = note;
            cmd.Parameters.Add("iuser", OracleType.VarChar).Value = euser;
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            cmd.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();
        }

        public void update_salary_deduction(string oid, string empl, string acyl, string hshd, string fdat, string amnt, string note, string euser)
        {

            conn = new OracleConnection(clsConnection.ConnectionSave);
            conn.Open();
            strComm = "update tflhr.t_esam set esam_empl = :esam_empl, esam_acyl = :esam_acyl, esam_hshd = :esam_hshd, esam_date = :esam_date, esam_amnt = :esam_amnt, esam_note = :esam_note, euser = :euser  where oid ='" + oid + "'";
            cmd = new OracleCommand(strComm, conn);
            cmd.Parameters.Add("esam_empl", OracleType.VarChar).Value = empl;
            cmd.Parameters.Add("esam_acyl", OracleType.VarChar).Value = acyl;
            cmd.Parameters.Add("esam_hshd", OracleType.VarChar).Value = hshd;
            cmd.Parameters.Add("esam_date", OracleType.DateTime).Value = fdat;
            cmd.Parameters.Add("esam_amnt", OracleType.VarChar).Value = amnt;
            cmd.Parameters.Add("esam_note", OracleType.VarChar).Value = note;
            cmd.Parameters.Add("euser", OracleType.VarChar).Value = euser;
            cmd.ExecuteNonQuery();
            conn.Dispose();
            conn.Close();
        }
    }
}