﻿(function () {
    "use strict";
    angular.module("employeeDelete")
        .component("employeeDelete", {
            templateUrl: "/js/app/components/employee/employee-delete/employee-delete.template.html",
            controller: function EmployeeListController($scope, $http, $routeParams, repoEmployees)
            {
                this.employeeId = $routeParams.employeeId;
                var holder = this;
                holder.employee = {};

                if ($routeParams.employeeId == null) {
                    this.employeeId = $routeParams.employeeId;
                    console.log("The employeeId is null");
                } else {
                    this.employeeId = $routeParams.employeeId;

                    holder.employee = {};

                    repoEmployees.get(holder.employeeId).then(function (response) {
                        holder.employee = response;
                    });

                    $scope.deleteEmployee = function () {
                        repoEmployees.delete(holder.employeeId).then(function (response) {
                            location.replace("#!/allemployees");
                            location.reload();
                        });
                        
                    };
                     

                }
                 
            }
            
        });
      
})();