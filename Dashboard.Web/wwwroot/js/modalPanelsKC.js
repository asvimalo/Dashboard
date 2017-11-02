angular.module('ui.bootstrap.demo', ['ngAnimate', 'ngSanitize', 'ui.bootstrap']);
angular.module('ui.bootstrap.demo').controller('ProjectAddController', function ($uibModal, $log, $document) {
    var self = this;
    self.items = ['item1', 'item2', 'item3'];

    self.animationsEnabled = true;

    self.open = function (size, parentSelector) {
        var parentElem = parentSelector ?
            angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
        var modalInstance = $uibModal.open({
            animation: self.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'project-add.html',
            controller: 'ModalInstanceCtrl',
            controllerAs: 'self',
            size: size,
            appendTo: parentElem,
            resolve: {
                items: function () {
                    return self.items;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            self.selected = selectedItem;
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    self.openComponentModal = function () {
        var modalInstance = $uibModal.open({
            animation: self.animationsEnabled,
            component: 'modalComponent',
            resolve: {
                items: function () {
                    return self.items;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            self.selected = selectedItem;
        }, function () {
            $log.info('modal-component dismissed at: ' + new Date());
        });
    };

    self.openMultipleModals = function () {
        $uibModal.open({
            animation: self.animationsEnabled,
            ariaLabelledBy: 'modal-title-bottom',
            ariaDescribedBy: 'modal-body-bottom',
            templateUrl: 'stackedModal.html',
            size: 'sm',
            controller: function ($scope) {
                $scope.name = 'bottom';
            }
        });

        $uibModal.open({
            animation: self.animationsEnabled,
            ariaLabelledBy: 'modal-title-top',
            ariaDescribedBy: 'modal-body-top',
            templateUrl: 'stackedModal.html',
            size: 'sm',
            controller: function ($scope) {
                $scope.name = 'top';
            }
        });
    };

    self.toggleAnimation = function () {
        self.animationsEnabled = !self.animationsEnabled;
    };
});

// Please note that $uibModalInstance represents a modal window (instance) dependency.
// It is not the same as the $uibModal service used above.

angular.module('ui.bootstrap.demo').controller('ModalInstanceCtrl', function ($uibModalInstance, items) {
    var self = this;
    self.items = items;
    self.selected = {
        item: self.items[0]
    };

    self.ok = function () {
        $uibModalInstance.close(self.selected.item);
    };

    self.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});

// Please note that the close and dismiss bindings are from $uibModalInstance.

angular.module('ui.bootstrap.demo').component('modalComponent', {
    templateUrl: 'project-add.html',
    bindings: {
        resolve: '<',
        close: '&',
        dismiss: '&'
    },
    controller: function () {
        var self = this;

        self.$onInit = function () {
            self.items = self.resolve.items;
            self.selected = {
                item: self.items[0]
            };
        };

        self.ok = function () {
            self.close({ $value: self.selected.item });
        };

        self.cancel = function () {
            self.dismiss({ $value: 'cancel' });
        };
    }
});