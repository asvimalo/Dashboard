(function () {
    "use strict";
    angular.module("acquiredKnowledgeList")
        .component("acquiredKnowledgeList", {
            templateUrl: "/js/app2/components/acquiredKnowledge/acquiredKnowledge-list/acquiredKnowledge-list.template.html",
            controller: function AcquiredKnowledgeController(
                repoAcquiredKnowledges,
                repoKnowledges,
                repoEmployees
                ) {
                var self = this;
                //self.orderProp = 'projectName';
                self.acquiredKnowledges = [];
                repoAcquiredKnowledges.getAll().then(function (response) {
                    angular.copy(response, self.acquiredKnowledges);
                });

                self.knowledges = [];
                repoKnowledges.getAll().then(function (response) {
                    angular.copy(response, self.knowledges);
                });

                self.employees = [];
                repoEmployees.getAll().then(function (response) {
                    angular.copy(response, self.employees);
                });
            }
            
        });
      
})();