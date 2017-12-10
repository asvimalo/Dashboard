
var repoProjects = function ($http) {

    var getProjects = function () {
        return $http.get('http://localhost:8890/api/dashboard/projects')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get projects: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    var getEmpClientList = function () {
        return $http.get("http://localhost:8890/api/dashboard/projects/employeesclientslist")
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get Employee and Client list: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    var getProjectById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/projects/" + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get project: " + error.message);
            }); 
    };
    var getProjectByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/projects/" + name)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get project: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    var addProject = function (project) {
        return $http.post('http://localhost:8890/api/dashboard/projects', project).then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add project: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    var updateProject = function (id, project) {
        return $http.put('http://localhost:8890/api/dashboard/projects/'+ id, project)
            .then(function (response) {
                return response;
            }, function (error) {

                console.log("didn't update project: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    var deleteProject = function (id) {
        return $http.delete('http://localhost:8890/api/dashboard/projects/' + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't delete project: " + error.message);
            })
            .finally(function () {

                //console.log("Finally...??");
            });
    };
    return {
        getAll: getProjects,
        getEmpClientList: getEmpClientList,
        get: getProjectById,
        getByName: getProjectByName,
        add: addProject,
        update: updateProject,
        delete: deleteProject
    };
};

var module = angular.module('app-dashboard');
module.factory('repoProjects', repoProjects);
