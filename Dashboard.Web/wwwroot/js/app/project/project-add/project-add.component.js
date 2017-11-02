(function () {
    "use strict";
    angular.module("projectAdd")
        .component("projectAdd", {
            templateUrl: "/js/app/project/project-add/project-add.template.html",
            controller: function ProjectAddController($http,$routeProvider) {
                var self = this;

                //var self.project = {};

                self.addProject = function () {
                    self.isBusy = true;
                    self.errorMessage = "";

                    $http.post("http://localhost:8899/api/dashboard/projects", $routeProvider.project)
                        .then(function (response) {
                            //success
                            self.projects.push(response.data);
                            self.newProject = {};

                        }, function () {
                            //failure
                            self.errorMessage = "Failure to save new project";
                        })
                        .finally(function () {
                            self.isBusy = false;
                        });

                };
            }
            
        });
      
})();