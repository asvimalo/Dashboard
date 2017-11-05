(function () {
    "use strict";

    angular.module("projectAdd")
        .component("projectAdd", {
            templateUrl: "/js/app/project/project-add/project-add.template.html",
            controller: function ProjectAddController($http, $scope) {
                var self = this;
                 
                // Putting all my Clients in this array.
                self.clients = []; 
                // Putting all my Employees in this array.
                self.employees = [];

                 
                // Getting my Clients
                $http.get('http://localhost:8890/api/dashboard/clients').then(function (response) {
                    angular.copy(response.data, self.clients);
                });

                // Getting my Employee
                $http.get('http://localhost:8890/api/dashboard/employees').then(function (response) {
                    angular.copy(response.data, self.employees);
                });

                // Add Project Function
                $scope.addProject = function () {
                    console.log("in the addProject function");
                    self.isBusy = true;
                    self.errorMessage = "";

                    var data = { "projectName": self.project.projectName, "startDate": new Date(self.project.startDate).toLocaleDateString(), "stopDate": new Date(self.project.stopDate).toLocaleDateString(), "timeBudget": self.project.timeBudget, "ClientId": $scope.formInfo.clients.clientId, "EmployeeId": $scope.formInfo.employees.employeeId, "notes": self.project.notes };
                    var dataTmp = JSON.stringify(data);

                    $http.post("http://localhost:8890/api/dashboard/projects", dataTmp)
                        .then(function (response) {
                            console.log("Response from server api" + response.data);

                        }, function () {
                            //failure
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

 