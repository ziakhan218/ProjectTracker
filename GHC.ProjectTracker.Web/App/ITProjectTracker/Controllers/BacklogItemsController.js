
angular.module('ITProjectTrackerApp').controller("BacklogItemsCtrl", ['$scope', '$rootScope', 'controllerHelper', 'sharedModels', 'uiGridConstants', '$location',
        function ($scope, $rootScope, controllerHelper, sharedModels, uiGridConstants, $location) {
            $scope.project = {};
            $scope.project.Path = localStorage.getItem("projectPath");           
            $scope.getAllWorkItems = function () {

                controllerHelper.apiGet('api/getAllBacklogItems/?projectName=' + localStorage.getItem("projectName"), null, function (result) {
                    $scope.gridOptions.totalItems = result.data.length;
                    $scope.gridOptions.data = result.data;
                    filtersConfig($scope.gridOptions.data);
                    for (i = 0; i < result.data.length; i++) {
                        result.data[i].subGridOptions = {
                            columnDefs: [{ name: "Assignedto", field: "Assignedto" }, { name: "State", field: "State" }, { name: "Task", field: "Task" }, { name: "TimeRemaining", field: "TimeRemaining" }],
                            data: result.data[i].childs,
                            enablePaginationControls: false,
                            enableHorizontalScrollbar: 0,
                            enableVerticalScrollbar: 0,
                            enableRowSelection: false,
                            enableRowHeaderSelection: false,
                            multiSelect: false,
                            enableRowSelection: false,
                        }
                    }
                    $scope.childs = result.data;


                }, function (res) {
                    $scope.isProcessing = false;
                 
                    $scope.modalOptions = {
                        title: 'Error',
                        body: 'Some error occured while fetching data from server.',
                        ok: true
                    };
                });
            }
            $scope.gridOptions = {
                enableFiltering: true,
                paginationPageSize: 20,
                enablePaginationControls: false,
                enableHorizontalScrollbar: 0,
                enableVerticalScrollbar: 0,
                enableRowSelection: false,
                enableRowHeaderSelection: false,
                multiSelect: false,
                enableRowSelection: false,
                expandableRowTemplate: '/App/ITProjectTracker/Views/expandableRowTemplate.html',
                expandableRowHeight: 150,
                columnDefs: [
                   { field: 'Task', displayName: 'Task', width: '**', enableColumnMenu: false, cellTooltip: true, type: 'string' },
                   { field: 'State', displayName: 'State', width: '**', enableColumnMenu: false, cellTooltip: true, type: 'string' },
                   { field: 'Assignedto', displayName: 'Assigned To', width: '***', enableColumnMenu: false, cellTooltip: true, type: 'string' , filter: { type: uiGridConstants.filter.SELECT, selectOptions: [] } }
                ],
            }

            $scope.gridOptions.onRegisterApi = function (gridApi) {
                $scope.gridApi = gridApi;
            };
            $scope.pageChanged = function () {
                $scope.gridApi.pagination.seek($scope.currentPage);
                //$scope.$apply();
            };
            $scope.getProject = function () {


                controllerHelper.apiGet('api/getProjectByName?name=' + localStorage.getItem("projectName"), null, function (res) {
                    $scope.project = res.data;                  
                    $scope.isProcessing = false;
                }, function (res) {
                    $scope.isProcessing = false;
                    $scope.modalOptions = {
                        title: 'Error',
                        body: 'Some error occured while fetching data from server.',
                        ok: true
                    };
                });

            }
            $scope.getProject();
            $scope.getAllWorkItems();

            $scope.saveVSTSPath = function () {
                console.log($scope.project)
                $scope.projectParams = {
                    project: $scope.project                    
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
            filtersConfig = function (data) {
                var unique = {};
                var distinct = [];
                for (var i in data) {
                    if (typeof (unique[data[i].Assignedto]) == "undefined") {
                        distinct.push(data[i].Assignedto);
                    }
                    unique[data[i].Assignedto] = '';
                }
                distinct.sort();               
                var assignedToColumn = $scope.gridOptions.columnDefs[2];
                assignedToColumn.filter.selectOptions = [];
                for (var i in distinct) {
                    var type = distinct[i];
                    if (type != null && type.length > 0) {
                        assignedToColumn.filter.selectOptions.push({ value: type, label: type });
                    }
                }
            };
        }]);
