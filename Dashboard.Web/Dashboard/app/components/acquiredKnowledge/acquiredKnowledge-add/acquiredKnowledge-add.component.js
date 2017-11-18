(function () {
    "use strict";
    angular.module("acquiredKnowledgeAdd")
        .component("acquiredKnowledgeAdd", {
            templateUrl: "/Dashboard/app/components/acquiredKnowledge/acquiredKnowledge-add/acquiredKnowledge-add.template.html",
            controller: function AcquiredKnowledgeController(
                $scope,
                $location,
                repoAcquiredKnowledges,
                repoKnowledges,
                repoEmployees
                ) {

                var holder = this;

                holder.acquiredKnowledges = [];
                //repoAcquiredKnowledges service:
                repoAcquiredKnowledges.getAll().then(function (response) {
                    angular.copy(response, holder.acquiredKnowledges);
                });

                holder.knowledges = [];
                //repoKnowledges service:
                repoKnowledges.getAll().then(function (response) {
                    angular.copy(response, holder.knowledges);
                });

                holder.employees = [];
                //repoEmployees service:
                repoEmployees.getAll().then(function (response) {
                    angular.copy(response, holder.employees);
                });

                $scope.addAcquiredKnowledges = function () {

                    var acquiredKnowledges = $scope.acquiredKnowledges;      

                    var newAcquiredKnowledges = {};                                 
                    newAcquiredKnowledges.knowledges = [];
                    newAcquiredKnowledges.employees = [];


                    console.log("inside acquired knowledges Controller ");
                    holder.errorMessage = "";
                    holder.isBusy = true;
                    holder.progress = "";

                    var json = JSON.stringify(newAcquiredKnowledges);
                    //repoAcquiredKnowledges service POST:
                    repoAcquiredKnowledges.add(json).then(function (response) {
                        //success
                        console.log("Response from server api" + response);
                        newAcquiredKnowledges = {};
                        //file = {};
                        window.location.templateUrl = '#/acquiredKnowledges';
                    }, function () {
                        //failure
                        holder.errorMessage = "Failure to save new acquiredknowledge";
                    })
                        .finally(function () {
                            holder.isBusy = false;
                        });
                        
                };
            }
            
        });
      
})();