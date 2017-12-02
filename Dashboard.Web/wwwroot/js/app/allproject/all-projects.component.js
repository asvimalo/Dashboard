(function () {
    "use strict";
    angular.module("allProjects", [])
        .component("allProjects", {
            templateUrl: "/js/app/allproject/all-projects.template.html",
            controller: function allProjectsController($http, $scope, $location, $q, repoEmployees) {
                var holder = this;

                //All Employee
                holder.allEmployees = [];
                holder.assignments = [];

                $q.all([
                    repoEmployees.getAll(),
                    repoAssignments.getAll()
                ]).then(function (response) {
                    //success
                    console.log("inside");
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

                // --------------------------------------------------------------------------------------
                // All Projects
                holder.allProjects = [];
                //holder.assignments = [];

                //$q.all([
                //    repoProjects.getAll(),
                //    repoAssignments.getAll()
                //]).then(function (response) {
                //    //success
                //    angular.copy(response[0], holder.allProjects);
                //    angular.copy(response[1], holder.assignments);

                //    initWeek();

                //}, function (error) {
                //    //failure
                //    holder.errorMessage = "Failed to load data: " + error;
                //})
                //    .finally(function () {
                //        holder.isBusy = false;
                //    });
                // --------------------------------------------------------------------------------------
                $scope.weekButtonClick = function () {
                    //initWeek(); -- allProject()
                    initWeekEmp();
                };

                $scope.monthButtonClick = function () {
                    //if ($(this).hasClass('active')) {
                    //    $(this).removeClass('active')
                    //} else {
                    //    $(this).addClass('active')
                    //}
                    //initMonth(); --allProject()
                    initMonthEmp();
                };

                /*****************************************************************************************************************************************************************
                 * * * * * * * *     WEEK    * * * * * * * *   EMPLOYEEE   * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 *****************************************************************************************************************************************************************/
                function initWeekEmp() {
                    var tmp = holder.allEmployees;
                }

                function initMonthEmp() {
                }


                // --------------------------------------------------------------------------------------

                var startdate = moment();
                var visibleEmployeeProjectNames = [];

                /*****************************************************************************************************************************************************************
                 * * * * * * * *     WEEK    * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 *****************************************************************************************************************************************************************/
                function initWeek() {
                    //**********   CALENDAR   ***********
                    var dates = [];
                    var formatedDates = [];
                    var currdate = startdate.clone();
                    var enddate = 10;
                    for (var i = 0; i < enddate; i++) {
                        dates.push(currdate.format('YYYY-MM-DD'));
                        //var plusOneDayStr = currdate.format('ddd DD/MM'); //.format('DD MMM ddd');
                        var plusOneDayStr = currdate.format('MM/DD ddd');
                        formatedDates.push(plusOneDayStr);
                        currdate = currdate.add(1, 'days');
                    }
                    var tabledate = $("<table></table>").addClass("table borderless");
                    var rowdate = $(tabledate[0].insertRow(-1));
                    //createCalendar(startDate); //new
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
                    rowdate.append(createCell("", "", "")); //colspan =\"2\" 
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
                        row.append(createCell("projectArrowButton"));

                        var phaseData = [];
                        for (var j = 0; j < dates.length; j++) {                 //for calendar
                            var projectStart = moment(project.startDate).format('YYYY-MM-DD');
                            var projectEnd = moment(project.stopDate).format('YYYY-MM-DD');
                            var data = '';
                            var phaseIds = [];
                            if (projectStart == dates[j]) {
                                data = 'S';
                            }
                            else if (projectEnd == dates[j]) {
                                data = 'E';
                            } else {
                                if (projectStart < dates[j] && projectEnd > dates[j]) {
                                    data = 'x'
                                }
                                
                                for (var k = 0; k < project.phases.length; k++) {      //for project's phases
                                    var phase = project.phases[k];
                                    var phaseStart = moment(phase.startDate).format('YYYY-MM-DD');
                                    var phaseEnd = moment(phase.endDate).format('YYYY-MM-DD');
                                    if ((phaseStart <= dates[j]) && (phaseEnd >= dates[j])) {
                                        phaseIds.push(k);   

                                        if (phaseStart == dates[j]) {
                                            if (data == "PS" || data == "PE") { 
                                                var dataObj = { "id": k, "data": "PS" };
                                                phaseData.push(dataObj);
                                            }
                                            data = 'PS';
                                        }
                                        else if (phaseEnd == dates[j]) {
                                            if (data == "PS" || data == "PE") {
                                                var dataObj = { "id": k, "data": "PE" };
                                                phaseData.push(dataObj);
                                            }
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
                                if (phaseData.length >= 1) {
                                    celldiv.addClass("phasemultidiv");
                                    phaseData = [];
                                } else { 
                                    addCssClass(data, celldiv);
                                }
                                cell.attr("data-trigger", "hover");
                                cell.attr("proj", i);
                                cell.attr("phase", phaseIds);   
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

                                var startDateString = moment(startdate).format('YYYY-MM-DD');

                                for (var j = 0; j < dates.length; j++) {                            //for calendar
                                    data = '';
                                    for (var m = 0; m < commitments.length; m++) {     //for commitments
                                        var commit = commitments[m];
                                        var commitStart = moment(commit.startDate).format('YYYY-MM-DD');
                                        var commitEnd = moment(commit.stopDate).format('YYYY-MM-DD');

                                        // TODO: borde inte förekomma
                                        if (commitEnd == "0001-01-01")
                                            commitEnd = commitStart;

                                        if (commitStart == dates[j])
                                            data = commit.hours + "%";
                                        else if (startDateString == dates[j]) //TODO: Why not?
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
                        } else {

                            //TODO: hide projectArrowButton
                            //$(".projectArrowButton").hide();
                        }

                        addEmptyRowToHtml(tabledate, dates);
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
                                for (var i = 0; i < phaseId.length; i+=2) { //phaseId is coming with comma
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

                } //end of function initweek() **********************************************************************************************************************************


                /*****************************************************************************************************************************************************************
                 * * * * * * * *     MONTH    * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 *****************************************************************************************************************************************************************/
                function initMonth() {
                    //**********   CALENDAR   ***********
                    var dates = [];
                    var formatedDates = [];
                    var currdate = startdate.clone();
                    var currentWeek = startdate.clone().week();
                    var nbrOfWeekToShow = 10;
                    for (var i = 0; i < nbrOfWeekToShow; i++) {
                        dates.push(currdate.format('YYYY-MM-DD'));
                        var plusOneDayStr = currdate.format('MM/DD ddd'); //.format('DD MMM ddd');
                        formatedDates.push(plusOneDayStr);
                        currdate = currdate.add(7, 'days');
                    }
                    var tabledate = $("<table></table>").addClass("table borderless");
                    var rowdate = $(tabledate[0].insertRow(-1));
                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-fast-backward").click(function () {
                        dates = [];
                        formatedDates = [];
                        startdate = startdate.add(-35, 'days'); //5weeks * 7days (10 weeks shows)
                        initMonth();
                    }).appendTo(rowdate);
                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-chevron-left").click(function () {
                        dates = [];
                        formatedDates = [];
                        startdate = startdate.add(-7, 'days');
                        initMonth();
                    }).appendTo(rowdate);
                    rowdate.append(createCell("", "", "")); //colspan =\"2\" 
                    for (var i = 0; i < formatedDates.length; i++) {
                        rowdate.append(createCell("cellDate", formatedDates[i].toString() + " |"));
                    }
                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-chevron-right").click(function () {
                        dates = [];
                        formatedDates = [];
                        startdate = startdate.add(7, 'days');
                        initMonth();
                    }).appendTo(rowdate);
                    $('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-fast-forward").click(function () {
                        dates = [];
                        formatedDates = [];
                        startdate = startdate.add(35, 'days'); //5weeks * 7days (10 weeks shows)
                        initMonth();
                    }).appendTo(rowdate);

                    //**********   PROJECT   ************

                    addEmptyRowToHtml(tabledate, dates);

                    var nbrOfProjects = holder.allProjects.length;
                    for (var i = 0; i < nbrOfProjects; i++) {

                        var project = holder.allProjects[i];
                        var row = $(tabledate[0].insertRow(-1));
                        row.addClass("project");
                        row.attr('projectId', project.projectName);
                        row.attr('projectNbr', project.projectId);

                        row.append(createCell("projectName", "  " + project.projectName, "style=\"white-space:PRE\""));
                        row.append(createCell("projectArrowButton"));

                        var phaseData = [];
                        for (var j = 0; j < dates.length; j++) {                 //for calendar
                            var projectStart = moment(project.startDate).format('YYYY-MM-DD');
                            var projectEnd = moment(project.stopDate).format('YYYY-MM-DD');
                            var data = '';
                            var phaseIds = [];
                            if (moment(projectStart).week() == moment(dates[j]).week()) {
                                data = 'S';
                            }
                            else if (moment(projectEnd).week() == moment(dates[j]).week()) {
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
                                    if ((moment(phaseStart).week() <= moment(dates[j]).week()) && (moment(phaseEnd).week() >= moment(dates[j]).week())) {
                                        phaseIds.push(k);

                                        if (moment(phaseStart).week() == moment(dates[j]).week()) {
                                            if (data == "PS" || data == "PE") {
                                                var dataObj = { "id": k, "data": "PS" };
                                                phaseData.push(dataObj);
                                            }
                                            data = 'PS';
                                        }
                                        else if (moment(phaseEnd).week() == moment(dates[j]).week()) {
                                            if (data == "PS" || data == "PE") {
                                                var dataObj = { "id": k, "data": "PS" };
                                                phaseData.push(dataObj);
                                            }
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
                                if (phaseData.length >= 1) {
                                    celldiv.addClass("phasemultidiv");
                                    phaseData = [];
                                } else {
                                    addCssClass(data, celldiv);
                                }
                                cell.attr("data-trigger", "hover");
                                cell.attr("proj", i);
                                cell.attr("phase", phaseIds);
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

                                        // TODO: borde inte förekomma
                                        if (commitEnd == "0001-01-01")
                                            commitEnd = commitStart;

                                        if (moment(commitStart).week() == moment(dates[j]).week())
                                            data = commit.hours + "%";
                                        else if (moment(startdate).week() == moment(dates[j]).week()) //TODO: Why not?
                                            data = commit.hours + "%";
                                        else if (moment(commitEnd).week() == moment(dates[j]).week())
                                            data = 'CE';
                                        else if ((moment(commitStart).week() < moment(dates[j]).week()) && (moment(commitEnd).week() > moment(dates[j]).week()))
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
                        } else {
                            //TODO: hide projectArrowButton
                            //$(".projectArrowButton").hide();
                        }
                        addEmptyRowToHtml(tabledate, dates);
                    }

                    //**********   Data to HTML table   ***********
                    var dateTable = $("#ganttchart");
                    dateTable.html("");
                    dateTable.append(tabledate);

                    dateTable.find(".employee").hide();

                    //Popover
                    $('[data-trigger="hover"]').popover({
                        'trigger': 'hover',
                        'placement': 'bottom',
                        'container': 'body',
                        'data-html': 'true',
                        'html': 'true',
                        'content': function () {
                            var phaseId = $(this).attr('phase');
                            if (phaseId.length == 0)
                                return "";

                            var projectId = $(this).attr('proj');
                            var projObj = holder.allProjects[projectId]; //projects[projectId];

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
                    }).appendTo($('td.projectArrowButton'));

                    //Go to OneProject
                    $(".projectName").click(function () {
                        var projectId = $(this).parent().attr('projectnbr');
                        window.location.assign("http://localhost:8899/#!/projects/project-details/" + projectId);
                    }).eq(0);
                }

                //**********   Helper functions   *********************************************************************************************************************************
                function createCalendar(start) {
                    var enddate = 7;
                    for (var i = 0; i < enddate; i++) {
                        dates.push(start.format('YYYY-MM-DD'));
                        var plusOneDayStr = start.format('DD MMM'); //.format('DD MMM ddd');
                        formatedDates.push(plusOneDayStr);
                        start = start.add(1, 'days');
                    }
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

                function addEmptyRowToHtml(table, dates) {
                    var row1 = $(table[0].insertRow(-1));
                    row1.addClass("emptyRow");
                    var cell2 = $("<td colspan=\"" + (dates.length + 2) + "\"></td>");
                    row1.append(cell2);
                }

                // TODO: Convenient for debugging. 
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

            } //Controller end
        });
})();


