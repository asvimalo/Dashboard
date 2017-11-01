//projectsController.js
(function () {
    "use strict";

    angular.module("app-dashboard")
        .controller("projectsController", projectsController);

    function projectsController($http) {

        var holder = this;

        holder.projects = [];


        holder.newProject = {};

        console.log("inside projects controller");
        holder.errorMessage = "";
        holder.isBusy = true;

        ///////////////////////Projects/////////////////////////////////
        $http.get("http://localhost:8899/api/dashboard/projects")
            .then(function (response) {
                //success
                angular.copy(response.data, holder.projects);
            }, function (error) {
                //failure
                holder.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                holder.isBusy = false;
            });


        holder.addProject = function () {
            holder.isBusy = true;
            holder.errorMessage = "";

            $http.post("http://localhost:8899/api/dashboard/projects", holder.newProject)
                .then(function (response) {
                    //success
                    holder.projects.push(response.data);
                    holder.newProject = {};

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save new project";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };
        //holder.UpdateProject = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.put("http://localhost:8899/api/dashboard/projects", //TODO object to Update)
        //        .then(function (response) {
        //            //success

        //            //// TODO ///////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to update project";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};
        //holder.DeleteProject = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.delete("http://localhost:8899/api/dashboard/projects/", //TODO => ID)
        //        .then(function (response) {
        //            //success

        //            ////// TODO    //////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to delete project";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};


    }
})();