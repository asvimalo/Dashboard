(function () {
    "use strict";
    angular.module("phaseDelete")
        .component("phaseDelete", {
            templateUrl: "/js/app/phase/phase-delete/phase-delete.template.html",
            controller: function PhaseListController($http, $scope, $routeParams, repoPhases)
            {
                this.projectId = $routeParams.projectId;
                var self = this; 
                self.phase = {};
                  
                 
                if ($routeParams.phaseId === null) {
                    this.projectId = $routeParams.projectId;
                    console.log("The phaseId is null");
                } else {
                    this.phaseId = $routeParams.phaseId;

                    self.phase = {};

                    repoPhases.get(self.phaseId).then(function (response) {
                        self.phase = response;
                    });

                    
                }

                $scope.alert = true;
                $scope.deleteShow = true;
                $scope.deleteBtn = "Delete";
                $scope.closeBtn = "Cancel";

                $scope.deletePhase = function () {
                    repoPhases.delete(self.phaseId).then(function (response) {

                        $scope.alert = false;
                        $scope.successMessage = response;
                        $scope.closeBtn = "Close";
                        $scope.deleteShow = false; 

                    });
                };

                $scope.closeModal = function () {
                    location.replace("#!/projects/project-details/" + self.phase.projectId);
                    location.reload();
                };
                
                
                 
            }
            
        });
      
})();