
var repoPhases = function ($http) {

    var getPhases = function () {
        return $http.get('http://localhost:8890/api/dashboard/phases')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get phases: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
   
    var getPhaseById = function (id) {
        return $http.get('http://localhost:8890/api/dashboard/phases/', id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get phase: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var getPhaseByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/phases/", name)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get phase: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var addPhase = function (phase) {
        $http.post('http://localhost:8890/api/dashboard/phases', phase)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add phase: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var updatePhase = function (phase) {
        $http.put('http://localhost:8890/api/dashboard/phases', phase)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't update phase: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deletePhase = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/phases', id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't delete phase: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    return {
        getAll: getPhases,
        get: getPhaseById,
        getByName: getPhaseByName,
        add: addPhase,
        update: updatePhase,
        delete: deletePhase
    };
};

var module = angular.module('app-dashboard');
module.factory('repoPhases', repoPhases);
