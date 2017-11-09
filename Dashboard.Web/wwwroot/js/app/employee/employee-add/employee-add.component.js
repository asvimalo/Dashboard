(function () {
    "use strict";
    angular.module("employeeAdd")
        .component("employeeAdd", {
            templateUrl: "/js/app/employee/employee-add/employee-add.template.html",
            controller: function EmployeeListController($scope,$http,$location) {
                
                var holder = this;

                holder.employees = [];
                holder.knowledges = [];
                holder.newKnowledges = [];

                $http.get('http://localhost:8890/api/dashboard/knowledges').then(function (response) {
                    angular.copy(response.data, holder.knowledges);
                });

                $scope.addKnowledges = function () {
                    holder.newKnowledges.push(holder.knowledges.knowledgeName);
                };

                $scope.remove = function (array, index) {
                    array.splice(index, 1);
                    var y = holder.knowledges;
                };

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
                    newEmployee.newKnowledges = holder.newKnowledges;
                    newEmployee.knowledges = employee.knowledges;
                    
                    

                    console.log("inside employees Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";

                    
                    var json = JSON.stringify(newEmployee);
                    

                    $http.post('http://localhost:8890/api/dashboard/employees', json)
                        .then(function (response) {
                            //success
                            console.log("Response from server api" + response.data);
                            employee = {};
                            ////file = {};
                            //window.location.reload();
                        }, function (err) {
                            //failure
                            // review Error response is coming back....
                            holder.errorMessage = "Failure to save new employee";
                            alert("Failure to save new employee" + err)
                        })
                        .finally(function () {
                            holder.isBusy = false;
                        });

                };

                
            }
            
        });
      
})();