(function () {
    "use strict";
    angular.module("phaseDelete", [])
        .component("phaseDelete", {
            templateUrl: "/js/app2/components/phase/phase-delete/phase-delete.template.html",
            controller: function PhaseListController(
                $http,
                $scope,
                $routeParams,
                repoPhases
            ) {
                this.projectId = $routeParams.projectId;
                var self = this; 
                self.phase = {};
                //$scope.$on('$locationChangeStart', function (e, next, previous) {
                //    $scope.oldUrl = previous;
                //    $scope.oldHash = $window.location.hash;
                //}); 

                //$scope.showAModal = function () {

                //    // Just provide a template url, a controller and call 'showModal'.
                //    ModalService.showModal({
                //        templateUrl: "yesno/yesno.html",
                //        controller: "YesNoController",
                //        inputs: {
                //            phaseId: self.phaseId,
                //            phaseName: self.phaseName
                //        }
                //    }).then(function (modal) {
                //        // The modal object has the element built, if this is a bootstrap modal
                //        // you can call 'modal' to show it, if it's a custom modal just show or hide
                //        // it as you need to.
                //        modal.element.modal();
                //        modal.close.then(function (result) {
                //            $scope.message = result ? "You said Yes" : "You said No";
                //        });
                //    });
                //};

                $scope.dismissModal = function (result) {
                    close(result, 200); // close, but give 200ms for bootstrap to animate
                };
                 
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

                $scope.deletePhase = function () {
                    self.isBusy = true;
                    repoPhases.delete(self.phaseId).then(function (response) {
                        console.log("Deleted done ", response);

                    }, function () {
                        //failure
                        self.errorMessage = "Failure to  get  phases";

                    }).finally(function () {
                        self.isBusy = false;
                    });
                };
                
                
                 
            }
            
        });
      
})();