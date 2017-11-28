
var repoAcquiredKnowledges = function ($http) {

    var getAcquiredKnowledges = function () {
        return $http.get('http://localhost:8890/api/dashboard/acquiredKnowledges')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get acquired knowledges: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };

    var getAcquiredKnowledgeById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/acquiredKnowledges/" + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get acquired knowledge: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    
    var addAcquiredKnowledge = function (acquiredKnowledge) {
        $http.post('http://localhost:8890/api/dashboard/acquiredKnowledges', acquiredKnowledge)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add acquired knowledge: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var updateAcquiredKnowledge = function (acquiredKnowledge) {
        $http.put('http://localhost:8890/api/dashboard/acquiredKnowledges', acquiredKnowledge)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't update acquired knowledge: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deleteAcquiredKnowledge = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/acquiredKnowledges' + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't delete acquired knowledge: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    return {
        getAll: getAcquiredKnowledges,
        get: getAcquiredKnowledgeById,        
        add: addAcquiredKnowledge,
        update: updateAcquiredKnowledge,
        delete: deleteAcquiredKnowledge
    };
};

var module = angular.module('app-dashboard');
module.factory('repoAcquiredKnowledges', repoAcquiredKnowledges);
