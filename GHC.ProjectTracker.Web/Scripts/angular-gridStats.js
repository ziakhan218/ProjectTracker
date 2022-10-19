(function() {
    'use strict';

    angular.module('ui.gridStats', []).directive('gridStats', function () {

        var pageNumber = 0, pageSize = 0, totalItems = 0;

        return {
            restrict: 'A',
            scope: {pageSize: '=', pageNumber: '=?', totalItems: '=?'},
            link: function (scope, element, attrs) {
                var $stats = angular.element(element);
                
                scope.$watch('pageNumber', function (val) {
                    pageNumber = val;
                    setText();
                });
                
                scope.$watch('pageSize', function (val) {
                    pageSize = val;
                    setText();
                });
                
                scope.$watch('totalItems', function (val) {
                    totalItems = val;
                    setText();
                });

                function setText() {
                    var res = totalItems < 1 ? "No results" : "(" + (((pageNumber - 1) * pageSize) + 1) + " - " + ((pageNumber * pageSize) > totalItems ? totalItems : pageNumber * pageSize) + " of " + totalItems + " results)";
                    $stats.text(res);
                }
            }
        };
    });
}());