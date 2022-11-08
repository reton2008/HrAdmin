using System;
using System.Web;
using System.Data;
using System.Web.Mvc;
using HRAdmin.Models;
using System.Globalization;
using System.Collections.Generic;

namespace HRAdmin.Controllers
{
    public class SalaryController : BaseController
    {
        private DataAccess _clsDataAccess = new DataAccess();
        private SelectData _clsSelectData = new SelectData();
        private DataSet ds, ds2, ds3, ds4;
        string strComm, strComm2;



        public ActionResult Salary(string cpid, string bt)
        {

            strComm = "select * from tflhr.t_esal";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.Salary = ds.Tables[0];
            ViewBag.bt = bt;

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_esal where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.SalInfo = ds2.Tables[0];
            }

            return View();

        }

        public ActionResult SaveSalary(string cpid, string oid, string tsal, string gsal, string basic, string hrnt, string mcal, string conv, string food, string grade, string actv, string bt)
        {
            if (bt == "0")
            {
                string packge = "p_esal.insert_data";
                _clsDataAccess.package_save_Salary(packge, tsal, gsal, basic, hrnt, mcal, conv, food, grade, Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                string packge = "p_esal.update_data";
                _clsDataAccess.package_update_salary(packge, oid, tsal, gsal, basic, hrnt, mcal, conv, food, grade, Session["UserId"].ToString());
            }


            ViewBag.bt = bt;
            return RedirectToAction("Salary", new { cpid = 0, bt = 0 });
        }


        public ActionResult EidBonus(int cpid, int bt)
        {
            return View();
        }
        public ActionResult YearlyIncrements(int cpid, int bt)
        {
            ViewBag.Message = "Yearly Increment Management";
            return View();
        }

        public ActionResult TaDa(int cpid, int bt)
        {
            return View();
        }



        public ActionResult SalaryCycle(string cpid, string bt)
        {
            strComm = "select * from tflhr.t_acyl";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.Holiday = ds.Tables[0];
            ViewBag.bt = bt;

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_acyl where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.AcylInfo = ds2.Tables[0];
            }

            return View();
        }

        public ActionResult SaveSalCycl(string cpid, string oid, string sname, string bname, string days, string fdate, string tdate, string wdays, string hours, string actv, string bt)
        {

            DateTimeFormatInfo dtInfo = new DateTimeFormatInfo();
            dtInfo.ShortDatePattern = @"MM/dd/yyyy";
            DateTime fdat, tdat;
            fdat = Convert.ToDateTime(fdate, dtInfo);
            tdat = Convert.ToDateTime(tdate, dtInfo);

            if (bt == "0")
            {
                string packge = "p_acyl.insert_data";
                _clsDataAccess.package_save_cycl(packge, sname, bname, fdat.ToString(), tdat.ToString(), days, wdays, hours, Session["UserId"].ToString(), Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                string packge = "p_acyl.update_data";
                _clsDataAccess.package_update_cycl(packge, oid, sname, bname, fdat.ToString(), tdat.ToString(), days, wdays, hours, Session["UserId"].ToString());
            }
            else
            {
                if (actv == "1") { actv = "0"; }
                else { actv = "1"; }

                strComm = "update tflhr.t_acyl set acyl_pflg ='" + actv + "',euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }
            ViewBag.bt = bt;
            return RedirectToAction("SalaryCycle", new { cpid = 0, bt = 0 });

        }

        public ActionResult Holidays(string cpid, string bt)
        {
            strComm = "select t_hday.oid, t_hday.hday_name,t_hday.hday_fdat,t_hday.hday_acyl, t_acyl.acyl_name as cycle from tflhr.t_hday,tflhr.t_acyl where t_hday.hday_acyl = t_acyl.oid";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.Holiday = ds.Tables[0];
            ViewBag.bt = bt;

            strComm = "select * from tflhr.t_acyl";
            ds3 = new DataSet();
            ds3 = _clsDataAccess.read_data(strComm);
            ViewBag.Acyl = ds3.Tables[0];

            if (bt == "1")
            {
                strComm = "select t_hday.oid, t_hday.hday_name,t_hday.hday_fdat,t_hday.hday_acyl, t_acyl.acyl_name as cycle from tflhr.t_hday,tflhr.t_acyl where t_hday.hday_acyl = t_acyl.oid and  t_hday.OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.HdayInfo = ds2.Tables[0];
            }

            return View();
        }

        public ActionResult SaveHday(string cpid, string oid, string hname, string date, string acyl, string actv, string bt)
        {

            DateTimeFormatInfo dtInfo = new DateTimeFormatInfo();
            dtInfo.ShortDatePattern = @"MM/dd/yyyy";
            DateTime fdat;
            fdat = Convert.ToDateTime(date, dtInfo);


            if (bt == "0")
            {
                string packge = "p_hday.insert_data";
                _clsDataAccess.package_save_Hday(packge, hname, fdat.ToString(), acyl, Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                string packge = "p_hday.update_data";
                _clsDataAccess.package_update_hday(packge, oid, hname, fdat.ToString(), acyl, Session["UserId"].ToString());
            }

            ViewBag.bt = bt;
            return RedirectToAction("Holidays", new { cpid = 0, bt = 0 });

        }


        public ActionResult SalaryHead(string cpid, string bt)
        {
            strComm = "select * from tflhr.t_hshd";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.SalaryHead = ds.Tables[0];
            ViewBag.bt = bt;

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_hshd where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.SheadInfo = ds2.Tables[0];
            }

            return View();
        }

        public ActionResult SaveSalaryHead(string cpid, string oid, string name, string sign, string actv, string bt)
        {


            if (bt == "0")
            {
                string packge = "p_hshd.insert_data";
                _clsDataAccess.package_save_three(packge, name, sign, Session["UserId"].ToString());

            }
            else if (bt == "1")
            {
                string packge = "p_hshd.update_data";
                _clsDataAccess.package_Update_Four(packge, oid, name, sign, Session["UserId"].ToString());
            }
            else
            {
                if (actv == "1") { actv = "0"; }
                else { actv = "1"; }

                strComm = "update tflhr.t_hshd set hshd_actv ='" + actv + "', euser='" + Session["UserID"].ToString() + "' where OID ='" + cpid + "'";
                ds = new DataSet();
                ds = _clsDataAccess.read_data(strComm);
            }
            ViewBag.bt = bt;
            return RedirectToAction("SalaryHead", new { cpid = 0, bt = 0 });
        }

        public ActionResult Deduction(string cpid, string bt)
        {
            ds = new DataSet();
            ds = _clsSelectData.select_data_c4("p_esam.select_data_c4", "select4", "", "");

            ViewBag.employee = ds.Tables[0];
            ViewBag.SalCycle = ds.Tables[1];
            ViewBag.SalHead = ds.Tables[2];
            ViewBag.Deduction = ds.Tables[3];

            if (bt == "1")
            {
                strComm = "select * from tflhr.t_esam where OID ='" + cpid + "'";
                ds2 = new DataSet();
                ds2 = _clsDataAccess.read_data(strComm);
                ViewBag.DeductionDetails = ds2.Tables[0];
            }

            ViewBag.bt = bt;
            return View();
        }

        public ActionResult SaveDeduction(string cpid, string oid, string empl, string amnt, string note, string hshd,string fdate, string acyl, string actv, string bt)
        {

            DateTimeFormatInfo dtInfo = new DateTimeFormatInfo();
            dtInfo.ShortDatePattern = @"MM/dd/yyyy";
            DateTime fdat;
            fdat = Convert.ToDateTime(fdate, dtInfo);

            String ID = "ESAM" + "-"+ empl + "-" + fdate;   

            if (bt == "0")
            {
                _clsDataAccess.save_salary_deduction(ID, empl, acyl, hshd, fdat.ToString(), amnt, note, Session["UserId"].ToString());
            }
            else if (bt == "1")
            {
               _clsDataAccess.update_salary_deduction(oid, empl, acyl, hshd, fdat.ToString(), amnt, note, Session["UserId"].ToString());
            }
            else
            {
                strComm = "delete from tflhr.t_esam where t_esam.OID ='" + cpid + "'";
                _clsDataAccess.read_data(strComm);
            }
            ViewBag.bt = bt;
            return RedirectToAction("Deduction", new { cpid = 0, bt = 0 });

        }
	}
}