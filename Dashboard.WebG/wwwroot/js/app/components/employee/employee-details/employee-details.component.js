(function () {
    "use strict";
    angular.module("employeeDetails")
        .component("employeeDetails", {
            templateUrl: "/js/app/components/employee/employee-details/employee-details.template.html",
            controller: function EmployeeDetailsController($scope, $http, $location, $rootScope, $routeParams, repoAssignments)
            {
                this.employeeId = $routeParams.employeeId;

                var holder = this;
                holder.assignments = [];

                // Get Employee 
                repoAssignments.get(holder.employeeId).then(function (response) {
                    angular.copy(response, holder.assignments);
                    
                });

                $scope.deleteAssignment = function (assignmentId) {
                    $location.path("assignments/assignment-delete/" + assignmentId);
                    $location.replace();
                };

                $scope.editAssignment = function (assignmentId) {
                    $location.path("assignments/assignment-edit/" + assignmentId);
                    $location.replace();
                };

                  
            }
            
        });
      
})();