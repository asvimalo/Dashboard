(function () {
    "use strict";
    angular.module("knowledgeDelete")
        .component("knowledgeDelete", {
            templateUrl: "/Dashboard/app/components/knowledge/knowledge-delete/knowledge-delete.template.html",
            controller: function KnowledgeListController(
                repoKnowledges,
                $routeParams
                ) {
                var holder = this;

                $scope.deleteKnowledge = function () {

                    console.log("inside detele Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";

                    repoKnowledges.detete($routeParams.knowledge.knowledgeId).then(function (data) {
                            //success
                            console.log("Response from server api" + data);
                            window.location.templateUrl = '#/employees';
                         
                        }, function () {
                            //failure
                            holder.errorMessage = "Failure to save new employee";

                        }).finally(function () {
                            holder.isBusy = false;
                        });



                };
            }

        });

})();