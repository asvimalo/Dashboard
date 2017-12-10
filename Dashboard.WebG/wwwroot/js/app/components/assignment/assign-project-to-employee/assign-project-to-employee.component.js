(function () {
    "use strict";
    angular.module("assignProjectToEmployee", [])
        .component("assignProjectToEmployee", {
            templateUrl: "/js/app/components/assignment/assign-project-to-employee/assign-project-to-employee.template.html",
            controller: function assignProjectToEmployeeController($http, $scope, $location, $q, repoAssignments, repoJobTitles) {

                var holder = this;
                holder.isBusy = true;
                $scope.alert = true;
                $scope.alertAdd = true;

                holder.employeesAndProjects = [];
                holder.jobTitles = [];

                fetchData(doWork);

                function fetchData(completedCallback) {
                    $q.all([
                        repoAssignments.lists(),
                        repoJobTitles.getAll()
                    ]).then(function (response) {
                        angular.copy(response[0], holder.employeesAndProjects);
                        angular.copy(response[1], holder.jobTitles);
                        if (completedCallback != null)
                            completedCallback();
                    }, function (error) {
                        holder.errorMessage = "Failed to load data: " + error;
                    }).finally(function () {
                        holder.isBusy = false;
                    });
                };

                function doWork() {
                    holder.newJobTitles = [];
                    $scope.addJobTitle = function () {
                        holder.newJobTitles.push($scope.formInfo.jobTitle);
                    };

                    holder.commitmentHours = ["0", "25", "50", "75", "100"];
                    holder.commitments = [];

                    $scope.addCommitment = function () {

                        if ($scope.commitment == null)
                            return false;

                        $scope.errorMessageAdd = "";
                        $scope.alertAdd = true;

                        if ($scope.commitment.startDate == null) {
                            $scope.errorMessageAdd = "Please enter start date.";
                            $scope.alertAdd = false;
                            return false;
                        }
                        if ($scope.commitment.stopDate == null) {
                            $scope.errorMessageAdd = "Please enter TO date.";
                            $scope.alertAdd = false;
                            return false;
                        }
                        if ($scope.commitment.hours == null) {
                            $scope.errorMessageAdd = "Please enter commitment percentage.";
                            $scope.alertAdd = false;
                            return false;
                        }
                        var projectStart = moment($scope.formInfo.project.startDate);
                        var projectStop = moment($scope.formInfo.project.stopDate);
                        var commitStart = moment($scope.commitment.startDate);
                        var commitStop = moment($scope.commitment.stopDate);
                        if (commitStart.isAfter(projectStop, 'day') || commitStart.isBefore(projectStart, 'day') ||
                            commitStop.isAfter(projectStop, 'day') || commitStop.isBefore(projectStart, 'day')) 
                        {
                            $scope.errorMessageAdd = "Commitment extends outside the project (" + projectStart.format('YYYY-MM-DD') + " - " + projectStop.format('YYYY-MM-DD') + ").";
                            $scope.alertAdd = false;
                            return false;
                        }

                        for (var i = 0; i < holder.commitments.length; i++) {
                            var existingCommitment = holder.commitments[i];
                            var existingStart = moment(existingCommitment.startDate);
                            var existingStop = moment(existingCommitment.stopDate);

                            if (commitStop.isBefore(existingStart, 'day') || commitStart.isAfter(existingStop, 'day'))
                                continue;

                            $scope.errorMessageAdd = "Commitments overlap.";
                            $scope.alertAdd = false;
                            return false;
                        }


                        holder.commitments.push($scope.commitment);
                        $scope.commitment = {};
                        if ($scope.formInfo != null && $scope.formInfo.project != null) {
                            $scope.commitment.startDate = new Date($scope.formInfo.project.startDate);
                            $scope.commitment.stopDate = new Date($scope.formInfo.project.stopDate);
                        }
                        return true;
                    };

                    $scope.projectChanged = function () {
                        $scope.commitment = {};
                        $scope.commitment.startDate = new Date($scope.formInfo.project.startDate);
                        $scope.commitment.stopDate = new Date($scope.formInfo.project.stopDate);
                    };

                    $scope.reload = function () {
                        window.location.reload();
                    };

                    $scope.remove = function (array, index) {
                        array.splice(index, 1);
                        var y = holder.commitments;
                    };

                    $scope.validateEndDate = function (start, end) {
                        if (start == null || end == null)
                            return true;
                        $scope.errorMessage = "";
                        if (new Date(start) > new Date(end)) {
                            $scope.errorMessage = "To date should be greater or equal to start date.";
                            $scope.alert = false;
                            return false;
                        }
                        $scope.alert = true;
                        return true;
                    };

                    $scope.assignProjectToEmployee = function () {
                        holder.isBusy = true;
                        holder.errorMessage = "";

                        if ($scope.formInfo == null || $scope.formInfo.project == null || $scope.formInfo.employee == null)
                            return;

                        var data = {};
                        if (holder.commitments.length === 0) {
                            holder.commitments = [{ "startDate": $scope.formInfo.project.startDate, "stopDate": $scope.formInfo.project.stopDate, "hours": "100" }];
                        }
                        data = { "ProjectId": $scope.formInfo.project.projectId, "EmployeeId": $scope.formInfo.employee.employeeId, "Location": $scope.formInfo.location, "Commitments": holder.commitments };
                        data.newJobTitles = holder.newJobTitles;
                        data.jobTitles = $scope.formInfo.jobTitles;
                        var dataTmp = JSON.stringify(data);

                        var serverResponse = {};
                        repoAssignments.add(dataTmp).then(function (response) {

                            serverResponse = response;
                            $scope.formInfo = {};
                            holder.commitments = [];
                            holder.newJobTitles = [];
                            fetchData(null); //Update jobtitles in select
                        }, function (error) {
                            self.errorMessage = "Failed to save new project";
                            console.log("didn't add assignment: " + error.message);
                        }).finally(function () {
                            self.isBusy = false;
                        });
                    };
                };
            }
        });
})();