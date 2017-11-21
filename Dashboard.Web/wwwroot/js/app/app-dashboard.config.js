(function () {
    angular.
        module('app-dashboard').
        config(['$locationProvider', '$routeProvider',
            function config($locationProvider, $routeProvider) {
                $locationProvider.hashPrefix('!');

                $routeProvider.
                    when('/dashboard', {
                        template: '<dashboard-view></dashboard-view>'
                    }).
                    when('/employees', {
                        template: '<employee-list></employee-list>'
                    }).
                    when('/employees/employee-add', {
                        template: '<employee-add></employee-add>'
                    }).
                    //when('/employees/:employeeId', {
                    //    template: '<employee-detail></employee-detail>'
                    //}).
                    when('/projects', {
                        template: '<project-list></project-list>'
                    }).
                    //when('/projects/:projectId', {
                    //    template: '<project-detail></phone-detail>'
                    //}).
                    when('/projects/project-add', {
                        template: '<project-add></project-add>'
                    }).
                    when('/projects/project-details/:projectId', {
                        template: '<project-details></project-details>'
                    }).
                    when('/projects/project-delete/:projectId', {
                        template: '<project-delete></project-delete>'
                    }).
                    //when('/assignments', {
                    //    template: '<assignment-list></assignment-list>'
                    //}).
                    //when('/assignments/:employeeId', {
                    //    template: '<assignment-detail></assignment-detail>'
                    //}).
                    //when('/acquiredKnowledges', {
                    //    template: '<acquired-knowledge-list></acquired-knowledge-list>'
                    //}).
                    //when('/acquiredKnowledge/:acquiredKnowledge', {
                    //    template: '<acquiredKnowledge-detail></acquiredKnowledge-detail>'
                    //}).
                    when('/clients', {
                        template: '<client-list></client-list>'
                    }).
                    //when('/client/:client', {
                    //    template: '<acquiredKnowledge-detail></acquiredKnowledge-detail>'
                    //}).
                    when('/clients/client-add', {
                        template: '<client-add></client-add>'
                    }).
                    //when('/commitments', {
                    //    template: '<commitment-list></commitment-list>'
                    //}).
                    //when('/commitment/:commitmentId', {
                    //    template: '<commitment-detail></commitment-detail>'
                    //}).
                    //when('/knowledges', {
                    //    template: '<knowledge-list></knowledge-list>'
                    //}).
                    //when('/knowledge/:knowledgeId', {
                    //    template: '<knowledge-detail></knowledge-detail>'
                    //}).
                    //when('/phases', {
                    //    template: '<phase-list></phase-list>'
                    //}).
                    //when('/phases/:phaseId', {
                    //    template: '<phase-detail></phase-detail>'
                    //}).
                    when('/phases/phase-add', {
                        template: '<phase-add></phase-add>'
                    }).
                    when('/phases/phase-delete/:phaseId', {
                        template: '<phase-delete></phase-delete>'
                    }).
                    when('/phases/phase-edit/:phaseId', {
                        template: '<phase-edit></phase-edit>'
                    }).
                    //when('/tasks', {
                    //    template: '<task-list></task-list>'
                    //}).
                    //when('/tasks/:taskId', {
                    //    template: '<task-detail></task-detail>'
                    //}).
                    //when('/locations', {
                    //    template: '<location-list></location-list>'
                    //}).
                    //when('/locations/:locationId', {
                    //    template: '<location-detail></location-detail>'
                    //}).
                    //when('/projectAdd', {
                    //    template: '<project-add></project-add>'
                    //}).
                    when('/assignProjectToEmployee', {
                        template: '<assign-project-to-employee></assign-project-to-employee>'
                    }).
                    when('/allProjects', {
                        template: '<all-projects></all-projects>'
                    }).
                    when('/allEmployees', {
                        template: '<all-employees></all-employees>'
                    }).
                    otherwise('/dashboard');
            }
        ]);
})();