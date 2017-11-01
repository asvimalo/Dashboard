//phasesController.js
(function () {
    "use strict";

    angular.module("app-dashboard")
        .controller("phasesController", phasesController);

    function phasesController($http) {

        var holder = this;

        holder.phases = [];


        holder.newPhase = {};

        console.log("inside phases controller");
        holder.errorMessage = "";
        holder.isBusy = true;

        ///////////////////////Assignments/////////////////////////////////
        $http.get("http://localhost:8899/api/dashboard/phases")
            .then(function (response) {
                //success
                angular.copy(response.data, holder.phases);
            }, function (error) {
                //failure
                holder.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                holder.isBusy = false;
            });


        holder.addPhase = function () {
            holder.isBusy = true;
            holder.errorMessage = "";

            $http.post("http://localhost:8899/api/dashboard/phases", holder.newPhase)
                .then(function (response) {
                    //success
                    holder.phases.push(response.data);
                    holder.newPhase = {};

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save new phase";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };
        //holder.updatePhase = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.put("http://localhost:8899/api/dashboard/phases", //TODO object to Update)
        //        .then(function (response) {
        //            //success

        //            //// TODO ///////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to update phase";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};
        //holder.deletePhase = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.delete("http://localhost:8899/api/dashboard/phases/", //TODO => ID)
        //        .then(function (response) {
        //            //success

        //            ////// TODO    //////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to delete phase";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};


    }
})();