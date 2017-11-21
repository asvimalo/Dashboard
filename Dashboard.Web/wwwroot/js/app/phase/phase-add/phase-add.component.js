(function () {
    "use strict";
    angular.module("phaseAdd")
        .component("phaseAdd", {
            templateUrl: "/js/app/phase/phase-add/phase-add.template.html",
            controller: function PhaseListController($http, $scope, repoPhases, $location)
            {
                var self = this;
                self.phases = [];

                $scope.addPhase = function () {
                    var data = {
                        "phaseName": self.phase.phaseName, "startDate": new Date(self.phase.startDate).toLocaleDateString(), "endDate": new Date(self.phase.endDate).toLocaleDateString(), "timeBudget": self.phase.timeBudget, "projectId": $scope.$parent.$ctrl.projectId };
                    var dataTmp = JSON.stringify(data);
                    
                    repoPhases.add(dataTmp);
                }
            }
            
        });
      
})();