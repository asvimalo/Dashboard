(function () {
    "use strict";
    angular.module("employeeAdd")
        .component("employeeAdd", {
            templateUrl: "/js/app2/components/employee/employee-add/employee-add.template.html",
            controller: function EmployeeListController(
                $scope,
                $location,
                repoEmployees,
                repoKnowledges
                ) {
                
                var holder = this;
                holder.isBusy = true;
                holder.employees = [];
                holder.knowledges = [];
                holder.newKnowledges = [];

                repoKnowledges.getAll().then(function (response) {
                        angular.copy(response, holder.knowledges);

                    }, function(err) {
                        holder.errorMessage = "Failure to save new employee";
                    }).finally(function () {
                        holder.isBusy = false;
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
                    //holder.isBusy = true;

                    holder.progress = "";             
                    var employeeJson = JSON.stringify(newEmployee);
                    
                    repoEmployees.addEmployee(employeeJson).then(function (response) {
                            //success
                            console.log("Response from server api" + response);
                            employee = {};

                        //window.location.reload();
                        }, function (err) {
                        
                            holder.errorMessage = "Failure to save new employee";
                        
                        }).finally(function () {
                            holder.isBusy = false;
                        });
                       
                        

                };

                
            }
            
        });
      
})();