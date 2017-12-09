(function () {
    "use strict";
    angular.module("phaseEdit")
        .component("phaseEdit", {
            templateUrl: "/js/app/phase/phase-edit/phase-edit.template.html",
            controller: function PhaseListController($http, $scope, repoPhases, $location, $routeParams)
            {
                var self = this;
                self.phase = {};

                if ($routeParams.phaseId == null) {
                    console.log("The phaseId is NULL")
                } else {
                    this.phaseId = $routeParams.phaseId;

                    repoPhases.get(self.phaseId).then(function (response) {
                        angular.copy(response, self.phase)
                        self.phase.startDate = new Date(self.phase.startDate).toLocaleDateString();
                        self.phase.endDate = new Date(self.phase.endDate).toLocaleDateString();
                    });

                }

                $scope.alert = false;
                $scope.validateEndDate = function (startDate, endDate) {
                    if (new Date(endDate) < new Date(startDate)) {
                        $scope.errorMessage = "To:date should be greater than start date.";
                        $scope.alert = true;
                        return true;
                    }
                    else {
                        $scope.alert = false;
                        return false;
                    }
                };

                $scope.alertProgress = false;
                $scope.validateProgress = function (progress) {
                    if (progress > 100) {

                        $scope.alertProgress = true;
                        $scope.errorMessage = "Progress can't be bigger than 100"

                    } else if (progress < 0) {

                        $scope.alertProgress = true;
                       $scope.errorMessage = "Progress can't be less than 0"

                    } else {

                        $scope.alertProgress = false;
                        return false;
                    }
                };

                $scope.editPhase = function () {
                    var data = {
                        "timeBudget": self.phase.timeBudget,
                        "progress": self.phase.progress,
                        "comments": self.phase.comments,
                        "startDate": new Date(self.phase.startDate).toLocaleDateString(),
                        "endDate": new Date(self.phase.endDate).toLocaleDateString()
                    };
                    var dataTmp = JSON.stringify(data);

                    repoPhases.update(self.phase.phaseId, dataTmp).then(function (response) {

                        location.replace("#!/projects/project-details/" + response.projectId);
                        location.reload();
                    });

                    
                };

                $scope.closeModal = function () {
                    location.replace("#!/projects/project-details/" + self.phase.projectId);
                    location.reload();
                };
            }
            
        });
      
})();