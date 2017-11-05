(function () {
    "use strict";
    angular.module("commitmentAdd")
        .component("commitmentAdd", {
            templateUrl: "/js/app/commitment/commitment-add/commitment-add.template.html",
            controller: function CommitmentListController($scope,$http,$location) {

                var holder = this;
                
                $scope.addCommitment = function () {
                    var newCommitment = {};
                    var commitment = $scope.commitment;
                      
                    newCommitment.assignment = commitment.assignment;
                    newCommitment.startDate = commitment.startDate;
                    newCommitment.stopDate = commitment.stopDate;
                    newCommitment.hours = commitment.hours; 
                     
                    console.log("inside commitment Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";

                    
                    var json = JSON.stringify(newCommitment);
                    

                    $http.post('http://localhost:8890/api/dashboard/commitments/', json)
                        .then(function (response) {
                            //success
                            console.log("Response from server api" + response.data);
                            newCommitment = {};
                            //file = {};
                            window.location.templateUrl = '#/commitments';
                        }, function () {
                            //failure
                            holder.errorMessage = "Failure to save new employee";
                        })
                        .finally(function () {
                            holder.isBusy = false;
                        });

                };

                
            }
            
        });
      
})();