(function () {
    "use strict";
    angular.module("phaseAdd")
        .component("phaseAdd", {
            templateUrl: "/js/app/phase/phase-add/phase-add.template.html",
            controller: function PhaseListController($http) {
                var self = this;
                //self.orderProp = 'projectName';
                self.phases = [];
                $http.get('http://localhost:8890/api/dashboard/phases').then(function (response) {
                    angular.copy(response.data, self.phases);
                });
            }
            
        });
      
})();