(function () {
    "use strict";
    angular.module("employeeList")
        .component("employeeList", {
            templateUrl: "/Dashboard/app/components/employee/employee-list/employee-list.template.html",
            controller: function EmployeeListController($http,repoEmployees) {
                var self = this;
                //self.orderProp = 'projectName';
                self.employees = [];
                holder.isBusy = true;
                repoEmployees.getAll().then(function (data) {
                    angular.copy(data, self.employees);
                    }, function () {
                        //failure
                        holder.errorMessage = "Failure to save get employees";

                    }).finally(function () {
                        holder.isBusy = false;
                    });
            }
            
        });
      
})();