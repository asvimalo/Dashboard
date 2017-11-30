(function () {
    "use strict";
    angular.module("clientAdd")
        .component("clientAdd", {
            templateUrl: "/js/app/components/client/client-add/client-add.template.html",
            controller: function ClientAddController(
                repoClients,
                $scope,
                $routeParams,
                $location
                ) {

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
                    repoClients.add(dataTmp).then(function (response) {
                        console.log("Response from server api" + response);
                        // Sparar datan i $scope.locationObj för att få locationId
                        angular.copy(response, $scope.locationObj);
                        window.location.reload();

                    }, function () {
                        //failure
                        console.log("failure");
                        self.errorMessage = "Failure to save new project";
                        window.location.reload();

                    }).finally(function () {
                            console.log("finally");
                            self.isBusy = false;
                            window.location.reload();

                        });        
                                                            
                };              
            }
        });
})();