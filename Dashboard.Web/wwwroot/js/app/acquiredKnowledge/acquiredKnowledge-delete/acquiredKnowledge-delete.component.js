(function () {
    "use strict";
    angular.module("acquiredKnowledgeDelete")
        .component("acquiredKnowledgeDelete", {
            templateUrl: "/js/app/acquiredKnowledge/acquiredKnowledge-delete/acquiredKnowledge-delete.template.html",
            controller: function AcquiredKnowledgeController($scope, $http, $location, $routeParams) {

                var holder = this;

                $scope.deleteAcquiredKnowledge = function () {

                    console.log("inside detele Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";

                    $http.delete('http://localhost:8890/api/dashboard/acquiredknowledges/', $routeParams.acquiredKnowledge.acquiredKnowledgeId)
                        .then(function (response) {
                            //success
                            console.log("Response from server api" + response.data);

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