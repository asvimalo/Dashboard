(function () {
    "use strict";
    angular.module("assignmentList")
        .component("assignmentList", {
            templateUrl: "/js/app2/components/assignment/assignment-list/assignment-list.template.html",
            controller: function AssignmentListController(repoAssignments) {
                var self = this;
                //self.orderProp = 'projectName';
                self.employees = [];
                repoAssignments.getAll().then(function (response) {
                    angular.copy(response, self.employees);
                });
            }
            
        });
      
})();