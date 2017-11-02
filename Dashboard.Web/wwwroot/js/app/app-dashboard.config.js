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
                    //when('/employees/:employeeId', {
                    //    template: '<employee-detail></employee-detail>'
                    //}).
                    when('/projects', {
                        template: '<project-list></project-list>'
                    }).
                    //when('/projects/:projectId', {
                    //    template: '<project-detail></phone-detail>'
                    //}).
                    when('/assignments', {
                        template: '<assignment-list></assignment-list>'
                    }).
                    //when('/assignments/:employeeId', {
                    //    template: '<assignment-detail></assignment-detail>'
                    //}).
                    when('/acquiredKnowledges', {
                        template: '<acquiredKnowledge-list></acquiredKnowledge-list>'
                    }).
                    //when('/acquiredKnowledge/:acquiredKnowledge', {
                    //    template: '<acquiredKnowledge-detail></acquiredKnowledge-detail>'
                    //}).
                    when('/clients', {
                        template: '<client-list></client-list>'
                    }).
                    //when('/client/:client', {
                    //    template: '<acquiredKnowledge-detail></acquiredKnowledge-detail>'
                    //}).
                    when('/commitments', {
                        template: '<commitment-list></commitment-list>'
                    }).
                    //when('/commitment/:commitmentId', {
                    //    template: '<commitment-detail></commitment-detail>'
                    //}).
                    when('/knowledges', {
                        template: '<knowledge-list></knowledge-list>'
                    }).
                    //when('/knowledge/:knowledgeId', {
                    //    template: '<knowledge-detail></knowledge-detail>'
                    //}).
                    when('/phases', {
                        template: '<phase-list></phase-list>'
                    }).
                    //when('/phases/:phaseId', {
                    //    template: '<phase-detail></phase-detail>'
                    //}).
                    when('/tasks', {
                        template: '<task-list></task-list>'
                    }).
                    //when('/tasks/:taskId', {
                    //    template: '<task-detail></task-detail>'
                    //}).
                    otherwise('/dashboard');
            }
        ]);
})();