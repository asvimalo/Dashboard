//tasksController.js
(function () {
    "use strict";

    angular.module("app-dashboard")
        .controller("tasksController", phasesController);

    function tasksController($http) {

        var holder = this;

        holder.tasks = [];


        holder.newTask = {};

        console.log("inside tasks controller");
        holder.errorMessage = "";
        holder.isBusy = true;

        ///////////////////////Assignments/////////////////////////////////
        $http.get("http://localhost:8899/api/dashboard/tasks")
            .then(function (response) {
                //success
                angular.copy(response.data, holder.tasks);
            }, function (error) {
                //failure
                holder.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                holder.isBusy = false;
            });


        holder.addTask = function () {
            holder.isBusy = true;
            holder.errorMessage = "";

            $http.post("http://localhost:8899/api/dashboard/tasks", holder.newTask)
                .then(function (response) {
                    //success
                    holder.tasks.push(response.data);
                    holder.newPhase = {};

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save new task";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };
        //holder.updateTask = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.put("http://localhost:8899/api/dashboard/tasks", //TODO object to Update)
        //        .then(function (response) {
        //            //success

        //            //// TODO ///////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to update task";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};
        //holder.deleteTask = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.delete("http://localhost:8899/api/dashboard/tasks/", //TODO => ID)
        //        .then(function (response) {
        //            //success

        //            ////// TODO    //////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to delete task";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};


    }
})();