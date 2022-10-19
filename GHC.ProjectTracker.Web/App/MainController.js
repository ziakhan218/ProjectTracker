angular.module('main').controller('mainController', ['$scope', '$http', function ($scope, $http) {
    $scope.menuList = [];    
    var str = window.location.pathname + window.location.hash;
    
    if (str == "/ITProjectTracker#/") {
        $scope.myValue = true;
    }
    else {
        $scope.myValue = false;
    }   
    //$http({ url: "Home/GetMenu" }).then(function mySucces(response) {       
    //    $scope.menuList = response.data;
    //    if (window.location.hash.match("ProjectDetails") != null) {
    //        $scope.menuList[13].ModulePath = window.location.pathname + window.location.hash;
    //    }      
    //});
}]);