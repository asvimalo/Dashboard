var repoJobTitles = function ($http) {

    var getJobTitles = function () {
        return $http.get('http://localhost:8890/api/dashboard/JobTitles')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };

    var getJobTitleById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/JobTitles", id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    //OBS!!! There's no things in backend - bugg
    var getJobTitleByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/knowledges/", name)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var addJobTitle = function (jobtitle) {
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
    var updateJobTitle = function (jobtitle) {
        $http.put('http://localhost:8890/api/dashboard/JobTitles', jobtitle)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't update jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deleteJobTitle = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/JobTitles', id)
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
        getAll: getJobTitles,
        get: getJobTitleById,
        getByName: getJobTitleByName,
        add: addJobTitle,
        update: updateJobTitle,
        delete: deleteJobTitle
    };
};

var module = angular.module('app-dashboard');
module.factory('repoJobTitles', repoJobTitles);