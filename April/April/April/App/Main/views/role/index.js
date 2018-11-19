(function () {
    angular.module("app").controller('app.views.role.index', [
        '$scope', '$uibModal', '$stateParams', "$http",
        function ($scope, $uibModal, $stateParams, $http) {
            var vm = this;
            //页面数据
            vm.data = null;
            

            //#region 分页
            vm.page = {
                pageSize: 10 //每页显示数量
            };

            //请求参数申明
            vm.requestParams = {
                filterText: null,
                maxResultCount: vm.page.pageSize,
                skipCount: 0
            };

            //绑定分页事件，只需要绑定一次即可
            $("#pagination").on("page", function (event, num) {
                //设置跳转的数量
                vm.requestParams.skipCount = (num - 1) * vm.page.pageSize;
                $(".pageIndex").html(num);
                $scope.$apply(vm.getList());
            });

            //获取分页列表
            vm.getList = function () {
                $http({
                    method: "POST",
                    url: "/api/RoleService/GetPagedRoleAsync",
                    data: vm.requestParams
                })
                    .then(function (result) {
                        vm.page.totalCount = result.data.totalCount;
                        vm.data = result.data.items;
                        //需要将元素制空，然后再设置分页数据（用于解决两个tab公用一个分页的情况）
                        var total = 0;
                        if (vm.page.totalCount <= vm.page.pageSize && vm.page.totalCount !== 0) {
                            total = 1;
                        } else if (vm.page.totalCount > vm.page.pageSize) {
                            total = Math.ceil(vm.page.totalCount / vm.page.pageSize);
                        }
                        //重新创建分页对象
                        $('#pagination').empty().bootpag({
                            first: "首页",
                            last: "末页",
                            maxVisible: 5,
                            firstLastUse: true, //启用首，末页
                            next: "下一页",
                            prev: "上一页",
                            total: total
                        });
                    });
            };
            //#endregion

            //#region 操作
            //锁定或解锁
            vm.lock = function (item) {
                $http({
                    method: "POST",
                    url: "/api/RoleService/LockRoleAsync",
                    data: { id: item.id }
                })
                    .then(function (result) {
                        april.notify.success("保存成功！");
                        vm.getList();
                    });
            };

            //刷新
            vm.refresh = function () {
                $('#pagination').empty().bootpag({
                    page: 1
                });
                vm.requestParams.skipCount = 0;

                vm.getList();
            };

            //编辑
            vm.edit = function (item) {
                if (item != null) {
                    openCreateOrEditModal(item.id);
                } else {
                    openCreateOrEditModal(null);
                }
            };

            //编辑模态框
            function openCreateOrEditModal(id) {
                debugger;
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/role/edit.html',
                    controller: 'app.views.role.edit as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return id;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getList();
                });
            }

            //#endregion

            //#region
            vm.menuAuthorization = function (item) {
                openAuthorizationModal(item);
            }

            //编辑模态框
            function openAuthorizationModal(item) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/role/menuAuthorization.html',
                    controller: 'app.views.role.menuAuthorization as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return item.id;
                        },
                        name: function () {
                            return item.name;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getList();
                });
            }

            //#endregion

            vm.getList();
        }
    ]);
})();