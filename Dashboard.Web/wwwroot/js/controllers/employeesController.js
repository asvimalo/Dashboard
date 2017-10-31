//employeesController.js
(function () {
    "use strict";
    console.log("inside func dashboard controller");

    angular.module("app-dashboard")
        .controller("employeesController", employeesController);

    console.log("outside employeesController ");

    function employeesController($http) {

        var holder = this;

        
        holder.employees = [];

        
        holder.newEmployee = {};
        console.log("inside employees Controller ");
        holder.errorMessage = "";
        holder.isBusy = true;
        ///////////////////////Employees//////////////////////////////////////
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
                    holder.errorMessage = "Failure to save new employee";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };
        holder.updateEmployee = function () {//TODO IMPORTANT
            holder.isBusy = true;
            holder.errorMessage = "";

            $http.put("http://localhost:8899/api/dashboard/employees", //TODO object to Update)
                .then(function (response) {
                    //success
                  
                    //// TODO ///////////////

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to update employee";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };
        holder.deleteEmployee = function () {//TODO IMPORTANT
            holder.isBusy = true;
            holder.errorMessage = "";

            $http.delete("http://localhost:8899/api/dashboard/employees/", //TODO => ID)
                .then(function (response) {
                    //success

                    ////// TODO    //////////////

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to delete new employee";
                })
                .finally(function () {
                    holder.isBusy = false;
                });

        };

        
    }
})();