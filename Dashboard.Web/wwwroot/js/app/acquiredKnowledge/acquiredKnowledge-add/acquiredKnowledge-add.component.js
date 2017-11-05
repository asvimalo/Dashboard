(function () {
    "use strict";
    angular.module("acquiredKnowledgeAdd")
        .component("acquiredKnowledgeAdd", {
            templateUrl: "/js/app/acquiredKnowledge/acquiredKnowledge-add/acquiredKnowledge-add.template.html",
            controller: function AcquiredKnowledgeController($scope, $http, $location) {
                var holder = this;

                holder.acquiredKnowledges = [];
                $http.get('http://localhost:8890/api/dashboard/acquiredKnowledges').then(function (response) {
                    angular.copy(response.data, holder.acquiredKnowledges);
                });

                holder.knowledges = [];
                $http.get('http://localhost:8890/api/dashboard/knowledges').then(function (response) {
                    angular.copy(response.data, holder.knowledges);
                });

                holder.employees = [];
                $http.get('http://localhost:8890/api/dashboard/employees').then(function (response) {
                    angular.copy(response.data, holder.employees);
                });

                $scope.addAcquiredKnowledges = function () {
                    var newAcquiredKnowledges = {};
                    var acquiredKnowledges = $scope.acquiredKnowledges;
                     

                    newAcquiredKnowledges.knowledges = [];
                    newAcquiredKnowledges.employees = [];


                    console.log("inside acquired knowledges Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";


                    var json = JSON.stringify(newAcquiredKnowledges);


                    $http.post('http://localhost:8890/api/dashboard/acquiredKnowledges/', json)
                        .then(function (response) {
                            //success
                            console.log("Response from server api" + response.data);
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