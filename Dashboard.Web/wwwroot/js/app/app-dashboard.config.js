//app-dashboard.config.js

(function () {
    "use strict";
    angular.module("app-dashboard")
        .config(function ($locationProvider,  $routeProvider) {
            $locationProvider.hashPrefix('!')
            debugger;
            $routeProvider.when("/", {               
                templateUrl: "js/dashboard/dashboard.template.html"
            });
            $routeProvider.when("/employees", {
                
                templateUrl: "<employee-list></employee-list>"
            });
            $routeProvider.when("/assignments", {
                
                templateUrl: "<assignment-list></assignment-list>"
            });
            $routeProvider.when("/projects", {
                
                templateUrl: "<project-list></project-list>"
            });
            $routeProvider.otherwise({redirect: "/"})
        })


}());