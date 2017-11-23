(function () {
    "use strict";
    angular.module("phaseEdit")
        .component("phaseEdit", {
            templateUrl: "/js/app/phase/phase-edit/phase-edit.template.html",
            controller: function PhaseListController($http, $scope, repoPhases, $location, $routeParams)
            {
                var self = this;
                self.phase = {};

                if ($routeParams.phaseId == null) {
                    console.log("The phaseId is NULL")
                } else {
                    this.phaseId = $routeParams.phaseId;

                    repoPhases.get(self.phaseId).then(function (response) {
                        angular.copy(response, self.phase)
                        self.phase.startDate = new Date(self.phase.startDate).toLocaleDateString();
                        self.phase.endDate = new Date(self.phase.endDate).toLocaleDateString();
                    });

                }

                $scope.editPhase = function () {
                    var data = {
                        "timeBudget": self.phase.timeBudget,
                        "progress": self.phase.progress,
                        "comments": self.phase.comments,
                        "startDate": new Date(self.phase.startDate).toLocaleDateString(),
                        "endDate": new Date(self.phase.endDate).toLocaleDateString()
                    };
                    var dataTmp = JSON.stringify(data);
                    
                    repoPhases.update(self.phase.phaseId, dataTmp);

                    location.replace("#!/projects/project-details/" + self.phase.projectId);
                    location.reload();
                };

                $scope.closeModal = function () {
                    location.replace("#!/projects/project-details/" + self.phase.projectId);
                    location.reload();
                };
            }
            
        });
      
})();