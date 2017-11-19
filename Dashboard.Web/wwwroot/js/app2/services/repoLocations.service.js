
var repoLocations = function ($http) {

    var getLocations = function () {
        return $http.get('http://localhost:8890/api/dashboard/locations')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get locations: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };

    var getLocationById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/locations/" + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get location: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    
    var addLocation = function (location) {
        $http.post('http://localhost:8890/api/dashboard/locations/' + location)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add location: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var updateLocation = function (location) {
        $http.put('http://localhost:8890/api/dashboard/locations/', location)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't uipdate location: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deleteLocation = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/locations/' + id)
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
        getAll: getLocations,
        get: getLocationById,
        getByName: getClientByName,
        add: addLocation,
        update: updateLocation,
        delete: deleteLocation
    };
};

var module = angular.module('app-dashboard2');
module.factory('repoLocations', repoLocations);
