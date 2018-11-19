(function () {
    angular.module("app").controller("app.views.role.menuAuthorization",
        [
            '$scope', "$uibModalInstance", "id", "name", "$timeout",
            "$http",
            function ($scope, $uibModalInstance, id, name, $timeout, $http) {
                var vm = this;

                vm.id = id;
                vm.name = name;

                //数据实体
                vm.data = null;
                //保存对象构建
                vm.edit = {
                    roleId: vm.id,
                    menuIds: []
                };
                //复选框选择的值
                vm.selectedIdArray = [];

                /**
                 * 取消
                 * @returns {} 
                 */
                vm.cancel = function () {
                    $uibModalInstance.close();
                }

                //正在保存中
                vm.saving = false;

                //保存
                vm.save = function () {                    
                    //重置数组数据
                    vm.edit.menuIds = [];
                    vm.edit.menuAppAuthorizeIds = [];
                    //循环处理选择的Id
                    angular.forEach(vm.selectedIdArray,
                        function (item, index) {
                            if (item.indexOf("menu_") >= 0)
                                vm.edit.menuIds.push(parseInt(item.replace("menu_", "")));
                            if (item.indexOf("menuAppAuthorize_") >= 0)
                                vm.edit.menuAppAuthorizeIds.push(parseInt(item.replace("menuAppAuthorize_", "")));
                        });

                    //提交到接口
                    $http({
                        method: "POST",
                        url: "/api/RoleService/SetRoleMenusAsync",
                        data: vm.edit
                    })
                        .then(function (result) {
                            april.notify.success("保存成功！");
                            $uibModalInstance.close("保存成功");
                        });
                }
                //取消
                vm.cancel = function () {
                    $uibModalInstance.close();
                }

                /**
                 * 获取菜单JS树
                 * @returns {} 
                 */
                vm.treeMenu = function () {
                    $timeout(function () {
                        ////滚动脚本
                        $(".selectClass").slimScroll({
                            height: 300,
                            alwaysVisible: true
                        });
                        $http({
                            method: "POST",
                            url: "/api/MenuService/GetMenuJsTreeAuthorizeAsync",
                            data: { id: vm.id }
                        })
                            .then(function (result) {
                                //JStree搜索
                                $(".seachTreeNode").keyup(function () {
                                    var $thisVal = $(this).val();
                                    setTimeout(function () {
                                        $("#treeContainer").jstree(true).search($thisVal);
                                    },
                                        10);
                                });
                                //JStree初始化
                                $("#treeContainer").jstree({
                                    "core": {
                                        "data": result.data
                                    },
                                    "types": {
                                        "menu": {
                                            "icon": "fa fa-tags"
                                        },
                                        "text": {
                                            "icon": "fa fa-folder"
                                        },
                                        "opration": {
                                            "icon": "fa fa-cogs"
                                        }
                                    },
                                    "checkbox": {
                                        "keep_selected_style": false
                                    },
                                    "plugins": [
                                        "search",
                                        "checkbox",
                                        //"types",
                                        "wholerow"
                                    ]
                                }).on("changed.jstree",
                                    function (e, data) {
                                        //都先清空选择的数组对象
                                        vm.selectedIdArray = [];
                                        $scope.$apply(function () {
                                            for (var i = 0; i < data.selected.length; i++) {
                                                //选中节点Id
                                                var selectId = data.selected[i];
                                                if ($.inArray(selectId, vm.selectedIdArray) === -1)
                                                    vm.selectedIdArray.push(selectId);
                                                //选中节点的父级节点集合
                                                var selectNodeParents = data.instance.get_node(selectId).parents;
                                                for (var k = 0; k < selectNodeParents.length; k++) {
                                                    var parentId = selectNodeParents[k];
                                                    if (parentId !== "#" &&
                                                        $.inArray(parentId, vm.selectedIdArray) === -1)
                                                        vm.selectedIdArray.push(parentId);
                                                }
                                            }
                                        });
                                    });
                            });
                    },
                        100);
                }

                /**
                 * 初始化事件
                 * @returns {} 
                 */
                vm.init = function () {
                    vm.treeMenu();
                }

                vm.init();
            }
        ]);
})();