(function () {
    "use strict";
    angular.module("assignProjectToEmployee", [])
        .component("assignProjectToEmployee", {
            templateUrl: "/js/app/assign-project-to-employee/assign-project-to-employee.template.html",
            controller: function assignProjectToEmployeeController(
                $http,
                $scope,
                $location,
                $q,
                repoAssignments,
                repoJobTitles
            ) {
                //$routeProvider
                var holder = this;
                holder.isBusy = true;

                holder.employeesAndProjects = [];
                holder.jobTitles = [];

                $q.all([
                    repoAssignments.lists(),
                    repoJobTitles.getAll()
                ]).then(function (response){
                    angular.copy(response[0], holder.employeesAndProjects);
                    angular.copy(response[1], holder.jobTitles);

                    //doWork();
                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
        .finally(function () {
            holder.isBusy = false;
        });



                //repoAssignments.lists().then(function (response) {
                //    //success
                //    console.log("Check");
                //    angular.copy(response, holder.employeesAndProjects);
                //}, function (error) {
                //    //failure
                //    holder.errorMessage = "Failed to load data: " + error;
                //})
                //.finally(function () {
                //    holder.isBusy = false;
                //    });

                ////http://localhost:8890/api/dashboard/jobTitleAssignments
               
                //repoJobTitles.getAll().then(function (response) {
                //    angular.copy(response.data, holder.jobTitles);
                //});
                //holder.jobTitles = [];
                //repoJobTitles.getAll().then(function (response) {
                //    angular.copy(response.data, holder.jobTitles);
                //});

                holder.newJobTitles = [];
                $scope.addJobTitle = function () {
                    holder.newJobTitles.push($scope.forminfo.jobtitle);
                }
                    
                holder.commitmentHours = ["0", "25", "50", "75", "100"];
                holder.commitments = [];
                $scope.addCommitment = function () {

                    holder.commitments.push($scope.commitment);
                    $scope.commitment = {};

                };

                $scope.remove = function (array, index) {
                    array.splice(index, 1);
                    var y = holder.commitments;
                };

                $scope.validateStopDate = function (start, end) {
                    $scope.errorMessage = "";
                    if (new Date(start) > new Date(end)) {
                        $scope.errorMessage = "To:date should be greater than start date.";
                        return false;
                    }
                };

                //if ($scope.formInfo.project && $scope.formInfo.employee) {
                //    $('addAssigmentsButton').prop('disabled', false);
                //} else {
                //    $('addAssigmentsButton').prop('disabled', true);
                //}

                $scope.assignProjectToEmployee = function () {
                    console.log("in the function");
                    holder.isBusy = true;
                    holder.errorMessage = "";

                    var test = $scope.formInfo.employee;

                    if (holder.commitments.length == 0) {
                        holder.commitments = [{ "startDate": $scope.formInfo.project.startDate, "stopDate": $scope.formInfo.project.stopDate, "hours" : "100" }];
                    }

                    //TODO: fix jobtitles

                    var data = { "ProjectId": $scope.formInfo.project.projectId, "EmployeeId": $scope.formInfo.employee.employeeId, "JobTitle": $scope.formInfo.jobtitle, "Location": $scope.formInfo.location, "Commitments": holder.commitments };
                    var dataTmp = JSON.stringify(data);

                    repoAssignments.add(dataTmp)
                        .then(function (response) {

                            $scope.formInfo = {};
                            holder.commitments = [];
                            console.log("Response from server api" + response.data);

                        }, function (error) {
                            self.errorMessage = "Failure to save new project";
                            console.log("didn't add assignment: " + error.message);
                        })
                        .finally(function () {
                            self.isBusy = false;
                            console.log("Finally...??");
                        });
                };
            }
        });
})();