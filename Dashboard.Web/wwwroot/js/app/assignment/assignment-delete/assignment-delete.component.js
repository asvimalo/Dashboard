(function () {
    "use strict";
    angular.module("assignmentDelete")
        .component("assignmentDelete", {
            templateUrl: "/js/app/assignment/assignment-delete/assignment-delete.template.html",
            controller: function AssignmentController($http, $scope, $location, $routeParams, repoAssignments)
            {
                this.assignmentId = $routeParams.assignmentId;

                var self = this;

                self.assignment = []; 

                repoAssignments.get(self.assignmentId).then(function (response) {
                    angular.copy(response, self.assignment); 
                }); 

                $scope.deleteProject = function () {
                    repoAssignments.delete(self.assignmentId).then(function (response) {
                        location.replace("#!/allemployees");
                        location.reload();
                    });
                    

                };
                 
            }
        });
      
})(); 

 