//acquiredKnowledgesController.js
(function () {
    "use strict";

    angular.module("app-dashboard")
        .controller("acquiredKnowledgesController", acquiredKnowledgesController);

    function acquiredKnowledgesController($http) {

        var holder = this;

        holder.acquiredKnowledges = [];


        holder.newAcquiredKnowledge = {};

        console.log("inside assignment controller");
        holder.errorMessage = "";
        holder.isBusy = true;

        ///////////////////////Assignments/////////////////////////////////
        $http.get("http://localhost:8899/api/dashboard/acquiredKnowledges")
            .then(function (response) {
                //success
                angular.copy(response.data, holder.acquiredKnowledges);
            }, function (error) {
                //failure
                holder.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                holder.isBusy = false;
            });


        holder.addAcKnowledge = function () {
            holder.isBusy = true;
            holder.errorMessage = "";

            $http.post("http://localhost:8899/api/dashboard/acquiredKnowledges", holder.newAcquiredKnowledge)
                .then(function (response) {
                    //success
                    holder.acquiredKnowledges.push(response.data);
                    holder.newProject = {};

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save assignment";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };
        //holder.updateAcKnowledge = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.put("http://localhost:8899/api/dashboard/acquiredKnowledges", //TODO object to Update)
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
        //holder.deleteAcKnowledge = function () {//TODO IMPORTANT
        //    holder.isBusy = true;
        //    holder.errorMessage = "";

        //    $http.delete("http://localhost:8899/api/dashboard/acquiredKnowledges/", //TODO => ID)
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