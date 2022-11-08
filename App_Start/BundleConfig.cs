using System.Web;
using System.Web.Optimization;

namespace HRAdmin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/jquery-3.1.1.min.js",
                      "~/Scripts/jquery-ui.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/material.min.js",
                      "~/Scripts/perfect-scrollbar.jquery.min.js",
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/moment.min.js",
                      "~/Scripts/chartist.min.js",
                      "~/Scripts/jquery.bootstrap-wizard.js",
                      "~/Scripts/bootstrap-notify.js",
                      "~/Scripts/jquery.sharrre.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/jquery-jvectormap.js",
                      "~/Scripts/nouislider.min.js",
                      "~/Scripts/jquery.select-bootstrap.js",
                      "~/Scripts/jquery.datatables.js",
                      "~/Scripts/sweetalert2.js",
                      "~/Scripts/jasny-bootstrap.min.js",
                      "~/Scripts/fullcalendar.min.js",
                      "~/Scripts/jquery.tagsinput.js",
                      "~/Scripts/material-dashboard.js",
                      "~/Scripts/demo.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/material-dashboard.css",
                      "~/Content/demo.css",
                      "~/Content/font-awesome.css",
                      "~/Content/google-roboto-300-700.css"));
        }
    }
}
