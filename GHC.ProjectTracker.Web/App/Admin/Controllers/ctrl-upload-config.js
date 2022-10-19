angular.module('adminApp').controller("UploadConfigCtrl", ['$scope', 'controllerHelper', 'uiGridConstants', '$timeout', '$filter', function ($scope, controllerHelper, uiGridConstants, $timeout, $filter) {
    controllerHelper.openModal($scope);

    $scope.selectedItem = {};
    $scope.currentGridRow = {};
    $scope.parentMenuList = [];
    $scope.paramTypes = ["Dropdown", "Text", "Checkbox", "Label", "DatePicker"];
    $scope.user = "";

    var init = function () {
        controllerHelper.apiGet('api/adminGetUploadConfigInits', null, function (res) {
            $scope.user = res.data.user;
            $scope.uploadConfigGrid.data = res.data.uploadConfigList;
            $scope.adGroupList = res.data.adGroupList;
            $scope.parentMenuList = res.data.parentMenu;

            controllerHelper.closeModal();
        });
    }

    $scope.uploadConfigGrid = {
        enableFiltering: true,
        enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
        enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
        paginationPageSize: 15,
        enablePaginationControls: false,
        columnDefs: [
            { field: 'Name', displayName: 'Name', type: 'string', enableColumnMenu: false, width: '***', filter: { condition: uiGridConstants.filter.CONTAINS, flags: { caseSensitive: false } } },
            { field: 'PackageName', displayName: 'Package', type: 'string', cellTooltip: true, enableColumnMenu: false, width: 230, filter: { condition: uiGridConstants.filter.CONTAINS, flags: { caseSensitive: false } } },
            { field: 'UpdatedBy', displayName: 'Updated By', cellTooltip: true, type: 'string', enableColumnMenu: false, width: 130, filter: { condition: uiGridConstants.filter.CONTAINS, flags: { caseSensitive: false } } },
            { field: 'UpdatedDate', displayName: 'Updated Date', cellTemplate: '<div ng-show="row.entity.UpdatedDate != \'0001-01-01T00:00:00\'" style="padding-top:5px">{{row.entity.UpdatedDate | date:"dd MMM yyyy"}}</div>', enableColumnMenu: false, width: 100, type: "date", filter: { condition: dateStringFilter } },
            { field: ' ', displayName: ' ', cellTemplate: '<button class="btn btn-warning btn-sm" ng-click="grid.appScope.editConfig(row)" title="Edit"><i class="glyphicon glyphicon-pencil"></i></button> <button class="btn btn-danger btn-sm" ng-click="grid.appScope.deleteConfig(row)"><i class="glyphicon glyphicon-trash"></i></button>', enableColumnMenu: false, width: 75, enableFiltering: false, enableSorting: false, type: "string" }
        ],
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    }

    $scope.addConfig = function () {
        $scope.currentGridRow = { Params: [] };
        $scope.selectedItem = { Id: 0, Params: [] };
        controllerHelper.openModal($scope, null, 'lg', null, '/App/Admin/Views/UploadConfigEntityTmpl.html');
    }

    $scope.editConfig = function (row) {
        $scope.modalOptions = null;
        controllerHelper.openModal($scope);

        controllerHelper.apiGet('api/adminGetUploadConfig?configId=' + row.entity.Id, null, function (res) {
            $scope.currentGridRow = row.entity;
            $scope.selectedItem = res.data;

            controllerHelper.closeModal();
            $timeout(function () {
                controllerHelper.openModal($scope, null, 'lg', null, '/App/Admin/Views/UploadConfigEntityTmpl.html');
            });
        });
    }

    $scope.saveConfig = function (isValid) {
        if (isValid) {
            var selection = $scope.selectedItem;
            controllerHelper.apiPost('api/adminSaveUploadConfig', $scope.selectedItem, function (res) {
                var item = $filter('filter')($scope.uploadConfigGrid.data, { Id: res.data }, true)[0];
                var roleName = $filter('filter')($scope.adGroupList, { Id: selection.RoleId }, true)[0].Name;
                if (item) {
                    item.Name = selection.Name;
                    item.PackageName = selection.PackageName;
                    item.ADGroup = roleName;
                    item.UpdatedBy = $scope.user;
                    item.UpdatedDate = new Date();
                }
                else {
                    $scope.uploadConfigGrid.data.push({ Id: res.data, Name: selection.Name, ADGroup: roleName, PackageName: selection.PackageName, UpdatedBy: $scope.user, UpdatedDate: new Date() });
                }
            });
            $scope.selectedItem = {};
            $scope.currentGridRow = {};
            controllerHelper.closeModal();
        }
    }

    $scope.deleteConfig = function (row) {
        $scope.modalOptions = {
            body: 'Do you want to delete ' + row.entity.Name + '?',
            ok: true,
            cancel: true
        };

        controllerHelper.openModal($scope, function () {
            controllerHelper.apiGet('api/adminDeleteUploadConfig?configId=' + row.entity.Id, null, function (res) {

            });
            $scope.modalOptions = null;
            $scope.uploadConfigGrid.data.splice($scope.uploadConfigGrid.data.indexOf(row.entity), 1);
        });
    }

    $scope.$watch("selectedItem.Name", function (val) {
        try {
            var parentMenu = '';
            if ($scope.selectedItem.ParentMenuId != null && !$scope.selectedItem.ParentMenuId != undefined) {
                parentMenu = $filter('filter')($scope.parentMenuList, { Id: $scope.selectedItem.ParentMenuId }, true)[0].ModulePath;
            }
            $scope.selectedItem.ModulePath = parentMenu + '/' + $filter('slugify')(val);
        } catch(ex){}
    });

    $scope.$watch("selectedItem.ParentMenuId", function (val) {
        try {
            var parentManu = '';
            if (val != null || val != undefined) {
                parentManu = $filter('filter')($scope.parentMenuList, { Id: val }, true)[0].ModulePath;
            }
            $scope.selectedItem.ModulePath = parentManu + '/' + $filter('slugify')($scope.selectedItem.Name);
        } catch (ex){}
    });

    $scope.addParam = function () {
        if ($scope.selectedItem.Params == undefined) $scope.selectedItem.Params = [];

        var ln = $scope.selectedItem.Params.length + 1;
        for (var x in $scope.selectedItem.Params) {
            $scope.selectedItem.Params[x].Edit = false;
        }
        $scope.selectedItem.Params.push({ Name: '', Type: '', ViewColumn: '', DefaultValue: '', IsRequired: false, SequenceNumber: ln, Edit: true });
    }

    $scope.editParam = function (item) {
        for (var x in $scope.selectedItem.Params) {
            $scope.selectedItem.Params[x].Edit = false;
        }
        item.Edit = true;
    }

    $scope.deleteParam = function (item) {
        event.preventDefault();
        $scope.selectedItem.Params.splice($scope.selectedItem.Params.indexOf(item), 1);

        for (var x in $scope.selectedItem.Params) {
            $scope.selectedItem.Params[x].SequenceNumber = parseInt(x) + 1;
        }
    }

    $scope.moveParamUp = function (item) {
        event.preventDefault();
        var old = controllerHelper.getRowsByValue($scope.selectedItem.Params, "SequenceNumber", item.SequenceNumber - 1)[0];
        old.SequenceNumber++;
        item.SequenceNumber--;
    }

    $scope.moveParamDown = function (item) {
        event.preventDefault();
        var next = controllerHelper.getRowsByValue($scope.selectedItem.Params, "SequenceNumber", item.SequenceNumber + 1)[0];
        next.SequenceNumber--;
        item.SequenceNumber++;
    }

    init();
}]);