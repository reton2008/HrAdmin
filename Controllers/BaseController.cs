using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Data;
using System.Web.Mvc;
using HRAdmin.Models;
using System.Collections.Generic;
using SystemWebHttpContext = System.Web.HttpContext;

namespace HRAdmin.Controllers
{
    public class BaseController : Controller
    {
        private DataAccess _clsDataAccess = new DataAccess();
        private SelectData _clsSelectData = new SelectData();
        private DataSet ds, ds2;
        string strComm, strComm2;
        // GET: Base
        public BaseController()
        {

            if (SystemWebHttpContext.Current.Session["UserID"].ToString() == null)
            {
                RedirectToAction("Login", "Login");
            }

            ViewBag.Users = SystemWebHttpContext.Current.Session["UserID"].ToString();



            strComm = "select mnum_name, dpclass, a.oid from TFLHR.t_mnum a, TFLHR.t_mnud b, TFLHR.t_umnu c where (mnum_actv =1 and mnud_actv = 1 and umnu_actv = 1 and "
                   + "umnu_user = '" + ViewBag.Users + "')  and  (a.oid = b.mnud_mnum) and  (b.mnud_text = c.umnu_mnud) group by mnum_name, a.oid, mnum_text,dpclass order by mnum_text";
            ds = new DataSet();
            ds = _clsDataAccess.read_data(strComm);
            ViewBag.DataPass = ds.Tables[0];



            strComm2 = "select mnud_name, mnud_snam, mnud_page, mnud_mnum from tflhr.t_mnud a, tflhr.t_umnu b where(mnud_actv = 1 and umnu_actv = 1 and "
                     + "umnu_user = '" + ViewBag.Users + "') and  (a.mnud_text = b.umnu_mnud) order by mnud_mnum, mnud_seqn";
            ds2 = new DataSet();
            ds2 = _clsDataAccess.read_data(strComm2);
            ViewBag.DataPass2 = ds2.Tables[0];


        }
	}
}