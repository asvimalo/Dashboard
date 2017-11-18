
var repoProjects = function ($http) {

    var getProjects = function () {
        return $http.get('http://localhost:8890/api/dashboard/projects')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get employees: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };

    var getProjectById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/projects/" + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employees: " + error.message);
            }); 
    };
    var getProjectByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/projects/" + name)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var addProject = function (project) {
        $http.post('http://localhost:8890/api/dashboard/projects', project)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var updateProject = function (project) {
        $http.put('http://localhost:8890/api/dashboard/projects', project)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't update employee: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deleteProject = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/projects/' + id)
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
        getAll: getProjects,
        get: getProjectById,
        getByName: getProjectByName,
        add: addProject,
        update: updateProject,
        delete: deleteProject
    };
};

var module = angular.module('app-dashboard');
module.factory('repoProjects', repoProjects);
