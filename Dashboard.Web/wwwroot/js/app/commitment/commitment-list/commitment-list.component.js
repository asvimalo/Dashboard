(function () {
    "use strict";
    angular.module("commitmentList")
        .component("commitmentList", {
            templateUrl: "/js/app/commitment/commitment-list/commitment-list.template.html",
            controller: function CommitmentListController($http) {
                var self = this;
                //self.orderProp = 'projectName';
                self.commitments = [];
                $http.get('http://localhost:8890/api/dashboard/commitments').then(function (response) {
                    angular.copy(response.data, self.commitments);
                });
            }
            
        });
      
})();