using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRAdmin.Models;
using System.Data;

namespace HRAdmin.Controllers
{
    public class SetupController : BaseController
    {
        private DataAccess _clsDataAccess = new DataAccess();
        private SelectData _clsSelectData = new SelectData();
        private DataSet ds, ds2, ds3, ds4;
        string strComm, strComm2;


        public ActionResult Index(int cpid, int bt)
        {
            return View();
        }
        public ActionResult Attendance(int cpid, int bt)
        {
            ViewBag.header = "Attendance Management";
            return View();
        }
        public ActionResult Food(int cpid, int bt)
        {
            ViewBag.header = "Food Management";
            return View();
        }
        public ActionResult JobConformation(int cpid, int bt)
        {

            ViewBag.header = "Job Conformation Management";
            return View();
        }




        public ActionResult LeaveStatus(string cpid, string bt)
        {
            strComm = "select * from tflhr.t_lsts";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.LeaveStatus = ds.Tables[0];

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_lsts where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.LeaveStatusInfo = ds2.Tables[0];
            }
            ViewBag.bt = bt;
            return View();

        }


        public ActionResult SaveLeaveStatus(string cpid, string oid, string name, string seqn, string sign, string duty, string bt)
        {
            if (bt == "0")
            {
                string packge = "p_lsts.insert_data";
                _clsDataAccess.package_save_Five(packge, name, seqn, sign, duty, Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                string packge = "p_lsts.update_data";
                _clsDataAccess.Update_Six(packge, oid, name, seqn, sign, duty, Session["UserId"].ToString());
            }

            ViewBag.bt = bt;
            return RedirectToAction("LeaveStatus", new { cpid = 0, bt = 0 });
        }




        public ActionResult PhoneBilling(int cpid, int bt)
        {
            ViewBag.header = "Phone Billing Management";
            return View();
        }


        public ActionResult LeaveType(string cpid, string bt)
        {
            strComm = "select * from tflhr.t_lvtp";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.LeaveType = ds.Tables[0];

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_lvtp where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.LeaveTypeInfo = ds2.Tables[0];
            }
            ViewBag.bt = bt;
            return View();
        }

        public ActionResult SaveLeaveType(string cpid, string oid, string lname, string dflv, string eflg, string actv, string bt)
        {
            if (bt == "0")
            {
                string packge = "p_lvtp.insert_data";
                _clsDataAccess.package_save_LeaveType(packge, lname, dflv, eflg, Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                string packge = "p_lvtp.update_data";
                _clsDataAccess.Update_dept_Leavetype(packge, oid, lname, dflv, eflg, Session["UserId"].ToString());
            }
            else
            {
                if (actv == "1") { actv = "0"; }
                else { actv = "1"; }

                strComm = "update tflhr.t_lvtp set lvtp_actv='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }
            ViewBag.bt = bt;
            return RedirectToAction("LeaveType", new { cpid = 0, bt = 0 });
        }

        public ActionResult YearWiseLeave(string cpid, string bt)
        {
            strComm = "select  t_lvep.oid,lvep_lvtp, lvep_aday, lvep_year, lvtp_name from tflhr.t_lvep, TFLHR.T_LVTP where tflhr.t_lvep.lvep_lvtp = TFLHR.T_LVTP.oid";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.YearlyLeave = ds.Tables[0];

            strComm = "select * from tflhr.t_lvtp";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.LeaveType = ds.Tables[0];

            if (bt == "1")
            {
                strComm = "select t_lvep.oid,lvep_lvtp, lvep_aday, lvep_year, lvtp_name from tflhr.t_lvep, TFLHR.T_LVTP where tflhr.t_lvep.lvep_lvtp = TFLHR.T_LVTP.oid and tflhr.t_lvep.OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.LeaveTypeInfo = ds2.Tables[0];
            }
            ViewBag.bt = bt;
            return View();
        }

        public ActionResult SaveYearWiseLeave(string cpid, string oid, string lvtp, string lday, string year, string bt)
        {
            if (bt == "0")
            {
                string packge = "p_lvep.insert_data";
                _clsDataAccess.package_save_YearlyLeave(packge, oid, lvtp, lday, year, Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                string packge = "p_lvep.update_data";
                _clsDataAccess.Update_dept_Leavetype(packge, oid, lvtp, lday, year, Session["UserId"].ToString());
            }

            ViewBag.bt = bt;
            return RedirectToAction("YearWiseLeave", new { cpid = 0, bt = 0 });
        }

        public ActionResult SIMCreate(string cpid, string bt)
        {


            if (bt == "1")
            {
                strComm = "select * from tflhr.t_esim where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.SimInfo = ds2.Tables[0];
            }
            ViewBag.bt = bt;
            return View();
        }


        public ActionResult SaveSim(string cpid, string oid, string oname, string actv, string snmbr, string limit, string bt)
        {
            if (bt == "0")
            {
                _clsDataAccess.package_save_four("p_esim.insert_data", oname, snmbr, limit, Session["UserId"].ToString());

            }
            else if (bt == "1")
            {

                _clsDataAccess.package_update_five("p_esim.update_data", oid, oname, snmbr, limit, Session["UserId"].ToString());
            }
            else
            {
                if (actv == "1") { actv = "0"; }
                else { actv = "1"; }

                strComm = "update tflhr.t_esim set esim_actv='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);

            }

            ViewBag.bt = bt;
            return RedirectToAction("AllSIM", new { cpid = 0, bt = 0 });
        }


        public ActionResult AllSIM(string cpid, string snmbr, string bt)
        {
            if (bt == "4")
            {
                strComm = "select * from tflhr.t_esim where ESIM_NMBR LIKE '%" + snmbr + "%'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.AllSIM = ds2.Tables[0];
            }
            else
            {
                strComm = "select * from tflhr.t_esim";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.AllSIM = ds2.Tables[0];

            }


            ViewBag.bt = bt;
            return View();
        }

        public ActionResult Department(string cpid, string bt)
        {

            strComm = "select t_dept.oid, dept_name, dept_name, dept_actv, acmp_name, dtyp_name  from t_dept, t_acmp, t_dtyp where t_dept.dept_acmp = t_acmp.oid  and  t_dept.dept_dtyp = t_dtyp.oid ";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.DeptList = ds.Tables[0];

            ds = new DataSet();
            ds = _clsSelectData.get_pro_data("p_dept.get_com_deptp", "get_data", "", "");
            ViewBag.Com = ds.Tables[0];
            ViewBag.DeptType = ds.Tables[1];

            if (bt == "1")
            {
                strComm = "select * from t_dept where t_dept.oid='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
                ViewBag.Deptinfo = ds.Tables[0];
            }

            ViewBag.bt = bt;
            return View();
        }

        public ActionResult SaveDept(string cpid, string oid, string cmp, string dtyp, string dname, string bname, string actv, string bt)
        {

            if (bt == "0")
            {
                _clsDataAccess.package_save_data("p_dept.insert_data", cmp, dtyp, dname, bname, Session["UserID"].ToString());
            }
            else if (bt == "1")
            {

                _clsDataAccess.Update_Six("p_dept.update_data", oid, cmp, dtyp, dname, bname, Session["UserID"].ToString());

            }
            else
            {
                if (actv == "1") { actv = "0"; } else { actv = "1"; }

                strComm = "update tflhr.t_dept set dept_actv='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }

            ViewBag.bt = bt;
            return RedirectToAction("Department", new { cpid = 0, bt = 0 });
        }


        public ActionResult Blood(string cpid, string bt)
        {
            strComm = "select * from tflhr.t_bldg";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.Blood = ds.Tables[0];
            ViewBag.bt = bt;

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_bldg where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.BloodInfo = ds2.Tables[0];
            }

            return View();
        }

        public ActionResult SaveBlood(string cpid, string oid, string uname, string bname, string actv, string bt)
        {
            if (bt == "0")
            {
                string packge = "p_bldg.insert_data";
                _clsDataAccess.package_save_data(packge, uname, bname, "1", Session["UserId"].ToString(), Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                string packge = "p_bldg.update_data";
                _clsDataAccess.package_update_data(packge, oid, uname, bname, Session["UserId"].ToString());
            }
            else
            {
                if (actv == "1") { actv = "0"; } else { actv = "1"; }

                strComm = "update tflhr.t_bldg set bldg_actv='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }
            ViewBag.bt = bt;
            return RedirectToAction("Blood", new { cpid = 0, bt = 0 });
        }


        public ActionResult DepartmentType(string cpid, string bt)
        {
            strComm = "select * from tflhr.t_dtyp";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.DeptType = ds.Tables[0];
            ViewBag.bt = bt;
            if (bt == "1")
            {
                strComm = "select * from tflhr.t_dtyp where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.DeptTypeInfo = ds2.Tables[0];
            }
            ViewBag.bt = bt;
            return View();
        }

        public ActionResult SaveDeptType(string cpid, string oid, string dname, string bname, string actv, string bt)
        {
            if (bt == "0")
            {

                _clsDataAccess.Create_dept_type(oid, dname, bname, Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                _clsDataAccess.Update_dept_type(oid, dname, bname, Session["UserId"].ToString());
            }
            else
            {
                if (actv == "1") { actv = "0"; }
                else { actv = "1"; }

                strComm = "update tflhr.t_dtyp set dtyp_actv='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }
            ViewBag.bt = bt;
            return RedirectToAction("DepartmentType", new { cpid = 0, bt = 0 });
        }


        public ActionResult DesignationType(string cpid, string bt)
        {
            strComm = "select * from tflhr.t_desgtp";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.DesgType = ds.Tables[0];
            ViewBag.bt = bt;

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_desgtp where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.DesgTypeInfo = ds2.Tables[0];
            }

            return View();
        }

        public ActionResult SaveDesgType(string cpid, string oid, string dname, string bname, string actv, string bt)
        {
            if (bt == "0")
            {

                _clsDataAccess.Create_desg_type(oid, dname, bname, Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                _clsDataAccess.Update_desg_type(oid, dname, bname, Session["UserId"].ToString());
            }

            ViewBag.bt = bt;
            return RedirectToAction("DesignationType", new { cpid = 0, bt = 0 });
        }


        public ActionResult Designation(string cpid, string bt)
        {
            strComm = "select t_desg.oid, desg_name,desgtp_name, desg_bnam, desg_actv from t_desg , t_desgtp where t_desg.desg_desgtp = t_desgtp.oid order by oid desc";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.Desg = ds.Tables[0];

            strComm = "select * from tflhr.t_desgtp";
            ds3 = new DataSet();
            ds3 = _clsDataAccess.read_data(strComm);
            ViewBag.DesgType = ds3.Tables[0];


            if (bt == "1")
            {
                strComm = "select t_desg.oid,t_desgtp.oid as dgtp, desg_name,desgtp_name, desg_bnam, desg_actv from t_desg , t_desgtp where t_desg.desg_desgtp = t_desgtp.oid and t_desg.OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.DesgInfo = ds2.Tables[0];

            }


            ViewBag.bt = bt;
            return View();
        }

        public ActionResult SaveDesignation(string cpid, string oid, string dptp, string dname, string bname, string actv, string bt)
        {
            if (bt == "0")
            {

                _clsDataAccess.Create_desg(oid, dname, dptp, bname, Session["UserId"].ToString(), Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                _clsDataAccess.Update_desg(oid, dname, dptp, bname, Session["UserId"].ToString());
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
            return RedirectToAction("Designation", new { cpid = 0, bt = 0 });
        }
	}
}