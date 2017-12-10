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

function createCalendarButton(buttoncell, baseClass, startDate, delta, timeUnit, initCallback) {
    $('<button></button>').attr({ 'type': 'submit' }).addClass(baseClass).click(function () {
        newStartdate = startDate.add(delta, timeUnit);
        initCallback(timeUnit, newStartdate);
    }).appendTo(buttoncell);
}

function createCalendarHeader(table, startDate, dates, leap, timeUnit, initCallback) {
    var header = $(table[0].createTHead());
    var rowdate = $(header[0].insertRow(-1));
    
    // Back buttons
    var buttoncellBack = $("<th></th>");
    createCalendarButton(buttoncellBack, "glyphicon glyphicon-fast-backward", startDate, -leap, timeUnit, initCallback);
    createCalendarButton(buttoncellBack, "glyphicon glyphicon-chevron-left" , startDate, -1, timeUnit, initCallback);
    rowdate.append(buttoncellBack);

    // Printed dates
    var cell = $("<th></th>");  
    rowdate.append(cell); //colspan =\"2\" 
    for (var i = 0; i < dates.length; i++) {
        if (timeUnit == 'day')
            formatedDate = dates[i].format('ddd DD/MM');
        else if (timeUnit == 'week')
            formatedDate = dates[i].format('DD/MM') + " - " + dates[i].add(6, 'days').format('DD/MM');
        else // month
            formatedDate = dates[i].format('MMM YYYY');
        var style = dates[i].isSame(moment(), timeUnit) ? "todayCellDate" : "cellDate";
        var datecell = $("<th></th>");
        datecell.addClass(style);
        datecell.html(formatedDate.toString() + " |  ");
        rowdate.append(datecell);
    }
    // Forward buttons
    var buttoncellFwd = $("<th></th>");
    createCalendarButton(buttoncellFwd, "glyphicon glyphicon-chevron-right", startDate, 1   , timeUnit, initCallback);
    createCalendarButton(buttoncellFwd, "glyphicon glyphicon-fast-forward" , startDate, leap, timeUnit, initCallback);
    rowdate.append(buttoncellFwd);
}
