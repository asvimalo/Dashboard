// loaderControl

(function () {
    "use-strict";

    angular.module("loaderControl", [])
        .directive("loaderCursor", loaderCursor);

    function loaderCursor() {
        return {
            scope: {
                show: "=displayWhen"
            },
            restrict: "E",
            templateUrl: "loaderCursor.html"
        };
    };
})()