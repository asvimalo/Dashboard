(function () {
    "use strict";

    angular.module("projectAdd")
        .component("projectAdd", {
            templateUrl: "/js/app2/components/project/project-add/project-add.template.html",
            controller: function ProjectAddController(
                repoProjects,
                $scope
                ) {
                var self = this;
                self.isBusy = true;
                self.employeesAndClients = []; 
               
                
                repoProjects.lists().then(function (data) {
                    angular.copy(data, self.employeesAndClients);
                }, function () {
                    console.log("failure");
                    self.errorMessage = "Failure getting project and client lists";

                }).finally(function () {
                        console.log("finally");
                        self.isBusy = false;
                        window.location.reload();
                    });
                    

                
                 
                $scope.addProject = function () {
                    console.log("in the addProject function");
                    self.isBusy = true;
                    self.errorMessage = "";

                    var data = { "projectName": self.project.projectName, "startDate": new Date(self.project.startDate).toLocaleDateString(), "stopDate": new Date(self.project.stopDate).toLocaleDateString(), "timeBudget": self.project.timeBudget, "ClientId": $scope.formInfo.clients.clientId, "Employees": $scope.formInfo.employees, "notes": self.project.notes };
                    var dataTmp = JSON.stringify(data);

                    repoProjects.add(dataTmp).then(function (data) {
                        console.log("Response from server api" + data);

                    }, function () {
                        console.log("failure");
                        self.errorMessage = "Failure to save new project";

                    }).finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                            window.location.reload();
                        });                                        
                };                                
            }
        });     
})(); 

 