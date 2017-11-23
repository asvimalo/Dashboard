(function () {
    "use strict";

    angular.module("projectEdit")
        .component("projectEdit", {
            templateUrl: "/js/app/project/project-edit/project-edit.template.html",
            controller: function ProjectAddController($http, $scope, $routeParams, repoProjects) {

                this.projectId = $routeParams.projectId; 
                var self = this; 
                self.employeesAndClients = []; 
                self.project = {};

                $http.get('http://localhost:8890/api/dashboard/projects/employeesclientslist').then(function (response) {
                    angular.copy(response.data, self.employeesAndClients);
                });

                repoProjects.get(self.projectId).then(function (response) {
                    angular.copy(response, self.project);               
                });
                  
                $scope.editProject = function () {

                    var data = { "projectName": self.project.projectName, "startDate": new Date(self.project.startDate).toLocaleDateString(), "stopDate": new Date(self.project.stopDate).toLocaleDateString(), "timeBudget": self.project.timeBudget, "notes": self.project.notes, "ClientId": $scope.addProjectForm.clients.clientId };
                    var dataTmp = JSON.stringify(data);

                    repoProjects.update(self.projectId, dataTmp)
                        .then(function (response) {
                            console.log("Response from server api" + response.data);

                            location.replace("#!/projects/project-details/" + self.projectId);
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

 