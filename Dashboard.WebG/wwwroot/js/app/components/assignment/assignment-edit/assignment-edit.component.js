(function () {
    "use strict";

    angular.module("assignmentEdit")
        .component("assignmentEdit", {
            templateUrl: "/js/app/components/assignment/assignment-edit/assignment-edit.template.html",
            controller: function AssignmentController($http, $scope, $routeParams, repoAssignments, $location) {

                this.assignmentId = $routeParams.assignmentId; 
                var holder = this; 
                holder.assignment = [];  
                
                repoAssignments.get(holder.assignmentId).then(function (response) {
                    angular.copy(response, holder.assignment);

                    var oneAssignment = holder.assignment[0]; 
                       
               });

                $scope.editAssignment = function () {

                    var obj = ctrl.holder.assignment[0];

                    var data = {
                        "employeeId": holder.employeeId,
                        "firstName": holder.employee.firstName,
                        "lastName": holder.employee.lastName,
                        //"jobTitle": obj.jobTitleAssignments.jobTitle.titleName,
                        "projectId": holder.projectId,
                        "location": holder.location, 
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

                $scope.closeModal = function () {
                    var oneAssignment = holder.assignment[0];

                    location.replace("#!/projects/project-details/" + oneAssignment.projectId);
                };
                 
            }
        });
      
})(); 

 