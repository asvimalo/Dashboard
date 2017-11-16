
var repoEmloyees = function ($http) {

    var getEmployees = function () {
        return $http.get("http://localhost:8890/api/dashboard/employees")
            .then(function (response) {
                return response.data;
            });
    };

    var getEmployeeById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/employees/", id)
            .then(function (response) {
                return response.data;
            });
    };
    var getEmployeeByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/employees/", id)
            .then(function (response) {
                return response.data;
            });
    };
    var addEmployee = function (employee) {
        $http.post('http://localhost:8890/api/dashboard/employees', employee)
            .then(function (response) {
                return response.data;
            });
    };
    var updateEmployee = function (employee) {
        $http.put('http://localhost:8890/api/dashboard/employees', employee)
            .then(function (response) {
                return response.data;
            });
    };
    var deleteEmployee = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/employees', employee)
            .then(function (response) {
                return response.data;
            });
    };
    return {
        getAll: getEmployees,
        get: getEmployeeById,
        getByName: getEmployeeByName,
        add: addEmployee,
        update: updateEmployee,
        delete: deleteEmployee
    };
};

var module = angular.module('app-dashboard');
module.factory('repoEmloyees', repoEmloyees);
        