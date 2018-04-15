using System.Web;
using System.Web.Optimization;

namespace BaoKhoe
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/assets/data/themes/saostarv4/assets/vendors/swiper/swiper.min.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/iscroll/iscroll.min.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/owl-carousel/owl.carousel.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/appear/jquery.appear.js",
                        "~/assets/data/themes/saostarv4/assets/js/main.js",
                        "~/Scripts/script.js"
                        ));

            bundles.Add(new StyleBundle("~/bundles/style").Include(
                      "~/assets/data/themes/saostarv4/assets/css/fonts.min.css",
                      "~/assets/data/themes/saostarv4/assets/vendors/swiper/swiper.min.css",
                      "~/assets/data/themes/saostarv4/assets/vendors/owl-carousel/owl.carousel.css",
                      "~/assets/data/themes/saostarv4/assets/vendors/saostar-custom-icon/style.css",
                      "~/assets/data/themes/saostarv4/assets/css/style.css",
                      "~/assets/data/themes/saostarv4/assets/css/Responsive.css"
                      ));
        }
    }
}
