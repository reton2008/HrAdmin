using System;
using System.IO;
using System.Web;
using System.Data;
using System.Web.Mvc;
using HRAdmin.Models;
using System.Globalization;
using System.Web.UI.WebControls;


namespace HRAdmin.Controllers
{
    public class EmployeeController : BaseController
    {
        private DataAccess _clsDataAccess = new DataAccess();
        private SelectData _clsSelectData = new SelectData();
        private DataSet ds, ds2, ds3, ds4;
        string strComm, strComm2;



        public ActionResult AllEmployee(int cpid, int bt)
        {

            strComm = "select t_empl.oid,empl_name, empl_stat,  jloc_name, empl_prof, desgtp_name, dept_name, atyp_name , desg_name, prof_text , esim_nmbr from  t_empl , t_jloc, t_desgtp, t_dept, t_atyp, t_desg, t_prof, t_esim  where (t_empl.empl_jloc = t_jloc.oid and t_empl.empl_desgtp = T_desgtp.OID) and  ( t_empl.empl_dept = t_dept.oid and t_empl.empl_atyp = t_atyp.oid ) and ( t_empl.empl_desg = t_desg.oid and t_empl.empl_prof = t_prof.oid) and  t_empl.empl_esim = T_ESIM.OID  order by oid desc";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.EmployeeList = ds.Tables[0];


            return View();
        }



        public ActionResult Employee(string cpid, string bt)
        {
            ds = new DataSet();
            ds = _clsSelectData.select_data_c7("p_empl.select_empl_c7", "SELECT7", "", "");

            ViewBag.Location = ds.Tables[0];
            ViewBag.DesgType = ds.Tables[1];
            ViewBag.Dept = ds.Tables[2];
            ViewBag.AttType = ds.Tables[3];
            ViewBag.Designation = ds.Tables[4];
            ViewBag.BlodGroup = ds.Tables[5];
            ViewBag.Salary = ds.Tables[6];
            ViewBag.esim = ds.Tables[7];

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_empl where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.EmployeeInfo = ds2.Tables[0];
            }

            ViewBag.bt = bt;
            return View();
        }


        public ActionResult SaveEmployee(string cpid, string oid, string fname, string bname, string card, string emid, string jdate, string cdate,
            string jloc, string destp, string dept, string attn, string deg, string Blood, string salary, string esim, string actv, string bt)
        {
            DateTimeFormatInfo dtInfo = new DateTimeFormatInfo();
            dtInfo.ShortDatePattern = @"MM/dd/yyyy";
            DateTime Jdat, jcat;
            Jdat = Convert.ToDateTime(jdate, dtInfo);
            jcat = Convert.ToDateTime(cdate, dtInfo);


            if (bt == "0")
            {

                _clsDataAccess.package_insert_prof("p_prof.insert_data", emid, Session["UserId"].ToString());

                _clsDataAccess.package_save_fifteen("p_empl.insert_data", fname, bname, card, jloc, destp, dept, attn, deg, Blood,
                   salary, emid, Jdat.ToString(), jcat.ToString(), Session["UserId"].ToString(), esim);

            }
            else if (bt == "1")
            {
                _clsDataAccess.package_update_sixteen("p_empl.update_data", oid, fname, bname, card, jloc, destp, dept, attn, deg, Blood,
                   salary, emid, Jdat.ToString(), jcat.ToString(), esim, Session["UserId"].ToString());
            }
            else
            {
                if (actv == "1") { actv = "0"; }
                else { actv = "1"; }

                strComm = "update tflhr.t_desg set desg_actv='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }
            ViewBag.bt = bt;
            return RedirectToAction("AllEmployee", new { cpid = 0, bt = 0 });
        }






        public ActionResult LeaveDetails(string cpid, string bt)
        {
            if (cpid != "0" && bt == "0")
            {
                ViewBag.Message = cpid;
            }

            strComm = "select * from( (select t_elvd.oid,elvd_lday, elvd_fdat, elvd_tdat, lvtp_name, lsts_name, empl_name from t_elvd, t_empl, t_lsts, t_lvtp where (t_elvd.elvd_empl = t_empl.oid and t_elvd.elvd_lsts = t_lsts.oid) and t_elvd.elvd_lvtp = t_lvtp.oid order by t_elvd.oid desc )) where ROWNUM < 11 ";
            ds2 = new DataSet();
            ds2 = _clsDataAccess.read_data(strComm);
            ViewBag.LeaveDetails = ds2.Tables[0];

            ds = new DataSet();
            ds = _clsSelectData.select_data_c3("p_elvd.select_data_c3", "select3", "", "");

            ViewBag.Em = ds.Tables[0];
            ViewBag.LeaveType = ds.Tables[1];
            ViewBag.LeaveStatus = ds.Tables[2];

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_elvd where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.LeaveDetailsInfo = ds2.Tables[0];
            }

            ViewBag.bt = bt;


            return View();
        }


        public ActionResult SaveLeaveDetails(string cpid, string oid, string empl, string lvtp, string lsts, string prps, string lday, string fdate, string tdate, string actv, string bt)
        {
            DateTimeFormatInfo dtInfo = new DateTimeFormatInfo();
            dtInfo.ShortDatePattern = @"MM/dd/yyyy";
            DateTime fdat, tdat;
            fdat = Convert.ToDateTime(fdate, dtInfo);
            tdat = Convert.ToDateTime(tdate, dtInfo);
            double date_data = (tdat - fdat).TotalDays;

            if (fdat > tdat)
            {
                cpid = "Please select valid Date";
            }
            else if (date_data != Convert.ToInt32(lday))
            {
                cpid = "Total Leave and Leave date do not match";
            }
            else
            {
                if (bt == "0")
                {

                    _clsDataAccess.package_insert_eight("p_elvd.insert_data", empl, lvtp, lsts, prps, lday, fdat.ToString(), tdat.ToString(), Session["UserId"].ToString());

                }
                else if (bt == "1")
                {

                    _clsDataAccess.package_update_leave_details("p_elvd.update_data", oid, empl, lvtp, lsts, prps, lday, fdat.ToString(), tdat.ToString(), Session["UserId"].ToString());
                }
            }

            ViewBag.bt = bt;
            return RedirectToAction("LeaveDetails", new { cpid = cpid, bt = 0 });
        }


        public ActionResult Unit(string cpid, string bt)
        {
            strComm = "select * from tflhr.t_unit";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.Unit = ds.Tables[0];
            ViewBag.bt = bt;

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_unit where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.UnitInfo = ds2.Tables[0];
            }

            return View();
        }

        public ActionResult SaveUnit(string cpid, string oid, string uname, string bname, string actv, string bt)
        {
            if (bt == "0")
            {
                string packge = "p_unit.insert_data";
                _clsDataAccess.package_save_data(packge, uname, bname, "1", Session["UserId"].ToString(), Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                string packge = "p_unit.update_data";
                _clsDataAccess.package_update_data(packge, oid, uname, bname, Session["UserId"].ToString());
            }
            else
            {
                if (actv == "1") { actv = "0"; }
                else { actv = "1"; }

                strComm = "update tflhr.t_unit set unit_actv='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }
            ViewBag.bt = bt;
            return RedirectToAction("Unit", new { cpid = 0, bt = 0 });
        }




        public ActionResult CreateProfile(string cpid, string bt)
        {
            strComm = "select * from tflhr.t_prof where OID ='" + cpid + "'";
            ds2 = new DataSet();
            ds2 = _clsDataAccess.read_data(strComm);
            ViewBag.Profile = ds2.Tables[0];

            ViewBag.EmployeeID = cpid;
            return View();
        }


        public ActionResult UpdateProfile(string cpid, string oid, HttpPostedFileBase image, string nname, string fname, string mname, string padd, string radd,
            string pnumbr, string hnumbr, string nid, string email, string bname, string refr, string remark, string actv, string bt)
        {

            if (image != null)
            {
                string FileName = Path.GetFileName(image.FileName);
                string path = Path.Combine(Server.MapPath("~/img/employee"), oid + FileName);
                image.SaveAs(path);

                string imageName = oid + FileName;
                strComm = "update tflhr.t_prof set image='" + imageName.ToString() + "', euser='" + Session["UserID"].ToString() + "' where OID ='" + oid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }

            if (bt == "0")
            {
                _clsDataAccess.package_update_fourteen("p_prof.update_data", oid, nname, fname, mname, padd, radd,
                pnumbr, hnumbr, nid, email, bname, refr, remark, Session["UserId"].ToString());

                ViewBag.EmployeeID = cpid;
                return RedirectToAction("EmProfile", new { cpid = Session["em_id"].ToString(), pro = Session["pro_id"].ToString(), bt = 0 });
            }
            else if (bt == "3")
            {
                if (actv == "1") { actv = "0"; } else { actv = "1"; }
                strComm = "update tflhr.t_empl set empl_stat='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }

            ViewBag.EmployeeID = cpid;
            return RedirectToAction("AllEmployee", new { cpid = 0, bt = 0 });
        }

        public ActionResult EmProfile(string cpid, string pro, string bt)
        {


            ds = new DataSet();
            ds = _clsSelectData.get_pro_data("p_empl.get_pro", "get_data", cpid, pro);
            ViewBag.EmployeeInfo = ds.Tables[0];
            ViewBag.Profile = ds.Tables[1];

            Session["em_id"] = cpid;
            Session["pro_id"] = pro;
            return View();
        }
	}
}