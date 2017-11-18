(function () {
    "use strict";
    angular.module("employeeUpdate")
        .component("employeeUpdate", {
            templateUrl: "/Dashboard/app/components/employee/employee-update/employee-update.template.html",
            controller: function EmployeeListController(
                $scope,
                $location,
                employeeUpdate
                ) {

                var holder = this;
                
                $scope.updateEmployee = function () {
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
                    

                    repoEmployees(json).then(function (data) {
                            //success
                            console.log("Response from server api" + data);
                            newEmployee = {};
                            //file = {};
                            window.location.templateUrl = '#/employees';
                        }, function () {
                            //failure
                            holder.errorMessage = "Failure to save new employee";

                        }).finally(function () {
                            holder.isBusy = false;
                        });
                        
                        

                };

                
            }
            
        });
      
})();