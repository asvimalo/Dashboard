(function () {
    "use strict";
    angular.module("clientList")
        .component("clientList", {
            templateUrl: "/Dashboard/app/components/client/client-list/client-list.template.html",
            controller: function CientListController(
                repoClients,
                repoProjects
                ) {
                var self = this;
               
                self.clients = [];
                self.projects = [];

                repoClients.getAll().then(function (response) {
                    angular.copy(response, self.clients);
                });
                               
                repoProjects.getAll().then(function (response) {
                    
                    angular.copy(response, self.projects);

                });
            }
            
        });
      
})();