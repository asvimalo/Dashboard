
var repoTasks = function ($http) {

    var getTasks = function () {
        return $http.get('http://localhost:8890/api/dashboard/tasks')
            .then(function (response) {

                return response.data;

            }, function (error) {

                console.log("didn't get tasks: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    
    var getTaskById = function (id) {
        return $http.get("http://localhost:8890/api/dashboard/tasks/" + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get employees: " + error.message);
            }).finally(function () {

                console.log("Finally...??");
            }); 
    };
    var getTaskByName = function (name) {
        return $http.get("http://localhost:8890/api/dashboard/tasks/" + name)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't get task: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var addTask = function (task) {
        $http.post('http://localhost:8890/api/dashboard/tasks/', task)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't add task: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var updateTask = function (task) {
        $http.put('http://localhost:8890/api/dashboard/tasks/', task)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't update task: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    var deleteTask = function (id) {
        $http.delete('http://localhost:8890/api/dashboard/tasks/' + id)
            .then(function (response) {
                return response.data;
            }, function (error) {

                console.log("didn't delete task: " + error.message);
            })
            .finally(function () {

                console.log("Finally...??");
            });
    };
    return {
        getAll: getTasks,
        get: getTaskById,
        getByName: getTaskByName,
        add: addTask,
        update: updateTask,
        delete: deleteTask
    };
};

var module = angular.module('app-dashboard');
module.factory('repoTasks', repoTasks);
