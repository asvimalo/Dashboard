(function () {
    "use strict";
    angular.module("projectDelete")
        .component("projectDelete", {
            templateUrl: "/js/app/project/project-delete/project-delete.template.html",
            controller: function ProjectListController($http, $scope, $location, $routeParams, repoProjects)
            {
                this.projectId = $routeParams.projectId;

                var self = this;

                self.project = {};

                repoProjects.get(self.projectId).then(function (response) {
                    angular.copy(response, self.project);
                }); 

                $scope.deleteProject = function () {
                    repoProjects.delete(self.projectId);
                    location.replace("#!/dashboard");
                    location.reload();

                };
                 
            }
        });
      
})(); 

 