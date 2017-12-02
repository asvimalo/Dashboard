(function () {
    "use strict";
    angular.module("app-dashboard",
        ["ngRoute",          
         'ngFileUpload',
         'angularModalService',
         "assignProjectToEmployee",
         "clientAdd",
         "employeeAdd",
         "employeeDetails",
         "dashboardView",
         "phaseAdd",
         "phaseDelete",
         "phaseEdit",
         "projectAdd",
         "projectDelete",
         "projectEdit",
         "projectDetails",
         "allProjects",
         "allEmployees",
        ]);
})();

