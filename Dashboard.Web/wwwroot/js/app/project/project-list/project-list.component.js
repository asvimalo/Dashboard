(function () {
    "use strict";
    angular.module("projectList")
        .component("projectList", {
            templateUrl: "/js/app/project/project-list/project-list.template.html",
            controller: function ProjectListController($http) {
                var self = this;
                //self.orderProp = 'projectName';
                self.projects = [];
                $http.get('http://localhost:8899/api/dashboard/projects').then(function (response) {
                    angular.copy(response.data, self.projects);
                });
            }
            
        });
      
})();