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
                    otherwise('/dashboard');
            }
        ]);
})();