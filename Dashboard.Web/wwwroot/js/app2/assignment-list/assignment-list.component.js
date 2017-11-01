(function () {
    "use strict";
    angular.module("assignmentList")
        .component("assignmentList", {
            templateUrl: "/js/app2/assignment-list/assignment-list.template.html",
            controller: function AssignmentListController($http) {
                var self = this;
                //self.orderProp = 'projectName';
                self.employees = [];
                $http.get('http://localhost:8899/api/dashboard/assignments').then(function (response) {
                    angular.copy(response.data, self.employees);
                });
            }
            
        });
      
})();