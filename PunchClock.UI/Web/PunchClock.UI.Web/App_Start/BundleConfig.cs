using System.Web;
using System.Web.Optimization;

namespace PunchClock.UI.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            //bundles.Add(new ScriptBundle("~/bundles/kScripts").Include(
            //             "~/Scripts/kendo/2013.2.716/kendo.web.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                        "~/Scripts/pcCore.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryInputMask").Include(
                      "~/Scripts/jquery.inputmask/jquery.inputmask-2.4.15.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryMask").Include(
                      "~/Scripts/jquery.maskedinput-1.3.1.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

          

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
              "~/Content/bootstrap.css",
              "~/Content/superhero.css",
               "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/cssMobile").Include("~/Content/Site.Mobile"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            //bundles.Add(new StyleBundle("~/Content/kStyles").Include(
            //    "~/Content/kendo/2013.2.716/kendo.common.min.css",
            //    "~/Content/kendo/2013.2.716/kendo.default.min.css",
            //    "~/Content/kendo/2013.2.716/kendo.blueopal.min.css"));

         
        }
    }
}
