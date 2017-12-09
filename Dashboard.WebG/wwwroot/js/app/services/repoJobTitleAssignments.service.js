var repoJobTitleAssignments = function ($http) {

    var getJobTitleAssignments = function () {
        return $http.get('http://localhost:8890/api/dashboard/JobTitleAssignments')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };

    var getJobTitleAssignmentsById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/JobTitleAssignments", id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };

    var getJobTitleAssignmentsByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/JobTitleAssignments/", name)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var addJobTitleAssignments = function (jobtitle) {
        $http.post('http://localhost:8890/api/dashboard/JobTitleAssignments', jobtitle)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var updateJobTitleAssignments = function (jobtitle) {
        $http.put('http://localhost:8890/api/dashboard/JobTitleAssignments', jobtitle)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't update jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deleteJobTitleAssignments = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/JobTitleAssignments', id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't delete jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    return {
        getAll: getJobTitleAssignments,
        get: getJobTitleAssignmentsById,
        getByName: getJobTitleAssignmentsByName,
        add: addJobTitleAssignments,
        update: updateJobTitleAssignments,
        delete: deleteJobTitleAssignments
    };
};

var module = angular.module('app-dashboard');
module.factory('repoJobTitleAssignments', repoJobTitleAssignments);