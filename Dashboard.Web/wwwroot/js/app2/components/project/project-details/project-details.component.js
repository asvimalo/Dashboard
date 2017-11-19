(function () {
    "use strict";
    angular.module("projectDetails")
        .component("projectDetails", {
            templateUrl: "/js/app2/components/project/project-details/project-details.template.html",
            controller: function ProjectListController(
                $scope,
                $location,
                $routeParams,
                repoProjects
                ) {
                this.projectId = $routeParams.projectId;

                var self = this;
                self.isBusy = true;
                self.project = {};

                repoProjects.get(self.projectId).then(function (data) {
                    self.project = data;
                }, function () {
                    console.log("failure");
                    self.errorMessage = "Failure getting project";

                }).finally(function () {
                    console.log("finally");
                    self.isBusy = false;
                    window.location.reload();
                });

                
                
            }
        });
      
})(); 

 