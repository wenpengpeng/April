(function () {
    angular.module("app").controller("app.views.toolbar",
        [
            "$scope", 
            function ($scope) {
                var vm = this;
                vm.name = "April";                
               
            }
        ]);
})();