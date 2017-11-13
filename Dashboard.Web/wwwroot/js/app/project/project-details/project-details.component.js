﻿(function () {
    "use strict";

    angular.module("projectDetails")
        .component("projectDetails", {
            templateUrl: "/js/app/project/project-details/project-details.template.html",
            controller: function ProjectListController($http, $scope) {
                var self = this;
                 
                self.projects = [];
                $http.get('http://localhost:8890/api/dashboard/projects').then(function (response) {
                    angular.copy(response.data, self.projects);
                });
                 
                $scope.addProject = function () {
                    console.log("in the addProject function");
                    self.isBusy = true;
                    self.errorMessage = "";

                    var data = { "projectName": self.project.projectName, "startDate": new Date(self.project.startDate).toLocaleDateString(), "stopDate": new Date(self.project.stopDate).toLocaleDateString(), "timeBudget": self.project.timeBudget, "ClientId": $scope.formInfo.clients.clientId, "Employees": $scope.formInfo.employees, "notes": self.project.notes };
                    var dataTmp = JSON.stringify(data);

                    $http.post("http://localhost:8890/api/dashboard/projects", dataTmp)
                        .then(function (response) {
                            console.log("Response from server api" + response.data);

                        }, function () {
                            console.log("failure");
                            self.errorMessage = "Failure to save new project";
                        })
                        .finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                            window.location.reload();

                        });
                                        
                };
                 
                
            }
        });
      
})(); 

 