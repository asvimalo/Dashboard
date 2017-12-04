(function () {
    "use strict";
    angular.module("allProjects", [])
        .component("allProjects", {
            templateUrl: "/js/app/allproject/all-projects.template.html",
            controller: function allProjectsController($http, $scope, $location, $q, repoProjects, repoAssignments) {
                var holder = this;

                holder.allProjects = [];
                holder.assignments = [];

                $q.all([
                    repoProjects.getAll(),
                    repoAssignments.getAll()
                ]).then(function (response) {
                    //success
                    angular.copy(response[0], holder.allProjects);
                    angular.copy(response[1], holder.assignments);

                    initWeek();

                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
                    .finally(function () {
                        holder.isBusy = false;
                    });

                $scope.weekButtonClick = function () {
                    //if ($(this).hasClass('active')) {
                    //    $(this).removeClass('active')
                    //} else {
                    //    $(this).addClass('active')
                    //}
                    initWeek();
                };

                $scope.monthButtonClick = function () {
                    //if ($(this).hasClass('active')) {
                    //    $(this).removeClass('active')
                    //} else {
                    //    $(this).addClass('active')
                    //}
                    initMonth();
                };

                $scope.yearButtonClick = function () {
                    initYear();
                };

                var startdate = moment();
                var visibleEmployeeProjectNames = [];
                $(".ganttpanel").show();

                function initWeek() {
                    resetGantt('day', 7, true);
                }
                function initMonth() {
                    resetGantt('week', 5, true);
                }
                function initYear() {
                    resetGantt('month', 3, false);
                }

                /*****************************************************************************************************************************************************************
                 * * * * * * * *     GANTT    * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 *****************************************************************************************************************************************************************/
                function resetGantt(timeUnit, leap, checkAllEvents) {
                    //**********   CALENDAR   ***********
                    var dates = [];
                    var formatedDates = [];
                    var currdate = startdate.clone().startOf(timeUnit);
                    var dateCount = 10;
                    var columnCount = dateCount + 2;

                    for (var i = 0; i < dateCount; i++) {
                        dates.push(currdate.clone());
                        formatedDates.push(currdate.format('MM/DD ddd'));
                        currdate = currdate.add(1, timeUnit);
                    }
                    var tabledate = $("<table></table>").addClass("table borderless");
                    var rowdate = $(tabledate[0].insertRow(-1));

                    createCalendarButton(rowdate, "glyphicon glyphicon-fast-backward", timeUnit, -leap, leap, checkAllEvents);
                    createCalendarButton(rowdate, "glyphicon glyphicon-chevron-left", timeUnit, -1, leap, checkAllEvents);
                    rowdate.append(createCell("", "", "")); //colspan =\"2\" 
                    // Dates
                    for (var i = 0; i < formatedDates.length; i++) {
                        rowdate.append(createCell("cellDate", formatedDates[i].toString() + " |  "));
                    }
                    createCalendarButton(rowdate, "glyphicon glyphicon-chevron-right", timeUnit, 1, leap, checkAllEvents);
                    createCalendarButton(rowdate, "glyphicon glyphicon-fast-forward", timeUnit, leap, leap, checkAllEvents);

                    //**********   PROJECT   ***********
                    addEmptyRowToHtml(tabledate, dates.length);

                    var nbrOfProjects = holder.allProjects.length;
                    for (var i = 0; i < nbrOfProjects; i++) {

                        var project = holder.allProjects[i];
                        var row = $(tabledate[0].insertRow(-1));
                        row.addClass("project");
                        row.attr('projectId', project.projectName);
                        row.attr('projectNbr', project.projectId);

                        row.append(createCell("projectName", "  " + project.projectName, "style=\"white-space:PRE\""));
                        row.append(createCell("projectArrowButton"));

                        var projectStart = moment(project.startDate);
                        var projectEnd = moment(project.stopDate);
                        for (var j = 0; j < dates.length; j++) {                 //for calendar
                            var style = '';
                            var phaseIds = [];
                            var phaseData = [];

                            if (projectStart.isSame(dates[j], timeUnit)) {
                                style = 'projectstartdiv';
                            }
                            else if (projectEnd.isSame(dates[j], timeUnit)) {
                                style = 'projectenddiv';
                            } else {
                                if (projectStart.isBefore(dates[j], timeUnit) && projectEnd.isAfter(dates[j], timeUnit)) {
                                    style = 'celldiv'
                                }

                                if (checkAllEvents) {
                                    for (var k = 0; k < project.phases.length; k++) {      //for project's phases
                                        var phase = project.phases[k];
                                        var phaseStart = moment(phase.startDate);
                                        var phaseEnd = moment(phase.endDate);
                                        if (dates[j].isAfter(phaseEnd, timeUnit) || dates[j].isBefore(phaseStart, timeUnit))
                                            continue;

                                        phaseIds.push(k);

                                        if (phaseStart.isSame(dates[j], timeUnit)) {
                                            if (style == "phasestartdiv" || style == "phaseenddiv") {
                                                var dataObj = { "id": k, "data": "phasestartdiv" };
                                                phaseData.push(dataObj);
                                            }
                                            style = 'phasestartdiv';
                                        }
                                        else if (phaseEnd.isSame(dates[j], timeUnit)) {
                                            if (style == "phasestartdiv" || style == "phaseenddiv") {
                                                var dataObj = { "id": k, "data": "phaseenddiv" };
                                                phaseData.push(dataObj);
                                            }
                                            style = 'phaseenddiv';
                                        }
                                    }
                                }
                            }

                            // Write project cell to html
                            var cell = createCell("datacell");
                            var celldiv = $("<div></div>");
                            if (!isEmpty(style)) {
                                if (phaseData.length >= 1) {
                                    celldiv.addClass("phasemultidiv");
                                    phaseData = [];
                                } else {
                                    //addCssClass(style, celldiv);
                                    celldiv.addClass(style);
                                }
                                cell.attr("data-trigger", "hover");
                                cell.attr("proj", i);
                                cell.attr("phase", phaseIds);
                            }
                            row.append(cell.append(celldiv));
                        }

                        if (project.assignments && checkAllEvents) {
                            for (var k = 0; k < holder.assignments.length; k++) {
                                var assignment = holder.assignments[k];
                                if (project.projectId != assignment.projectId)
                                    continue;

                                var employee = assignment.employee;
                                var commitments = assignment.commitments;
                                var row = $(tabledate[0].insertRow(-1));
                                row.addClass("employee");
                                row.attr('projectId', project.projectName);

                                row.append(createCell("employeeName", employee.lastName + " " + employee.firstName + "  ", "colspan=\"2\" style=\"white-space:PRE\"></td>"));

                                for (var j = 0; j < dates.length; j++) {                            //for calendar
                                    var style = '';
                                    var text = '';
                                    for (var m = 0; m < commitments.length; m++) {     //for commitments
                                        var commit = commitments[m];
                                        var commitStart = moment(commit.startDate);
                                        var commitEnd = moment(commit.stopDate);

                                        if ((startdate.isSame(dates[j], timeUnit) && !startdate.isBefore(commitStart) && !startdate.isAfter(commitEnd)) || commitStart.isSame(dates[j], timeUnit))
                                            text = commit.hours + "%";

                                        if (commitStart.isSame(dates[j], timeUnit))
                                            style = 'comstartdiv';
                                        else if (commitEnd.isSame(dates[j], timeUnit))
                                            style = 'comenddiv';
                                        else if (commitStart.isBefore(dates[j], timeUnit) && commitEnd.isAfter(dates[j], timeUnit))
                                            style = 'comdiv';
                                    }

                                    // Write employee cell to html
                                    var cell = createCell("datacell");
                                    var celldiv = $("<div></div>");
                                    if (!isEmpty(style))
                                        celldiv.addClass(style);
                                    if (!isEmpty(text))
                                        celldiv.html(text);
                                    row.append(cell.append(celldiv));
                                }
                            }
                        } else {

                            //TODO: hide projectArrowButton
                            //$(".projectArrowButton").hide();
                        }

                        addEmptyRowToHtml(tabledate, dates.length);
                    }

                    //**********   Data to HTML table   ***********
                    var dateTable = $("#ganttchart");
                    dateTable.html("");
                    dateTable.append(tabledate);

                    dateTable.find(".employee").hide();
                    dateTable.find(".employee").each(function () {
                        var employeeProjectId = $(this).attr('projectId');
                        for (var i = 0; i < visibleEmployeeProjectNames.length; i++) {
                            if (employeeProjectId == visibleEmployeeProjectNames[i]) {
                                $(this).show();
                            }
                        }
                    });

                    //Popover

                    $('[data-trigger="hover"]').popover({
                        'trigger': 'hover',
                        'placement': 'bottom',
                        'container': 'body',
                        'data-html': 'true',
                        'html': 'true',
                        'content': function () {
                            if (checkAllEvents) {
                                var phaseId = $(this).attr('phase');
                                if (phaseId.length == 0)
                                    return "";

                                var projectId = $(this).attr('proj');
                                var projObj = holder.allProjects[projectId];

                                if (phaseId.length == 1) {
                                    var startDate = moment(projObj.phases[phaseId[0]].startDate);
                                    var endDate = moment(projObj.phases[phaseId[0]].endDate);
                                    var contentData =
                                        projObj.phases[phaseId[0]].phaseName + "<br/><br/>" +
                                        "From: " + startDate.format('YYYY-MM-DD') + " <br/>" +
                                        "To: " + endDate.format('YYYY-MM-DD') + " <br/><br/>" +
                                        "Progress: " + projObj.phases[phaseId[0]].progress + "%<br/>" +
                                        "Timebudget: " + projObj.phases[phaseId[0]].timeBudget + "h<br/>" +
                                        "Comment: " + projObj.phases[phaseId[0]].comments + "<br/>";

                                } else {
                                    var contentData = "";
                                    for (var i = 0; i < phaseId.length; i += 2) { //phaseId is coming with comma
                                        var tmp = phaseId[i];
                                        var startDate = moment(projObj.phases[phaseId[i]].startDate);
                                        var endDate = moment(projObj.phases[phaseId[i]].endDate);
                                        contentData +=
                                            projObj.phases[phaseId[i]].phaseName + "<br/>" +
                                            "From: " + startDate.format('YYYY-MM-DD') + " <br/>" +
                                            "To: " + endDate.format('YYYY-MM-DD') + " <br/>" +
                                            "Progress: " + projObj.phases[phaseId[0]].progress + "%<br/><br/>";
                                    }
                                }
                            } else {
                                var projectId = $(this).attr('proj');
                                var projObj = holder.allProjects[projectId];
                                var contentData =
                                    "From: " + moment(projObj.startDate).format('YYYY-MM-DD') + " <br/>" +
                                    "To: " + moment(projObj.stopDate).format('YYYY-MM-DD') + " <br/><br/>" +
                                    "Timebudget: " + projObj.timeBudget + "h<br/>";
                            }
                            return contentData;
                        }
                    });

                    //Collapse
                    $('<button></button>').attr({ 'type': 'button' }).addClass("glyphicon glyphicon-triangle-bottom").click(function () {
                            var projectRow = $(this).parent().parent();
                            var projectId = projectRow.attr('projectId');

                            var index = visibleEmployeeProjectNames.indexOf(projectId);
                            if (index > -1) {
                                visibleEmployeeProjectNames.splice(index, 1);
                            } else {
                                visibleEmployeeProjectNames.push(projectId);
                            }
                            projectRow.siblings(".employee").each(function () {
                                var employeeProjectId = $(this).attr('projectId');
                                if (employeeProjectId == projectId) {
                                    $(this).fadeToggle(300);
                                }
                            });
                        }).appendTo($('td.projectArrowButton'));

                    //Go to OneProject
                    $(".projectName").click(function () {
                        var projectId = $(this).parent().attr('projectnbr');
                        $(".ganttpanel").hide();
                        window.location.assign("http://localhost:8899/#!/projects/project-details/" + projectId);

                    }).eq(0);

                } //end of function resetGantt() **********************************************************************************************************************************

                //**********   Helper functions   *********************************************************************************************************************************
                function createCalendarButton(row, baseClass, timeUnit, delta, leap, checkAllEvents) {
                    $('<button></button>').attr({ 'type': 'submit' }).addClass(baseClass).click(function () {
                        startdate = startdate.add(delta, timeUnit);
                        resetGantt(timeUnit, leap, checkAllEvents);
                    }).appendTo(row);
                }


                function getEmployeeFromId(id) {
                    for (var i = 0; i < holder.employees.length; i++) {
                        if (holder.employees[i].employeeId == id)
                            return holder.employees[i];
                    }
                    return null;
                }

                function createCell(baseclass, text = "", td = "") {
                    var cell = $("<td " + td + "></td>");
                    if (!isEmpty(baseclass))
                        cell.addClass(baseclass);
                    if (!isEmpty(text))
                        cell.html(text);
                    return cell;
                }

                function addEmptyRowToHtml(table, columnCount) {
                    var row1 = $(table[0].insertRow(-1));
                    row1.addClass("emptyRow");
                    var cell2 = $("<td colspan=\"" + columnCount + "\"></td>");
                    row1.append(cell2);
                }

                function isEmpty(val) {
                    return (val === undefined || val == null || val.length <= 0 || val === " ") ? true : false;
                }

            } //Controller end
        });
})();


