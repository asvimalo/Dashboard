(function () {
    "use strict";
    angular.module('ui.bootstrap.demo', ['ngAnimate', 'ngSanitize', 'ui.bootstrap']);

       angular.module("projectAdd")
        .component("projectAdd", {
            templateUrl: "/js/app/project/project-add/project-add.template.html",
            controller: function ProjectAddController($http,$routeProvider) {
                var self = this;

                //var self.project = {};
                self.animationsEnabled = true;

                // Putting all my Clients in this array.
                self.clients = [];
                // Getting my Clients
                $http.get('http://localhost:8899/api/dashboard/clients').then(function (response) {
                    angular.copy(response.data, self.clients);
                });

                self.addProject = function () {
                    self.isBusy = true;
                    self.errorMessage = "";

                    $http.post("http://localhost:8899/api/dashboard/projects", $routeProvider.project)
                        .then(function (response) {
                            //success
                            self.projects.push(response.data);
                            self.newProject = {};

                        }, function () {
                            //failure
                            self.errorMessage = "Failure to save new project";
                        })
                        .finally(function () {
                            self.isBusy = false;
                        });

                };


                self.open = function (size, parentSelector) {
                    var parentElem = parentSelector ?
                        angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
                    var modalInstance = $uibModal.open({
                        animation: $ctrl.animationsEnabled,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'myModalContent.html',
                        controller: 'ModalInstanceCtrl',
                        controllerAs: '$ctrl',
                        size: size,
                        appendTo: parentElem,
                        resolve: {
                            items: function () {
                                return $ctrl.items;
                            }
                        }
                    });
            }
            
        });
      
})();