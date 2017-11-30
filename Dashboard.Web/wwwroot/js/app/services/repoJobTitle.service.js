var repoJobTitle = function ($http) {


    //TODO: change url när det blir klart i backend
    var getJobTitles = function () {
        return $http.get('http://localhost:8890/api/dashboard/knowledges')
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
        return $http.get("http://localhost:8890/api/dashboard/knowledges/", id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get jobtitle: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
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
        $http.post('http://localhost:8890/api/dashboard/knowledges', knowledge)
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
        $http.put('http://localhost:8890/api/dashboard/knowledges', jobtitle)
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
        $http.delete('http://localhost:8890/api/dashboard/knowledges', id)
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
        getAll: getJobTitle,
        get: getJobTitleById,
        getByName: getJobTitleByName,
        add: addJobTitle,
        update: updateJobTitle,
        delete: deleteJobTitle
    };
};

var module = angular.module('app-dashboard');
module.factory('repoKnowledges', repoKnowledges);