(function () {
    angular.module("app").controller("app.views.menu.menuAppAuthorize", [
        '$scope', "$http", "$uibModalInstance", "id", "displayName", "code",
        function ($scope, $http, $uibModalInstance, id, displayName, code) {
            var vm = this;
            vm.menu = {
                id: id,
                displayName: displayName,
                code: code
            };
            //数据实体
            vm.data = null;


            //获取data
            vm.getData = function () {
                $http({
                    method: "POST",
                    url: "/api/MenuAppAuthorizeService/GetAppAuthorizeListAsync",
                    data: { id: vm.menu.id }
                })
                    .then(function (result) {
                        vm.data = result.data;
                    });
            };

            //取消
            vm.cancel = function () {
                $uibModalInstance.close();
            }

            //保存
            vm.save = function () {
                var methodList = [];
                $(".app-method").each(function () {
                    if ($(this).find("input").is(":checked")) {
                        var method = {};
                        method.menuId = vm.menu.id;
                        method.MenuCode = vm.menu.code;
                        method.operationCode = $(this).find("input").attr("code");
                        method.appCode = $(this).attr("appcode");
                        method.operationDescription = $(this).find("input").attr("desc");
                        methodList.push(method);
                    }
                });
                var input = {
                    editMenuAppAuthorizes: methodList,
                    menuId: vm.menu.id
                };
                $http({
                    method: 'POST',
                    url: "/api/MenuAppAuthorizeService/SaveMenuAppAuthorizeAsync",
                    data: input
                }).then(function () {
                    april.notify.success("保存成功！");
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });;
            };

            vm.init = function () {
                vm.getData();
            }

            vm.init();
        }
    ]);
})();