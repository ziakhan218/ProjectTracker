angular.module('ITProjectTrackerApp').controller("ProjectTrackerCtrl", ['$scope', '$rootScope', 'controllerHelper', 'uiGridConstants', '$location','sharedModels',
    function ($scope, $rootScope, controllerHelper, uiGridConstants, $location, sharedModels) {
        controllerHelper.openModal($scope);
        $scope.gridOptions = {
            enableFiltering: true,
            paginationPageSize: 25,
            enablePaginationControls: false,
            enableHorizontalScrollbar: 0,
            enableVerticalScrollbar: 0,
            enableRowSelection: true,
            enableRowHeaderSelection: false,
            multiSelect: false,
            columnDefs: [
               { field: 'Id', visible: false },
               { field: 'ProjectReference', displayName: 'Project Reference', width: '**', enableColumnMenu: false, cellTooltip: true, type: 'number' },
               { field: 'ProjectName', displayName: 'Project Name', width: '***', enableColumnMenu: false, cellTooltip: true, type: 'string' },
               { field: 'CategoryName', displayName: 'Category', width: '***', enableColumnMenu: false, cellTooltip: true, type: 'string' },
               { field: 'SystemTypeName', displayName: 'System Type', width: '***', enableColumnMenu: false, cellTooltip: true, type: 'string' },
               { field: 'PriorityName', displayName: 'Priority', width: '*', enableColumnMenu: false, cellTooltip: true, type: 'string' },              
            ],
        }
        
        $scope.gridOptions.onRegisterApi = function (gridApi) {
            $scope.gridApi = gridApi;
            gridApi.selection.on.rowSelectionChanged($scope, function (row) {  
                var name = row.entity.ProjectName.split(' ').join('_');                
                window.location.href = '/ITProjectTracker#/ProjectDetails/' + name;
                sharedModels.data.name = name;
                $location.url('/ProjectDetails/' + name);
            });
        };
        $scope.pageChanged = function () {
            $scope.gridApi.pagination.seek($scope.currentPage);
            $scope.$apply();
        };
        $scope.getAllProjects = function () {
            controllerHelper.openModal($scope);
            controllerHelper.apiGet("api/getAllProjects", null, function (result) {
                $scope.gridOptions.totalItems = result.data.length;
                $scope.gridOptions.data = result.data;
                controllerHelper.closeModal();
                
                
            }, function (res) {
                $scope.isProcessing = false;
                alert();
                $scope.modalOptions = {
                    title: 'Error',
                    body: 'Some error occured while fetching data from server.',
                    ok: true
                };
            });
        }
      
        $scope.createProject = function () {
                       
            $location.url('/NewProject');            
        }
        
        //delete Project
        $scope.deleteInputs = function (row) {
            rowsToDelete = row ? [row.entity] : $scope.gridApi.selection.getSelectedRows();

            $scope.modalOptions = {
                body: 'Do you want to delete ' + rowsToDelete.length + ' item(s)?',
                ok: true,
                cancel: true
            };

            controllerHelper.openModal($scope, function () {
                $scope.modalOptions = null;
                var idsToDelete = [];
                angular.forEach(rowsToDelete, function (x) {
                    idsToDelete.push(x.Id);
                });
                controllerHelper.apiPost('api/deleteProjects', idsToDelete, function (res) {
                    angular.forEach(rowsToDelete, function (x) {
                        $scope.gridOptions.data.splice($scope.gridOptions.data.indexOf(x), 1);
                    });
                    controllerHelper.closeModal();
                }, function () {
                    $scope.modalOptions = {
                        title: 'Error',
                        body: 'Some error occured while deleting data from server.',
                        ok: true
                    };
                    controllerHelper.openModal($scope, null, 'md');
                });
            });
        }
        $scope.init = function() {
            $scope.getAllProjects();            
          
        }
        $scope.init();
    }]);
