﻿(function () {
    "use strict";
    angular.module("allProjects", [])
        .component("allProjects", {
            templateUrl: "/js/app/allproject/all-projects.template.html",
            controller: function allProjectsController($http, $scope, $location, $q, repoProjects, repoAssignments) {
                var holder = this;

                holder.allProjects = [];
                holder.assignments = [];

                var currentDate = moment();

                $q.all([
                    repoProjects.getAll(),
                    repoAssignments.getAll()
                ]).then(function (response) {
                    //success
                    angular.copy(response[0], holder.allProjects);
                    angular.copy(response[1], holder.assignments);

                    initProjectGantt('day', currentDate);
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
                var yearButton = $("#yearButton");

                $scope.weekButtonClick = function () {
                    if (!weekButton.hasClass('active')) weekButton.addClass('active');
                    if (monthButton.hasClass('active')) monthButton.removeClass('active');
                    if (yearButton.hasClass('active')) yearButton.removeClass('active');
                    initProjectGantt('day', currentDate);
                };

                $scope.monthButtonClick = function () {
                    if (weekButton.hasClass('active')) weekButton.removeClass('active');
                    if (!monthButton.hasClass('active')) monthButton.addClass('active');
                    if (yearButton.hasClass('active')) yearButton.removeClass('active');
                    initProjectGantt('week', currentDate);
                };

                $scope.yearButtonClick = function () {
                    if (monthButton.hasClass('active')) monthButton.removeClass('active');
                    if (weekButton.hasClass('active')) weekButton.removeClass('active');
                    if (!yearButton.hasClass('active')) yearButton.addClass('active');
                    initProjectGantt('month', currentDate);
                };

                var visibleEmployeeProjectNames = [];

                function initProjectGantt(timeUnit, startDate) {
                    if (timeUnit === 'day')
                        projectGantt('day', 7, true, startDate);
                    else if (timeUnit === 'week')
                        projectGantt('week', 5, true, startDate);
                    else // month
                        projectGantt('month', 3, false, startDate);
                }

                /*****************************************************************************************************************************************************************
                 * * * * * * * *     GANTT    * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 *****************************************************************************************************************************************************************/
                function projectGantt(timeUnit, leap, checkAllEvents, startDate) {
                    //**********   CALENDAR   ***********

                    var dates = createCalendarDates(timeUnit, startDate, 10);
                    var columnCount = dates.length + 2;

                    var tabledate = $("<table></table>").addClass("table borderless");
                    createCalendarHeader(tabledate, startDate, dates, leap, timeUnit, initProjectGantt);

                    //**********   PROJECT   ***********
                    addEmptyRowToHtml(tabledate, columnCount);

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
                                if (projectStart.isBefore(dates[j], timeUnit) && projectEnd.isAfter(dates[j], timeUnit))
                                    style = 'celldiv'

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

                                        if ((startDate.isSame(dates[j], timeUnit) && !startDate.isBefore(commitStart) && !startDate.isAfter(commitEnd)) || commitStart.isSame(dates[j], timeUnit))
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

                        addEmptyRowToHtml(tabledate, columnCount);
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
                                    var start = moment(projObj.phases[phaseId[0]].startDate);
                                    var end = moment(projObj.phases[phaseId[0]].endDate);
                                    var contentData =
                                        projObj.phases[phaseId[0]].phaseName + "<br/><br/>" +
                                        "From: " + start.format('YYYY-MM-DD') + " <br/>" +
                                        "To: " + end.format('YYYY-MM-DD') + " <br/><br/>" +
                                        "Progress: " + projObj.phases[phaseId[0]].progress + "%<br/>" +
                                        "Timebudget: " + projObj.phases[phaseId[0]].timeBudget + "h<br/>" +
                                        "Comment: " + projObj.phases[phaseId[0]].comments + "<br/>";

                                } else {
                                    var contentData = "";
                                    for (var i = 0; i < phaseId.length; i += 2) { //phaseId is coming with comma
                                        var start = moment(projObj.phases[phaseId[i]].startDate);
                                        var end = moment(projObj.phases[phaseId[i]].endDate);
                                        contentData +=
                                            projObj.phases[phaseId[i]].phaseName + "<br/>" +
                                            "From: " + start.format('YYYY-MM-DD') + " <br/>" +
                                            "To: " + end.format('YYYY-MM-DD') + " <br/>" +
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
                        window.location.assign("http://localhost:8899/#!/projects/project-details/" + projectId);
                   }).eq(0);

                } //end of function resetGantt() **********************************************************************************************************************************

                //**********   Helper functions   *********************************************************************************************************************************
                function getEmployeeFromId(id) {
                    for (var i = 0; i < holder.employees.length; i++) {
                        if (holder.employees[i].employeeId == id)
                            return holder.employees[i];
                    }
                    return null;
                }


            } //Controller end
        });
})();


