//dashboardController.js
(function () {
    "use strict";

    angular.module("app-dashboard")
        .controller("dashboardController", dashboardController);

    function dashboardController($http) {

        var holder = this;

        holder.projects = [];
        holder.employees = [];

        holder.newProject = {};
        holder.newEmployee = {};
        console.log("inside dashboard controller");
        holder.errorMessage = "";
        holder.isBusy = true;
        /////////////////////Employees//////////////////////////////////////
        $http.get("http://localhost:8899/api/dashboard/employees")
            .then(function (response) {
                console.log("got employees");
                //success
                angular.copy(response.data, holder.employees);
            }, function (error) {
                //failure
                holder.errorMessage = "Failed to load data: " + error;
                console.log("didn't get employees: " + error);
            })
            .finally(function () {
                holder.isBusy = false;
            });


        holder.addEmployee = function () {
            holder.isBusy = true;
            holder.errorMessage = "";

            $http.post("http://localhost:8899/api/dashboard/employees", holder.newEmployee)
                .then(function (response) {
                    //success
                    holder.employees.push(response.data);
                    holder.newEmployee = {};

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save new trip";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };

        /////////////////////Projects/////////////////////////////////
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

            $http.post("http://localhost:8899/api/dashboard/projects", holder.newEmployee)
                .then(function (response) {
                    //success
                    holder.projects.push(response.data);
                    holder.newProject = {};

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save new trip";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };


    }
})();