(function () {
    "use strict";
    angular.module("phaseList")
        .component("phaseList", {
            templateUrl: "/js/app2/components/phase/phase-list/phase-list.template.html",
            controller: function PhaseListController(repoPhases) {
                var self = this;
                self.isBusy = true;
                //self.orderProp = 'projectName';
                self.phases = [];
                repoPhases.getAll().then(function (data) {
                    angular.copy(data, self.phases);
                }, function () {
                    //failure
                    self.errorMessage = "Failure to  get  phases";

                }).finally(function () {
                    self.isBusy = false;
                });
            }
            
        });
      
})();