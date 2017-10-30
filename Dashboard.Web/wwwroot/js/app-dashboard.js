//app-dashboard

(function () {
    "use strict";
    angular.module("app-dashboard", ["loaderControl", "ngRoute"])
        .config(function ($routeProvider) {
            $routeProvider.when("/", {
                controller: "dashboardController",
                controllerAs: "holder",
                templateUrl: "/views/dashboardView.html"
            });
            $routeProvider.when("/employees", {
                controller: "employeesController",
                controllerAs: "holder",
                templateUrl: "/views/employeesView.html"
            });
            $routeProvider.when("/assignments", {
                controller: "assignmentsController",
                controllerAs: "holder",
                templateUrl: "/views/assignmentsView.html"
            });
            $routeProvider.when("/projects", {
                controller: "projectsController",
                controllerAs: "holder",
                templateUrl: "/views/projectsView.html"
            });
            $routeProvider.otherwise({redirect: "/"})
        })


})();