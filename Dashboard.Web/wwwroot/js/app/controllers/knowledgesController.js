//knowledgesController.js
(function () {
    "use strict";

    angular.module("app-dashboard")
        .controller("knowledgesController", knowledgesController);

    function knowledgesController($http) {

        var holder = this;

        holder.knowledges = [];


        holder.newKnowledge = {};

        console.log("inside knowledge controller");
        holder.errorMessage = "";
        holder.isBusy = true;

        ///////////////////////Knowledge/////////////////////////////////
        $http.get("http://localhost:8899/api/dashboard/knowledges")
            .then(function (response) {
                //success
                angular.copy(response.data, holder.knowledges);
            }, function (error) {
                //failure
                holder.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                holder.isBusy = false;
            });


        holder.addKnowledge = function () {
            holder.isBusy = true;
            holder.errorMessage = "";

            $http.post("http://localhost:8899/api/dashboard/knowledges", holder.newKnowledge)
                .then(function (response) {
                    //success
                    holder.knowledges.push(response.data);
                    holder.newPhase = {};

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save new knowledge";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };
        //holder.updateKnowledge = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.put("http://localhost:8899/api/dashboard/knowledges", //TODO object to Update)
        //        .then(function (response) {
        //            //success

        //            //// TODO ///////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to update knowledge";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};
        //holder.deleteKnowledge = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.delete("http://localhost:8899/api/dashboard/knowledges/", //TODO => ID)
        //        .then(function (response) {
        //            //success

        //            ////// TODO    //////////////

        //        }, function () {
        //            //failure
        //            holder.errorMessage = "Failure to delete knowledge";
        //        })
        //            .finally(function () {
        //                holder.isBusy = false;
        //            });

        //};


    }
})();