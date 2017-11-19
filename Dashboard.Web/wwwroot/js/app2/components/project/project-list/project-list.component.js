(function () {
    "use strict";
    angular.module("projectList")
        .component("projectList", {
            templateUrl: "/js/app2/components/project/project-list/project-list.template.html",
            controller: function ProjectListController(
                repoProjects,
                $scope,
                $location
                ) {
                //var self = this;
                //self.isBusy = true;
                ////self.orderProp = 'projectName';
                //self.projects = [];
                //repoProjects.getAll().then(function (data) {
                //    angular.copy(data, self.projects);
                //}, function () {
                //    console.log("failure");
                //    self.errorMessage = "Failure getting projects";

                //}).finally(function () {
                //    console.log("finally");
                //    self.isBusy = false;
                //    //window.location.reload();
                //});
            }
            
        });
      
})();