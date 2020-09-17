using System.Web;
using System.Web.Optimization;

namespace MovieWeb
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/layui").Include(
                      "~/Content/layui.css"));

            bundles.Add(new StyleBundle("~/Content/mjx_package/css").Include(
         "~/Content/mjx_package/fontawesome-free/css/all.min.css",
         "~/Content/mjx_package/semantic/semantic.min.css",
         "~/Content/mjx_package/unicons-2.0.1/css/unicons.css"));
            bundles.Add(new StyleBundle("~/Content/list").Include(
              "~/Content/css/amazeui.min.css",
"~/Content/css/amazeui.datatables.min.css",
"~/Content/css/app.css"
));
            bundles.Add(new ScriptBundle("~/list/js").Include(
                "~/Content/js/jquery-3.3.1.min.js",
                "~/Content/js/admin.js",
                "~/Content/js/theme.js",
                "~/Content/js/amazeui.min.js.js",
                "~/Content/js/amazeui.datatables.min.js",
                "~/Content/js/app.js",
                "~/Content/js/category-list.js",
                "~/Content/js/category-add.js"
                ));
        }
    }
}
