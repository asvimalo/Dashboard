angular.module('modal', ['ui.bootstrap']);
angular.module('modal').controller('ModalDemoCtrl', function ($http, $scope, $modal, $log) {

    //$scope.items = ['item1', 'item2', 'item3'];

    //var holder = this;
    $scope.employees = [];
    $http.get("http://localhost:8899/api/dashboard/employees")
        .then(function (response) {
            //success
            angular.copy(response.data, $scope.employees);
        }, function (error) {
            //failure
            $scope.errorMessage = "Failed to load data: " + error;
        })
        .finally(function () {
            $scope.isBusy = false;
        });
    

    $scope.open = function (size) {

        var modalInstance = $modal.open({
            templateUrl: 'testpage.html',
            controller:'ModalInstanceCtrl',
            size: size,
            resolve: {
                items: function () {
                    //return $scope.items;
                    return $scope.employees;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };
});

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.

angular.module('modal').controller('ModalInstanceCtrl', function ($scope, $modalInstance, items) {

    $scope.employees = items;
    $scope.selected = {
        item: $scope.employees[0]
    };

    $scope.ok = function () {
        $modalInstance.close($scope.selected.item);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});