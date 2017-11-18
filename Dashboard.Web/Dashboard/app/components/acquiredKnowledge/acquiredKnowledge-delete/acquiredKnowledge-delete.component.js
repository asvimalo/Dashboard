(function () {
    "use strict";
    angular.module("acquiredKnowledgeDelete")
        .component("acquiredKnowledgeDelete", {
            templateUrl: "/Dashboard/app/components/acquiredKnowledge/acquiredKnowledge-delete/acquiredKnowledge-delete.template.html",
            controller: function AcquiredKnowledgeController(
                $scope,
                repoAcquiredKnowledges,
                $location,
                $routeParams
                ) {

                var holder = this;

                $scope.deleteAcquiredKnowledge = function () {

                    console.log("inside detele Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";

                    repoAcquiredKnowledges.delete($routeParams.acquiredKnowledge.acquiredKnowledgeId)
                        .then(function (response) {
                            //success
                            console.log("Response from server api" + response);

                            window.location.templateUrl = '#/acquiredKnowledge';
                        }, function () {
                            //failure
                            holder.errorMessage = "Failure to save new acquiredKnowledge";
                        })
                        .finally(function () {
                            holder.isBusy = false;
                        });

                };
            }
            
        });
      
})();