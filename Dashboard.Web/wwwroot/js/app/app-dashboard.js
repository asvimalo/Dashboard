(function () {
    "use strict";
    angular.module("app-dashboard",
        ["ngRoute",          
            'ngFileUpload',
            'angularModalService',
            "assignProjectToEmployee",
            "assignmentDelete",
            "assignmentEdit",
            "clientAdd",
            "employeeAdd",
            "employeeDetails",
            "employeeDelete",
            "employeeEdit",
            "dashboardView",
            "phaseAdd",
            "phaseDelete",
            "phaseEdit",
            "projectAdd",
            "projectDelete",
            "projectEdit",
            "projectDetails",
            "allProjects"
            //"allEmployees"
            ]);
})();

