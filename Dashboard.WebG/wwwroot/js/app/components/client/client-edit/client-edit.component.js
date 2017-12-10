(function () {
    "use strict";
    angular.module("clientEdit")
        .component("clientEdit", {
            templateUrl: "/js/app/components/client/client-edit/client-edit.template.html",
            controller: function ClientAddController(
                $http,
                $scope,
                $routeParams,
                $location,
                $window,
                repoClients,
                repoProjects) {

                var self = this;

                self.clients = [];
                self.locations = [];
                self.editClient = {};
                var client = repoClients.get(routeParams.ClientId);
                $scope.addClientForm.clientName = client.clientName;
                $scope.addClientForm.city = client.location.city;
                $scope.addClientForm.adress = client.location.address

                $scope.editClient = function () {
                    console.log("in the editProject function");
                    self.isBusy = true;
                    self.errorMessage = "";

                    var client = {
                        clientId: $routeParams.ClientId,
                        clientName: $scope.addClientForm.clientName,
                        location: {
                            city: $scope.addClientForm.city,
                            address: $scope.addClientForm.adress
                        }
                    };
                    var dataTmp = JSON.stringify(client);

                    repoClients.update(dataTmp)
                        .then(function (response) {

                            console.log("Response from server api" + response);
                            //angular.copy(response, self.editClient);

                            window.location.reload();

                        }, function (error) {
                            //failure
                            console.log("failure");
                            self.errorMessage = "Failure to edit client";
                            window.location.reload();
                        })
                        .finally(function () {
                            console.log("finally");
                            self.isBusy = false;

                        });  
                };
             }
        });
})();