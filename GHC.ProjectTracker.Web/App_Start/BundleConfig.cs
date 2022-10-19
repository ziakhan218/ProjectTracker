using System.Web;
using System.Web.Optimization;

namespace GHC.ProjectTracker.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/metroui").Include(
                        "~/Scripts/metro.js"));

            //Temporarily disabled minification of Angular as requires additional changes (change Bundle back to ScriptBundle to enable minification again)
            bundles.Add(new Bundle("~/bundles/angular").Include(                
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/ui-grid.js",
                        //"~/Scripts/angular-ui/ui-bootstrap.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                        "~/Scripts/xeditable.js",
                        "~/Scripts/multiselect.js",
                        "~/Scripts/jquery.bootpag.min.js",
                        "~/Scripts/angular-gridStats.js",
                        "~/Scripts/fcsaNumber.min.js",
                        "~/Scripts/angular-aria.js",
                        "~/Scripts/angular-animate.js",
                        "~/Scripts/angular-messages.js",
                        "~/Scripts/angular-material-icons.js",
                        "~/Scripts/angular-material.js"
                        ));
           

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/metro-panels.css",
                      "~/Content/grosvenor.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/ui-grid.css",
                      "~/Content/slick.css",
                      "~/Content/slick-theme.css",
                      "~/Content/angular-material.css",
                      "~/Content/angular-material.layouts.css",
                      "~/Content/xeditable.css"));

            bundles.Add(new Bundle("~/bundles/dashbaord").Include(
                                  "~/App/Dashboard/App.js",
                                  "~/Scripts/moment.js",
                                  "~/App/Dashboard/Controllers/ctrl-dashboard.js"
                                  ));
        }
    }
}
