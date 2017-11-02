(function () {
    "use strict";
    angular.module("acquiredKnowledgeList")
        .component("acquiredKnowledgeList", {
            templateUrl: "/js/app/acquiredKnowledge/acquiredKnowledge-list/acquiredKnowledge-list.template.html",
            controller: function AcquiredKnowledgeController($http) {
                var self = this;
                //self.orderProp = 'projectName';
                self.acquiredKnowledges = [];
                $http.get('http://localhost:8899/api/dashboard/acquiredKnowledges').then(function (response) {
                    angular.copy(response.data, self.acquiredKnowledges);
                });
            }
            
        });
      
})();