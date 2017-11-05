(function () {
    "use strict";
    angular.module("clientList")
        .component("clientList", {
            templateUrl: "/js/app/client/client-list/client-list.template.html",
            controller: function CientListController($http) {
                var self = this;
                //self.orderProp = 'projectName';
                self.clients = [];
                $http.get('http://localhost:8899/api/dashboard/clients').then(function (response) {
                    angular.copy(response.data, self.clients);
                });

                // I get all the projects with the clientId
                // For example I want all my projects for clientId = 1
                self.projects = [];
                $http.get('http://localhost:8899/api/dashboard/projects').then(function (response) {
                    //self.projects = response.data;
                    angular.copy(response.data, self.projects);

                });
            }
            
        });
      
})();