(function () {
    "use strict";
    angular.module("assignProjectToEmployee", [])
        .component("assignProjectToEmployee", {
            templateUrl: "/js/app/assign-project-to-employee/assign-project-to-employee.template.html",
            controller: function assignProjectToEmployeeController($http, $scope, $location) {
                //$routeProvider
                var holder = this;

                holder.employees = [];
                $http.get("http://localhost:8890/api/dashboard/employees")
                    .then(function (response) {
                        //success
                        console.log("Check");
                        angular.copy(response.data, holder.employees);
                    }, function (error) {
                        //failure
                        holder.errorMessage = "Failed to load data: " + error;
                    })
                    .finally(function () {
                        holder.isBusy = false;
                    });

                holder.projects = [];
                $http.get("http://localhost:8890/api/dashboard/projects")
                    .then(function (response) {
                        //success
                        angular.copy(response.data, holder.projects);
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

                //$scope.commitment.stopDate = {

                //    if($scope.commitment.startDate > $scope.commitment.stopDate) {
                //        alert("Start date is later then end date.");
                //        $scope.commitment.stopDate = {};
                //    }
                        
                //};

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

                    var data = { "ProjectId": $scope.formInfo.project.projectId, "EmployeeId": $scope.formInfo.employee.employeeId, "JobTitle": $scope.formInfo.jobtitle, "Location": $scope.formInfo.location, "Commitments": holder.commitments };
                    var dataTmp = JSON.stringify(data);
                    //var dataTmp = JSON.stringify(data);
                    $http.post("http://localhost:8890/api/dashboard/assignments", dataTmp)  
                        .then(function (response) {
                            console.log("Response from server api" + response.data);
                            $scope.formInfo = {};
                            holder.commitments = [];
                            $location.path("/dashboard");
                        }, function () {
                            console.log("failure");
                            //failure
                            holder.errorMessage = "Failure to save assign a project to an employee.";
                        })
                        .finally(function () {
                            console.log("finally");
                            holder.isBusy = false;
                        });
                };
            }
        });
})();