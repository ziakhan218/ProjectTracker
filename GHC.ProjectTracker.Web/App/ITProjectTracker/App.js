var projectTrackerApp = angular.module('ITProjectTrackerApp', ['ngTouch', 'ui.grid', 'ui.grid.expandable', 'common', 'ui.bootstrap', 'ui.grid.pagination', 'ui.grid.pinning', 'ui.grid.selection', 'ui.multiselect', 'ui.gridStats', 'fcsa-number']);

angular.module('ITProjectTrackerApp').config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('', { templateUrl: '/App/ITProjectTracker/Views/ProjectTrackerView.html', controller: 'ProjectTrackerCtrl' });
    $routeProvider.when('/', { templateUrl: '/App/ITProjectTracker/Views/ProjectTrackerView.html', controller: 'ProjectTrackerCtrl' });
    $routeProvider.when('/ITProjectTracker', { templateUrl: '/App/ITProjectTracker/Views/ProjectTrackerView.html', controller: 'ProjectTrackerCtrl' });
    $routeProvider.when('/ProjectTrackerView', { templateUrl: '/App/ITProjectTracker/Views/ProjectTrackerView.html', controller: 'ProjectTrackerCtrl' });
    $routeProvider.when('/BacklogItemsView', { templateUrl: '/App/ITProjectTracker/Views/BacklogItemsView.html', controller: 'BacklogItemsCtrl' });
    $routeProvider.when('/NewProject', { templateUrl: '/App/ITProjectTracker/Views/ProjectDetails.html', controller: 'ProjectDetailsCtrl' });
    $routeProvider.when('/ProjectDetails/:Id', { templateUrl: '/App/ITProjectTracker/Views/ProjectDetails.html', controller: 'ProjectDetailsCtrl' });
    $routeProvider.when('/ProjectResources', { templateUrl: '/App/ITProjectTracker/Views/ProjectResources.html', controller: 'ProjectResourcesCtrl' });
    $routeProvider.otherwise({
        redirectTo: function () {
            window.location = location.pathname;
        }
    });
});
angular.module('ITProjectTrackerApp').run(function () {

}).filter('customArray', function ($filter) {

    return function (list, arrayFilter, element) {
        if (arrayFilter) {
            return $filter("filter")(list, function (listItem) {
                return arrayFilter.indexOf(listItem[element]) != -1;
            });
        }
    };
}).directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^-0-9\.]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
}).factory('sharedModels', [function () {
    
    this.data = { breakfast: 'eggs'};

    return this;

}]);
