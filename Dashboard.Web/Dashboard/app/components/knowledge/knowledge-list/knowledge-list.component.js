(function () {
    "use strict";
    angular.module("knowledgeList")
        .component("knowledgeList", {
            templateUrl: "/Dashboard/app/knowledge/knowledge-list/knowledge-list.template.html",
            controller: function KnowledgeListController(
                repoKnowledges,
                repoAcquiredKnowledges
                ) {
                var self = this;
                holder.isBusy = true;
                //self.orderProp = 'projectName';
                self.knowledges = [];
                repoKnowledges.getAll().then(function (data) {
                    angular.copy(data, self.knowledges);
                }, function () {
                    //failure
                    holder.errorMessage = "Failure to  get  knowledges";

                }).finally(function () {
                    holder.isBusy = false;
                });

                self.acquiredKnowledges = [];
                repoAcquiredKnowledges.getAll().then(function (data) {
                    
                    angular.copy(data, self.acquiredKnowledges);

                }, function () {
                    //failure
                    holder.errorMessage = "Failure to list get acquired knowledges";

                }).finally(function () {
                    holder.isBusy = false;
                });
            }
            
        });
      
})();