angular.module('modal', ['ngAnimate', 'ui.bootstrap']);

angular.module('modal')
  .component('projectAddModal', {
    template: 'I am content! <button type="button" class="btn btn-default" ng-click="$ctrl.open()">Open Modal</button>',
    controller: function($uibModal) {
      $ctrl = this;
      $ctrl.dataForModal = {
        name: 'NameToEdit',
        value: 'ValueToEdit'
      }
      $ctrl.open = function() {
        $uibModal.open({
          component: "myModal",
          resolve: {
            modalData: function() {
              return $ctrl.dataForModal;
            }
          }
        }).result.then(function(result) {
          console.info("I was closed, so do what I need to do myContent's controller now.  Result was->");
          console.info(result);
        }, function(reason) {
          console.info("I was dimissed, so do what I need to do myContent's controller now.  Reason was->" + reason);
        });
      };
    }
  });

angular.module('modal')
  .component('myModal', {
    template: `<div class="modal-body"><div>{{$ctrl.greeting}}</div> 
    <label>Name To Edit</label> <input ng-model="$ctrl.modalData.name"><br>
    <label>Value To Edit</label> <input ng-model="$ctrl.modalData.value"><br>
    <button class="btn btn-warning" type="button" ng-click="$ctrl.handleClose()">Close Modal</button>
    <button class="btn btn-warning" type="button" ng-click="$ctrl.handleDismiss()">Dimiss Modal</button>
    </div>`,
    bindings: {
      modalInstance: "<",
      resolve: "<"
    },
    controller: [function() {
      var $ctrl = this;

      $ctrl.$init = function() {
        $ctrl.modalData = $ctrl.resolve.modalData;
      }

      $ctrl.handleClose = function() {
        console.info("in handle close");
        $ctrl.modalInstance.close($ctrl.modalData);
      };

      $ctrl.handleDismiss = function() {
        console.info("in handle dismiss");
        $ctrl.modalInstance.dismiss("cancel");
      };
    }]
  });