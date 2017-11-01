//assignmentsController.js
(function () {
    "use strict";

    angular.module("app-dashboard")
        .controller("locationsController", locationsController);

    function locationsController($http) {

        var holder = this;

        holder.locations = [];


        holder.newLocation = {};

        console.log("inside location controller");
        holder.errorMessage = "";
        holder.isBusy = true;

        ///////////////////////Assignments/////////////////////////////////
        $http.get("http://localhost:8899/api/dashboard/locations")
            .then(function (response) {
                //success
                angular.copy(response.data, holder.locations);
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

            $http.post("http://localhost:8899/api/dashboard/locations", holder.newLocation)
                .then(function (response) {
                    //success
                    holder.locations.push(response.data);
                    holder.newProject = {};

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save assignment";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };
        //holder.updateLocation = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.put("http://localhost:8899/api/dashboard/locations", //TODO object to Update)
        //        .then(function (response) {
        //            //success

        //            //// TODO ///////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to update location";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};
        //holder.deleteLocation = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.delete("http://localhost:8899/api/dashboard/locations/", //TODO => ID)
        //        .then(function (response) {
        //            //success

        //            ////// TODO    //////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to delete location";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};


    }
})();