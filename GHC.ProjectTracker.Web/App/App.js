var commonModule = angular.module('common', ['ngRoute', 'ui.grid', 'ui.grid.edit', 'ui.grid.rowEdit', 'ui.grid.cellNav']);
var mainModule = angular.module('main', ['common']);

commonModule.factory("controllerHelper", ['$http', '$q', '$window', '$location', '$uibModal', '$uibModalStack', '$timeout', function ($http, $q, $window, $location, $uibModal, $uibModalStack, $timeout) {
    return MyApp.viewModelHelper($http, $q, $window, $location, $uibModal, $uibModalStack, $timeout);
}]);


(function (myApp) {
    var viewModelHelper = function ($http, $q, $window, $location, $uibModal, $uibModalStack, $timeout) {
        var self = this;

        self.modelIsValid = true;
        self.modelErrors = [];

        self.resetModelErrors = function () {
            self.modelErrors = [];
            self.modelIsValid = true;
        }

        self.apiGet = function (uri, data, success, failure, always) {
            self.modelIsValid = true;
            $http.get(MyApp.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always != null)
                        always();
                }, function (result) {
                    if (failure != null) {
                        failure(result);
                    }
                    else {
                        var errorMessage = result.status + ':' + result.statusText;
                        if (result.data != null && result.data.Message != null)
                            errorMessage += ' - ' + result.data.Message;
                        self.modelErrors = [errorMessage];
                        self.modelIsValid = false;
                    }
                    if (always != null)
                        always();
                });
        }

        self.apiPost = function (uri, data, success, failure, always) {
            self.modelIsValid = true;
            $http.post(MyApp.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always != null)
                        always();
                }, function (result) {
                    if (failure != null) {
                        failure(result);
                    }
                    else {
                        var errorMessage = result.status + ':' + result.statusText;
                        if (result.data != null && result.data.Message != null)
                            errorMessage += ' - ' + result.data.Message;
                        self.modelErrors = [errorMessage];
                        self.modelIsValid = false;
                    }
                    if (always != null)
                        always();
                });
        }

        self.goBack = function () {
            $window.history.back();
        }

        self.navigateTo = function (path) {
            $location.path(MyApp.rootPath + path);
        }

        self.refreshPage = function (path) {
            $window.location.href = MyApp.rootPath + path;
        }

        self.clone = function (obj) {
            return JSON.parse(JSON.stringify(obj))
        }

        self.openModal = function (scope, callback, size, tmpl, tmplUrl) {
            var modalInstance;
            if (tmplUrl)
            {
                modalInstance = $uibModal.open({
                    templateUrl: tmplUrl,
                    size: size || 'sm',
                    windowClass: 'amp-face',
                    backdrop: 'static',
                    keyboard: false,
                    scope: scope
                });
            }
            else {
                var tpl = '<div class="modal-header" ng-show="modalOptions.title">';
                tpl += '   <strong class="modal-title">{{modalOptions.title}}</strong>';
                tpl += '</div>';
                tpl += '<div class="modal-body" ng-show="modalOptions.body" ng-bind-html="modalOptions.body | html"></div>';
                tpl += '<div class="modal-body" ng-hide="modalOptions.body"><img src="/Images/ajax-loader.gif" style="height:32px; width:32px;" /> Please wait while processing...</div>';
                tpl += '<div class="modal-footer" ng-show="modalOptions.ok || modalOptions.cancel">';
                tpl += '   <button class="btn btn-primary" type="button" ng-show="modalOptions.ok" ng-click="$close()">OK</button>';
                tpl += '   <button class="btn btn-default" type="button" ng-show="modalOptions.cancel" ng-click="$dismiss()">Cancel</button>';
                tpl += '</div>';

                modalInstance = $uibModal.open({
                    template: tmpl ? tmpl : tpl,
                    size: size || 'sm',
                    windowClass: 'amp-face',
                    backdrop: 'static',
                    keyboard: false,
                    scope: scope
                });
            }

            if(callback) modalInstance.result.then(callback);
        };

        self.closeModal = function (reason) {
            $timeout(function () {
                $uibModalStack.dismissAll(reason);
            });
        };

        self.getRowsByValue = function (array, columnToCompare, valueToCompare) {
            if (array === undefined) return [];
            var result = [];
            for (var i = 0; i < array.length; i++) if (jQuery.trim(array[i][columnToCompare]) === jQuery.trim(valueToCompare)) result.push(array[i]);
            return result.sort();
        }

        self.getColumnSum = function (array, columnToSum) {
            if (array === undefined) return 0;
            var result = 0;
            for (var i = 0; i < array.length; i++) result += parseFloat(jQuery.trim(array[i][columnToSum]));
            return result;
        }

        return this;
    };
    myApp.viewModelHelper = viewModelHelper;
}(window.MyApp));

commonModule.filter('ampDateFormat', function ($filter) {
    return function (input) {
        if (input) {
            return $filter('date')(input, 'dd MMM yyyy');
        }
        return null;
    }
});

commonModule.filter('negativeToBrackets', function ($filter) {
    return function (input, hideZero) {
        if (input == 0 && hideZero) {
            return '';
        }
        else if (input < 0) {
            return "(" + Math.abs(input) + ")";
        }
        else {
            return input;
        }
    }
});

commonModule.filter('ampRound', function ($filter) {
    return function (input) {
        return input >= 0 ? Math.round(input) : input;
    }
});

function convertUTCDateToLocalDate(date) {
    var newDate = new Date(date.getTime() + date.getTimezoneOffset() * 60 * 1000);

    var offset = date.getTimezoneOffset() / 60;
    var hours = date.getHours();

    newDate.setHours(hours - offset);

    return newDate;
}

commonModule.filter('ampLocaleDateTime', function ($filter) {
    return function (input) {
        if (input) {
            return $filter('date')(convertUTCDateToLocalDate(new Date(input)), 'dd MMM yyyy hh:mm a');
        }
        return null;
    }
});

commonModule.filter('ampBoolFormat', function ($filter) {
    return function (text, length, end) {
        if (text) {
            return 'Yes';
        }
        return 'No';
    }
});

commonModule.filter('NaNToZero', function ($filter) {
    return function (input) {
        return isNaN(input) || input === undefined ? 0 : input;
    }
});

commonModule.filter('html', function ($sce) { return $sce.trustAsHtml; });

function dateStringFilter(searchTerm, cellValue) {
    return (new Date(cellValue).ddMyy().toLowerCase().indexOf(searchTerm.toLowerCase()) > -1);
}

commonModule.filter('unique', function() {
   return function(collection, keyname) {
      var output = [], 
          keys = [];

      angular.forEach(collection, function(item) {
          var key = item[keyname];
          if(keys.indexOf(key) === -1) {
              keys.push(key);
              output.push(item);
          }
      });

      return output;
   };
});
   
commonModule.filter('groupBy', function(){
    return function(list, group_by) {

        var filtered = [];
        var prev_item = null;
        var group_changed = false;
        // this is a new field which is added to each item where we append "_CHANGED"
        // to indicate a field change in the list
        var new_field = group_by + '_CHANGED';

        // loop through each item in the list
        angular.forEach(list, function(item) {

            group_changed = false;

            // if not the first item
            if (prev_item !== null) {

                // check if the group by field changed
                if (prev_item[group_by] !== item[group_by]) {
                    group_changed = true;
                }

                // otherwise we have the first item in the list which is new
            } else {
                group_changed = true;
            }

            // if the group changed, then add a new field to the item
            // to indicate this
            if (group_changed) {
                item[new_field] = true;
            } else {
                item[new_field] = false;
            }

            filtered.push(item);
            prev_item = item;

        });
    }
});


Date.prototype.ddMyy = function () {
    var monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var yyyy = this.getFullYear().toString();
    var mm = (monthNames[this.getMonth()]);
    var dd = this.getDate().toString();
    return (dd[1] ? dd : "0" + dd[0]) + ' ' + (mm[1] ? mm : "0" + mm[0]) + ' ' + yyyy;
};

Number.prototype.toDate = function () {
    var monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

    return monthNames[this];
};