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

                repoProjects.getEmpClientList().then(function (response) {
                    angular.copy(response, self.employeesAndClients);
                });

                repoProjects.get(self.projectId).then(function (response) {
                    angular.copy(response, self.project);
                    self.project.startDate = new Date(self.project.startDate).toLocaleDateString();
                    self.project.stopDate = new Date(self.project.stopDate).toLocaleDateString();

                });
                  
                $scope.editProject = function () {

                    var projectData = {
                        "projectName": self.project.projectName,
                        "startDate": new Date(self.project.startDate).toLocaleDateString(),
                        "stopDate": new Date(self.project.stopDate).toLocaleDateString(),
                        "timeBudget": self.project.timeBudget,
                        "notes": self.project.notes
                    };
                    var dataTmp = JSON.stringify(projectData);

                    repoProjects.update(self.projectId, dataTmp).then(function (response) {
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

 