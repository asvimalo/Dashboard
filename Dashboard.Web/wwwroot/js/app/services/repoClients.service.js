
var repoClients = function ($http) {

    var getClients = function () {
        return $http.get('http://localhost:8890/api/dashboard/clients')
            .then(function (response) {
                
                return response.data;

            }, function (error) {

                console.log("didn't get clients: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };

    var getClientById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/clients/", id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get client: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var getClientByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/clients/", name)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employees: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var addClient = function (client) {
        return $http.post('http://localhost:8890/api/dashboard/clients', client)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employees: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var updateClient = function (client) {
        $http.put('http://localhost:8890/api/dashboard/employees', client)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employees: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deleteClient = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/employees', id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employees: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    return {
        getAll: getClients,
        get: getClientById,
        getByName: getClientByName,
        add: addClient,
        update: updateClient,
        delete: deleteClient
    };
};

var module = angular.module('app-dashboard');
module.factory('repoClients', repoClients);
