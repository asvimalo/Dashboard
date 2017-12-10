(function () {
    "use strict";
    angular.module("projectDelete")
        .component("projectDelete", {
            templateUrl: "/js/app/components/project/project-delete/project-delete.template.html",
            controller: function ProjectListController($http, $scope, $location, $routeParams, repoProjects)
            {
                this.projectId = $routeParams.projectId;

                var self = this;

                self.project = {}; 

                repoProjects.get(self.projectId).then(function (response) {
                    angular.copy(response, self.project); 
                }); 

                $scope.alert = false;
                $scope.deleteBtn = "Delete";
                $scope.cancelBtn = "Cancel";
                $scope.deleteProject = function () {
                    repoProjects.delete(self.projectId).then(function (response) {
                        $scope.alert = true;
                        $scope.successMessage = response;
                        $scope.cancelBtn = true;

                    }); 

                };

                $scope.closeModal = function () {
                    location.replace("#!/allprojects");
                    location.reload();
                };
            }
        });
      
})(); 

 