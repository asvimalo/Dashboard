(function () {
    "use strict";
    angular.module("phaseList")
        .component("phaseList", {
            templateUrl: "/js/app/phase/phase-list/phase-list.template.html",
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