(function () {
    "use strict";

    angular.
        module('projectList').
        component('projectList', {
            templateUrl: '/js/app/projectList/project-list.template.html',
            controller: function projectsController($http) {
                var self = this;
                self.projects = [];
                //self.newProject = {};
                console.log("inside projects controller");
                self.errorMessage = "";
                self.isBusy = true;

                ///////////////////////Get Projects/////////////////////////////////
                $http.get("http://localhost:8899/api/dashboard/projects")
                    .then(function (response) {
                        //success
                        angular.copy(response.data, self.projects);
                    }, function (error) {
                        //failure
                        self.errorMessage = "Failed to load data: " + error;
                    })
                    .finally(function () {
                        self.isBusy = false;
                    });

        });

})();