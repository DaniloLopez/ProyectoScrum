using System.Web;
using System.Web.Optimization;

namespace AppEjemploLayout
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                        "~/Scripts/index.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js", "~/Scripts/respond.js", "~/Scripts/controls.js",
                      "~/Scripts/easing.js", "~/Scripts/flickerplate.min.js", "~/Scripts/jquery.chocolat.js",
                      "~/Scripts/jquery.filterizr.js", "~/Scripts/jquery.flexslider.js",
                      "~/Scripts/jquery.magnific-popup.js", "~/Scripts/jquery-2.1.4.min.js",
                      "~/Scripts/move-top.js", "~/Scripts/smoothbox.jquery2.js"));

            bundles.Add(new ScriptBundle("~/scripts/autocompletado").Include(
                "~/Scripts/jquery.easy-autocomplete.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/eventosProyecto").Include(
                "~/Scripts/ScriptsProyecto/ManejoEventos.js"));

            bundles.Add(new StyleBundle("~/Content/autocompletado").Include(
                "~/Content/easy-autocomplete.min.css",
                "~/Content/easy-autocomplete.themes.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css", "~/Content/chocolat.css",
                      "~/Content/estilo_login.css", "~/Content/flexslider.css", "~/Content/flickerplate.css",
                      "~/Content/popuo-box.css", "~/Content/smoothbox.css", "~/Content/style.css", "~/Content/estilo.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
