(function () {
    "use strict";
    angular.module("phaseAdd")
        .component("phaseAdd", {
            templateUrl: "/js/app2/components/phase/phase-add/phase-add.template.html",
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