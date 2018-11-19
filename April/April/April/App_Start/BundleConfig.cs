// 文件名：Class1.cs
// 
// 创建标识：温朋朋 2018-06-22 10:49
// 
// 修改标识：温朋朋2018-06-22 10:49
// 
// ------------------------------------------------------------------------------
using System.Web.Optimization;
namespace April
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            //~/Bundles/App/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/App/vendor/css")
                    .Include("~/Content/themes/base/all.css", new CssRewriteUrlTransform())
                    .Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/toastr.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/ionicons.min.css", new CssRewriteUrlTransform())
                    .Include("~/Scripts/sweetalert/sweet-alert.css", new CssRewriteUrlTransform())
                    .Include("~/Content/flags/famfamfam-flags.css", new CssRewriteUrlTransform())
                    .Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/common.css")
            );
            //~/Bundles/App/vendor/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/vendor/js")
                    .Include(                       
                        "~/Scripts/json2/json2.min.js",
                        "~/Scripts/modernizr/modernizr-2.8.3.js",
                        "~/Scripts/jquery/jquery-2.1.4.min.js", 
                        "~/Scripts/jquery-ui/jquery-ui-1.11.4.min.js",
                        "~/Scripts/bootstrap/bootstrap.min.js",
                        "~/Scripts/moment/moment.min.js",
                        "~/Scripts/moment/moment-with-locales.min.js",
                        "~/Scripts/jquery-blockui/jquery.blockui.min.js",
                        "~/Scripts/toastr/toastr.min.js",
                        "~/Scripts/sweetalert/sweet-alert.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",
                        "~/Scripts/angular/angular.min.js",
                        "~/Scripts/angular-animate/angular-animate.min.js",
                        "~/Scripts/angular-sanitize/angular-sanitize.min.js",
                        "~/Scripts/angular-ui-router/angular-ui-router.min.js",
                        "~/Scripts/angular-ui/ui-bootstrap.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                        "~/Scripts/angular-ui/ui-utils.min.js",
                        "~/Scripts/angular-ocLazyLoad/ocLazyLoad.min.js",                        
                        //新增js                       
                        "~/Scripts/ui-router-extras/ct-ui-router-extras.core.js",//多tab状态暂存
                        "~/Scripts/ui-router-extras/ct-ui-router-extras.statevis.js",
                        "~/Scripts/ui-router-extras/ct-ui-router-extras.sticky.js",
                        "~/Scripts/ui-router-extras/ct-ui-router-extras.dsr.js",
                        "~/App/Main/adminlte/js/adminlte.min.js",//adminlte
                        "~/april/april.js",
                        "~/App/Main/app.js", //angularjs入口函数                        
                        "~/App/Main/views/layout/header.js",//布局js
                        "~/App/Main/views/layout/layout.js",
                        "~/App/Main/views/layout/sidebar.js",
                        "~/App/Main/views/layout/toolbar.js",                        
                        //验证
                        "~/Scripts/jquery.validate/jquery.validate.min.js",
                        "~/Scripts/jquery.validate/localization/messages_zh.min.js",
                        "~/Scripts/jquery-slimscroll/jquery.slimscroll.min.js"
                    )
            );

            //APPLICATION RESOURCES

            //~/Bundles/App/Main/css
            bundles.Add(
                new StyleBundle("~/Bundles/App/Main/css")
                    .Include("~/App/Main/adminlte/css/AdminLTE.min.css", new CssRewriteUrlTransform())
                    .Include("~/App/Main/adminlte/css/skins/skin-green.min.css", new CssRewriteUrlTransform())
                    .Include("~/App/Main/adminlte/css/skins/_all-skins.min.css", new CssRewriteUrlTransform())
                    .Include("~/App/Main/views/layout/layoutCss.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/ngclock.css", new CssRewriteUrlTransform())
                    .Include("~/Scripts/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css", new CssRewriteUrlTransform())
                    .Include("~/Scripts/bootstrap-daterangepicker/daterangepicker.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/iconfontNew.css",new CssRewriteUrlTransform())

                    .Include("~/Public/styles/publicTable.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/publicSelected.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/publicIconStyle.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/publicForm.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/tableLength.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/buttonColor.css", new CssRewriteUrlTransform())

                    .Include("~/Public/styles/firstModify.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/secondPublic.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/baseInfoDisplay.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/tabSwitch.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/businessJs.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/quotationAttachment.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/registerInfomation.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/bankCardStyle.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/ruleTable.css", new CssRewriteUrlTransform())
                    .Include("~/Public/styles/upload.css", new CssRewriteUrlTransform())
                    .Include("~/Scripts/iCheck/skins/all.css", new CssRewriteUrlTransform())
                    .Include("~/Scripts/treegrid/jquery.treegrid.css", new CssRewriteUrlTransform())
                    .Include("~/Scripts/jstree/themes/default/style.css",new CssRewriteUrlTransform())
            );
            //~/Bundles/App/Main/js
            bundles.Add(new ScriptBundle("~/Bundles/App/Main/js")               
                .Include("~/Public/scripts/helper.js")
                .Include("~/Scripts/jqPaginator/jquery.bootpag.min.js")
                .Include("~/Scripts/iCheck/icheck.min.js")
                .Include("~/Scripts/jstree/jstree.min.js")
                .Include("~/Scripts/treegrid/jquery.treegrid.min.js")
                .Include("~/Scripts/treegrid/jquery.treegrid.bootstrap3.js")
                .Include("~/App/Main/views/role/index.js")
                .Include("~/App/Main/views/role/edit.js")
                .Include("~/App/Main/views/role/menuAuthorization.js")
                .Include("~/App/Main/views/menu/index.js")
                .Include("~/App/Main/views/menu/edit.js")
                .Include("~/App/Main/views/menu/facss.js")
                .Include("~/App/Main/views/menu/menuAppAuthorize.js")
                .Include("~/App/Main/views/manager/index.js")
                .Include("~/App/Main/views/manager/edit.js")
                );
                
             BundleTable.EnableOptimizations = false;
        }
    }
}