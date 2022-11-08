using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRAdmin.Models;
using System.Data;
using System.Web.Security;

namespace HRAdmin.Controllers
{
    public class LoginController : Controller
    {
        private DataAccess _clsDataAccess = new DataAccess();
        private DataSet ds, ds2, ds3, ds4;
        string strComm, strComm2;

        public ActionResult Login()
        {
            if (Session["UserName"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.header = "Log In ";
            try
            {
                string message = Request.QueryString["message"];
                if (message == "2")
                {
                    ViewBag.Message = "User Name or Password do not match.";
                }
            }
            catch
            {
                return View();
            }
            return View();
        }


        public ActionResult LoginUser(string username, string password)
        {
            if (Session["UserName"] != null)
            {
                return RedirectToAction("Index", "Home");
            }


            string UserName = username;
            string UserPass = password;


            DataSet ds = new DataSet();
            ds = _clsDataAccess.read_data("select * from tflhr.t_user where upper(user_enid)='" + UserName.ToUpper() + "' "
                + "and user_pass='" + UserPass + "' and user_actv='1'");

            DataTable dt = new DataTable();

            if (ds == null)
            {
                return null;
            }
            dt = ds.Tables[0];
            try
            {
                byte[] img = (byte[])(dt.Rows[0]["image"]);
                string strBase64 = Convert.ToBase64String(img);
                ViewBag.ImageUrl = "data:Image/jpg;base64," + strBase64;
            }
            catch
            {
                ViewBag.ImageUrl = 0;
            }


            if (dt.Rows.Count > 0)
            {
                Session["image"] = ViewBag.ImageUrl;
                Session["UserID"] = dt.Rows[0]["USER_TEXT"].ToString();
                Session["UserName"] = dt.Rows[0]["USER_NAME"].ToString();
                Session["type"] = dt.Rows[0]["USER_type"].ToString();
                HttpContext.Session["UserID"] = dt.Rows[0]["USER_TEXT"].ToString();
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Login", "Login", new { message = 2 });
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
	}
}