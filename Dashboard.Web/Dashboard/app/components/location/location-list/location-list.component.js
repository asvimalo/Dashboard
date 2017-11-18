(function () {
    "use strict";
    angular.module("locationList")
        .component("locationList", {
            templateUrl: "/Dashboard/app/components/location/location-list/location-list.template.html",
            controller: function LocationListController(repoLocations) {
                var self = this;
                holder.isBusy = true;
                //self.orderProp = 'projectName';
                self.locations = [];
                repoLocations.getAll().then(function (data) {
                    angular.copy(data, self.locations);
                }, function () {
                    //failure
                    holder.errorMessage = "Failure to save new knowledges";
                }).finally(function () {
                    holder.isBusy = false;
                });

            }
            
        });
      
})();