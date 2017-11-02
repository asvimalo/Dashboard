(function () {
    "use strict";
    angular.module("assignProjectToEmployee")
        .component("assignProjectToEmployee", {
            templateUrl: "/js/app/assign-project-to-employee/assign-project-to-employee.template.html",
            controller: function assignProjectToEmployeeController($http) {
                //$routeProvider
                var holder = this;
                holder.employees = [];
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

                holder.assignProjectToEmployee = function () {
                    holder.isBusy = true;
                    holder.errorMessage = "";

                    $http.post("http://localhost:8899/api/dashboard/assignments", { header: { "Content-Type": "application/json" } } ) //$routeProvider.project
                        .then(function (response) {
                            //success
                            holder.assignments.push(response.data);
                            holder.assignments = {}; //??
                        }, function () {
                            //failure
                            holder.errorMessage = "Failure to save new project";
                        })
                        .finally(function () {
                            holder.isBusy = false;
                        });
                };
            }
        });
})();