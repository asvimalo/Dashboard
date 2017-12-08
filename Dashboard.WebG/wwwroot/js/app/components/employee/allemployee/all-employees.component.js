(function () {
    "use strict";
    angular.module("allEmployees", [])
        .component("allEmployees", {
            templateUrl: "/js/app/components/employee/allemployee/all-employees.template.html",
            styleUrls: ["/css/allEmployee.css"],
            controller: function allEmployeesController($http, $scope, $location, $q, repoEmployees, repoAssignments) {
                var holder = this;

                //All Employee
                holder.allEmployees = [];
                holder.assignments = [];

                var currentDate = moment();

                $q.all([
                    repoEmployees.getAll(),
                    repoAssignments.getAll()
                ]).then(function (response) {
                    //success
                    angular.copy(response[0], holder.allEmployees);
                    angular.copy(response[1], holder.assignments);

                    initEmployeeGantt('day', currentDate);
                    $("#weekButton").addClass('active');

                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
                    .finally(function () {
                        holder.isBusy = false;
                    });

                var weekButton = $("#weekButton");
                var monthButton = $("#monthButton");

                $scope.weekButtonClick = function () {
                    if (!weekButton.hasClass('active')) weekButton.addClass('active');
                    if (monthButton.hasClass('active')) monthButton.removeClass('active');
                    initEmployeeGantt('day', currentDate);
                };

                $scope.monthButtonClick = function () {
                    if (weekButton.hasClass('active')) weekButton.removeClass('active');
                    if (!monthButton.hasClass('active')) monthButton.addClass('active');
                    initEmployeeGantt('week', currentDate);
                };

                var visibleProjectNames = [];

                function initEmployeeGantt(timeUnit, startDate) {
                    if (timeUnit === 'day')
                        employeeGantt('day', 7, true, startDate);
                    else if (timeUnit === 'week')
                        employeeGantt('week', 5, true, startDate);
                    else // month
                        employeeGantt('month', 3, false, startDate);
                }

                /*****************************************************************************************************************************************************************
                 * * * * * * * *     GANTT    * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 *****************************************************************************************************************************************************************/
                function employeeGantt(timeUnit, leap, checkAllEvents, startDate) {
                    var dates = createCalendarDates(timeUnit, startDate, 10);
                    var columnCount = dates.length + 2;

                    var tabledate = $("<table></table>").addClass("table borderless");
                    createCalendarHeader(tabledate, startDate, dates, leap, timeUnit, initEmployeeGantt);

                    //**********   EMPLOYEE   ***********
                    addEmptyRowToHtml(tabledate, columnCount);

                    var nbrOfEmployees = holder.allEmployees.length;
                    var rowCount = 2; // Header + empty
                    for (var i = 0; i < nbrOfEmployees; i++) {
                        var employee = holder.allEmployees[i];
                        var assignmentCount = 0;

                        var sumCommit = Array.apply(null, Array(dates.length)).map(Number.prototype.valueOf, 0); // Init to zero array

                        if (employee.assignments) {

                            for (var k = 0; k < holder.assignments.length; k++) {
                                var assignment = holder.assignments[k];
                                if (employee.employeeId != assignment.employeeId) {
                                    continue;
                                }
                                assignmentCount++;
                                var project = assignment.project;
                                var commitments = assignment.commitments;
                                var row = $(tabledate[0].insertRow(-1));
                                rowCount++;
                                row.addClass("projectEmp");
                                row.attr("employeeId", employee.employeeId);
                                var projectCell = createCell("projectNameEmp", project.projectName + "  ", "colspan=\"2\" style=\"white-space:PRE\"></td>");
                                projectCell.attr("projectId", project.projectId); //TODO: behövs eller omnämna till proj?
                                row.append(projectCell);
                                //row.append(createCell("projectName", project.projectName + "  ", "colspan=\"2\" style=\"white-space:PRE\"></td>"));

                                var projectStart = moment(project.startDate);
                                var projectEnd = moment(project.stopDate);
                                for (var j = 0; j < dates.length; j++) {               //for calendar
                                    var style = '';
                                    var text = "";
                                    var prevHours = -1;

                                    for (var m = 0; m < commitments.length; m++) {     //for commitments
                                        var commit = commitments[m];
                                        var commitStart = moment(commit.startDate);
                                        var commitEnd = moment(commit.stopDate);

                                        if (!dates[j].isBefore(commitStart, timeUnit) && !dates[j].isAfter(commitEnd, timeUnit))
                                            sumCommit[j] += commit.hours;
                                        
                                        if ((startDate.isSame(dates[j], timeUnit) && !startDate.isBefore(commitStart) && !startDate.isAfter(commitEnd)) || commitStart.isSame(dates[j], timeUnit)) {
                                            text = "";
                                            if (prevHours != commit.hours) {
                                                text = "" + commit.hours;
                                                prevHours = commit.hours;
                                            }
                                            text = commit.hours + "%";
                                        }
                                        if (commitStart.isSame(dates[j], timeUnit))
                                            style = 'comstartdiv';
                                        else if (commitEnd.isSame(dates[j], timeUnit))
                                            style = 'comenddiv';
                                        else if (commitStart.isBefore(dates[j], timeUnit) && commitEnd.isAfter(dates[j], timeUnit))
                                            style = 'comdiv';
                                    } //for-commitments

                                    //Write employee cell to html
                                    var cell = createCell("datacell");
                                    cell.attr("data-trigger", "hover"); 
                                    cell.attr("proj", project.projectId); 
                                    var celldiv = $("<div></div>");
                                    if (!isEmpty(style))
                                        celldiv.addClass(style);
                                    if (!isEmpty(text))
                                        celldiv.html(text);
                                    row.append(cell.append(celldiv));
                                } //for-calendar
                            } //for- holder.empAssigments
                        } //if- employee.assigments

                        var row = $(tabledate[0].insertRow(rowCount - assignmentCount));
                        rowCount++;
                        row.addClass("employeeHeader");
                        row.attr("employeeId", employee.employeeId);
                        row.append(createCell("employeeCss", "  " + employee.firstName + " " + employee.lastName, "style=\"white-space:PRE\""));
                        row.append(createCell("employeeArrowButton"));

                        var prevSum = -1;
                        for (var j = 0; j < dates.length; j++) {
                            var cellText = "";
                            if (timeUnit === 'day' && prevSum != sumCommit[j]) {
                                if (sumCommit[j] != 0)
                                    cellText = "" + sumCommit[j] + "%";
                                prevSum = sumCommit[j];
                            }
                            var style = '';
                            if (timeUnit === 'day' && sumCommit[j] > 100)
                                style = 'overtime';
                            else if (sumCommit[j] != 0)
                                style = 'celldiv';
                            var cell = createCell("", "");
                            var celldiv = $("<div></div>");
                            if (!isEmpty(style))
                                celldiv.addClass(style);
                            if (!isEmpty(cellText))
                                celldiv.html(cellText);
                            row.append(cell.append(celldiv));
                        }

                        addEmptyRowToHtml(tabledate, columnCount);
                        rowCount++;
                    } // for-loop for Employee

                    //**********   Data to HTML table   ***********
                    var dateTable = $("#ganttchart");
                    dateTable.html("");
                    dateTable.append(tabledate);

                    dateTable.find(".projectEmp").hide();
                    dateTable.find(".projectEmp").each(function () {
                        var employeeId = $(this).attr('employeeId');
                        var tmp = visibleProjectNames;
                        for (var i = 0; i < visibleProjectNames.length; i++) {
                            if (employeeId == visibleProjectNames[i]) {
                                $(this).show();
                            }
                        }
                    });

                    //Collapse
                    $('<button></button>').attr({ 'type': 'button' }).addClass("glyphicon glyphicon-triangle-bottom").click(function () {
                        var employeeRow = $(this).parent().parent();
                        var employeeId = employeeRow.attr("employeeId");

                        var index = visibleProjectNames.indexOf(employeeId);
                        if (index > -1) {
                            visibleProjectNames.splice(index, 1);
                        } else {
                            visibleProjectNames.push(employeeId);
                        }

                        employeeRow.siblings(".projectEmp").each(function () {
                            var employeeProjectId = $(this).attr('employeeId');
                            if (employeeProjectId == employeeId) {
                                $(this).fadeToggle(300);
                            }
                        });
                    }).appendTo($('td.employeeArrowButton'));

                    //popover
                    $('[data-trigger="hover"]').popover({
                        'trigger': 'hover',
                        'placement': 'bottom',
                        'container': 'body',
                        'data-html': 'true',
                        'html': 'true',
                        'content': function () {

                            var projectId = $(this).attr('proj');
                            if (projectId.length == 0)
                                return "";

                            for (var i = 0; i < holder.assignments.length; i++) {
                                if (holder.assignments[i].projectId != projectId)
                                    continue;
                                var projObj = holder.assignments[i].project;
                                var contentData =
                                    "" + projObj.projectName + " <br/>" +
                                    "From: " + moment(projObj.startDate).format('YYYY-MM-DD') + " <br/>" +
                                    "To: " + moment(projObj.stopDate).format('YYYY-MM-DD') + " <br/><br/>" +
                                    "Timebudget: " + projObj.timeBudget + "h<br/>";
                                return contentData;
                            } 
                            return "";
                        }
                    }); 

                    //Go to EmployeeDetails
                    $(".employeeCss").click(function () {
                        var employeeId = $(this).parent().attr('employeeId');
                        window.location.assign("http://localhost:8899/#!/employees/employee-details/" + employeeId);
                    }).eq(0);

                } //function initWeekEmp()

            } //Controller end
        });
})();         