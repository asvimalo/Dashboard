//clientsController.js
(function () {
    "use strict";

    angular.module("app-dashboard")
        .controller("clientsController", clientsController);

    function clientsController($http) {

        var holder = this;

        holder.clients = [];


        holder.newClient = {};

        console.log("inside clients controller");
        holder.errorMessage = "";
        holder.isBusy = true;

        ///////////////////////Clients/////////////////////////////////
        $http.get("http://localhost:8899/api/dashboard/clients")
            .then(function (response) {
                //success
                angular.copy(response.data, holder.clients);
            }, function (error) {
                //failure
                holder.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                holder.isBusy = false;
            });


        holder.addClient = function () {
            holder.isBusy = true;
            holder.errorMessage = "";

            $http.post("http://localhost:8899/api/dashboard/clients", holder.newClient)
                .then(function (response) {
                    //success
                    holder.clients.push(response.data);
                    holder.newClient = {};

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save new client";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };
        //holder.updateClient = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.put("http://localhost:8899/api/dashboard/clients", //TODO object to Update)
        //        .then(function (response) {
        //            //success

        //            //// TODO ///////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to update client";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};
        //holder.deleteClient = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.delete("http://localhost:8899/api/dashboard/clients/", //TODO => ID)
        //        .then(function (response) {
        //            //success

        //            ////// TODO    //////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to delete client";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};


    }
})();