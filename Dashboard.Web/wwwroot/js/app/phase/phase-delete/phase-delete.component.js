(function () {
    "use strict";
    angular.module("phaseDelete")
        .component("phaseDelete", {
            templateUrl: "/js/app/phase/phase-delete/phase-delete.template.html",
            controller: function PhaseListController(
                $http,
                $scope,
                $routeParams,
                repoPhases,
                $window
                ){
                this.projectId = $routeParams.projectId;
                var self = this; 
                self.phase = {};
                $scope.$on('$locationChangeStart', function (e, next, previous) {
                    $scope.oldUrl = previous;
                    $scope.oldHash = $window.location.hash;
                });  
                 
                if ($routeParams.phaseId == null) {
                    this.projectId = $routeParams.projectId;
                    console.log("The phaseId is null");
                } else {
                    this.phaseId = $routeParams.phaseId;

                    self.phase = {};

                    repoPhases.get(self.phaseId).then(function (response) {
                        self.phase = response;
                    });

                    
                }

                $scope.deletePhase = function () {
                    repoPhases.delete(self.phaseId);
                    location.replace("#!/projects/project-details/" + self.phase.projectId).reload();
                };

                $scope.closeModal = function () {
                    location.replace("#!/projects/project-details/" + self.phase.projectId).reload();
                };
                
                
                 
            }
            
        });
      
})();