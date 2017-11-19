(function () {
    "use strict";
    angular.module("employeeDelete")
        .component("employeeDelete", {
            templateUrl: "/js/app2/components/employee/employee-delete/employee-delete.template.html",
            controller: function EmployeeListController(
                $scope,
                $http,
                $location,
                $routeParams,
                repoEmployees
            ) {

                var holder = this;
                
                $scope.deleteEmployee = function () {
                   
                    console.log("inside detele Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";

                    repoEmployees.detete($routeParams.employee.employeeId).then(function (response) {
                            //success
                            console.log("Response from server api" + response);
                            window.location.templateUrl = '#/employees';

                        }, function () {
                            //failure
                            holder.errorMessage = "Failure to save new employee";

                        }).finally(function () {
                            holder.isBusy = false;
                        });
                            
                        

                };

                
            }
            
        });
      
})();