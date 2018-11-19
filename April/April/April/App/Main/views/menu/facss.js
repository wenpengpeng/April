(function () {
    angular.module("app").controller("app.views.menu.facss", [
        '$scope', "$uibModalInstance", '$timeout',
        function ($scope, $uibModalInstance, $timeout) {
            var vm = this;
            //标题
            vm.title = "选取图标";

            //取消
            vm.cancel = function () {
                $uibModalInstance.close('');
            }

            // 当执行完成后在绑定事件
            $scope.$watch('vm.title', function () {
                $timeout(function () {
                    $("#icons").find('a').each(function () {
                        $(this).on('dblclick', function () {
                            $uibModalInstance.close($(this).find('i').attr('class'));
                        });
                    });


                });

            });
        }
    ]);
})();