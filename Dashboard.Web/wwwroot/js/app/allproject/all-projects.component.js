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
                    repoAssignments.getAll(),
                ]).then(function (response) {
                    //success
                    console.log("Check1");
                    angular.copy(response[0], holder.allProjects);
                    angular.copy(response[1], holder.assignments);

                    init();

                }, function (error) {
                    //failure
                    holder.errorMessage = "Failed to load data: " + error;
                })
                    .finally(function () {
                        holder.isBusy = false;
                    });

                var startdate = moment();
                var employeeHidden = [];
                
                function init() {
                    //**********   CALENDAR   ***********
                    var dates = [];
                    var formatedDates = [];
                    //var startdate = moment();
                    var currdate = startdate.clone();
                    var enddate = 14;    //getInitDateToCalendar().end;
                    for (var i = 0; i < enddate; i++) {
                        dates.push(currdate.format('YYYY-MM-DD'));
                        var plusOneDayStr = currdate.format('DD MMM'); //.format('DD MMM ddd');
                        formatedDates.push(plusOneDayStr);
                        currdate = currdate.add(1, 'days');
                    }
                    //var startDate = moment(); //new
                    var tabledate = $("<table></table>").addClass("table borderless"); 
                    var rowdate = $(tabledate[0].insertRow(-1));
                    //createCalendar(startDate); //new
                    rowdate.append(createCell("", "", "")); //colspan =\"2\"
                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-chevron-left").click(function () { //new
                        dates = [];
                        formatedDates = [];
                        startdate = startdate.add(-1, 'days');
                        init();
                    }).appendTo(rowdate);
                    for (var i = 0; i < formatedDates.length; i++) {
                        rowdate.append(createCell("cellDate", formatedDates[i].toString() + "  | "));
                    }
                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-chevron-right").click(function () { //new
                        dates = [];
                        formatedDates = [];
                        startdate = startdate.add( 1, 'days');
                        init();
                    }).appendTo(rowdate);

                    //**********   PROJECT   ***********
                    addEmptyRowToHtml(tabledate, dates);

                    var nbrOfProjects = holder.allProjects.length;
                    for (var i = 0; i < nbrOfProjects; i++) {

                        var project = holder.allProjects[i];
                        var row = $(tabledate[0].insertRow(-1));
                        row.addClass("project");
                        row.attr('projectId', project.projectName); 
                        row.attr('projectNbr', project.projectId);

                        row.append(createCell("projectName", "  " + project.projectName, "style=\"white-space:PRE\""));
                        row.append(createCell("insertButton"));

                        for (var j = 0; j < dates.length; j++) {                 //for calendar
                            var projectStart = moment(project.startDate).format('YYYY-MM-DD');
                            var projectEnd = moment(project.stopDate).format('YYYY-MM-DD');
                            var data = '';
                            var phaseId = -1;
                            if (projectStart == dates[j]) {
                                data = 'S';
                            }
                            else if (projectEnd == dates[j]) {
                                data = 'E';
                            }
                            else {
                                if (projectStart < dates[j] && projectEnd > dates[j]) {
                                    data = 'x'
                                }
                                for (var k = 0; k < project.phases.length; k++) {      //for project's phases
                                    var phase = project.phases[k];
                                    var phaseStart = moment(phase.startDate).format('YYYY-MM-DD');
                                    var phaseEnd = moment(phase.endDate).format('YYYY-MM-DD');
                                    if ((phaseStart <= dates[j]) && (phaseEnd >= dates[j])) {
                                        phaseId = k;
                                        data = 'x';

                                        if (phaseStart == dates[j]) {
                                            data = 'PS';
                                        }
                                        else if (phaseEnd == dates[j]) {
                                            data = 'PE';
                                        }
                                    }
                                }
                            }

                            // Write project cell to html
                            var cell = createCell("datacell");
                            var celldiv = $("<div></div>");
                            celldiv.html(data);
                            if (!isEmpty(data)) {
                                addCssClass(data, celldiv);
                                cell.attr("data-trigger", "hover");
                                cell.attr("proj", i);
                                cell.attr("phase", phaseId);
                            }
                            row.append(cell.append(celldiv));
                        }

                        if (project.assignments) {
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
                                    data = '';
                                    for (var m = 0; m < commitments.length; m++) {     //for commitments
                                        var commit = commitments[m];
                                        var commitStart = moment(commit.startDate).format('YYYY-MM-DD');
                                        var commitEnd = moment(commit.stopDate).format('YYYY-MM-DD');

                                        // TODO
                                        if (commitEnd == "0001-01-01")
                                            commitEnd = commitStart;

                                        if (commitStart == dates[j])
                                            data = commit.hours + "%";
                                        else if (commitEnd == dates[j])
                                            data = 'CE';
                                        else if ((commitStart < dates[j]) && (commitEnd > dates[j]))
                                            data = 'xC';
                                    }

                                    // Write employee cell to html
                                    var cell = createCell("datacell");
                                    var celldiv = $("<div></div>");
                                    if (!isEmpty(data)) {
                                        addCssClass(data, celldiv);
                                        celldiv.html(data);
                                    }
                                    row.append(cell.append(celldiv));
                                }
                            }
                        }

                        addEmptyRowToHtml(tabledate, dates);
                    }

                    //**********   Data to HTML table   ***********
                    var dateTable = $("#ganttchart");
                    dateTable.html("");
                    dateTable.append(tabledate);

                    dateTable.find("tr").eq(".employee").hide;
                    dateTable.find(".employee").hide();
                    dateTable.find("tr").eq(0).show();

                    //Popover
                    $('[data-trigger="hover"]').popover({
                        'trigger': 'hover',
                        'placement': 'bottom',
                        'container': 'body',
                        'data-html': 'true',
                        'html': 'true',
                        'content': function () {
                            var phaseId = $(this).attr('phase');
                            if (phaseId == -1)
                                return "";
                            var projectId = $(this).attr('proj');
                            var projObj = holder.allProjects[projectId]; //projects[projectId];
                            var startDate = moment(projObj.phases[phaseId].startDate);
                            var endDate = moment(projObj.phases[phaseId].endDate);
                            var contentData =
                                projObj.phases[phaseId].phaseName + "<br/><br/>" +
                                "From: " + startDate.format('YYYY-MM-DD') + " <br/>" +
                                "To: " + endDate.format('YYYY-MM-DD') + " <br/><br/>" +
                                "Progress: " + projObj.phases[phaseId].progress + "%<br/>" +
                                "Timebudget: " + projObj.phases[phaseId].timeBudget + "h<br/>" +
                                "Comment: " + projObj.phases[phaseId].comments + "<br/>";
                            return contentData;
                        }
                    });

                    //Collapse
                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-triangle-bottom").click(function () {
                        var projectRow = $(this).parent().parent();
                        var projectId = projectRow.attr('projectId');
                        projectRow.siblings(".employee").each(function () {
                            var employeeProjectId = $(this).attr('projectId');
                            if (employeeProjectId == projectId) {
                                $(this).fadeToggle(300);
                            }
                        });
                    }).appendTo($('td.insertButton'));

                    //Go to OneProject
                    $(".projectName").click(function () {
                        var projectId = $(this).parent().attr('projectnbr');
                        window.location.assign("http://localhost:8899/#!/projects/project-details/" + projectId);
                    }).eq(0);
                }

                //**********   Helper functions   ***********
                function createCalendar(start) {
                    var enddate = 7;
                    for (var i = 0; i < enddate; i++) {
                        dates.push(start.format('YYYY-MM-DD'));
                        var plusOneDayStr = start.format('DD MMM'); //.format('DD MMM ddd');
                        formatedDates.push(plusOneDayStr);
                        start = start.add(1, 'days');
                    }
                }

                function getEmployeeFromId(id)
                {
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

                function addEmptyRowToHtml(table, dates) {
                    var row1 = $(table[0].insertRow(-1));
                    row1.addClass("emptyRow");
                    var cell2 = $("<td colspan=\"" + (dates.length + 2) + "\"></td>");
                    row1.append(cell2);
                }

                // TODO: Convenient for debugging. Remove later
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

                function isEmpty(val) {
                    return (val === undefined || val == null || val.length <= 0 || val === " ") ? true : false;
                }

                function getInitDateToCalendar() {
                    var today = moment();

                    var end = moment(holder.allProjects[0].stopDate, 'YYYY-MM-DDTHH:MM:SS');
                    var start = moment(holder.allProjects[0].startDate, 'YYYY-MM-DDTHH:MM:SS');
                    for (var i = 1; i < holder.allProjects.length; i++) {
                        var project = holder.allProjects[i];

                        if (project.endDate > end) {
                            end = project.endDate;
                        }
                        if (project.startDate < start) {
                            start = project.startDate;
                        }
                    }
                    var tEnd = end.diff(today, 'days') + 10;
                    tEnd = tEnd > 7 ? tEnd : 7;
                    var tStart = today.diff(start, 'days') - 10;
                    //tStart = tStart < 10 ? tStart : 
                    var result = { "start": start, "end": tEnd };
                    return result;
                }

            } //Controller end
        });
})();


        