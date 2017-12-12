(function () {
    angular.
        module('app-dashboard').
        config(['$locationProvider', '$routeProvider',
            function config($locationProvider, $routeProvider) {
                $locationProvider.hashPrefix('!');

                $routeProvider.
                    when('/dashboardView', {
                        template: '<dashboard-view></dashboard-view>'
                    }).
                    when('/employees', {
                        template: '<employee-list></employee-list>'
                    }).
                    when('/employees/employee-add', {
                        template: '<employee-add></employee-add>'
                    }).
                    when('/employees/employee-details/:employeeId', {
                        template: '<employee-details></employee-details>'
                    }).
                    when('/employees/employee-delete/:employeeId', {
                        template: '<employee-delete></employee-delete>'
                    }).
                    when('/employees/employee-edit/:employeeId', {
                        template: '<employee-edit></employee-edit>'
                    }).
                    when('/projects', {
                        template: '<project-list></project-list>'
                    }). 
                    when('/projects/project-add', {
                        template: '<project-add></project-add>'
                    }).
                    when('/projects/project-details/:projectId', {
                        template: '<project-details></project-details>'
                    }).
                    when('/projects/project-delete/:projectId', {
                        template: '<project-delete></project-delete>'
                    }).
                    when('/projects/project-edit/:projectId', {
                        template: '<project-edit></project-edit>'
                    }).
                    when('/assignments/assignment-delete/:assignmentId', {
                        template: '<assignment-delete></assignment-delete>'
                    }).
                    when('/assignments/assignment-edit/:assignmentId', {
                        template: '<assignment-edit></assignment-edit>'
                    }). 
                    when('/clients', {
                        template: '<client-list></client-list>'
                    }). 
                    when('/clients/client-add', {
                        template: '<client-add></client-add>'
                    }). 
                    when('/phases/phase-add', {
                        template: '<phase-add></phase-add>'
                    }).
                    when('/phases/phase-delete/:phaseId', {
                        template: '<phase-delete></phase-delete>'
                    }).
                    when('/phases/phase-edit/:phaseId', {
                        template: '<phase-edit></phase-edit>'
                    }). 
                    when('/assignProjectToEmployee', {
                        template: '<assign-project-to-employee></assign-project-to-employee>'
                    }).
                    when('/allProjects', {
                        template: '<all-projects></all-projects>'
                    }).
                    when('/allEmployees', {
                        template: '<all-employees></all-employees>'
                    }).
                    otherwise('/allProjects');
            }
        ]);
})();