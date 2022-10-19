angular.module('ITProjectTrackerApp').controller("ProjectResourcesCtrl", ['$scope', 'controllerHelper', function ($scope, controllerHelper) {
    $scope.users = [];
    $scope.developers = [];
    $scope.Id;
    controllerHelper.apiGet('api/getProjectByName?name=' + localStorage.getItem("projectName"), null, function (res) {        
        $scope.project = res.data;        
        $scope.Id = $scope.project.Id;
        $scope.Ids();
        $scope.isProcessing = false;
    }, function (res) {
        $scope.isProcessing = false;
        $scope.modalOptions = {
            title: 'Error',
            body: 'Some error occured while fetching data from server.',
            ok: true
        };
    });
    
    controllerHelper.apiGet("api/allActiveUsers", null, function (result) {
        $scope.users = result.data;        
        $scope.isProcessing = false;
        $scope.DevelopersId;

    }, function (res) {
        $scope.isProcessing = false;
        $scope.modalOptions = {
            title: 'Error',
            body: 'Some error occured while fetching data from server.',
            ok: true
        };
    });
    $scope.Ids = function () {
        controllerHelper.apiGet('api/getDeveloperIds?Id=' + $scope.Id, null, function (res) {

            $scope.developers = res.data;

            console.log($scope.developers);
        }, function (res) {
            $scope.isProcessing = false;
            $scope.modalOptions = {
                title: 'Error',
                body: 'Some error occured while fetching data from server.',
                ok: true
            };
        });
    }
   

    $scope.project = {};
    
    $scope.saveProjectResource = function () {
      console.log($scope.project)
        $scope.projectParams = {
            project: $scope.project,
            Developers: $scope.developers
        }
        controllerHelper.apiPost("api/saveProjectResource", $scope.projectParams, function (result) {            
            

        }, function (res) {

            $scope.modalOptions = {
                title: 'Error',
                body: 'Some error occured while updating data to server.',
                ok: true
            };
        });
    }
}]);
  