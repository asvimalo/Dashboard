﻿(function () {
    "use strict";
    angular.module("assignProjectToEmployee", [])
        .component("assignProjectToEmployee", {
            templateUrl: "/js/app/assign-project-to-employee/assign-project-to-employee.template.html",
            controller: function assignProjectToEmployeeController(
                $http,
                $scope,
                $location,
                repoEmployees,
                repoAssignments
            ) {
                //$routeProvider
                var holder = this;

                holder.employeesAndProjects = [];
                repoAssignments.lists().then(function (response) {
                    //success
                    console.log("Check");
                    angular.copy(response, holder.employeesAndProjects);
                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
                .finally(function () {
                    holder.isBusy = false;
                });
                    

                holder.commitments = [];
                $scope.addCommitment = function () {

                    holder.commitments.push($scope.commitment);                   
                    $scope.commitment = {};
                    
                };

                $scope.remove = function (array, index) {
                    array.splice(index, 1);
                    var y = holder.commitments;
                };

                $scope.validateEndDate = function (start, end) {
                    $scope.errorMessage = "";
                    if (new Date(start) > new Date(end)) {
                        $scope.errorMessage = "To:date should be greater than start date.";
                        return false;
                    }
                };

                //$('input[name="daterange"]').daterangepicker(
                //    {
                //        locale: {
                //            format: 'YYYY-MM-DD'
                //        },
                //        startDate: '2017-01-01',
                //        endDate: '2017-12-31'
                //    },
                //    function (start, end, label) {
                //        alert("A new date range was chosen: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
                //    }
                //);

                $scope.assignProjectToEmployee = function () {
                    console.log("in the function");
                    holder.isBusy = true;
                    holder.errorMessage = "";

                    var test = $scope.formInfo.employee;

                    var data = { "ProjectId": $scope.formInfo.project.projectId, "EmployeeId": $scope.formInfo.employee.employeeId, "JobTitle": $scope.formInfo.jobtitle, "Location": $scope.formInfo.location, "Commitments": holder.commitments };
                    var dataTmp = JSON.stringify(data);

                    repoAssignments.add(dataTmp);
                        
                };
            }
        });
})();