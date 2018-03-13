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
                        "~/assets/data/themes/saostarv4/assets/vendors/iframe-resizer/iframeResizer.min.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/swiper/swiper.min.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/iscroll/iscroll.min.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/videojs/video.min.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/ima3js/ima3.js",
//                        "~/assets/data/themes/saostarv4/assets/vendors/videojs/videojs.ads.min.js",
//                        "~/assets/data/themes/saostarv4/assets/vendors/videojs/videojs.ima.js",
//                        "~/assets/data/themes/saostarv4/assets/vendors/videojs/videojs-contrib-hls.min.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/lazyload/jquery.lazyloadxt.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/sticky-kit/jquery.sticky-kit.min.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/menu/headroom.min.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/menu/jQuery.headroom.min.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/owl-carousel/owl.carousel.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/appear/jquery.appear.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/remodal/js/remodal.js",
                        "~/assets/data/themes/saostarv4/assets/vendors/audio/jquery.cleanaudioplayer.js",
                        "~/assets/data/themes/saostarv4/assets/js/main.js",
                        "~/assets/data/themes/saostarv4/assets/js/setup-video.js",
                        "~/Scripts/script.js"
                        ));

            bundles.Add(new StyleBundle("~/bundles/style").Include(
                      "~/assets/data/themes/saostarv4/assets/vendors/videojs/video-js.min.cs",
                      "~/assets/data/themes/saostarv4/assets/vendors/videojs/videojs.ads.css",
                      "~/assets/data/themes/saostarv4/assets/vendors/videojs/videojs.ima.css",
                      "~/assets/data/themes/saostarv4/assets/css/fonts.min.css",
                      "~/assets/data/themes/saostarv4/assets/vendors/swiper/swiper.min.css",
                      "~/assets/data/themes/saostarv4/assets/vendors/videojs/saostar-player.css",
                      "~/assets/data/themes/saostarv4/assets/vendors/owl-carousel/owl.carousel.css",
                      "~/assets/data/themes/saostarv4/assets/vendors/remodal/css/remodal.css",
                      "~/assets/data/themes/saostarv4/assets/vendors/audio/player.css",
                      "~/assets/data/themes/saostarv4/assets/vendors/saostar-custom-icon/style.css",
                      "~/assets/data/themes/saostarv4/assets/css/style.css",
                      "~/assets/data/themes/saostarv4/assets/css/Responsive.css"
                      ));
        }
    }
}
