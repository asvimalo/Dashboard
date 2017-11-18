
var repoEmployees = function ($http) {

    var getEmployees = function () {
        return $http.get('http://localhost:8890/api/dashboard/employees')
            .then(function (response) {
                
                return response.data;   
                
            }, function (error) {
                
                console.log("didn't get employees: " + error.message);
            })
            .finally(function () {
                
                console.log("Finally...??");
            });
    };

    var getEmployeeById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/employees/", id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employees: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var getEmployeeByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/employees/", id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var addEmployee = function (employee) {
        $http.post('http://localhost:8890/api/dashboard/employees', employee)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var updateEmployee = function (employee) {
        $http.put('http://localhost:8890/api/dashboard/employees', employee)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't update employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deleteEmployee = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/employees', employee)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't delete employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
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
module.factory('repoEmployees', repoEmployees);
        