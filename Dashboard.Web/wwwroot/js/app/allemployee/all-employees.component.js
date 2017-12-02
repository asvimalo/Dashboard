(function () {
    console.log("!!!!!!!!!!!!!!adsdas!!");
    "use strict";
    angular.module("allEmployees", [])
        .component("allEmployees", {
            templateUrl: "/js/app/allemployee/all-employees.template.html",
            controller: function allEmployeesController($http, $scope, $location, $q, repoEmployees) {
                //var holder = this;
                console.log("Employee component!");

                //holder.allEmployees = [];

                //$q.all([
                //    repoEmployees.getAll()
                //]).then(function (response) {
                //    //success
                //    console.log("Check1");
                //    angular.copy(response[0], holder.allEmployees);

                //    initWeek();

                //}, function (error) {
                //    //failure
                //    holder.errorMessage = "Failed to load data: " + error;
                //})
                //    .finally(function () {
                //        holder.isBusy = false;
                //    });

                //var startdate = moment();

                /*****************************************************************************************************************************************************************
                 * * * * * * * *     WEEK    * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 *****************************************************************************************************************************************************************/
                //function initWeek() {
                    //**********   CALENDAR   ***********
                    //var dates = [];
                    //var formatedDates = [];
                    //var currdate = startdate.clone();
                    //var enddate = 10;
                    //for (var i = 0; i < enddate; i++) {
                    //    dates.push(currdate.format('YYYY-MM-DD'));
                    //    //var plusOneDayStr = currdate.format('ddd DD/MM'); //.format('DD MMM ddd');
                    //    var plusOneDayStr = currdate.format('MM/DD ddd');
                    //    formatedDates.push(plusOneDayStr);
                    //    currdate = currdate.add(1, 'days');
                    //}
                    //var tabledate = $("<table></table>").addClass("table borderless");
                    //var rowdate = $(tabledate[0].insertRow(-1));
                    ////createCalendar(startDate); //new
                    //$('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-fast-backward").click(function () {
                    //    dates = [];
                    //    formatedDates = [];
                    //    startdate = startdate.add(-7, 'days');
                    //    initWeek();
                    //}).appendTo(rowdate);
                    //$('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-chevron-left").click(function () {
                    //    dates = [];
                    //    formatedDates = [];
                    //    startdate = startdate.add(-1, 'days');
                    //    initWeek();
                    //}).appendTo(rowdate);
                    //rowdate.append(createCell("", "", "")); //colspan =\"2\" 
                    //for (var i = 0; i < formatedDates.length; i++) {
                    //    rowdate.append(createCell("cellDate", formatedDates[i].toString() + " |  "));
                    //}
                    //$('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-chevron-right").click(function () {
                    //    dates = [];
                    //    formatedDates = [];
                    //    startdate = startdate.add(1, 'days');
                    //    initWeek();
                    //}).appendTo(rowdate);
                    //$('<button></button>').attr({ 'type': 'submit' }).addClass("glyphicon glyphicon-fast-forward").click(function () {
                    //    dates = /*[];*/
                    //    formatedDates = [];
                    //    startdate = startdate.add(7, 'days');
                    //    initWeek();
                    //}).appendTo(rowdate);
                //}
            }
        });
});         