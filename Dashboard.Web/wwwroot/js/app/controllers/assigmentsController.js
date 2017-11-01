//assignmentsController.js
(function () {
    "use strict";

    angular.module("app-dashboard")
        .controller("assignmentsController", assignmentsController);

    function assignmentsController($http) {

        var holder = this;

        holder.assignments = [];

        holder.title = "Assignments";
        holder.newAssignment = {};

        console.log("inside assignment controller");
        holder.errorMessage = "";
        holder.isBusy = true;

        ///////////////////////Assignments/////////////////////////////////
        $http.get("http://localhost:8899/api/dashboard/assignments")
            .then(function (response) {
                //success
                angular.copy(response.data, holder.assignments);
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

            $http.post("http://localhost:8899/api/dashboard/assignments", holder.newAssignment)
                .then(function (response) {
                    //success
                    holder.assignments.push(response.data);
                    holder.newProject = {};

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save assignment";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };
        //holder.updateProject = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.put("http://localhost:8899/api/dashboard/assignments", //TODO object to Update)
        //        .then(function (response) {
        //            //success

        //            //// TODO ///////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to update assignment";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};
        //holder.deleteProject = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.delete("http://localhost:8899/api/dashboard/assignments/", //TODO => ID)
        //        .then(function (response) {
        //            //success

        //            ////// TODO    //////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to delete assignment";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};


    }
})();