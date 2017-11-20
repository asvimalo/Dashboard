(function () {
    "use strict";
    angular.module("clientAdd")
        .component("clientAdd", {
            templateUrl: "/js/app/client/client-add/client-add.template.html",
            controller: function ClientAddController($http, $scope, $routeParams, $location, repoClients) {

                var self = this;

                self.clients = [];
                self.locations = [];

                $scope.addClient = function () {
                    console.log("in the addProject function");
                    self.isBusy = true;
                    self.errorMessage = "";

                    var data = { "ClientName": self.client.clientName, "City": self.location.city, "Address": self.location.adress };
                    var dataTmp = JSON.stringify(data);

                    // Http Post for Location 
                    repoClients.add(dataTmp)
                        .then(function (response) {
                            console.log("Response from server api" + response.data);
                            // Sparar datan i $scope.locationObj för att få locationId
                            angular.copy(response.data, $scope.locationObj);
                            //window.location.reload();

                        }, function () {
                            //failure
                            console.log("failure");
                            self.errorMessage = "Failure to save new project";
                            window.location.reload();
                        })
                        .finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                            window.location.reload();
                            //console.log(locationdata)

                        });  
                };

                $scope.updateClientList = function () {

                };

             }

        });

})();