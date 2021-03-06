﻿
var repoAssignments = function ($http) {
    var getAssignments = function () {
        return $http.get('http://localhost:8890/api/dashboard/assignments')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get assignments: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    var getLists = function () {
        return $http.get("http://localhost:8890/api/dashboard/assignments/projectsemployeeslist")
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get assignments: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    var getAssignmentById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/assignments/" + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get assignment: " + error.message);
            })
            .finally(function () {

               // console.log("Finally...??");
            });
    };
    var getAssignmentByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/assignments/", name)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get assignment: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    var addAssignment = function (assignment) {
        return $http.post('http://localhost:8890/api/dashboard/assignments', assignment)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add assignment: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    var updateAssignment = function (id, assignment) {
        return $http.put('http://localhost:8890/api/dashboard/assignments/' + id, assignment).then(function (response){
                return response.data;
            }, function (error) {
                return error;
                console.log("didn't update assignment: " + error.data);
            })
            .finally(function () {

                console.log("Finally...??");
                //holder.isBusy = false;

            });
    };
    var deleteAssignment = function (id) {
        return $http.delete('http://localhost:8890/api/dashboard/assignments/' + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't delete employee: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    return {
        getAll: getAssignments,
        lists: getLists,
        get: getAssignmentById,
        getByName: getAssignmentByName,
        add: addAssignment,
        update: updateAssignment,
        delete: deleteAssignment
    };
};

var module = angular.module('app-dashboard');
module.factory('repoAssignments', repoAssignments);
