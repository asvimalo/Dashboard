(function () {
    "use strict";
    angular.module("knowledgeAdd")
        .component("knowledgeAdd", {
            templateUrl: "/Dashboard/app/components/knowledge/knowledge-add/knowledge-add.template.html",
            controller: function KnowledgeListController(
                $scope,
                $location,
                repoKnowledges
                
                ) {

                var holder = this;

                $scope.addKnowledge = function () {
                    var newKnowledge = {};
                    var knowledge = $scope.knowledge;


                    newKnowledge.knowledgeName = knowledge.knowledgeName;
                    newKnowledge.description = knowledge.description;
                    newKnowledge.acquiredKnowledges = [];


                    console.log("inside employees Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";


                    var json = JSON.stringify(newKnowledge);


                    repoKnowledges.add(json).then(function (data) {
                            //success
                            console.log("Response from server api" + data);
                            newKnowledge = {};
                            //file = {};
                            window.location.templateUrl = '#/knowledges';
                        }, function () {
                            //failure
                            holder.errorMessage = "Failure to save new knowledges";
                        }).finally(function () {
                            holder.isBusy = false;
                        });
                        
                        

                };


            }

        });

})();