(function () {
    "use strict";
    angular.module("commitmentList")
        .component("commitmentList", {
            templateUrl: "/Dashboard/app/components/commitment/commitment-list/commitment-list.template.html",
            controller: function CommitmentListController(repoCommitments) {
                var self = this;
                //self.orderProp = 'projectName';
                self.commitments = [];
                //http get to commitments
                repoCommitments.getAll().then(function (response) {
                    angular.copy(response, self.commitments);
                });
            }
            
        });
      
})();