(function () {
    "use strict";
    angular.module("employeeList")
        .component("employeeList", {
            templateUrl: "/js/app2/components/employee/employee-list/employee-list.template.html",
            controller: function EmployeeListController($http,repoEmployees) {
                var self = this;
                //self.orderProp = 'projectName';
                self.employees = [];
                self.isBusy = true;
                repoEmployees.getAll().then(function (data) {
                    angular.copy(data, self.employees);
                    }, function () {
                        //failure
                        self.errorMessage = "Failure to save get employees";

                    }).finally(function () {
                        self.isBusy = false;
                    });
            }
            
        });
      
})();