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

                    var data = { "ClientName": self.client.clientName, "City": self.location.city, "Address": self.location.adress };
                    var dataTmp = JSON.stringify(data);

                    // Http Post for Location 
                    $http.post("http://localhost:8890/api/dashboard/clients", dataTmp)
                        .then(function (response) {
                            console.log("Response from server api" + response.data);
                            // Sparar datan i $scope.locationObj för att få locationId
                            angular.copy(response.data, $scope.locationObj);

                        }, function () {
                            //failure
                            console.log("failure");
                            self.errorMessage = "Failure to save new project";
                        })
                        .finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                            //console.log(locationdata)

                        }); 

                    //var clientData = { "clientName": self.client.clientName, "LocationId": $scope.locationObj.id };
                    //var dataTmpClient = JSON.stringify(clientData);

                    //// Http Post for Client 
                    //$http.post("http://localhost:8899/api/dashboard/clients", dataTmpClient)
                    //    .then(function (response) {
                    //        console.log("Response from server api" + response.data);
                    //        console.log("Client Post")

                    //    }, function () {
                    //        //failure
                    //        console.log("failure");
                    //        self.errorMessage = "Failure to save new project";
                    //    })
                    //    .finally(function () {
                    //        console.log("finally");
                    //        self.isBusy = false;
                    //    });

                };

                // End of controller
            }

        });

})();