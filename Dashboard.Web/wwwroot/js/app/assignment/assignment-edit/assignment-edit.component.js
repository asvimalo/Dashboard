(function () {
    "use strict";

    angular.module("assignmentEdit")
        .component("assignmentEdit", {
            templateUrl: "/js/app/assignment/assignment-edit/assignment-edit.template.html",
            controller: function AssignmentController($http, $scope, $routeParams, repoAssignments, $location) {

                this.assignmentId = $routeParams.assignmentId; 
                var holder = this; 
                holder.assignment = [];  
                
                repoAssignments.get(holder.assignmentId).then(function (response) {
                    angular.copy(response, holder.assignment);
                       
               });

                $scope.editAssignment = function () {

                    var obj = holder.assignment[0];

                    var data = {
                        "employeeId": obj.employeeId,
                        "projectId": obj.projectId,
                        "location": obj.location,
                        "startDate": new Date(obj.startDate).toLocaleDateString(),
                        "stopDate": new Date(obj.stopDate).toLocaleDateString()
                    };
                    var dataTmp = JSON.stringify(data);

                    repoAssignments.update(obj.assignmentId, dataTmp).then(function (response) {
                        console.log("Response from server api" + response.data);

                        $location.replace("#!/employees/employee-details/" + response.data.employeeId);
                        $location.reload();

                    }, function () {
                        console.log("failure");
                        holder.errorMessage = "Failure to save new project";
                    })
                    .finally(function () {
                        console.log("finally");
                        holder.isBusy = false;
                    });
                };
                 
            }
        });
      
})(); 

 