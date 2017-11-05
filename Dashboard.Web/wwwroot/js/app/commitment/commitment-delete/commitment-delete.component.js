(function () {
    "use strict";
    angular.module("commitmentDelete")
        .component("commitmentDelete", {
            templateUrl: "/js/app/commitment/commitment-delete/commitment-delete.template.html",
            controller: function CommitmentListController($scope,$http,$location,$routeParams) {

                var holder = this;
                
                $scope.deleteCommitment = function () {
                   
                    console.log("inside detele Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";

                    $http.delete('http://localhost:8899/api/dashboard/commitments/', $routeParams.commitment.assignmentId)
                        .then(function (response) {
                            //success
                            console.log("Response from server api" + response.data);
                            
                            window.location.templateUrl = '#/commitments';
                        }, function () {
                            //failure
                            holder.errorMessage = "Delete to save new commitment";
                        })
                        .finally(function () {
                            holder.isBusy = false;
                        });

                };

                
            }
            
        });
      
})();