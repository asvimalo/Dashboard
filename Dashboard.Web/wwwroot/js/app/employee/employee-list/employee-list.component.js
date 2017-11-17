(function () {
    "use strict";
    angular.module("employeeList")
        .component("employeeList", {
            templateUrl: "/js/app/employee/employee-list/employee-list.template.html",
            controller: function EmployeeListController($http,repoEmployees) {
                var self = this;
                //self.orderProp = 'projectName';
                self.employees = [];

                repoEmployees.getAll().then(function (data) {
                    angular.copy(data, self.employees);
                });
            }
            
        });
      
})();