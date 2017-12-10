(function () {
    "use strict";

    angular.module("assignmentEdit")
        .component("assignmentEdit", {
            templateUrl: "/js/app/assignment/assignment-edit/assignment-edit.template.html",
            controller: function AssignmentController($http, $scope, $routeParams, repoAssignments, $location, repoJobTitles) {

                this.assignmentId = $routeParams.assignmentId; 
                var holder = this; 
                holder.assignment = {};  
                holder.jobTitles = [];  

                repoJobTitles.getAll().then(function (response) {
                    angular.copy(response, holder.jobTitles)

                });

                repoAssignments.get(holder.assignmentId).then(function (response) {
                    angular.copy(response[0], holder.assignment);  

                    holder.assignment.startDate = new Date(holder.assignment.startDate).toLocaleDateString();
                    holder.assignment.stopDate = new Date(holder.assignment.stopDate).toLocaleDateString();

                });
                  
                $scope.alertJobTitle = false;
                $scope.alertSuccess = false;
                $scope.addJobTitle = function () {

                    var data = {
                        "titleName": $scope.formInfo.jobTitle
                    };

                    var dataTmp = JSON.stringify(data);
                    repoJobTitles.add(dataTmp).then(function (response) {

                        holder.jobTitles.push(response.result);
                        $scope.formInfo.jobTitle = "";
                        $scope.successMessage = 'Job title is to the list "Select a job title"'
                        $scope.alertSuccess = true;

                    }, function (error) {
                        $scope.alertJobTitle = true;
                        $scope.errorMessage = "didn't add jobtitle: " + response.data;
                    }).finally(function () {

                            console.log("Finally...??");
                   });

                }

                $scope.editAssignment = function () {


                    if ($scope.formInfo === undefined && holder.newJobTitles === undefined) {
                        var data = {
                            "employeeId": holder.assignment.employeeId,
                            "projectId": holder.assignment.projectId,
                            "location": holder.assignment.location,
                            "startDate": holder.assignment.startDate,
                            "stopDate": holder.assignment.stopDate
                        };

                        var dataTmp = JSON.stringify(data);
                        repoAssignments.update(holder.assignment.assignmentId, dataTmp).then(function (response) {
                            console.log("Response from server api" + response.result);

                            location.replace("#!/employees/employee-details/" + response.result.employeeId);
                            location.reload();

                        }, function (error) {
                            console.log("failure");
                            holder.errorMessage = "Failure to save new assignment";
                        }).finally(function () {
                            console.log("finally");
                            holder.isBusy = false;
                            });

                    } else if ($scope.formInfo.jobTitles === null || holder.newJobTitles !== null) {
                         
                        var jobTitle = [];
                        jobTitle.push($scope.formInfo.jobTitles);

                        var dataWithJobTitle = {
                            "employeeId": holder.assignment.employeeId,
                            "projectId": holder.assignment.projectId,
                            "location": holder.assignment.location,
                            "jobTitleAssignments": jobTitle,
                            "startDate": holder.assignment.startDate,
                            "stopDate": holder.assignment.stopDate
                        };

                        var dataTmpWithJobTitle = JSON.stringify(dataWithJobTitle);
                        repoAssignments.update(holder.assignment.assignmentId, dataTmpWithJobTitle).then(function (response) {
                            if (response.statusText !== "Bad Request") {
                                console.log("Response from server api" + response.result);

                                location.replace("#!/employees/employee-details/" + response.result.employeeId);
                                location.reload();
                            }

                        }, function (error) {
                            $scope.alertJobTitle = true;
                            $scope.errorMessage = "didn't add jobtitle: " + response.data;
                            console.log("failure");
                            holder.errorMessage = "Failure to save new assignment";
                        }).finally(function () {
                                console.log("finally");
                                holder.isBusy = false;
                            });

                    } 
                     
                };

                $scope.closeModal = function () {
                    location.replace("#!/employees/employee-details/" + holder.assignment.employeeId);
                    location.reload();

                };
                 
            }
        });
      
})(); 

 