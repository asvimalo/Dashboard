(function () {
    "use strict";

    angular.
        module('employeeList').
        component('employeeList', {
            templateUrl: '../employeeList/employee-list.template.html',
            controller: "employeesController"
        });

}());