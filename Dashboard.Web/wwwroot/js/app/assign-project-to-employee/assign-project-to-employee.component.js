(function () {
    "use strict";
    angular.module("assignProjectToEmployee", [])
        .component("assignProjectToEmployee", {
            templateUrl: "/js/app/assign-project-to-employee/assign-project-to-employee.template.html",
            controller: function assignProjectToEmployeeController($http, $scope, $location, $q, repoAssignments, repoJobTitles, repoJobTitles, repoJobTitlAssignments) {
                //$routeProvider
                var holder = this;
                holder.isBusy = true;

                holder.employeesAndProjects = [];
                holder.jobTitles = [];

                $q.all([
                    repoAssignments.lists(),
                    repoJobTitles.getAll()
                ]).then(function (response) {
                    angular.copy(response[0], holder.employeesAndProjects);
                    angular.copy(response[1], holder.jobTitles);

                    doWork();
                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                }).finally(function () {
                    holder.isBusy = false;
                });

                function doWork() {
                    holder.newJobTitles = [];
                    $scope.addJobTitle = function () {
                        holder.newJobTitles.push($scope.formInfo.jobTitle);
                    }

                    holder.commitmentHours = ["0", "25", "50", "75", "100"];
                    holder.commitments = [];

                    $scope.addCommitment = function () {

                        holder.commitments.push($scope.commitment);
                        $scope.commitment = {};

                    };

                    $scope.reload = function () {
                        window.location.reload();
                    }

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

                    $scope.assignProjectToEmployee = function () {
                        console.log("in the function");
                        holder.isBusy = true;
                        holder.errorMessage = "";

                        var data = {};

                        if (holder.commitments.length == 0) {
                            holder.commitments = [{ "startDate": $scope.formInfo.project.startDate, "stopDate": $scope.formInfo.project.stopDate, "hours": "100" }];
                        }
                        data = { "ProjectId": $scope.formInfo.project.projectId, "EmployeeId": $scope.formInfo.employee.employeeId, "Location": $scope.formInfo.location, "Commitments": holder.commitments };
                        var dataTmp = JSON.stringify(data);

                        var serverResponse = {};
                        repoAssignments.add(dataTmp).then(function (response) {

                                serverResponse = response;
                                $scope.formInfo = {};
                                holder.commitments = [];
                                holder.newJobTitles = [];
                                console.log("Response from server api" + response.data);

                            }, function (error) {
                                self.errorMessage = "Failure to save new project";
                                console.log("didn't add assignment: " + error.message);
                            }).finally(function () {
                                self.isBusy = false;
                                console.log("Finally...??");
                            });

                        data.newJobTitles = holder.newJobTitles;
                        data.jobTitles = $scope.formInfo.jobTitles;
                        var serverResponseJobTitle = {};
                        repoJobTitles.add(dataTmp).then(function (response) {

                        }, function (error) {
                            self.errorMessage = "Failure to save new project";
                            console.log("didn't add assignment: " + error.message);
                        }).finally(function () {
                            self.isBusy = false;
                            console.log("Finally...??");
                        });
                    }
                };
            }
        });
})();