﻿(function () {
    "use strict";
    angular.module("dashboardView")
        .component("dashboardView", {
            templateUrl: "/js/app2/dashboard-view/dashboard-view.template.html",
            controller: function DashboardViewController() {
                var self = this;
                self.title = "DASHBOARD";
                
            }
            
        });
      
})();