(function () {
    "use strict";
    angular.module("clientAdd")
        .component("clientAdd", {
            templateUrl: "/js/app/client/client-add/client-add.template.html",
            controller: function ClientAddController($http, $scope, $routeParams) {

                var self = this;

                // Putting all my Clients in this array.
                self.clients = [];

                // Putting all my Locations in this array.
                self.locations = [];

                // Add Project Function
                $scope.addClient = function () {
                    console.log("in the addProject function");
                    self.isBusy = true;
                    self.errorMessage = "";

                    var locationsData = { "City": self.location.city, "Address": self.location.adress };
                    var dataTmpLocation = JSON.stringify(locationsData);

                    // Http Post for Location 
                    $http.post("http://localhost:8899/api/dashboard/locations", dataTmpLocation)
                        .then(function (response) {
                            console.log("Response from server api" + response.data);
                            // Sparar datan i $scope.locationObj för att få locationId
                            $scope.locationObj = response.data;

                        }, function () {
                            //failure
                            console.log("failure");
                            self.errorMessage = "Failure to save new project";
                        })
                        .finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                            console.log(locationdata)

                        }); 

                    var clientData = { "clientName": self.client.clientName, "description": self.client.description, "locationId": $scope.locationObj.locationId };
                    var dataTmpClient = JSON.stringify(clientData);

                    // Http Post for Client 
                    $http.post("http://localhost:8899/api/dashboard/clients", dataTmpClient)
                        .then(function (response) {
                            console.log("Response from server api" + response.data);
                            console.log("Client Post")

                        }, function () {
                            //failure
                            console.log("failure");
                            self.errorMessage = "Failure to save new project";
                        })
                        .finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                        });

                };

                // End of controller
            }

        });

})();