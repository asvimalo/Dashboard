(function () {
    "use strict";
    angular.module("phaseDelete")
        .component("phaseDelete", {
            templateUrl: "/js/app/phase/phase-delete/phase-delete.template.html",
            controller: function PhaseListController($http, $scope, $routeParams) {                
                this.phaseId = $routeParams.phaseId;

                var self = this;
                self.phase = {};

                $http.get('http://localhost:8890/api/dashboard/phases/' + self.phaseId).then(function (response) {
                    angular.copy(response.data, self.phase);
                });

                $scope.deletePhase = function () {
                    $http.delete('http://localhost:8890/api/dashboard/phases/' + self.phaseId).then(function (response) {
                        angular.copy(response.data, self.phaseId);
                        // It's working
                    });
                }
            }
            
        });
      
})();