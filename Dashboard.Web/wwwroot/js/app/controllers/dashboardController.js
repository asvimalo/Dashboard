//dashboardController.js
(function () {
    "use strict";

    angular.module("app-dashboard")
        .controller("dashboardController", dashboardController);

    function dashboardController($http) {

        //var dash = this;

        //dash.projects = [];
        //dash.employees = [];

        //dash.newProject = {};
        //dash.newEmployee = {};
        //dash.log("inside dashboard controller");
        //dash.errorMessage = "";
        //dash.isBusy = true;
        ///////////////////////Employees//////////////////////////////////////
        //$http.get("http://localhost:8899/api/dashboard/employees")
        //    .then(function (response) {
        //        console.log("got employees");
        //        //success
        //        angular.copy(response.data, dash.employees);
        //    }, function (error) {
        //        //failure
        //        dash.errorMessage = "Failed to load data: " + error;
        //        console.log("didn't get employees: " + error);
        //    })
        //    .finally(function () {
        //        dash.isBusy = false;
        //    });


        //dash.addEmployee = function () {
        //    dash.isBusy = true;
        //    dash.errorMessage = "";

        //    $http.post("http://localhost:8899/api/dashboard/employees", dash.newEmployee)
        //        .then(function (response) {
        //            //success
        //            dash.employees.push(response.data);
        //            dash.newEmployee = {};

        //        }, function () {
        //            //failure
        //            dash.errorMessage = "Failure to save new trip";
        //        })
        //        .finally(function () {
        //            dash.isBusy = false;
        //        });

        //};

        ///////////////////////Projects/////////////////////////////////
        //$http.get("http://localhost:8899/api/dashboard/projects")
        //    .then(function (response) {
        //        //success
        //        angular.copy(response.data, dash.projects);
        //    }, function (error) {
        //        //failure
        //        dash.errorMessage = "Failed to load data: " + error;
        //    })
        //    .finally(function () {
        //        dash.isBusy = false;
        //    });


        //dash.addProject = function () {
        //    dash.isBusy = true;
        //    dash.errorMessage = "";

        //    $http.post("http://localhost:8899/api/dashboard/projects", dash.newEmployee)
        //        .then(function (response) {
        //            //success
        //            dash.projects.push(response.data);
        //            dash.newProject = {};

        //        }, function () {
        //            //failure
        //            dash.errorMessage = "Failure to save new trip";
        //        })
        //        .finally(function () {
        //            dash.isBusy = false;
        //        });

        //};


    }
})();