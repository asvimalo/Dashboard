(function () {
    "use strict";
    angular.module("employeeEdit")
        .component("employeeEdit", {
            templateUrl: "/js/app/components/employee/employee-edit/employee-edit.template.html",
            controller: function EmployeeListController($scope, $http, $location, $routeParams, repoEmployees, repoKnowledges)
            {
                
                var holder = this;
                holder.employee = {};

                this.employeeId = $routeParams.employeeId;

                repoEmployees.get(holder.employeeId).then(function (response) {
                    angular.copy(response, holder.employee);

                });

                $scope.editEmployee = function () {
                    var data = {
                        "firstName": holder.employee.firstName,
                        "lastName": holder.employee.lastName
                    };
                    var dataTmp = JSON.stringify(data);

                    repoEmployees.update(holder.employee.employeeId, dataTmp)
                        .then(function (response) {
                            console.log("Response from server api" + response.data);
                            location.replace("#!/employees/employee-details/" + holder.employeeId);
                            location.reload();
                        }, function () {
                            console.log("failure");
                            self.errorMessage = "Failure to save new project";
                        })
                        .finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                        });
                    
                };
                 
                 
            }
            
        });
      
})();