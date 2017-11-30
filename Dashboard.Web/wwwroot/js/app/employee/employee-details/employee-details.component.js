(function () {
    "use strict";
    angular.module("employeeDetails")
        .component("employeeDetails", {
            templateUrl: "/js/app/employee/employee-details/employee-details.template.html",
            controller: function EmployeeDetailsController($scope, $http, $location, $routeParams, repoAssignments, repoEmployees)
            {
                this.employeeId = $routeParams.employeeId;

                var holder = this;
                holder.assignments = [];

                // Get Employee 
                repoAssignments.get(holder.employeeId).then(function (response) {
                    angular.copy(response, holder.assignments);
                    
                });

                $scope.Delete = function (phaseId) {
                    location.replace("#!/employees/employee-delete/" + phaseId);
                };

                $scope.Edit = function (phaseId) {
                    location.replace("#!/employees/employee-edit/" + phaseId);
                };
            }
            
        });
      
})();