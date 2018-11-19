(function () {
    angular.module("app").controller("app.views.role.edit", [
        '$scope', "$uibModalInstance", "$http", "id", '$timeout',
        function ($scope, $uibModalInstance, $http, id, $timeout) {
            var vm = this;

            //标题
            vm.title = "新增";
            vm.id = id;
            if (vm.id) {
                vm.title = "编辑";
            }
            //数据实体
            vm.data = null;

            //保存
            vm.save = function () {
                if (vm.id) {
                    $http({
                        method: "POST",
                        url: "/api/RoleService/UpdateRoleAsync",
                        data: vm.data
                    })
                        .then(function (result) {
                            april.notify.success("保存成功！");
                            $uibModalInstance.close();
                        });
                } else {
                    $http({
                        method: "POST",
                        url: "/api/RoleService/CreateRoleAsync",
                        data: vm.data
                    })
                        .then(function (result) {
                            april.notify.success("保存成功！");
                            $uibModalInstance.close();
                        });
                }
            }
            //取消
            vm.cancel = function () {
                $uibModalInstance.close();
            }

            //获取data
            vm.getData = function () {
                $http({
                    method: "POST",
                    url: "/api/RoleService/GetRoleByIdAsync",
                    data: { id: vm.id }
                })
                    .then(function (result) {                        
                        vm.data = result.data;
                    });
            };

            vm.init = function () {
                vm.getData();
            }

            $scope.$watch('vm.data', function () {
                $timeout(function () {
                    $('.isValid').iCheck({
                        checkboxClass: 'icheckbox_square',
                        radioClass: 'iradio_square',
                        increaseArea: '15%' // optional
                    }).on('ifChanged', function (event) {
                        vm.data.isValid = !vm.data.isValid;
                    });
                });
            });

            vm.init();
        }
    ]);
})();