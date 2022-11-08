using System;
using System.IO;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using HRAdmin.Models;
using System.Data.OracleClient;
using System.Collections.Generic;

namespace HRAdmin.Controllers
{
    public class AdminController : BaseController
    {
        private DataAccess _clsDataAccess = new DataAccess();
        private OracleConnection conn;
        private OracleCommand cmd;
        private DataSet ds, ds2, ds3, ds4;
        string strComm, strComm2;


        public ActionResult User(string cpid, int bt)
        {
            strComm = "select * from tflhr.t_user order by USER_TEXT asc";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.Users = ds.Tables[0];
            ViewBag.header = "User Creating";
            ViewBag.bt = bt;
            if (bt == 1)
            {
                strComm = "select * from tflhr.t_user where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.SingleUser2 = ds2.Tables[0];

                DataTable dt = new DataTable();
                dt = ds2.Tables[0];

                try
                {
                    byte[] img = (byte[])(dt.Rows[0]["image"]);
                    string strBase64 = Convert.ToBase64String(img);
                    ViewBag.ImageUrl = "data:Image/jpg;base64," + strBase64;
                }

                catch
                {
                    ViewBag.ImageUrl = "0";
                }

            }
            return View();
        }



        public ActionResult SaveUser(string oid, string fname, string uname, string role, string pass, string pass2, string bt, HttpPostedFileBase image)
        {
            Byte[] imgByte = null;


            if (pass == pass2)
            {
                if (bt == "0")
                {
                    string packge = "tflhr.pro_new_user.insert_data";
                    _clsDataAccess.package_user(packge, fname, uname, pass, role, Session["UserID"].ToString());

                }
                else if (bt == "1")
                {
                    strComm = "update tflhr.T_USER set USER_NAME='" + fname + "',USER_ENID= '" + uname + "',USER_PASS='" + pass + "', USER_TYPE='" + role + "',EUSER='" + Session["UserID"].ToString() + "' where OID ='" + oid + "'";
                    _clsDataAccess.read_data(strComm);

                    if (image != null)
                    {
                        imgByte = new Byte[image.ContentLength];
                        image.InputStream.Read(imgByte, 0, image.ContentLength);

                        ds = new DataSet();
                        conn = new OracleConnection(clsConnection.ConnectionSave);
                        conn.Open();
                        strComm = "update tflhr.t_user set image = :image, user_path = :user_path where oid ='" + oid + "'";
                        cmd = new OracleCommand(strComm, conn);
                        cmd.Parameters.Add("user_path", OracleType.VarChar).Value = oid;
                        cmd.Parameters.Add("image", OracleType.Blob).Value = imgByte;
                        cmd.ExecuteNonQuery();
                        conn.Dispose();
                        conn.Close();

                    }
                }
            }

            return RedirectToAction("User", new { cpid = 0, bt = 0 });
        }



        public ActionResult UpdateUserActv(string cpid, string actv, string bt)
        {

            if (actv == "1")
            {
                actv = "0";
            }
            else
            {
                actv = "1";
            }
            strComm = "update tflhr.T_USER set USER_ACTV='" + actv + "',EUSER='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);

            return RedirectToAction("User", new { cpid = 0, bt = 0 });
        }




        public ActionResult Company(string cpid, int bt)
        {
            strComm = "select * from tflhr.t_acmp";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.Company = ds.Tables[0];
            ViewBag.bt = bt;
            if (bt == 1)
            {
                strComm = "select * from tflhr.t_acmp where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.SingleUser = ds2.Tables[0];
            }
            return View();
        }




        public ActionResult SaveCompany(string oid, string cpname, string sname, string ctype, string addr, string tphone, string fax, string email, string website, string bt)
        {
            if (bt == "0")
            {

                _clsDataAccess.Create_Company(oid, cpname, sname, ctype, addr, tphone, fax, email, website, Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                _clsDataAccess.Update_Company(oid, cpname, sname, ctype, addr, tphone, fax, email, website, Session["UserId"].ToString());
            }

            return RedirectToAction("Company", new { cpid = 0, bt = 0 });
        }





        public ActionResult UpdateCompany(string cpid, string actv, string bt)
        {

            if (actv == "1")
            {
                actv = "0";
            }
            else
            {
                actv = "1";
            }
            strComm = "update tflhr.t_acmp set acmp_actv='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);

            return RedirectToAction("Company", new { cpid = 0, bt = 0 });
        }


        public ActionResult MenuPermission(string cpid, string[] data, int bt)
        {
            ViewBag.bt = bt;
            string actv = "0";

            if (bt == 0 || bt == 3)
            {
                ViewBag.disa = "disabled";
            }
            else
            {
                ViewBag.disa = "";
            }


            if (cpid == "0")
            {

                ViewBag.cpid = Session["UserId"].ToString();
            }
            else
            {
                ViewBag.cpid = cpid;
            }

            strComm2 = "with a as (select * from tflhr.t_umnu where umnu_user='" + ViewBag.cpid + "' and umnu_actv in (0,1)) "
                + "select decode(a.umnu_actv,1,1,0) chk, b.mnud_text, c.mnum_name,c.oid, b.mnud_name , b.mnud_snam, nvl(a.oid,'N') umnu_oid "
                + "from tflhr.t_mnum c, tflhr.t_mnud b, a where b.mnud_actv=1 and c.oid = b.mnud_mnum and "
                + "(b.mnud_text=a.umnu_mnud(+)) order by c.mnum_name, b.mnud_name";
            ds2 = new DataSet();
            ds2 = _clsDataAccess.read_data(strComm2);
            ViewBag.MenuData2 = ds2.Tables[0];




            strComm = "select oid,user_text, user_name from tflhr.t_user where user_actv = 1";
            ds3 = new DataSet();
            ds3 = _clsDataAccess.read_data(strComm);
            ViewBag.UserInfo = ds3.Tables[0];



            strComm = "select OID, MNUM_NAME from tflhr.T_MNUM";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.MenuData1 = ds.Tables[0];


            if (ViewBag.bt == 2)
            {

                foreach (System.Data.DataRow dz in ViewBag.MenuData2.Rows)
                {
                    ViewBag.menu_id = @dz["mnud_text"];

                      for (int i = 0; i < data.Length; i++)
                        {

                            if (ViewBag.menu_id == data[i])
                            {
                                if (@dz["umnu_oid"] != "0")
                                {
                                 _clsDataAccess.update_menu_permission(cpid.ToString(), @dz["mnud_text"].ToString(), "1", @dz["umnu_oid"].ToString(), Session["UserId"].ToString());
                                  @dz["mnud_text"] = "0"; data[i] = "0"; @dz["umnu_oid"] = "0";
                                }
                                
                            }
                            else
                            {
                                if (@dz["umnu_oid"] != "0")
                                {
                                    _clsDataAccess.update_menu_permission(cpid.ToString(), @dz["mnud_text"].ToString(), "0", @dz["umnu_oid"].ToString(), Session["UserId"].ToString());
                                }
                            }


                        }
                                        

                }

                return RedirectToAction("MenuPermission", new { cpid = ViewBag.cpid, bt = 0 });
            }


            ViewBag.header = "Menu Permission";
            return View();
        }


        public ActionResult Location(string cpid, int bt)
        {
            strComm = "select * from tflhr.t_jloc";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.Location = ds.Tables[0];
            ViewBag.bt = bt;
            if (bt == 1)
            {
                strComm = "select * from tflhr.t_jloc where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.LocationInfo = ds2.Tables[0];
            }
            return View();
        }

        public ActionResult SaveLocation(string cpid, string oid, string lname, string actv, string bname, string add1, string add2, string bt)
        {
            if (bt == "0")
            {

                _clsDataAccess.Create_location(oid, lname, bname, add1, add2, Session["UserId"].ToString(), Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                _clsDataAccess.Update_location(oid, lname, bname, add1, add2, Session["UserId"].ToString());
            }
            else
            {
                if (actv == "1") { actv = "0"; }
                else { actv = "1"; }

                strComm = "update tflhr.t_jloc set jloc_actv='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }

            return RedirectToAction("Location", new { cpid = 0, bt = 0 });
        }

        public ActionResult RoleAndLocation(string cpid, string[] data, int bt)
        {
            ViewBag.bt = bt;
            string actv = "0";

            if (bt == 0 || bt == 3)
            {
                ViewBag.disa = "disabled";
            }
            else
            {
                ViewBag.disa = "";
            }


            if (cpid == "0")
            {

                ViewBag.cpid = Session["UserId"].ToString();
            }
            else
            {
                ViewBag.cpid = cpid;
            }

            strComm2 = "select t_uloc.oid, uloc_jloc, jloc_name, uloc_actv from t_uloc, t_jloc where t_uloc.uloc_jloc = t_jloc.oid";
            ds2 = new DataSet();
            ds2 = _clsDataAccess.read_data(strComm2);
            ViewBag.MenuData2 = ds2.Tables[0];




            strComm = "select oid,user_text, user_name from tflhr.t_user where user_actv = 1";
            ds3 = new DataSet();
            ds3 = _clsDataAccess.read_data(strComm);
            ViewBag.UserInfo = ds3.Tables[0];



            strComm = "select OID, JLOC_NAME from tflhr.T_JLOC";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.MenuData1 = ds.Tables[0];



            if (ViewBag.bt == 2)
            {

                foreach (System.Data.DataRow dz in ViewBag.MenuData2.Rows)
                {
                    ViewBag.menu_id = @dz["mnud_text"];

                      for (int i = 0; i < data.Length; i++)
                        {

                            if (ViewBag.menu_id == data[i])
                            {
                                if (@dz["umnu_oid"] != "0")
                                {
                                 _clsDataAccess.update_menu_permission(cpid.ToString(), @dz["mnud_text"].ToString(), "1", @dz["umnu_oid"].ToString(), Session["UserId"].ToString());
                                  @dz["mnud_text"] = "0"; data[i] = "0"; @dz["umnu_oid"] = "0";
                                }
                                
                            }
                            else
                            {
                                if (@dz["umnu_oid"] != "0")
                                {
                                    _clsDataAccess.update_menu_permission(cpid.ToString(), @dz["mnud_text"].ToString(), "0", @dz["umnu_oid"].ToString(), Session["UserId"].ToString());
                                }
                            }


                        }
                                        

                }

                return RedirectToAction("MenuPermission", new { cpid = ViewBag.cpid, bt = 0 });
            }


            ViewBag.header = "Menu Permission";
            return View();
        }

	}
}