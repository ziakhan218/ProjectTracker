angular.module('adminApp').controller('UserManagerCtrl', ['$scope', '$http', '$timeout', '$sce', '$mdDialog', '$mdToast', 'controllerHelper', function ($scope, $http, $timeout, $sce, $mdDialog, $mdToast, controllerHelper) {
    $scope.isProcessing = true;
    $scope.submitted = false;
    $scope.isEdit = false;
    $scope.isShow = false;
    $scope.isShowAll = false;
    $scope.isShowAddNew = true;
    $scope.selectUserDisable = false;
    $scope.showDeletedUsers = false;
    $scope.messageDialog = "";
    $scope.IsUpdate = false;


    var init = function () {
        $scope.getAllUsers();
    }

    $scope.getAllUsers = function () {
        if ($scope.showDeletedUsers) {
            $scope.isProcessing = true;
            controllerHelper.apiGet("api/allActiveUsers?isActive=false", null, function (result) {
                $scope.users = result.data;
                $scope.isProcessing = false;
            }, function (res) {
                $scope.isProcessing = false;
                $scope.showCustomDialog('Something went wrong!','alert');
            });
            
        }
        else {
            $scope.isProcessing = true;
            controllerHelper.apiGet("api/allActiveUsers?isActive=true", null, function (result) {
                $scope.users = result.data;
                $scope.isProcessing = false;
            }, function (res) {
                $scope.isProcessing = false;
                $scope.showCustomDialog('Something went wrong!', 'alert');
            });
        }
    }

    $scope.saveUser = function () {
        var userVal = $scope.userName.indexOf('\\') !== -1;
        if ($scope.manageUserForm.$valid && userVal) {

            var ddUser = $scope.selectedUser != null ? $scope.selectedUser.Id : 0;
            var data = {
                userId: ddUser,
                FirstName: $scope.firstName,
                LastName: $scope.lastName,
                UserName: $scope.userName,
                EmailAddress: $scope.emailAddress
            };
            controllerHelper.apiPost("api/saveUser?isUpdate=" +$scope.IsUpdate, data, function (result) {
                $scope.isProcessing = true;
                if (result.data) {
                    $scope.showSimpleToast('User details saved successfully!', 'top right', 3000, 'OK');
                    $scope.firstName = $scope.lastName = $scope.userName = $scope.emailAddress = '';
                    $scope.selectedUser = null;
                    $scope.isShow = false;
                    $scope.disableControl();
                    $scope.submitted = false;
                    init();
                }
                else {
                    $scope.showSimpleToast('Error while saving user details. Please try again!', 'top right', 3000, 'OK');
                }
                $scope.selectUserDisable = false;
                $scope.showDeletedUsers = false;
                $scope.isShow = false;
                $scope.isShowAll = false;
                $scope.isShowAddNew = true;
                $scope.isProcessing = false;
                $scope.manageUserForm.$setPristine();
            }, function (res) {
                $scope.showSimpleToast('Error while saving user details. Please try again!', 'top right', 3000, 'OK');
                $scope.isProcessing = false;
            });
        }
        else {
            if (!userVal) {
                $scope.manageUserForm.userName.$valid = false;
            }
            $scope.submitted = true;
            return false;
        }
    };

    $scope.changeUser = function () {
       
        if ($scope.selectedUser == undefined) {
            $scope.resetFormValues();
            $scope.isShow = false;
            $scope.disableControl();
        }
        else if ($scope.selectedUser > 0) {
            $scope.isProcessing = true;
            $scope.isShow = true;
            $scope.disableControl();

            controllerHelper.apiGet("api/userInfo?userId=" + $scope.selectedUser, null, function (res) {
                $scope.isShow = true;
                $scope.isShowAll = false;
                $scope.isShowAddNew = true;
                $scope.firstName = res.data.FirstName;
                $scope.lastName = res.data.LastName;
                $scope.userName = res.data.UserName;
                $scope.emailAddress = res.data.EmailAddress;
                $scope.isProcessing = false;
            }, function (res) {
                $scope.isProcessing = false;
                $scope.showSimpleToast('Error in fetching data. Please try again!', 'top right', 3000, 'OK');
            });
        }
        else if ($scope.selectedUser <= 0) {
            $scope.disableControl();
            $scope.isShow = false;
        }
    };
    
    $scope.resetFormValues = function () {

        $scope.selectedUser = null;
        $scope.firstName = $scope.lastName = $scope.userName = $scope.emailAddress = '';
        $scope.isShow = false;
        $scope.isShowAll = false;
        $scope.isShowAddNew = true;
        $scope.manageUserForm.$setPristine();
        $scope.disableControl();
        $scope.selectUserDisable = false;
        $scope.showDeletedUsers = false;
    }

    $scope.addNewUser = function () {
        $scope.firstName = $scope.lastName = $scope.userName = $scope.emailAddress = '';
        $scope.selectedUser = $scope.selectedPatch = $scope.selectedLocation = null;
        $scope.isShow = false;
        $scope.isShowAll = true;
        $scope.isShowAddNew = false;
        $scope.isEdit = false;
        $scope.enableControl();
        $scope.selectUserDisable = true;
        $scope.showDeletedUsers = false;
    };

    $scope.editUser = function () {
        $scope.enableControl();
        $scope.isEdit = true;
        $scope.isShowAll = true;
        $scope.isShow = false;
        $scope.isShowAddNew = false;
        $scope.selectUserDisable = true;
        $scope.showDeletedUsers = false;
        $scope.IsUpdate = true;
    };
    
    $scope.setUserState = function () {
        $scope.isEdit = false;
        var ddUser = $scope.selectedUser != null ? $scope.selectedUser : 0;
        var data = {
            userId: ddUser,
            IsActive: false
        };
        if ($scope.showDeletedUsers) {
            data.IsActive = true;
        }
       
        controllerHelper.apiPost("api/setUserState", data, function (result) {
            if (result.data) {
                if ($scope.showDeletedUsers == true) $scope.showSimpleToast('User Restored successfully.', 'top right', 3000, 'OK');
                else $scope.showSimpleToast('User Deleted successfully.', 'top right', 3000, 'OK');
                    
                $scope.disableControl();
                $scope.isShow = false;
                $scope.firstName = $scope.lastName = $scope.userName = $scope.emailAddress = '';
                $scope.selectedUser = null;
                init();
            }
            else {
                $scope.showSimpleToast('Error while attempting to delete the user. Please try again!', 'top right', 3000, 'OK');
            }
            $scope.isProcessing = false;
            $scope.manageUserForm.$setPristine();
            $scope.selectUserDisable = false;
            $scope.showDeletedUsers = false;
        }, function (res) {
            $scope.showSimpleToast('Error while attempting to delete the user. Please try again!', 'top right', 3000, 'OK');
            $scope.isProcessing = false;
        });
       
    };
    
    $scope.disableControl = function () {
        $scope.userSection = true;
    }

    $scope.enableControl = function () {
        $scope.userSection = false;
    }

    $scope.validateUserName = function () {
        if ($scope.selectedUser == null) {
            $scope.isProcessing = true;
            controllerHelper.apiGet("api/IsUserNameAlreadyTaken?userName=" + $scope.userName, null, function (res) {
                if (res.data) {
                    $scope.isProcessing = false;                    
                    $scope.showSimpleToast('A user with the username <b>' + $scope.userName + ' </b>already exist in the database. Please change User Name.', 'top right', 3000, 'OK');
                } else {
                    $scope.isProcessing = false;
                    $scope.saveUser();
                }
                }, function (res) {
                $scope.isProcessing = false;
            });
        }
        else {
            $scope.isProcessing = false;
            $scope.saveUser();
        }
    }

    $scope.showConfirmDialog = function (ev, title, textContent, ariaLabel, Ok, cancel) {
        var confirm = $mdDialog.confirm()
              .title(title)
              .textContent(textContent)
              .ariaLabel(ariaLabel)
              .targetEvent(ev)
              .ok(Ok)
              .cancel(cancel);

        $mdDialog.show(confirm).then(function () {
            $scope.resetFormValues();
        }, function () {
            //Canceled
        });
    };

    $scope.showSimpleToast = function (content, position, duration, action) {
        $mdToast.show(
          $mdToast.simple()
            .textContent(content)
            .position(position)
            .hideDelay(2000) // assign duration variable if duration changes for different toasts based on scenarios.
            .action(action)
        );
    };
    
    $scope.showCustomDialog = function (messageDialog, title, action) {
        $mdDialog.show({
            controller: CustomDialogController,
            templateUrl: 'App/Admin/Views/dialog.tmpl.html',
            parent: angular.element(document.body),
            clickOutsideToClose: true,
            fullscreen: false, // Only for -xs, -sm breakpoints.
            locals: {
                msgDialog: messageDialog,
                title: title,
                action : action
            }
        })
        .then(function (answer) {
            if (action == 'callReset') {
                $scope.resetFormValues();
            }
            
            if (action == 'callSetUserState') {
                $scope.setUserState();
            }
        }, function () {
            
        });
    };

    function CustomDialogController($scope, $mdDialog, msgDialog, title, action) {
        $scope.messageDialog = msgDialog;
        $scope.messageTitle = title;

        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };
    }

    $scope.disableControl();
    init();
   
}]);