var dashboardApp = angular.module('dashboardApp', ['ngTouch', 'ui.grid', 'ui.grid.expandable', 'common', 'ui.bootstrap', 'ui.grid.pagination', 'ui.grid.pinning', 'ui.grid.selection', 'ui.multiselect', 'ui.gridStats', 'fcsa-number']);

angular.module('dashboardApp').config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('', { templateUrl: '/App/Dashboard/Views/DashboardView.html', controller: 'DashboardCtrl' });
    $routeProvider.when('/', { templateUrl: '/App/Dashboard/Views/DashboardView.html', controller: 'DashboardCtrl' });
    $routeProvider.when('/Dashboard', { templateUrl: '/App/Dashboard/Views/DashboardView.html', controller: 'DashboardCtrl' });
    $routeProvider.when('/Dashboard/:Id', { templateUrl: '/App/Dashboard/Views/DashboardView.html', controller: 'DashboardCtrl' });
    $routeProvider.otherwise({
        redirectTo: function () {
            window.location = location.pathname;
        }
    });
});
angular.module('dashboardApp').run(function () {

});