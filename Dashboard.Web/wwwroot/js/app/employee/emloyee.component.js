(function () {
    "use strict";
    angular.module("employee")
        .component("employee", {
            templateUrl: "/js/app/employee/employee-add/employee-add.template.html",
            controller: function EmployeeListController($scope,$http,$location) {

                var holder = this;
                
                $scope.addEmployee = function () {
                    var newEmployee = {};
                    var employee = $scope.employee;
                    
                    
                    //var fi = this.fileInput.nativeElement;
                    //var file = $scope.file;
                    //var form = new FormData();
                    //form.append("file", file);
                    //newEmployee.file = form;

                    newEmployee.firstName = employee.firstName;
                    newEmployee.lastName = employee.lastName;
                    newEmployee.personNr = employee.personNr;
                    newEmployee.assignments = [];
                    newEmployee.acquiredKnowledges = []; 
                    

                    console.log("inside employees Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";

                    
                    var json = JSON.stringify(newEmployee);
                    

                    $http.post('http://localhost:8899/api/dashboard/employees/', json)
                        .then(function (response) {
                            //success
                            console.log("Response from server api" + response.data);
                            newEmployee = {};
                            //file = {};
                            window.location.templateUrl  = '#/employees';
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