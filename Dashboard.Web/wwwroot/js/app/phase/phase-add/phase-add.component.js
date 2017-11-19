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
                        "phaseName": self.phase.phaseName, "startDate": self.phase.startDate, "endDate": self.phase.endDate, "timeBudget": self.phase.timeBudget, "projectId": $scope.$parent.$ctrl.projectId };
                    var dataTmp = JSON.stringify(data);
                    

                    // Http Post for Location 
                    repoPhases.add(dataTmp);
                }
            }
            
        });
      
})();