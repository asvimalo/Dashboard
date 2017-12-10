(function () {
    "use strict";
    angular.module("projectDetails")
        .component("projectDetails", {
            templateUrl: "/js/app/components/project/project-details/project-details.template.html",
            controller: function ProjectListController( $http, $scope, $location, $routeParams, repoProjects, repoAssignments ) 
            {
                this.projectId = $routeParams.projectId;
                 
                var self = this;

                self.project = {};
                 
                repoProjects.get(self.projectId).then(function (response) {
                    angular.copy(response, self.project);
                    self.project.startDate = new Date(self.project.startDate).toLocaleDateString();
                    self.project.stopDate = new Date(self.project.stopDate).toLocaleDateString();
                });  
                  
                $scope.phaseId = {};

                $scope.Delete = function (phaseId) {
                    location.replace("#!/phases/phase-delete/" + phaseId);
                };

                $scope.Edit = function (phaseId) {
                    location.replace("#!/phases/phase-edit/" + phaseId);
                };
                 
                $scope.deleteDeveloperFromProject = function (assignmentId) {

                    repoAssignments.delete(assignmentId).then(function (response) {
                        console.log("Response from server api" + response.data);

                        location.reload();
                    }, function () {
                        console.log("failure");
                        self.errorMessage = "Failure to save new project";
                    })
                    .finally(function () {
                            console.log("finally");
                            self.isBusy = false;

                     });


                }

                $scope.editDeveloperAssignment = function (assignmentId) {

                    location.replace("#!/assignments/assignment-edit/" + assignmentId);

                }
            }
        });
      
})(); 

 