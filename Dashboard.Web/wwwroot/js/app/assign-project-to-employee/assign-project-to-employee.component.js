(function () {
    "use strict";
    angular.module("assignProjectToEmployee", [])
        .component("assignProjectToEmployee", {
            templateUrl: "/js/app/assign-project-to-employee/assign-project-to-employee.template.html",
            controller: function assignProjectToEmployeeController($http, $scope) {
                //$routeProvider
                var holder = this;
                holder.employees = [];
                // Här sparar jag mitt objekt för assignment

                $http.get("http://localhost:8899/api/dashboard/employees")
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
                $http.get("http://localhost:8899/api/dashboard/projects")
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

                holder.addCommitment = function () {
                //    holder.isBusy = true;
                //    holder.errorMessage = "";

                //    $http.post("http://localhost:8899/api/dashboard/commitments", $routeProvider.project)
                //        .then(function (response) {
                //            //success
                //            holder.commitments.push(response.data);
                //            holder.newCommitment = {}; //??
                //        }, function () {
                //            //failure
                //            holder.errorMessage = "Failure to save new project";
                //        })
                //        .finally(function () {
                //            holder.isBusy = false;
                //        });
                };

                $scope.assignProjectToEmployee = function () {
                    console.log("in the function");
                    holder.isBusy = true;
                    holder.errorMessage = "";

                    var data = { "ProjectId": $scope.formInfo.project.projectId, "EmployeeId": $scope.formInfo.employee.employeeId, "JobTitle": $scope.formInfo.jobtitle, "Location": $scope.formInfo.location };

                    var dataTmp = JSON.stringify(data);
                    $http.post("http://localhost:8899/api/dashboard/assignments", dataTmp)  
                        .then(function (response) {
                            console.log("Response from server api" + response.data);
                            $scope.formInfo = {}; 
                        }, function () {
                            console.log("failure");
                            //failure
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