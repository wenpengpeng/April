(function () {
    angular.module("app").controller("app.views.manager.edit", [
        '$scope', "$uibModalInstance", "$http", "id", '$timeout',
        function ($scope, $uibModalInstance, $http, id, $timeout) {

            //#region 申明

            var vm = this;
            //标题
            vm.title = "新增";
            //正在保存中
            vm.saving = false;

            vm.id = id;
            if (vm.id) {
                vm.title = "编辑";
            }
            //#endregion 

            //#region  操作

            //保存
            vm.save = function () {
                if (!vm.data) {
                    april.notify.error("查看授权失败！！");
                    return;
                }
                //角色部分
                vm.data.roleIds = [];
                $(".roleGroup :checkbox:checked").each(function (i, item) {
                    vm.data.roleIds.push($(item).val());
                });

                $http({
                    method: "POST",
                    url: "/api/ManagerService/CreateOrUpdateManager",
                    data: vm.data
                })
                    .then(function (result) {
                        april.notify.success("保存成功");
                        $uibModalInstance.close();
                    });
            }


            //初始化
            vm.init = function () {
                //获取用户基本信息
                $http({
                    method: "POST",
                    url: "/api/ManagerService/GetManagerByIdAsync",
                    data: { id: vm.id}
                }).then(function (resultUser) {
                    vm.data = resultUser.data;
                });
            }

            //取消
            vm.cancel = function () {
                $uibModalInstance.close();
            }

            //#endregion 

            //#region 监听部分

            $scope.$watch("vm.data", function () {
                $timeout(function () {
                    $(".roleGroup :checkbox").iCheck({
                        checkboxClass: 'icheckbox_square-blue',
                        radioClass: 'iradio_square-blue',
                        increaseArea: '15%' // optional
                    });
                });
            });

            //#endregion

            vm.init();
        }
    ]);
})();