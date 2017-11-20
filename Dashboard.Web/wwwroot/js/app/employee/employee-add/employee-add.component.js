(function () {
    "use strict";
    angular.module("employeeAdd")
        .component("employeeAdd", {
            templateUrl: "/js/app/employee/employee-add/employee-add.template.html",
            controller: function EmployeeListController($scope,
                $http,
                $location,
                repoEmployees,
                repoKnowledges
            ) {
                
                var holder = this;

                holder.employees = [];
                holder.knowledges = [];
                holder.newKnowledges = [];

                repoKnowledges.get().then(function (response) {
                    angular.copy(response, holder.knowledges);
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
                    
                    var file = new File([byteArrays], filename, { type: contentType, lastModified: Date.now() });


                    console.log("inside employees Controller ");

                    holder.errorMessage = "";
                    holder.isBusy = true;

                    holder.progress = "";

                    
                    var employeeJson = JSON.stringify(newEmployee);
                     
                    repoEmployees.add(employeeJson)
                        .then(function (response) {
                        //success
                        console.log("Response from server api" + response);
                        employee = {};

                        //window.location.reload();
                    }, function (err) {
                        //failure
                        // review Error response is coming back....
                        holder.errorMessage = "Failure to save new employee";
                        alert("Failure to save new employee" + err.stringify);
                    })
                        .finally(function () {
                            holder.isBusy = false;
                        });

                };

                
            }
            
        });
      
})();