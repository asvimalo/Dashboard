(function () {
    "use strict";
    angular.module("locationList")
        .component("locationList", {
            templateUrl: "/js/app/location/location-list/location-list.template.html",
            controller: function LocationListController($http) {
                var self = this;
                //self.orderProp = 'projectName';
                self.locations = [];
                $http.get('http://localhost:8890/api/dashboard/locations').then(function (response) {
                    angular.copy(response.data, self.locations);
                });
            }
            
        });
      
})();