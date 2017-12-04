(function () {
    "use strict";
    angular.module("projectDetails")
        .component("projectDetails", {
            templateUrl: "/js/app/project/project-details/project-details.template.html",
            controller: function ProjectListController( $http, $scope, $location, $routeParams, repoProjects ) 
            {
                this.projectId = $routeParams.projectId;
                 
                var self = this;

                self.project = {};


                repoProjects.get(self.projectId).then(function (response) {
                    angular.copy(response, self.project);
                    self.project.startDate = new Date(self.project.startDate).toLocaleDateString();
                    self.project.stopDate = new Date(self.project.stopDate).toLocaleDateString();
                });  

                $scope.showGantt = function () {
                    $(".ganttpanel").show();
                    $(".projectDetails").hide();
                };

                $scope.phaseId = {};

                $scope.Delete = function (phaseId) {
                    location.replace("#!/phases/phase-delete/" + phaseId);
                };

                $scope.Edit = function (phaseId) {
                    location.replace("#!/phases/phase-edit/" + phaseId);
                };
            }
        });
      
})(); 

 