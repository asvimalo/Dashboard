(function () {
    "use strict";
    angular.module("knowledgeAdd")
        .component("knowledgeAdd", {
            templateUrl: "/js/app/knowledge/knowledge-add/knowledge-add.template.html",
            controller: function KnowledgeListController($scope, $http, $location) {

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


                    $http.post('http://localhost:8890/api/dashboard/knowledges/', json)
                        .then(function (response) {
                            //success
                            console.log("Response from server api" + response.data);
                            newKnowledge = {};
                            //file = {};
                            window.location.templateUrl = '#/knowledges';
                        }, function () {
                            //failure
                            holder.errorMessage = "Failure to save new knowledges";
                        })
                        .finally(function () {
                            holder.isBusy = false;
                        });

                };


            }

        });

})();