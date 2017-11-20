(function () {
    "use strict";
    angular.module("app-dashboard",
        ["ngRoute",          
            'ngFileUpload',
            'angularModalService',
            "projectList",
            "employeeList",
            "assignmentList",
            "dashboardView",
            "clientList", 
            "commitmentList",
            "acquiredKnowledgeList",
            "knowledgeList",
            "phaseList",
            "taskList",
            "locationList",
            "employeeAdd",
            "projectAdd",
            "projectDelete",
            "projectDetails",
            "clientAdd",
            "phaseAdd",
            "phaseDelete",
            "assignProjectToEmployee",
            "allProjects"
            ]);
})();

