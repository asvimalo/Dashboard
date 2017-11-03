(function () {
    "use strict";
    angular.module("employeeAdd")
        .component("employeeAdd", {
            templateUrl: "/js/app/employee/employee-add/employee-add.template.html",
            controller: function EmployeeListController($scope,$http,$location) {

                var holder = this;
                holder.pictureFile = null;

                //holder.submit = function () {
                //    if ($scope.form.file.$valid && $scope.file) {
                //        $scope.addEmployee(employee);
                //    }
                //};


                

                //holder.employees = [];

                holder.newEmployee = {};
                console.log("inside employees Controller ");
                holder.errorMessage = "";
                holder.isBusy = true;

                holder.progress = "";

                holder.addEmployee = function (employee, file) {
                    var fileReader = new FileReader();
                    fileReader.onload = function (file) {

                        // split so we only get the bytes (descriptive info before ',')
                        var imagestr = file.target.result.split(',')[1];

                        debugger;

                        var imagestr = event.target.result.split(',')[1];
                        holder.newEmployee.bytes = imagestr;
                        holder.newEmployee.firstName = employee.firstName;
                        holder.newEmployee.lastName = employee.lastName;
                        holder.newEmployee.personNr = employee.personNr;



                    }
                    //var newJsonEmployee = JSON.stringify(holder.newEmployee);

                    $http.post('http://localhost:8899/api/dashboard/employees/', holder.newEmployee)
                        .then(function (response) {
                            //success
                            console.log("Response from server api" + response.data);
                            window.location.href = '#/employees';
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