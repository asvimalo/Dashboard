﻿(function () {
    "use strict";
    angular.module("knowledgeList")
        .component("knowledgeList", {
            templateUrl: "/js/app/knowledge/knowledge-list/knowledge-list.template.html",
            controller: function KnowledgeListController($http) {
                var self = this;
                //self.orderProp = 'projectName';
                self.knowledges = [];
                $http.get('http://localhost:8899/api/dashboard/knowledges').then(function (response) {
                    angular.copy(response.data, self.knowledges);
                });
            }
            
        });
      
})();