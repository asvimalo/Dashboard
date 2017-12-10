(function () {
    "use strict";
    angular.module("assignmentDelete")
        .component("assignmentDelete", {
            templateUrl: "/js/app/components/assignment/assignment-delete/assignment-delete.template.html",
            controller: function AssignmentController(
                $http,
                $scope,
                $location,
                $routeParams,
                repoAssignments
            )
            {
                this.assignmentId = $routeParams.assignmentId;

                var self = this;

                self.assignment = [];

                repoAssignments.get(self.assignmentId).then(function (response) {
                    angular.copy(response, self.assignment);
                });

                $scope.alert = false;
                $scope.deleteBtn = "Delete";
                $scope.cancelBtn = "Cancel";
                $scope.deleteAssignment = function () {
                    repoAssignments.delete(self.assignmentId).then(function (response) {
                        $scope.alert = true;
                        $scope.successMessage = response;
                        $scope.cancelBtn = true;
                    });


                };

                $scope.closeModal = function () {
                    location.replace("#!/allemployees");
                    location.reload();
                };
                 
            }
        });
      
})(); 

 