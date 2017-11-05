﻿(function () {
    "use strict";
    angular.module("employeeList")
        .component("employeeList", {
            templateUrl: "/js/app/employee/employee-list/employee-list.template.html",
            controller: function EmployeeListController($http) {
                var self = this;
                //self.orderProp = 'projectName';
                self.employees = [];
                $http.get('http://localhost:8890/api/dashboard/employees').then(function (response) {
                    angular.copy(response.data, self.employees);
                });
            }
            
        });
      
})();