using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Troncoso

{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            #region Bundles Generales
            bundles.Add(new ScriptBundle("~/bundles/javaScript").Include(
                 "~/scripts/plugins/jquery/jquery.min.js",
                 "~/scripts/plugins/bootstrap/js/bootstrap.js",
                 "~/scripts/plugins/bootstrap-select/js/bootstrap-select.js",
                 "~/scripts/plugins/jquery-slimscroll/jquery.slimscroll.js",
               "~/scripts/plugins/node-waves/waves.js",
               "~/scripts/plugins/momentjs/moment.js",
               "~/scripts/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.js",
               "~/scripts/plugins/sweetalert/sweetalert.min.js",
                 "~/scripts/vue.min.js",
             "~/scripts/axios.min.js",
             "~/scripts/Encabezado.js",
               "~/scripts/js/admin.js"
               //"~/scripts/js/demo.js"
                 ));
            bundles.Add(new ScriptBundle("~/bundles/javaScriptLogin").Include(
                 "~/scripts/plugins/jquery/jquery.min.js",
                 "~/scripts/plugins/bootstrap/js/bootstrap.js",
                 "~/scripts/plugins/bootstrap-select/js/bootstrap-select.js",
                 "~/scripts/plugins/jquery-slimscroll/jquery.slimscroll.js",
               "~/scripts/plugins/node-waves/waves.js",
               "~/scripts/js/adminLogin.js"

                 ));
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                 "~/Content/KendoUI/js/kendo.all.min.js",
                 "~/Content/KendoUI/js/cultures/kendo.culture.es-MX.min.js",
                 "~/Content/KendoUI/js/jszip.min.js"
                 ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/scripts/plugins/bootstrap/css/bootstrap.css",
                "~/scripts/plugins/bootstrap-select/css/bootstrap-select.css",
                 "~/scripts/plugins/node-waves/waves.css",
                 "~/scripts/plugins/sweetalert/sweetalert.css",
                 "~/scripts/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css",
                 "~/scripts/plugins/animate-css/animate.css",
                    "~/Content/css/style.css",
                    "~/Content/css/themes/all-themes.css"
             ));
            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                 "~/Content/KendoUI/styles/kendo.common.min.css",
                 "~/Content/KendoUI/styles/kendo.rtl.min.css",
                   "~/Content/KendoUI/styles/kendo.material.min.css",
                "~/Content/KendoUI/styles/kendo.dataviz.min.css",
                "~/Content/KendoUI/styles/kendo.dataviz.default.min.css",
                    "~/Content/KendoUI/styles/kendo.mobile.all.min.css",
                     "~/Content/popup/normalize.min.css",
                       "~/Content/popup/animate.min.css"


                 ));
            #endregion

            #region Bundles Especificos



            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                 "~/scripts/js/demo.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                "~/scripts/plugins/jquery-validation/jquery.validate.js",
                "~/scripts/js/pages/examples/sign-in.js" 
              ));


            bundles.Add(new ScriptBundle("~/bundles/javaScriptSinPermiso").Include(
             "~/scripts/plugins/jquery/jquery.min.js",
             "~/scripts/plugins/bootstrap/js/bootstrap.js",
           "~/scripts/plugins/node-waves/waves.js"
             ));

            bundles.Add(new StyleBundle("~/Content/cssSinPermiso").Include(
                "~/scripts/plugins/bootstrap/css/bootstrap.css",
                 "~/scripts/plugins/node-waves/waves.css",
                    "~/Content/css/style.css"
             ));
            
            #endregion
        }













    }
}