﻿
var repoPhases = function ($http) {

    var getPhases = function () {
        return $http.get('http://localhost:8890/api/dashboard/phases')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get assignments: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
   
    var getPhaseById = function (id) {
        return $http.get('http://localhost:8890/api/dashboard/phases/' + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get assignment: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var getPhaseByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/phases/" + name)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get assignment: " + error.message);
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

                console.log("didn't add assignment: " + error.message);
            })
            .finally(function () {
                window.location.reload();
                console.log("Success");

            });
    };
    var updatePhase = function (id, phase) {
        return $http.put('http://localhost:8890/api/dashboard/phases/' + id, phase).then(function (response) {
            return response.data;
            }, function (error) {

                console.log("didn't update employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deletePhase = function (id) {
        return $http.delete('http://localhost:8890/api/dashboard/phases/' + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't delete employee: " + error.message);
            })
            .finally(function () {
                
                console.log("Success delete");
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
