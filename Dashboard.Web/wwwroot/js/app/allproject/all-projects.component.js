(function () {
    "use strict";
    angular.module("allProjects", [])
        .component("allProjects", {
            templateUrl: "/js/app/allproject/all-projects.template.html",
            controller: function allProjectController(
                $http,
                $scope,
                $location,
                repoAssignments,
                repoPhases,
                repoProjects

            ) {  //repoEmployees,
                var holder = this;

                console.log("Inside.");

                holder.employeesAndProjects = [];
                repoAssignments.lists().then(function (response) {
                    //success
                    console.log("Check");
                    angular.copy(response, holder.employeesAndProjects);
                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
                    .finally(function () {
                        holder.isBusy = false;
                    });

                holder.allAssigments = [];
                repoAssignments.getAll().then(function (response) {
                    //success
                    console.log("Check");
                    angular.copy(response, holder.allAssigments);
                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
                    .finally(function () {
                        holder.isBusy = false;
                    });


                holder.allProjects = [];
                repoProjects.getAll().then(function (response) {
                    //success
                    console.log("Check");
                    angular.copy(response, holder.allProjects);
                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
                    .finally(function () {
                        holder.isBusy = false;
                    });

                holder.phases = [];
                repoPhases.getAll().then(function (response) {
                    //success
                    console.log("Check");
                    angular.copy(response, holder.phases);
                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
                    .finally(function () {
                        holder.isBusy = false;
                    });

                //TODO: backend
                //holder.allCommitments = [];
                //repoCommitments.getAll().then(function (response) {
                //    //success
                //    console.log("Check");
                //    angular.copy(response, holder.allCommitments);
                //}, function (error) {
                //    //failure
                //    holder.errorMessage = "Failed to load data: " + error;
                //})
                //    .finally(function () {
                //        holder.isBusy = false;
                //    });
            }
        });
})();