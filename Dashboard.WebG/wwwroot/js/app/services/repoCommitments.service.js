
var repoCommitments = function ($http) {

    var getCommitments = function () {
        return $http.get('http://localhost:8890/api/dashboard/commitments')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get commitments: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };

    var getCommitmentById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/commitments/" + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get commitment: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    
    var addCommitment = function (commitment) {
        $http.post('http://localhost:8890/api/dashboard/commitments/', commitment)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add commitment: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var updateCommitment = function (commitment) {
        $http.put('http://localhost:8890/api/dashboard/commitments/', commitment)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't update commitment: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deleteCommitment = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/commitments/' + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't delete commitment: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    return {
        getAll: getCommitments,
        get: getCommitmentById,       
        add: addCommitment,
        update: updateCommitment,
        delete: deleteCommitment
    };
};

var module = angular.module('app-dashboard');
module.factory('repoCommitments', repoCommitments);
