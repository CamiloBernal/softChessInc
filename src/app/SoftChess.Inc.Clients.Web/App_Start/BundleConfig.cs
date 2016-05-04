using System.Web.Optimization;

namespace SoftChess.Inc.Clients.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/libs/jquery/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/libs/bootstrap/js/bootstrap.js",
                "~/libs/respond/respond.js",
                "~/libs/pace/pace.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/chessboardjs").Include(
              "~/libs/chessboardjs/js/chessboard-0.3.0.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/libs/bootstrap/css/bootstrap.css",
                "~/libs/font-awesome/css/font-awesome.css",
                "~/libs/pace/themes/orange/pace-theme-center-radar.css",
                "~/styles/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/chessboardjs").Include(
               "~/libs/chessboardjs/css/chessboard-0.3.0.css"));

        }
    }
}