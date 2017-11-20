(function () {
    "use strict";
    angular.module("assignProjectToEmployee", [])
        .component("assignProjectToEmployee", {
            templateUrl: "/js/app/assign-project-to-employee/assign-project-to-employee.template.html",
            controller: function assignProjectToEmployeeController(
                $http,
                $scope,
                $location,
                repoAssignments
            ) {
                //$routeProvider
                var holder = this;
                holder.isBusy = true;

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

                $scope.assignProjectToEmployee = function () {
                    console.log("in the function");
                    holder.isBusy = true;
                    holder.errorMessage = "";

                    var test = $scope.formInfo.employee;

                    var data = { "ProjectId": $scope.formInfo.project.projectId, "EmployeeId": $scope.formInfo.employee.employeeId, "JobTitle": $scope.formInfo.jobtitle, "Location": $scope.formInfo.location, "Commitments": holder.commitments };
                    var dataTmp = JSON.stringify(data);
                    repoAssignments.add(dataTmp);
                    $scope.formInfo = {};
                    holder.commitments = [];
                    //$scope.message = "Project is assigned to emloyee.";
                        
                        //.then(function (response) {
                        //    holder.isBusy = true;
                        //    console.log("Response from server api" + response);
                        //    $scope.formInfo = {};
                        //    holder.commitments = [];
                        //    $location.path("/dashboard");
                        //}, function (error) {
                        //     holder.errorMessage = "Failed to save data: " + error;
                        //    
                        //    
                        //})
                        //.finally(function () {
                        //    console.log("finally");
                        //    holder.isBusy = false;
                        //});
                
                };
            }
        });
})();