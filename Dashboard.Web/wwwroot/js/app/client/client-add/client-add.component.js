(function () {
    "use strict";
    angular.module("clientAdd")
        .component("clientAdd", {
            templateUrl: "/js/app/client/client-add/client-add.template.html",
            controller: function ClientAddController($http, $scope, $routeParams, $location, repoClients) {

                var self = this;

                self.clients = [];
                self.locations = [];
                self.addedClient = {};
                $scope.addClient = function () {
                    console.log("in the addProject function");
                    self.isBusy = true;
                    self.errorMessage = "";

                    var data = { "ClientName": $scope.addClientForm.clientName, "City": $scope.addClientForm.city, "Address": $scope.addClientForm.adress };
                    var dataTmp = JSON.stringify(data);

                    // Http Post for Location 
                    repoClients.add(dataTmp)
                        .then(function (response) {
                            console.log("Response from server api" + response);
                            // Sparar datan i $scope.locationObj för att få locationId
                            $scope.addClientForm = {};
                            angular.copy(response, self.addedClient);
                        }, function () {
                            //failure
                            console.log("failure");
                            self.errorMessage = "Failure to save new project";
                            window.location.reload();
                        })
                        .finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                             //console.log(locationdata)

                        });  
                };

                $scope.updateClientList = function () {

                };

             }

        });

})();