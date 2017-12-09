function isEmpty(val) {
    return (val === undefined || val == null || val.length <= 0 || val === " ") ? true : false;
}

function createCalendarDates(timeUnit, firstDate, dateCount) {
    var dates = [];
    currdate = firstDate.clone().startOf(timeUnit);
    for (var i = 0; i < dateCount; i++) {
        dates.push(currdate.clone());
        currdate = currdate.add(1, timeUnit);
    }
    return dates;
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

function createCalendarButton(row, baseClass, startDate, delta, timeUnit, initCallback) {
    $('<button></button>').attr({ 'type': 'submit' }).addClass(baseClass).click(function () {
        newStartdate = startDate.add(delta, timeUnit);
        initCallback(timeUnit, newStartdate);
    }).appendTo(row);
}

function createCalendarHeader(table, startDate, dates, leap, timeUnit, initCallback) {
    var rowdate = $(table[0].insertRow(-1));

    // Back buttons
    createCalendarButton(rowdate, "glyphicon glyphicon-fast-backward", startDate, -leap, timeUnit, initCallback);
    createCalendarButton(rowdate, "glyphicon glyphicon-chevron-left" , startDate, -1   , timeUnit, initCallback);
    // Printed dates
    rowdate.append(createCell("", "", "")); //colspan =\"2\" 
    for (var i = 0; i < dates.length; i++) {
        formatedDate = dates[i].format('MM/DD ddd');
        rowdate.append(createCell("cellDate", formatedDate.toString() + " |  "));
    }
    // Forward buttons
    createCalendarButton(rowdate, "glyphicon glyphicon-chevron-right", startDate, 1   , timeUnit, initCallback);
    createCalendarButton(rowdate, "glyphicon glyphicon-fast-forward" , startDate, leap, timeUnit, initCallback);
    
}


//function createRowPrimary(table, primName, primId) {
//    var row = $(table[0].insertRow(-1));
//    row.addClass("project");
//    row.attr('primName', primName);
//    row.attr('primId', primId);
//    row.append(createCell("primName", "  " + primName, "style=\"white-space:PRE\""));
//    row.append(createCell("projectArrowButton"));
//    return row;
//}
