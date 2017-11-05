(function () {
    "use strict";
    angular.module("assignProjectToEmployee", [])
        .component("assignProjectToEmployee", {
            templateUrl: "/js/app/assign-project-to-employee/assign-project-to-employee.template.html",
            controller: function assignProjectToEmployeeController($http, $scope) {
                //$routeProvider
                var holder = this;

                holder.employees = [];
                $http.get("http://localhost:8890/api/dashboard/employees")
                    .then(function (response) {
                        //success
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
                $http.get("http://localhost:8890/api/dashboard/commitments")
                    .then(function (response) {
                        //success
                        angular.copy(response.data, holder.commitments);
                    }, function (error) {
                        //failure
                        holder.errorMessage = "Failed to load data: " + error;
                    })
                    .finally(function () {
                        holder.isBusy = false;
                    });

                $scope.addCommitment = function () {

                    holder.commitments.push($scope.commitment);
                    $scope.commitment = [];

                    //$scope.HouseBasket = $scope.HouseBasket.concat(data);
                    //console.log("in the addCommitment function");
                    //holder.isBusy = true;
                    //holder.errorMessage = "";

                    ////var commitments = $scope.commitment;
                    //var data = { "StartDate": $scope.commitment.startDate, "StopDate": $scope.commitment.stopDate, "Hours": $scope.commitment.hours }

                    //$http.post("http://localhost:8899/api/dashboard/commitments", JSON.stringify(data))
                    //    .then(function (response) {
                    //        //success
                    //        console.log("Response from server api" + response.data);
                    //        $scope.commitment = {}; 
                    //    }, function () {
                    //        //failure
                    //        console.log("failure");
                    //        holder.errorMessage = "Failure to save commitment.";
                    //    })
                    //    .finally(function () {
                    //        console.log("finally");
                    //        holder.isBusy = false;
                    //    });
                };

                $scope.assignProjectToEmployee = function () {
                    console.log("in the function");
                    holder.isBusy = true;
                    holder.errorMessage = "";

                    var data = { "ProjectId": $scope.formInfo.project.projectId, "EmployeeId": $scope.formInfo.employee.employeeId, "JobTitle": $scope.formInfo.jobtitle, "Location": $scope.formInfo.location };
                    var dataTmp = JSON.stringify(data);
                    //var dataTmp = JSON.stringify(data);
                    $http.post("http://localhost:8890/api/dashboard/assignments", dataTmp)  
                        .then(function (response) {
                            console.log("Response from server api" + response.data);
                            $scope.formInfo = {}; 
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