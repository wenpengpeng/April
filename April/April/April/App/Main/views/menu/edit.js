(function () {
    angular.module("app").controller("app.views.menu.edit", [
        '$scope', '$uibModal', "$uibModalInstance", "$http", "id", '$timeout',
        function ($scope, $uibModal, $uibModalInstance, $http, id, $timeout) {
            var vm = this;
            //标题
            vm.title = "新增";
            vm.id = id;
            if (vm.id) {
                vm.title = "编辑";
            }
            //数据实体
            vm.data = null;


            //父级菜单
            vm.menuSelects = [];
            //父级菜单
            vm.parent = null;
            vm.category = "1";

            //保存
            vm.save = function () {
                vm.data.parentId = $.isEmptyObject(vm.parent) ? null : parseInt(vm.parent);
                vm.data.category = parseInt(vm.category);
                if (vm.id > 0) {
                    $http({
                        method: "POST",
                        url: "/api/MenuService/UpdateMenuAsync",
                        data: vm.data
                    })
                        .then(function (result) {
                            april.notify.success("保存成功！");
                            $uibModalInstance.close();
                        });
                } else {
                    $http({
                        method: "POST",
                        url: "/api/MenuService/CreateMenuAsync",
                        data: vm.data
                    })
                        .then(function (result) {
                            april.notify.success("新增成功！");
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
                    url: "/api/MenuService/GetMenuForEditAsync",
                    data: { id: vm.id }
                })
                    .then(function (result) {
                        vm.data = result.data;

                        vm.parent = vm.data.parentId == null ? "" : vm.data.parentId + "";

                        if (vm.data.category > 0) {
                            vm.category = vm.data.category + "";
                        }
                    });
            };

            //获取select数据
            vm.getMenuSelects = function () {
                $http({
                    method: "POST",
                    url: "/api/MenuService/GetMenuSelectAsync"
                })
                    .then(function (result) {
                        vm.menuSelects = result.data;
                    });
            }

            // 获取图标
            vm.selectIcon = function () {
                openSelectIconModal();
            };

            //编辑模态框
            function openSelectIconModal() {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/App/Main/views/menu/facss.html',
                    controller: 'app.views.menu.facss as vm',
                    backdrop: 'static',
                    resolve: {//这是一个入参,这个很重要,它可以把主控制器中的参数传到模态框控制器中

                    }
                });

                modalInstance.result.then(function (result) {
                    vm.data.icon = result;
                });
            }


            vm.init = function () {
                vm.getMenuSelects();
                vm.getData();
            }

            $scope.$watch('vm.data', function () {
                $timeout(function () {
                    $('input').iCheck({
                        checkboxClass: 'icheckbox_square',
                        radioClass: 'iradio_square',
                        increaseArea: '15%' // optional
                    }).on('ifChanged', function (event) {
                        if ($(this).hasClass("isValid")) {
                            vm.data.isValid = !vm.data.isValid;
                        } else {
                            vm.data.isPublic = !vm.data.isPublic;
                        }
                    });
                });
            });

            vm.init();
        }
    ]);
})();