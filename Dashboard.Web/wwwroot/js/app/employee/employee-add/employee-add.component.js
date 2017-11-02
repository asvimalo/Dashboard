(function () {
    "use strict";
    angular.module("employeeAdd")
        .component("employeeAdd", {
            templateUrl: "/js/app/employee/employee-add/employee-add.template.html",
            controller: function EmployeeListController($http) {
                var holder = this;

                holder.employees = [];

                holder.newEmployee = {};
                console.log("inside employees Controller ");
                holder.errorMessage = "";
                holder.isBusy = true;

                holder.addEmployee = function (employee) {
                    holder.isBusy = true;
                    holder.errorMessage = "";

                    $http.post("http://localhost:8899/api/dashboard/employees", employee)
                        .then(function (response) {
                            //success
                            //holder.employees.push(response.data);
                            holder.newEmployee = {};
                            $location.path('#/employees');
                        }, function () {
                            //failure
                            holder.errorMessage = "Failure to save new employee";
                        })
                        .finally(function () {
                            holder.isBusy = false;
                        });

                };
            }
            
        });
      
})();