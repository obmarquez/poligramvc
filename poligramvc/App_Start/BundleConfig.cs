using System.Web;
using System.Web.Optimization;

namespace poligramvc
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/jquery-3.1.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/bootstrap.min.js",
                      "~/Content/js/plugins/metisMenu/jquery.metisMenu.js",
                      "~/Content/js/plugins/slimscroll/jquery.slimscroll.min.js",
                      "~/Content/js/inspinia.js",
                      "~/Content/js/plugins/pace/pace.min.js"));

            bundles.Add(new StyleBundle("~/Content/css/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      //"~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/css/animate.css",
                      //"~/Content/css/style_inspinia.css",
                      "~/Content/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/css/plugins/dataTables/dataTables").Include(
                     "~/Content/css/plugins/dataTables/dataTables.bootstrap.css",
                     "~/Content/css/plugins/dataTables/dataTables.responsive.css",
                     "~/Content/css/plugins/dataTables/dataTables.tableTools.min.css"));

            // chosen styles
            bundles.Add(new StyleBundle("~/Content/css/plugins/chosen/chosenStyles").Include(
                      "~/Content/css/plugins/chosen/bootstrap-chosen.css"));

            // chosen 
            bundles.Add(new ScriptBundle("~/Content/js/plugins/chosen/chosenjs").Include(
                      "~/Content/js/plugins/chosen/chosen.jquery.js"));
            // Select2 Styless
            bundles.Add(new StyleBundle("~/Content/css/plugins/select2/select2Styles").Include(
                      "~/Content/css/plugins/select2/select2.min.css"));

            // Select2
            bundles.Add(new ScriptBundle("~/Content/js/plugins/select2/select2js").Include(
                      "~/Content/js/plugins/select2/select2.full.min.js"));
            // dataPicker styles
            bundles.Add(new StyleBundle("~/Content/css/plugins/datapicker/dataPickerStyles").Include(
                      "~/Content/css/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/Content/js/plugins/datapicker/dataPickerjs").Include(
                      "~/Content/js/plugins/datapicker/bootstrap-datepicker.js"));

            // icheck style
            bundles.Add(new StyleBundle("~/Content/css/plugins/iCheck/iCheckStyles").Include(
                    "~/Content/css/plugins/custom.css"));

            //checks style
            bundles.Add(new StyleBundle("~/Content/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkboxStyles").Include(
                     "~/Content/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css"));

            //ichecks js
            bundles.Add(new ScriptBundle("~/Content/js/plugins/iCheck/iCheckjs").Include("~/Content/js/plugins/iCheck/icheck.min.js"));
        }
    }
}
