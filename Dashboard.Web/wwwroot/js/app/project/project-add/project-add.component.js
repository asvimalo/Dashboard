(function () {
    "use strict";

    angular.module("projectAdd")
        .component("projectAdd", {
            templateUrl: "/js/app/project/project-add/project-add.template.html",
            controller: function ProjectAddController($http, $scope, repoProjects) {
                var self = this;
                 
                self.employeesAndClients = [];  
                $scope.alert = true;

                repoProjects.getEmpClientList().then(function (response) {
                    angular.copy(response, self.employeesAndClients);
                });

                $scope.validateEndDate = function (startDate, endDate) {
                    if (new Date(endDate) < new Date(startDate)) {
                        $scope.errorMessage = "To:date should be greater than start date.";
                        $scope.alert = false;
                        return false;
                    }
                    else {
                        $scope.alert = true;
                        return true;
                    }
                };

                $scope.addProject = function (start, end) {
                    console.log("in the addProject function");
                    self.isBusy = true;
                    self.errorMessage = "";

                    var data = { "projectName": self.project.projectName, "startDate": new Date(self.project.startDate).toLocaleDateString(), "stopDate": new Date(self.project.stopDate).toLocaleDateString(), "timeBudget": self.project.timeBudget, "ClientId": $scope.formInfo.clients.clientId, "Employees": $scope.formInfo.employees, "notes": self.project.notes };
                    var dataTmp = JSON.stringify(data);

                    repoProjects.add(dataTmp)
                        .then(function (response) {
                            console.log("Response from server api" + response.data);

                        }, function () {
                            console.log("failure");
                            self.errorMessage = "Failure to save new project";
                        })
                        .finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                            $scope.addProjectForm = {};
                        });
                                        
                };
                 
                
            }
        });
      
})(); 

 