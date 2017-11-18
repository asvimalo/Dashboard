(function () {
    "use strict";
    angular.module("taskList")
        .component("taskList", {
            templateUrl: "/Dashboard/app/components/task/task-list/task-list.template.html",
            controller: function TaskListController(repoTasks) {
                var self = this;
                self.isBusy = true;
                //self.orderProp = 'projectName';
                self.tasks = [];
                repoTasks.getAll().then(function (data) {
                    angular.copy(data, self.tasks);
                }, function () {
                    console.log("failure");
                    self.errorMessage = "Failure getting tasks";

                }).finally(function () {
                    console.log("finally");
                    self.isBusy = false;
                    //window.location.reload();
                });
            }
            
        });
      
})();