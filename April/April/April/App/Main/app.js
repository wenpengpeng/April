(function () {
    "use strict"; 
    var appModule = angular.module("app",
        [
            "ngAnimate",
            "ngSanitize",
            "ui.router",
            "ui.bootstrap",
            "oc.lazyLoad",
            "ui.jq"
        ]);
    //定义全局变量menuCode初始化为home
    appModule.value("menuCode","home");
    appModule.provider("dynamicRoute",
        function () { //加载动态路由数据
            var routeList; //路由数据集合
            return {
                setRouteList: function () {                    
                    $.ajax({
                        type: "POST",
                        url: "api/menuService/getDynamicRouteAsync",
                        async: false,
                        success: function (result) {
                            routeList = result;
                        },
                        error: function (result) {
                            alert(result);
                        }
                    });
                },
                $get: function () {
                    return {
                        routeList: routeList
                    }
                }
            }
        });
    //错误拦截，把错误显示出来
    appModule.factory("errorInterceptor", ["$q", function ($q) {
        return {
            'responseError': function (response) {
                var defer = $q.defer();                
                swal({
                    title: "错误",
                    type:"error",
                    text: response.data.exceptionMessage,
                    showCancelButton: false,
                    closeOnConfirm: false
                });
                defer.reject(response);
                return defer.promise;
            }
        };
    }]);
    //每次请求时加上menuCode请求头
    appModule.factory("setMenuCodeInterceptor",
        [
            "$q", "$rootScope", function ($q, $rootScope) {
                return {
                    'request': function(config) {
                        var url = config.url;
                        if (!(url.indexOf(".html") >= 0)) {
                            config.headers['menuCode'] =
                                $rootScope.menuCode;
                        }
                        return config;
                    }
                };
            }
        ]);
    appModule.config(["$httpProvider", "$stateProvider", "$urlRouterProvider", "$locationProvider", "$qProvider", "dynamicRouteProvider", function ($httpProvider,$stateProvider, $urlRouterProvider, $locationProvider, $qProvider, dynamicRouteProvider) {
        $locationProvider.hashPrefix('');//去除地址前缀里的！
        $qProvider.errorOnUnhandledRejections(false);
        $httpProvider.interceptors.push("errorInterceptor");//注册错误拦截器
        $httpProvider.interceptors.push("setMenuCodeInterceptor");//注册设置menuCode请求拦截器

        $stateProvider.state('home',
            {
                url: '/home',
                templateUrl: '/App/Main/views/home/home.html' //这里只支持html文件，不支持cshtml                   
            });       

        dynamicRouteProvider.setRouteList();
        var routeList = dynamicRouteProvider.$get().routeList;//获取到路由数据
        if (!routeList) {
            return;
        }
        window.authorizationMenus = routeList;//将路由数据放到window对象中

        $.each(routeList,
            function (idx, item) {
                var menuCode = item.code;
                if (!$.isEmptyObject(menuCode) && item.requestUrl != null) {//有url的数据才加载到$stateProvider.state中                        
                    $stateProvider.state(menuCode,
                        {                               
                            url: "/" + menuCode,
                            templateUrl: item.requestUrl
                        });
                }
            });        
        $urlRouterProvider.otherwise("/home");//默认路由
    }]);

})();