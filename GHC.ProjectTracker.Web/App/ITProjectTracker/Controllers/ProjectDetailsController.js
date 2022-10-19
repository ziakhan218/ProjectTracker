angular.module('ITProjectTrackerApp').controller("ProjectDetailsCtrl", ['$scope', '$location', '$routeParams', 'sharedModels', '$window', 'controllerHelper', 'uiGridConstants', 'sharedModels', function ($scope, $location, $routeParams,sharedModels, $window, controllerHelper, uiGridConstants, sharedModels) {
   
    $scope.editing = false;
    $scope.UpdateRecord = false;    
    $scope.project = {};    
    var count = 1;
    $scope.reviewStr = null;
    $scope.Active = false;
    $scope.documentationStr = null;
    $scope.OpcoStr = null    
    $scope.projectDetails = [];
    $scope.selectedIds = {};
    $scope.dropdownOpts = [];
    $scope.lookups = {
        Category: [],
        Reviews: [],
        OpCo: [],
        StrategyLevel: [],
        Priority: [],
        StrategyGroup: [],
        DropDownStatus: [],
        RequiredDocumentation: [],
        SystemTypeName: [],
        ProjectType: [],
        Status: [],
        ExternalCostCurrency: [],
        SystemsStrategy: [],
        ProjectRequest: [],
        RiskRegisterFlag: [],
        ChangeRegisterFlag: [],
        TestPlanFlag: [],
        ProjectPlanFlag: [],
        NewOrExisting: []
    };
    $scope.users = [];
    $scope.opCo = [];
    $scope.reviews = {};
    $scope.Review = [];
    $scope.reqDoc = [];
    $scope.projectName = $routeParams.Id;
  
    $scope.Edit = function () {
        $scope.UpdateRecordbutton = true;
        $scope.UpdateRecord = true;
        $scope.editing = true;
    }
    $scope.Close = function () {
        $scope.UpdateRecordbutton = false;
        $scope.editing = false;
        $scope.editMode = false;        
        $scope.UpdateRecord = false;
        $scope.Active = false;
        $scope.UpdateRecord = false;
    }
    $scope.Cancil = function () {
        $location.url('/ITProjectTracker');
    }

    $scope.CheckName= function () {
        controllerHelper.apiGet('api/ProjectName?projectName=' + $scope.project.ProjectName, null, function (res) {
            if (res.data) {
                $scope.ErrorMessage = "This name is already Exist";
                $scope.Active = true;
            }
            else {
                $scope.Active = false;
                $scope.ErrorMessage = null;
            }
            
           
        }, function (res) {
            $scope.isProcessing = false;
            $scope.modalOptions = {
                title: 'Error',
                body: 'Some error occured while fetching data from server.',
                ok: true
            };
        });
    }

    var num2 = 0;
    
    $scope.GetReivew = function (i, num) {
        if (!$scope.UpdateRecord) {
            if (count % 2 == 0) {
                $scope.Review.push(i);
                if (num2 != num) {
                    $scope.reviewStr = $scope.Review.toString();
                    ++num2;
                }

                count++;
            }
            else {
                count++;

            }
        }
    }
   var num3 = 0
   var countDocument = 1;
    $scope.Documentation = [];
    $scope.ReqDocumentation = function (name, num) {
        if (!$scope.UpdateRecord) {
            if (countDocument % 2 == 0) {
                $scope.Documentation.push(name);
                if (num3 != num) {
                    $scope.documentationStr = $scope.Documentation.toString();                    
                    ++num3;
                }

                countDocument++;

            }
            else {
                countDocument++;

            }
        }
    }
    var filterLookupCategory = function (lookupItemList, lookupCategory) {
        return lookupItemList.filter(function (el) {
            return el.Category == lookupCategory;
        });
    }
    var num4 = 0
    var countOpco = 1;    
    $scope.OpCoarray = [];
    $scope.getOpco = function (name, num) {
        if (!$scope.UpdateRecord) {
            if (countOpco % 2 == 0) {
                $scope.OpCoarray.push(name);
                if (num4 != num) {
                    $scope.OpcoStr = $scope.OpCoarray.toString();
                    console.log($scope.OpcoStr);
                    ++num4;
                }
                countOpco++;
            }
            else {
                countOpco++;

            }
        }
    }
    var filterLookupCategory = function (lookupItemList, lookupCategory) {
        return lookupItemList.filter(function (el) {
            return el.Category == lookupCategory;
        });
    }
    $scope.getDropdownOpt = function () {

        var categories = ["ProjectTracker.TestPlanFlag", "ProjectTracker.ProjectPlanFlag", "ProjectTracker.NewOrExisting", "Review", "ProjectTracker.ProjectType", "ProjectTracker.SystemsStrategy", "ProjectTracker.Priority", "ProjectTracker.StrategyLevel", "ProjectTracker.StrategyGroup", "OpCo", "Category", "ProjectTracker.RequiredDocumentation", "ProjectTracker.SystemType", "ProjectTracker.Status", "ExternalCostCurrency", "ProjectTracker.ProjectRequest", "ProjectTracker.RiskRegisterFlag", "ProjectTracker.ChangeRegisterFlag"];
        controllerHelper.apiGet('api/getDropdownOpt', {
            params: {
                categoryList: categories
            }
        },
           function (result) {

               $scope.lookups.OpCo = filterLookupCategory(result.data, 'OpCo');
               $scope.lookups.Reviews = filterLookupCategory(result.data, 'Review');
               $scope.lookups.Category = filterLookupCategory(result.data, 'Category');
               $scope.lookups.StrategyLevel = filterLookupCategory(result.data, 'ProjectTracker.StrategyLevel');
               $scope.lookups.Priority = filterLookupCategory(result.data, 'ProjectTracker.Priority');
               $scope.lookups.StrategyGroup = filterLookupCategory(result.data, 'ProjectTracker.StrategyGroup');
               $scope.lookups.RequiredDocumentation = filterLookupCategory(result.data, 'ProjectTracker.RequiredDocumentation');
               $scope.lookups.ProjectType = filterLookupCategory(result.data, 'ProjectTracker.ProjectType');
               $scope.lookups.SystemTypeName = filterLookupCategory(result.data, 'ProjectTracker.SystemType');
               $scope.lookups.Status = filterLookupCategory(result.data, 'ProjectTracker.Status');
               $scope.lookups.ExternalCostCurrency = filterLookupCategory(result.data, 'ExternalCostCurrency');
               $scope.lookups.SystemsStrategy = filterLookupCategory(result.data, 'ProjectTracker.SystemsStrategy');
               $scope.lookups.ProjectRequest = filterLookupCategory(result.data, 'ProjectTracker.ProjectRequest');
               $scope.lookups.RiskRegisterFlag = filterLookupCategory(result.data, 'ProjectTracker.RiskRegisterFlag');
               $scope.lookups.ChangeRegisterFlag = filterLookupCategory(result.data, 'ProjectTracker.ChangeRegisterFlag');
               $scope.lookups.TestPlanFlag = filterLookupCategory(result.data, 'ProjectTracker.TestPlanFlag');
               $scope.lookups.ProjectPlanFlag = filterLookupCategory(result.data, 'ProjectTracker.ProjectPlanFlag');
               $scope.lookups.NewOrExisting = filterLookupCategory(result.data, 'ProjectTracker.NewOrExisting');
               $scope.projectParam = {
                   ProjectName: $scope.projectName
               }
               controllerHelper.apiPost("api/saveWorkItem", $scope.projectParam, function (result) {})
               
           },
           function (result) {
               $scope.lookups = {
                   Category: [],
                   Reviews: [],
                   OpCo: [],
                   StrategyLevel: [],
                   Priority: [],
                   StrategyGroup: [],                   
                   RequiredDocumentation: [],
                   SystemTypeName: [],
                   ProjectType: [],
                   Status: [],
                   ExternalCostCurrency: [],
                   SystemsStrategy: [],
                   ProjectRequest: [],
                   RiskRegisterFlag: [],
                   ChangeRegisterFlag: [],
                   TestPlanFlag: [],
                   ProjectPlanFlag: [],
                   NewOrExisting: []
               };
           });
    }

    //Get All Users
    $scope.getAllUsers = function () {
        
        controllerHelper.apiGet("api/allActiveUsers", null, function (result) {
            $scope.users = result.data;
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

    $scope.getAllOpCo = function () {        
        controllerHelper.apiGet('api/getAllIds?Id=' + $scope.project.Id, null, function (res) {
            $scope.selectedIds = res.data;
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

    $scope.getProjectById = function () {
        var name = $scope.projectName.split('_').join(' ');
        controllerHelper.apiGet('api/getProjectByName?name=' + name, null, function (res) {
            $scope.selectedIds = res.data;
            $scope.project = res.data;            
            localStorage.setItem("projectName", $routeParams.Id);
            localStorage.setItem("projectPath", res.data.Path);
            sharedModels.data = res.data;
            console.log(sharedModels.data);
            $scope.getAllOpCo();
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

    //DatePicker 
    $scope.datepicker = false;
    $scope.BaselineGoLiveDate = false;
    $scope.TargetGoLiveDate = false;
    $scope.GW1ProjectBriefApprovalDate = false;
    $scope.GW2ProjectBriefApprovalDate = false;
    $scope.GW4GoLiveApprovalDate = false;
    $scope.GW5ProjectClosureApprovalDate = false;
    $scope.StartYear = false;
    $scope.GW0ProjectKickOffDate = false;
    $scope.GW3DesignDate = false;
    $scope.ProjectAddedDate = false;

    $scope.open = function ($event, dtCtrlName) {
        switch (dtCtrlName) {
            case 'BaselineGoLiveDate':
                $scope.BaselineGoLiveDate = true;
                break;
            case 'TargetGoLiveDate':
                $scope.TargetGoLiveDate = true;
                break;
            case 'GW1ProjectBriefApprovalDate':
                $scope.GW1ProjectBriefApprovalDate = true;
                break;
            case 'GW2ProjectBriefApprovalDate':
                $scope.GW2ProjectBriefApprovalDate = true;
                break;
            case 'GW4GoLiveApprovalDate':
                $scope.GW4GoLiveApprovalDate = true;
                break;
            case 'GW5ProjectClosureApprovalDate':
                $scope.GW5ProjectClosureApprovalDate = true;
                break;
            case 'StartYear':
                $scope.StartYear = true;
                break;
            case 'GW0ProjectKickOffDate':
                $scope.GW0ProjectKickOffDate = true;
                break;
            case 'GW3DesignDate':
                $scope.GW3DesignDate = true;
                break;
            case 'ProjectAddedDate':
                $scope.ProjectAddedDate = true;
                break;
        }
    }

    $scope.dateYearOptions = {
        formatYear: 'yyyy',
        startingDay: 1,
        minMode: 'year'
    };
    //Save Project
    $scope.saveProject = function () {
        controllerHelper.openModal($scope);
        $scope.projectParams = {
            Project: $scope.project,
            Review: $scope.selectedIds.Review,
            OpCo: $scope.selectedIds.OpCoId,
            ReqDoc: $scope.selectedIds.DocumentaionId            
        }
        controllerHelper.apiPost("api/saveProject", $scope.projectParams, function (result) {
            controllerHelper.closeModal();
            $location.path("/ProjectDetails/" + $scope.project.ProjectName.split(' ').join('_')).replace().reload(false);
            
        }, function (res) {
            
            $scope.modalOptions = {
                title: 'Error',
                body: 'Some error occured while updating data to server.',
                ok: true
            };
        });
    }

    $scope.init = function () {
        $scope.getDropdownOpt();
        $scope.getAllUsers();

    }
    if ($routeParams.Id != null) {
        $scope.UpdateRecord = false;


        $scope.getProjectById();
    }
    else {
        UpdateRecordbutton = false;
        $scope.editing = true;
        $scope.editMode = true;
        $scope.NewRecord = true;
        $scope.UpdateRecord = true;
    }
   
    $scope.init();
}]);