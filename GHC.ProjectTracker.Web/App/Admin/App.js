var adminApp = angular.module('adminApp', ['common', 'ui.bootstrap', 'ui.multiselect', 'ngMaterial', 'ngMdIcons']);

angular.module('adminApp').config(function ($routeProvider, $locationProvider, $httpProvider) {
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }

    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';

    $routeProvider.when('', { templateUrl: '/App/Admin/Views/UploadConfig.html', controller: 'UploadConfigCtrl' });
    $routeProvider.when('/', { templateUrl: '/App/Admin/Views/UploadConfig.html', controller: 'UploadConfigCtrl' });
    $routeProvider.when('/UploadConfig', { templateUrl: '/App/Admin/Views/UploadConfig.html', controller: 'UploadConfigCtrl' });
    $routeProvider.when('/ManageUsers', { templateUrl: '/App/Admin/Views/ManageUsers.html', controller: 'UserManagerCtrl' });
    $routeProvider.otherwise({
        redirectTo: function () {
            window.location = location.pathname;
        }
    });
});


adminApp.filter('slugify', function ($filter) {
    return function (input) {
        if (!input)
            return '';

        var slug = input.toLowerCase().trim();

        slug = slug.replace(/[^a-z0-9\s-]/g, ' ');

        slug = slug.replace(/[\s-]+/g, '-');

        return slug;
    }
});