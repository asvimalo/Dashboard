(function () {
    "use strict";
    angular.module("commitmentAdd")
        .component("commitmentAdd", {
            templateUrl: "/js/app2/components/commitment/commitment-add/commitment-add.template.html",
            controller: function CommitmentListController(
                $scope,
                $location,
                repoCommmitments
                ) {

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
                    

                    repoCommmitments.add(json)
                        .then(function (response) {
                            //success
                            console.log("Response from server api" + response);
                            newCommitment = {};
                            //file = {};
                            window.location.templateUrl = '#/commitments';
                        }, function () {
                            //failure
                            holder.errorMessage = "Failure to save new commitment";
                        })
                        .finally(function () {
                            holder.isBusy = false;
                        });

                };

                
            }
            
        });
      
})();