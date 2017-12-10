(function () {
    "use strict";
    angular.module("projectAdd")
        .component("projectAdd", {
            templateUrl: "/js/app/components/project/project-add/project-add.template.html",
            controller: function ProjectAddController($http, $scope, $q, repoProjects, repoClients) {

                var self = this;
                $scope.alertSave = true;    
                $scope.alertClient = true;

                self.employeesAndClients = [];

                fetchData(doWork);

                function fetchData(completedCallback) {
                    $q.all([
                        repoProjects.getEmpClientList()
                    ]).then(function (response) {
                        angular.copy(response[0], self.employeesAndClients);
                        if (completedCallback != null)
                            completedCallback();
                    }, function (error) {
                        self.errorMessage = "Failed to load data: " + error;
                    }).finally(function () {
                        self.isBusy = false;
                    });
                };

                function doWork() {

                    $scope.validateEndDate = function (start, end) {
                        if (new Date(end) < new Date(start)) {
                            $scope.errorMessageSave = "To:date should be greater than start date.";
                            $scope.alertSave = false;
                            return false;
                        }
                        $scope.alertSave = true;
                        return true;
                    };

                    self.addedClient = {};
                    $scope.addClient = function () {
                        if ($scope.addClientForm == null)
                            return false;

                        self.isBusy = true;
                        $scope.alertClient = true;
                        self.errorMessageClient = "";

                        if ($scope.addClientForm.clientName == null) {
                            $scope.errorMessageClient = "Please, enter a client name.";
                            $scope.alertClient = false;
                            return false;
                        }

                        var data = { "ClientName": $scope.addClientForm.clientName, "City": $scope.addClientForm.city, "Address": $scope.addClientForm.adress };
                        var dataTmp = JSON.stringify(data);
                        repoClients.add(dataTmp).then(function (response) {
                            angular.copy(response, self.addedClient);
                            $scope.addClientForm = {};
                            fetchData(null); //Update Clients in select
                            }, function (error) {
                                self.errorMessage = "Failure to save new project";
                            }).finally(function () {
                                self.isBusy = false;
                            });  
                    };

                    $scope.reload = function () {
                        window.location.reload();
                    };

                    $scope.addProject = function () {
                        if (self.project == null)
                            return false;

                        self.isBusy = true;
                        $scope.alertSave = true;
                        self.errorMessageSave = "";

                        if (self.project.projectName == null) {
                            $scope.errorMessageSave = "Please enter project name.";
                            $scope.alertSave = false;
                            return false;
                        }

                        if ($scope.addProjectForm.clients == null || $scope.addProjectForm.clients.clientId == null) {
                            $scope.errorMessageSave = "Please, select a client.";
                            $scope.alertSave = false;
                            return false;
                        }

                        var data = { "projectName": self.project.projectName, "startDate": new Date(self.project.startDate).toLocaleDateString(), "stopDate": new Date(self.project.stopDate).toLocaleDateString(), "timeBudget": self.project.timeBudget, "ClientId": $scope.addProjectForm.clients.clientId, "notes": self.project.notes }; //, "employees": $scope.addProjectForm.employees };
                        var dataTmp = JSON.stringify(data);

                        repoProjects.add(dataTmp).then(function (response) {
                            self.project = [];
                            $scope.addProjectForm = {};

                        }, function () {
                            console.log("failure");
                            self.errorMessage = "Failure to save new project";
                        }).finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                            $scope.addProjectForm = {};
                        });
                    };
                }
            }
        });
})();











