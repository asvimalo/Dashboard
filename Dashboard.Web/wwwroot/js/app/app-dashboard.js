(function () {
    "use strict";
    angular.module("app-dashboard",
        ["ngRoute",          
            'ngFileUpload',
            'angularModalService',   
            "assignProjectToEmployee",
            "clientAdd",
            "employeeAdd",
            "dashboardView",
            "phaseAdd",
            "phaseDelete",
            "phaseEdit",
            "allProjects",
            "allEmployees"
            ]);
})();

