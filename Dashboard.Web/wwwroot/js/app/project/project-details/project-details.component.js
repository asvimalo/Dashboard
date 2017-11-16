(function () {
    "use strict";
    angular.module("projectDetails")
        .component("projectDetails", {
            templateUrl: "/js/app/project/project-details/project-details.template.html",
            controller: function ProjectListController($http, $scope, $location, $routeParams) {
                this.projectId = $routeParams.projectId;

                var self = this;

                self.project = {};

                $http.get('http://localhost:8890/api/dashboard/projects/' + self.projectId).then(function (response) {
                    angular.copy(response.data, self.project);
                }); 

                $scope.phaseId = {};

                $scope.Send = function (phaseId) {
                    location.replace("#!/phases/phase-delete/" + phaseId)
                    $scope.$apply();

                }
                
            }
        });
      
})(); 

 