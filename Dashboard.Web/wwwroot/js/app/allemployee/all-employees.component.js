(function () {
    "use strict";
    angular.module("allEmployees", [])
        .component("allEmployees", {
            templateUrl: "/js/app/allemployee/all-employees.template.html",
            controller: function allEmployeesController($http, $scope, $location, $q, repoEmployees, repoAssignments) {
                var holder = this;

                //All Employee
                holder.allEmployees = [];
                holder.assignments = [];

                $q.all([
                    repoEmployees.getAll(),
                    repoAssignments.getAll()
                ]).then(function (response) {
                    //success
                    angular.copy(response[0], holder.allEmployees);
                    angular.copy(response[1], holder.assignments);

                    initWeekEmp();

                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
                    .finally(function () {
                        holder.isBusy = false;
                    });

                $scope.emWeekButtonClick = function () {
                    initWeekEmp();
                };

                $scope.emMonthButtonClick = function () {
                    initMonthEmp();
                };

                function initMonthEmp() {

                }

                var startdate = moment();
                //var visibleEmployeeProjectNames = [];

                /*****************************************************************************************************************************************************************
                * * * * * * * *     WEEK    * * * * * * * *   EMPLOYEEE   * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                *****************************************************************************************************************************************************************/
                function initWeekEmp() {
                    var tmp = holder.allEmployees;
                    var tmpp = holder.assignments;
                    console.log("initWeekEmp");
                    var dates = [];
                    var formatedDates = [];
                    var currdate = startdate.clone();
                    var enddate = 10;
                    for (var i = 0; i < enddate; i++) {
                        dates.push(currdate.format('YYYY-MM-DD'));
                        var plusOneDayStr = currdate.format('MM/DD ddd');
                        formatedDates.push(plusOneDayStr);
                        currdate = currdate.add(1, 'days');
                    }
                    var tabledate = $("<table></table>").addClass("table borderless");
                    var rowdate = $(tabledate[0].insertRow(-1));

                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-fast-backward").click(function () {
                        dates = [];
                        formatedDates = [];
                        startdate = startdate.add(-7, 'days');
                        initWeek();
                    }).appendTo(rowdate);
                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-chevron-left").click(function () {
                        dates = [];
                        formatedDates = [];
                        startdate = startdate.add(-1, 'days');
                        initWeek();
                    }).appendTo(rowdate);
                    rowdate.append(createCell("", "", ""));
                    for (var i = 0; i < formatedDates.length; i++) {
                        rowdate.append(createCell("cellDate", formatedDates[i].toString() + " |  "));
                    }
                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-chevron-right").click(function () {
                        dates = [];
                        formatedDates = [];
                        startdate = startdate.add(1, 'days');
                        initWeek();
                    }).appendTo(rowdate);
                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-fast-forward").click(function () {
                        dates = [];
                        formatedDates = [];
                        startdate = startdate.add(7, 'days');
                        initWeek();
                    }).appendTo(rowdate);

                    //**********   EMPLOYEE   ***********
                    addEmptyRowToHtml(tabledate, dates);

                    var nbrOfEmployees = holder.allEmployees.length;
                    var rowCount = 2;
                    for (var i = 0; i < nbrOfEmployees; i++) {
                        var employee = holder.allEmployees[i];
                        var assignmentCount = 0;

                        var sumCommit = Array.apply(null, Array(dates.length)).map(Number.prototype.valueOf, 0);

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
                                row.addClass("project");
                                row.attr('projectId', project.projectName); //TODO: change, ProjectId is a very confusing name! 
                                row.append(createCell("projectName", project.projectName + "  ", "colspan=\"2\" style=\"white-space:PRE\"></td>"));

                                var projectStartString = moment(project.startDate).format('YYYY-MM-DD');
                                var projectEndString = moment(project.stopDate).format('YYYY-MM-DD');
                                var startDateString = moment(startdate).format('YYYY-MM-DD');

                                for (var j = 0; j < dates.length; j++) {               //for calendar
                                    var data = "";
                                    for (var m = 0; m < commitments.length; m++) {     //for commitments
                                        var commit = commitments[m];
                                        var commitStart = moment(commit.startDate).format('YYYY-MM-DD');
                                        var commitEnd = moment(commit.stopDate).format('YYYY-MM-DD');

                                        if (dates[j] >= commitStart && dates[j] <= commitEnd) {  //TODO: Why not?
                                            sumCommit[j] += commit.hours;
                                        }

                                        if (commitStart == dates[j]) {
                                            data = commit.hours + "%";
                                        }
                                        else if (startDateString == dates[j] && commitStart < dates[j] && commitEnd > dates[j]) {  //TODO: Why not?
                                            data = commit.hours + "%";
                                        }
                                        else if (commitEnd == dates[j]) {
                                            data = 'CE';
                                        }
                                        else if ((commitStart < dates[j]) && (commitEnd > dates[j])) {
                                            data = 'xC';
                                        }
                                    } //for-commitments

                                    //Write employee cell to html
                                    var cell = createCell("datacell");
                                    var celldiv = $("<div></div>");
                                    if (!isEmpty(data)) {
                                        addCssClass(data, celldiv);
                                        celldiv.html(data);
                                    }
                                    row.append(cell.append(celldiv));
                                } //for-calendar
                            } //for- holder.empAssigments
                        } //if- employee.assigments

                        var row = $(tabledate[0].insertRow(rowCount - assignmentCount));
                        rowCount++;
                        row.addClass("employee");
                        row.attr("employeeId", employee.employeeId);
                        row.append(createCell("employeeCss", "  " + employee.firstName + " " + employee.lastName, "style=\"white-space:PRE\""));
                        row.append(createCell("projectArrowButton"));
                        for (var j = 0; j < dates.length; j++) {
                            row.append(createCell("", "" + sumCommit[j] ));
                        }

                    } // for-loop for Employee

                    //**********   Data to HTML table   ***********
                    var dateTable = $("#ganttchartEmp");
                    dateTable.html("");
                    dateTable.append(tabledate);

                    //dateTable.find(".project").hide();

                    //Collapse
                    $('<button></button>').attr({ 'type': 'button' }).addClass("glyphicon glyphicon-triangle-bottom").click(function () {
                        var employeeRow = $(this).parent().parent();
                        var employeeId = employeeRow.attr("employeeId");

                        //var index = visibleEmployeeProjectNames.indexOf(projectId);
                        //if (index > -1) {
                        //    visibleEmployeeProjectNames.splice(index, 1);
                        //} else {
                        //    visibleEmployeeProjectNames.push(projectId);
                        //}

                        employeeRow.siblings(".project").each(function () {
                            var employeeProjectId = $(this).attr('projectId');
                            if (employeeProjectId == employeeId) {
                                $(this).fadeToggle(300);
                            }
                        });
                    }).appendTo($('td.projectArrowButton'));

                } //function initWeekEmp()

                //*********  Helper class

                function createCell(baseclass, text = "", td = "") {
                    var cell = $("<td " + td + "></td>");
                    if (!isEmpty(baseclass))
                        cell.addClass(baseclass);
                    if (!isEmpty(text))
                        cell.html(text);
                    return cell;
                }

                function addEmptyRowToHtml(table, dates) {
                    var row1 = $(table[0].insertRow(-1));
                    row1.addClass("emptyRow");
                    var cell2 = $("<td colspan=\"" + (dates.length + 2) + "\"></td>");
                    row1.append(cell2);
                }

                function isEmpty(val) {
                    return (val === undefined || val == null || val.length <= 0 || val === " ") ? true : false;
                }

                function addCssClass(content, div) {
                    switch (content) {
                        case "S":
                            div.addClass("projectstartdiv");
                            break;
                        case "E":
                            div.addClass("projectenddiv");
                            break;
                        case "PS":
                            div.addClass("phasestartdiv");
                            break;
                        case "PE":
                            div.addClass("phaseenddiv");
                            break;
                        case "x":
                            div.addClass("celldiv");
                            break;
                        case "CE":
                            div.addClass("comenddiv");
                            break;
                        case "xC":
                            div.addClass("comdiv");
                            break;

                        default:
                            div.addClass("comstartdiv");
                    }
                }

            } //Controller end
        });
})();         