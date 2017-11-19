
var repoKnowledges = function ($http) {

    var getKnowledges = function () {
        return $http.get('http://localhost:8890/api/dashboard/knowledges')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get employees: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };

    var getKnowledgeById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/knowledges/", id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employees: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var getKnowledgeByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/knowledges/", name)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var addKnowledge = function (knowledge) {
        $http.post('http://localhost:8890/api/dashboard/knowledges', knowledge)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var updateKnowledge = function (knowledge) {
        $http.put('http://localhost:8890/api/dashboard/knowledges', knowledge)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't update employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deleteKnowledge = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/knowledges', id)
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
        getAll: getKnowledges,
        get: getKnowledgeById,
        getByName: getKnowledgeByName,
        add: addKnowledge,
        update: updateKnowledge,
        delete: deleteKnowledge
    };
};

var module = angular.module('app-dashboard2');
module.factory('repoKnowledges', repoKnowledges);
