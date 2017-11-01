//app-dashboard.config.js

(function () {
    "use strict";
    angular.module("app-dashboard")
        .config(function ($locationProvider,  $routeProvider) {
            $locationProvider.hashPrefix('!')

            $routeProvider.when("/", {               
                templateUrl: "dashboard/dashboard.template.html"
            });
            $routeProvider.when("/employees", {
                
                templateUrl: "employeeList/employee-list.template.html"
            });
            $routeProvider.when("/assignments", {
                
                templateUrl: "assignmentList/assignment-list.template.html"
            });
            $routeProvider.when("/projects", {
                
                templateUrl: "projectList/project-list.template.html"
            });
            $routeProvider.otherwise({redirect: "/"})
        })


})();