(function () {
    angular.module("app").controller('app.views.menu.index', [
        '$scope', '$uibModal', '$stateParams', "$http",
        function ($scope, $uibModal, $stateParams, $http) {
            var vm = this;
            //页面数据
            vm.data = null;



            vm.getData = function () {
                $http({
                    method: "POST",
                    url: "/api/MenuService/GetMenuTreeAsync"
                })
                    .then(function (result) {
                        vm.data = result.data;
                    });
            };

            vm.getData();

            //等待dom渲染完毕，初始化轮播插件
            $scope.$on('ngRepeatFinished', function () {
                $('.tree').treegrid({ initialState: "collapsed" });
            });

            //锁定或解锁
            vm.lock = function (item) {
                $http({
                    method: "POST",
                    url: "/api/MenuService/LockMenuAsync",
                    data: { id: item.id }
                })
                    .then(function (result) {
                        april.notify.success("保存成功！");
                        vm.getData();
                    });
            };

            //刷新
            vm.refresh = function () {
                vm.getData();
            };


            vm.edit = function (item) {
                if (item != null) {
                    openCreateOrEditModal(item.id);
                } else {
                    openCreateOrEditModal(null);
                }
            };

            //编辑模态框
            function openCreateOrEditModal(id) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/menu/edit.html',
                    controller: 'app.views.menu.edit as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return id;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getData();
                });
            }

            vm.menuAppAuthorize = function (item) {
                openMenuAppAuthorizeModal(item);
            }

            //编辑模态框
            function openMenuAppAuthorizeModal(item) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/menu/menuAppAuthorize.html',
                    controller: 'app.views.menu.menuAppAuthorize as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return item.id;
                        },
                        displayName: function () {
                            return item.displayName;
                        },
                        code: function () {
                            return item.code;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getList();
                });
            }

        }
    ]).directive('renderFinish', function ($timeout) {
        return {
            restrict: 'E,C,A,M',
            link: function (scope, element, attr) {

                if (scope.$last === true) {

                    $timeout(function () {
                        scope.$emit('ngRepeatFinished');
                    });
                }
            }
        }
    });
})();