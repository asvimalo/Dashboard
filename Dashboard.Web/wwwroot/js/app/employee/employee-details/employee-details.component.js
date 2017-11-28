(function () {
    "use strict";
    angular.module("employeeDetails")
        .component("employeeDetails", {
            templateUrl: "/js/app/employee/employee-details/employee-details.template.html",
            controller: function EmployeeDetailsController($scope, $http, $location, $routeParams, repoAssignments, repoEmployees)
            {
                this.employeeId = $routeParams.employeeId;

                var holder = this;
                holder.employee = {};

                // Get Employee 
                repoAssignments.get(holder.employeeId).then(function (response) {
                    angular.copy(response, holder.employee);
                    
                });

                $scope.assignments = {};

                // Skapa som one project Details
                // Data finns i Assignments 
            }
            
        });
      
})();