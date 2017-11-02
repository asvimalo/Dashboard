(function () {
    "use strict";
    angular.module("taskList")
        .component("taskList", {
            templateUrl: "/js/app/task/task-list/task-list.template.html",
            controller: function TaskListController($http) {
                var self = this;
                //self.orderProp = 'projectName';
                self.tasks = [];
                $http.get('http://localhost:8899/api/dashboard/tasks').then(function (response) {
                    angular.copy(response.data, self.tasks);
                });
            }
            
        });
      
})();