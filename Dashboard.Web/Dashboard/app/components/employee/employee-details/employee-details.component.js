(function () {
    "use strict";
    angular.module("employeeDetails")
        .component("employeeDetails", {
            templateUrl: "/Dashboard/app/components/employee/employee-details/employee-details.template.html",
            controller: function EmployeeListController(
                $scope,
                $location,
                $routeParams,
                repoEmployee) {

                var holder = this;
                $scope.detailsEmployee = function () {
                };
            }
        });
        
})();