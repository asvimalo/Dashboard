(function () {
    "use strict";
    angular.module("allProjects", [])
        .component("allProjects", {
            templateUrl: "/js/app/allproject/all-projects.template.html",
            controller: function allProjectController($http, $scope, $location, $q, repoEmployees) {
                var holder = this;

                holder.allProjects = [];

                $q.all([
                    repoEmployees.getAll()
                ]).then(function (response) {
                    //success
                    console.log("Check1");
                    angular.copy(response[0], holder.allProjects);

                    init();

                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
                    .finally(function () {
                        holder.isBusy = false;
                    });

                function init() { }
            }
        });
});         