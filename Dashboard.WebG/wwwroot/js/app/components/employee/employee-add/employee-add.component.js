(function () {
    "use strict";
    angular.module("employeeAdd")
        .component("employeeAdd", {
            templateUrl: "/js/app/components/employee/employee-add/employee-add.template.html",
            controller: function EmployeeListController($scope, $http, $location, $q, repoEmployees, repoKnowledges) {

                var holder = this;
                $scope.alertSave = true;   

                holder.knowledges = [];

                fetchData(doWork);

                function fetchData(completedCallback) {
                    $q.all([
                        repoKnowledges.get()
                    ]).then(function (response) {
                        angular.copy(response[0], holder.knowledges);
                        if (completedCallback != null)
                            completedCallback();
                    }, function (error) {
                        holder.errorMessage = "Failed to load data: " + error;
                    }).finally(function () {
                        holder.isBusy = false;
                    });
                };

                function doWork() {

                    holder.newKnowledges = [];
                    $scope.addKnowledges = function () {
                        holder.newKnowledges.push(holder.knowledges.knowledgeName);
                    };

                    $scope.remove = function (array, index) {
                        array.splice(index, 1);
                        var y = holder.knowledges;
                    };

                    $scope.reload = function () {
                        window.location.reload();
                    }

                    function checkPersonNumber(pnbr) {
                        var rx = /^\d{8}-\d{4}$/;
                        if (!rx.test(pnbr))
                            return false;
                        var date = pnbr.slice(0, 8);
                        if (!moment(date, 'YYYYMMDD').isValid())
                            return false;
                        return true;
                    }

                    $scope.addEmployee = function () {
                        $scope.alertSave = true;
                        holder.errorMessageSave = "";

                        if ($scope.employee == null)
                            return false;

                        if ($scope.employee.firstName == null) {
                            $scope.errorMessageSave = "Please enter the first name.";
                            $scope.alertSave = false;
                            return false;
                        }

                        if ($scope.employee.lastName == null) {
                            $scope.errorMessageSave = "Please enter the last name.";
                            $scope.alertSave = false;
                            return false;
                        }

                        if ($scope.employee.personNr == null) {
                            $scope.errorMessageSave = "Please enter personal number.";
                            $scope.alertSave = false;
                            return false;
                        }
                        if (!checkPersonNumber($scope.employee.personNr)) {
                            $scope.errorMessageSave = "Please enter a valid personal number (YYYYMMDD-XXXX).";
                            $scope.alertSave = false;
                            return false;
                        }

                        var newEmployee = {};

                        var employee = $scope.employee;
                        newEmployee.firstName = employee.firstName;
                        newEmployee.lastName = employee.lastName;
                        newEmployee.personNr = employee.personNr;
                        newEmployee.newKnowledges = holder.newKnowledges;
                        newEmployee.knowledges = employee.knowledges;

                        var employeeJson = JSON.stringify(newEmployee);

                        repoEmployees.add(employeeJson).then(function (response) {
                            $scope.employee = {};
                            newEmployee = {};
                            holder.newKnowledges = [];
                            fetchData(null); //Update knowledges in select
                        }, function (err) {
                            holder.errorMessage = "Failure to save new employee";
                        }).finally(function () {
                            holder.isBusy = false;
                        });
                    };
                }
            }
        });
})();