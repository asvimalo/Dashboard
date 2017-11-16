(function () {
    "use strict";
    angular.module("phaseDelete", [])
        .component("phaseDelete", {
            templateUrl: "/js/app/phase/phase-delete/phase-delete.template.html",
            controller: function PhaseListController(ModalService, $scope, close) {
                //this.projectId = $routeParams.projectId;
                var self = this; 
                self.phase = {};
                //$scope.$on('$locationChangeStart', function (e, next, previous) {
                //    $scope.oldUrl = previous;
                //    $scope.oldHash = $window.location.hash;
                //}); 

                $scope.showAModal = function () {

                    // Just provide a template url, a controller and call 'showModal'.
                    ModalService.showModal({
                        templateUrl: "yesno/yesno.html",
                        controller: "YesNoController",
                        inputs: {
                            phaseId: self.phaseId,
                            phaseName: self.phaseName
                        }
                    }).then(function (modal) {
                        // The modal object has the element built, if this is a bootstrap modal
                        // you can call 'modal' to show it, if it's a custom modal just show or hide
                        // it as you need to.
                        modal.element.modal();
                        modal.close.then(function (result) {
                            $scope.message = result ? "You said Yes" : "You said No";
                        });
                    });
                }

                $scope.dismissModal = function (result) {
                    close(result, 200); // close, but give 200ms for bootstrap to animate
                };
                 
                //if ($routeParams.phaseId == null) {
                //    this.projectId = $routeParams.projectId;
                //    console.log("The phaseId is null");
                //} else {
                //    this.phaseId = $routeParams.phaseId;

                //    self.phase = {};

                //    $http.get('http://localhost:8890/api/dashboard/phases/' + self.phaseId).then(function (response) {
                //        angular.copy(response.data, self.phase);
                //    });

                    
                //}

                //$scope.deletePhase = function () {
                //    $http.delete('http://localhost:8890/api/dashboard/phases/' + self.phaseId).then(function (response) {
                //        console.log("Deleted done") 

                //    });
                //}
                
                
                 
            }
            
        });
      
})();