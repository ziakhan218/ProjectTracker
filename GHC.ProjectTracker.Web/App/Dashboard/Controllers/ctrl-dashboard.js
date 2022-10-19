angular.module('dashboardApp').controller("DashboardCtrl", ['$scope', '$rootScope', '$timeout', '$compile', 'controllerHelper', 'uiGridConstants', '$location',
    function ($scope, $rootScope, $timeout, $compile, controllerHelper, uiGridConstants, $location) {
       
        $scope.user = {};

        $scope.show = false;
        $scope.isUserHaveRole = true;
        $scope.isDashboardLoaded = false;

        $scope.userHaveGBIOpCo = false;
        $scope.userHaveEDPTeam = false;
        $scope.userHaveSustainabilityTeam = false;
        $scope.loading_priorityChart = false;
        $scope.loading_priorityGrid = false;
        $scope.loading_statusChart = false;
        $scope.loading_statusGrid = false;

        $scope.loading_summaryInfoPanel = false;
        $scope.loading_sustainabilityPanel = false;
        $scope.loading_dataIssuesPanel = false;
        $scope.loading_forecastApprovalsPanel = false;
        $scope.loading_forcastDetails = false;
        $scope.loading_headlineFinancialPanel = false;

        $scope.selectedView = {};       //WRONG please use ui-bootstrap modal

        $scope.selectedDashboard = '';

        $scope.performanceDashboard = null;
        $scope.ProjectPrioritiesData = [];
        $scope.projectPrioritiesGrid = null;
        $scope.priorityChartHeading = "";
        $scope.statusChartHeading = "";

        $scope.SummaryInfo = {};
        $scope.currentSummaryInfoGrid = [];

        $scope.Sustainability = {};
        $scope.currentSustainabilityGrid = [];

        $scope.dataIssues = {};
        $scope.negtiveDataIssues = {};
        $scope.dateIssues = {};
        $scope.pastDateIssues = {};
        $scope.currentNegtiveValueGridData = [];
        $scope.currentDateIssuesGridData = [];
        $scope.currentPastDateIssuesGridData = [];

        $scope.currentPastDateIssuesCount = 0;
        $scope.currentDateIssuesCount = 0;
        $scope.currentNegtiveValueCount = 0;

        $scope.forecastApprovals = {};
        $scope.currentForecastApprovalGrid = [];
        $scope.monthsArray = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

        $scope.HeadlineFinancial = {};
        $scope.currentHeadlineFinancialGrid = [];

        var initializeDashboardCharts = function () {
            getUserInformation();
        }

        var initializePanels = function () {
            if ($scope.isUserHaveRole) {
                if ($scope.userHaveEDPTeam) {
                    $scope.getSummaryInfoData();
                    $scope.getDashboardForecastApprovals();
                    $scope.getDashboardHeadlineFinancial();
                    $scope.getDashboardDataIssues();
                    $scope.getProjectPrioritiesChartData();
                    $scope.getProjectStatusChartData();
                }
                $scope.getDashboardSustainabilityData();
            }
        }

        var getUserInformation = function () {
            controllerHelper.apiGet('api/getUserInformation', null,
            function (result) {
                $scope.user = jQuery.parseJSON(result.data);
                $scope.isDashboardLoaded = true;
                if ($scope.user.UserConfigList.length > 0) {
                    $scope.isUserHaveRole = true;
                    $scope.checkUserHaveGBIOpCo($scope.user.UserConfigList);
                    $scope.checkUserHaveEDPTeam($scope.user.UserConfigList);
                    $scope.checkUserHaveSustainabilityTeam($scope.user.UserConfigList);
                } else {
                    $scope.isUserHaveRole = true;
                }
                initializePanels();
            },
            function (result) {
            });
        }

        google.charts.load('45', { packages: ['bar', 'corechart'] }); // Using this static version as an update from Google broke the charts in IE :(
        google.charts.setOnLoadCallback(function () {
            initializeDashboardCharts();
        });

        $scope.checkUserHaveGBIOpCo = function (userConfigList) {
            for (var i = 0; i < userConfigList.length; i++) {
                //73 GBI OpCo Id
                if (userConfigList[i].OpCoId == 73) {
                    $scope.userHaveGBIOpCo = true;
                }
            }
        }

        $scope.checkUserHaveEDPTeam = function (userConfigList) {
            for (var i = 0; i < userConfigList.length; i++) {
                //EDP Project Team 75
                if (userConfigList[i].ProjectTeamId == 75) {
                    $scope.userHaveEDPTeam = true;
                }
            }
        }

        $scope.checkUserHaveSustainabilityTeam = function (userConfigList) {
            for (var i = 0; i < userConfigList.length; i++) {
                //Sustainability Project Team 185
                if (userConfigList[i].ProjectTeamId == 185) {
                    $scope.userHaveSustainabilityTeam = true;
                }
            }
        }

        $scope.getSelectedDashboard = function (senderName, panelName) {
            $scope.selectedView = {};
            $timeout(function () {
                if (panelName == "Summary Info") {
                    filterSummaryInfoGridData(senderName);
                    $scope.selectedView.url = "/App/Dashboard/Views/SummaryInfo/SummaryInfoGrid.html";
                }
                else if (panelName == "Sustainability") {
                    filterSustainabilityGridData(senderName);
                    $scope.selectedView.url = "/App/Dashboard/Views/Sustainability/SustainabilityGrid.html";
                }
                else if (panelName == "Data Issues") {
                    assignDataIssueGridData(function () {
                        $scope.selectedView.url = "/App/Dashboard/Views/DataIssues/DataIssuesGrid.html";
                    });
                }
                $('#myModal').modal('show');
            });
        }

        //---------- Project Priorities Chart ----------//

        $scope.projectPrioritiesGrid = {
            enableFiltering: false,
            enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
            enableRowSelection: true,
            enableRowHeaderSelection: false,
            multiSelect: false,
            columnDefs: [
            { field: "DevelopmentId", displayName: 'Development Id', visible: false },
            {
                field: "ProjectTeam", displayName: 'Project Team', visible: false
            },
            {
                field: "DevelopmentName", displayName: 'Dev Name', width: '15%', enableColumnMenu: false
            },
            {
                field: "DevelopmentManager", displayName: 'Dev Manager', width: '15%', enableColumnMenu: false
            },
            {
                field: "ProjectManager", displayName: 'PM', width: '15%', enableColumnMenu: false
            },
            {
                field: "AssetManager", displayName: 'AM', width: '10%', enableColumnMenu: false
            },
            {
                field: "ProposedSector", displayName: 'Proposed Sector', width: '15%', enableColumnMenu: false
            },
            {
                field: "ProposedGIA", displayName: 'Proposed GIA', width: '12%', enableColumnMenu: false
            },
            {
                field: "ForecastERV", displayName: 'Forecast ERV', cellFilter: 'dppCurrencyFormat', width: '12%', enableColumnMenu: false
            },
            {
                field: "ForecastNDV", displayName: 'Forecast NDV', cellFilter: 'dppCurrencyFormat', width: '12%', enableColumnMenu: false
            },
            {
                field: "CurrentStatus", displayName: 'Current Status', width: '12%', enableColumnMenu: false
            },
            {
                field: "PlanningDetermination", displayName: 'Planning Determination', width: '18%', enableColumnMenu: false
            },
            {
                field: "StartOnSite", displayName: 'Start on Site', width: '10%', enableColumnMenu: false
            },
            {
                field: "PracticalCompletion", displayName: 'PC Date', width: '10%', enableColumnMenu: false
            },
            {
                field: "GrossYieldOnCost", displayName: 'GYOC', width: '10%', enableColumnMenu: false
            },
            {
                field: "NetMarginalYield", displayName: 'NMY', width: '10%', enableColumnMenu: false
            },
            {
                field: "ValueAdd", displayName: 'Value Add', cellFilter: 'dppCurrencyFormat', width: '10%', enableColumnMenu: false
            },
            {
                field: "NPVHurdle", displayName: 'NPV', cellFilter: 'dppCurrencyFormat', width: '10%', enableColumnMenu: false
            }
            ]
        };

        $scope.projectPrioritiesGrid.onRegisterApi = function (gridApi) {
            //set gridApi on scope
            $scope.projectPrioritiesGridApi = gridApi;
            $scope.projectPrioritiesGridApi.selection.on.rowSelectionChanged($scope, function (row) {
                window.location = "/Developments/#/details/" + row.entity.ProjectTeam + "/" + row.entity.DevelopmentId;
            });
        };

        $scope.getProjectPrioritiesChartData = function () {
            $scope.loading_priorityChart = true;
            controllerHelper.apiGet('api/getprojectPrioritiesData/' + $scope.isTeamView, null,
            function (result) {
                $scope.ProjectPrioritiesDataSet = result.data.RawData;
                $scope.ProjectPrioritiesData = result.data.ChartCounts;
                $scope.drawProjectPrioritiesChart($scope.ProjectPrioritiesData);
                $scope.loading_priorityChart = false;
            },
            function (result) {
                $scope.loading_priorityChart = false;
            });
        }

        $scope.drawProjectPrioritiesChart = function (ProjectPriorities) {
            var data = ProjectPriorities;
            tableByPriorities = new google.visualization.DataTable();

            tableByPriorities.addColumn('string', 'Key');
            tableByPriorities.addColumn('number', 'Value');
            tableByPriorities.addColumn({ type: 'string', role: 'tooltip', 'html': true });

            for (var i = 0; i < data.length; i++) {
                tableByPriorities.addRow([
                 data[i].Key,
                 data[i].Value,
                 "Priority - " + data[i].Key + "\n" +
                 "Count - " + data[i].Value
                ]);
            }
            var chartOptions = {
                width: '100%',
                height: 400,
                legend: 'bottom',
                pieSliceText: 'label',
                tooltip: { textStyle: { isHtml: true, color: '#2D2D2D', fontName: 'Arial', fontSize: '12' }, ignoreBounds: true },
                allowHtml: true,
                //'chartArea': { 'width': '100%', 'height': '80%' },
                pieSliceTextStyle: { color: 'white', fontName: 'Arial', fontSize: '13' },
                slices: {
                    0: { color: 'red' },
                    1: { color: 'orange' },
                    2: { color: 'blue' }
                }
            };

            var chartDiv = document.getElementById('projectPrioritiesChart');
            priorityChartwithCount = new google.visualization.PieChart(chartDiv);
            google.visualization.events.addListener(priorityChartwithCount, 'select', priorityChartSelectHandler);
            priorityChartwithCount.draw(tableByPriorities, chartOptions);

        };

        var priorityChartSelectHandler = function (e) {
            var selection = priorityChartwithCount.getSelection();
            if (selection.length > 0) {
                var item = selection[0];
                var priorityCode = tableByPriorities.getValue(item.row, 0);
                filterProjectPriorityByPriorityCode(priorityCode, function () {
                    $('#projectPrioritiesModal').modal('show');
                });
            }
        };

        var filterProjectPriorityByPriorityCode = function (priorityCode, callback) {

            var result = [];
            var set = $scope.ProjectPrioritiesDataSet;
            for (var i = 0; i < set.length; i++) {
                if (set[i].Priority === priorityCode) {
                    result.push(set[i].DevelopmentId);
                }
            }
            getProjectPriorityDetailsById(result, priorityCode);

            callback();
        };

        var getProjectPriorityDetailsById = function (idArray, senderName) {

            $scope.loading_priorityGrid = true;
            $scope.priorityChartHeading = senderName;
            controllerHelper.apiGet('api/getDashboardProjectModalDetails', {
                params: {
                    developmentIds: idArray
                }
            },
            function (result) {


                $scope.projectPrioritiesGrid.data = result.data;
                $scope.loading_priorityGrid = false;
            },
            function (result) {

                //$scope.loading_forcastDetails = false;
                $scope.projectPrioritiesGrid.data = [];
                $scope.loading_priorityGrid = false;
            });

        };

        //---------- Project Status Chart ----------//

        $scope.projectStatusGrid = {
            enableFiltering: false,
            enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
            enableRowSelection: true,
            enableRowHeaderSelection: false,
            multiSelect: false,
            columnDefs: [
            { field: "DevelopmentId", displayName: 'Development Id', visible: false },
            {
                field: "ProjectTeam", displayName: 'Project Team', visible: false
            },
            {
                field: "DevelopmentName", displayName: 'Dev Name', width: '15%', enableColumnMenu: false
            },
            {
                field: "DevelopmentManager", displayName: 'Dev Manager', width: '15%', enableColumnMenu: false
            },
            {
                field: "ProjectManager", displayName: 'PM', width: '15%', enableColumnMenu: false
            },
            {
                field: "AssetManager", displayName: 'AM', width: '10%', enableColumnMenu: false
            },
            {
                field: "ProposedSector", displayName: 'Proposed Sector', width: '15%', enableColumnMenu: false
            },
            {
                field: "ProposedGIA", displayName: 'Proposed GIA', width: '12%', enableColumnMenu: false
            },
            {
                field: "ForecastERV", displayName: 'Forecast ERV', cellFilter: 'dppCurrencyFormat', width: '12%', enableColumnMenu: false
            },
            {
                field: "ForecastNDV", displayName: 'Forecast NDV', cellFilter: 'dppCurrencyFormat', width: '12%', enableColumnMenu: false
            },
            {
                field: "CurrentStatus", displayName: 'Current Status', width: '12%', enableColumnMenu: false
            },
            {
                field: "PlanningDetermination", displayName: 'Planning Determination', width: '18%', enableColumnMenu: false
            },
            {
                field: "StartOnSite", displayName: 'Start on Site', width: '10%', enableColumnMenu: false
            },
            {
                field: "PracticalCompletion", displayName: 'PC Date', width: '10%', enableColumnMenu: false
            },
            {
                field: "GrossYieldOnCost", displayName: 'GYOC', width: '10%', enableColumnMenu: false
            },
            {
                field: "NetMarginalYield", displayName: 'NMY', width: '10%', enableColumnMenu: false
            },
            {
                field: "ValueAdd", displayName: 'Value Add', cellFilter: 'dppCurrencyFormat', width: '10%', enableColumnMenu: false
            },
            {
                field: "NPVHurdle", displayName: 'NPV', cellFilter: 'dppCurrencyFormat', width: '10%', enableColumnMenu: false
            }
            ]
        };

        $scope.projectStatusGrid.onRegisterApi = function (gridApi) {
            //set gridApi on scope
            $scope.projectStatusGridApi = gridApi;
            $scope.projectStatusGridApi.selection.on.rowSelectionChanged($scope, function (row) {
                window.location = "/Developments/#/details/" + row.entity.ProjectTeam + "/" + row.entity.DevelopmentId;
            });
        };

        $scope.getProjectStatusChartData = function () {
            $scope.loading_statusChart = true;
            controllerHelper.apiGet('api/getprojectStatusData/' + $scope.isTeamView, null,
            function (result) {
                $scope.ProjectStatusDataSet = result.data.RawData;
                $scope.ProjectStatusData = result.data.ChartCounts;
                $scope.drawProjectStatusChart($scope.ProjectStatusData);
                $scope.loading_statusChart = false;
            },
            function (result) {
                $scope.loading_statusChart = false;
            });


        }

        $scope.drawProjectStatusChart = function (ProjectStatus) {
            var data = ProjectStatus;
            tableByStatus = new google.visualization.DataTable();

            tableByStatus.addColumn('string', 'Project Status');
            tableByStatus.addColumn('number', 'Value');
            for (var i = 0; i < data.length; i++) {
                tableByStatus.addRow([data[i].Key, data[i].Value]);
            }

            var projectStatusChartOptions = {
                chart: {
                    title: '',
                },
                legend: { position: 'none' },
                colors: ['#217EC2'],
                width: '100%',
                height: 400,
                vAxis: {
                    title: 'Total'
                },
                chartArea: { width: '82%', height: '70%' },
                hAxis: {
                    slantedText: true,
                    slantedTextAngle: 45
                },
            };

            var projectStatusChartDiv = document.getElementById('projectStatusChart');
            projectStatusChart = new google.visualization.ColumnChart(projectStatusChartDiv);
            projectStatusChart.draw(tableByStatus, google.charts.Bar.convertOptions(projectStatusChartOptions));

            google.visualization.events.addListener(projectStatusChart, 'select', statusChartSelectHandler);

        };

        var statusChartSelectHandler = function (e) {
            var selection = projectStatusChart.getSelection();
            if (selection.length > 0) {
                var item = selection[0];
                var statusCode = tableByStatus.getValue(item.row, 0);

                filterProjectStatusByStatus(statusCode, function () {
                    $('#projectStatusModal').modal('show');
                });
            }
        };

        var filterProjectStatusByStatus = function (status, callback) {
            var result = [];
            var set = $scope.ProjectStatusDataSet;

            for (var i = 0; i < set.length; i++) {

                if (set[i].Status === status) {
                    result.push(set[i].DevelopmentId);
                }
            }
            getProjectStatusDetailsById(result, status);
            callback();
        };

        var getProjectStatusDetailsById = function (idArray, senderName) {
            $scope.loading_statusGrid = true;
            $scope.statusChartHeading = senderName;
            controllerHelper.apiGet('api/getDashboardProjectModalDetails', {
                params: {
                    developmentIds: idArray
                }
            },
            function (result) {

                $scope.projectStatusGrid.data = result.data;
                $scope.loading_statusGrid = false;
            },
            function (result) {
                //$scope.loading_forcastDetails = false;

                $scope.projectStatusGrid.data = [];
                $scope.loading_statusGrid = false;
            });

        };

        $('#myModal').on('hide.bs.modal', function (e) {
            $scope.selectedView = {};
            $scope.loading_priorityChart = false;
            $scope.loading_priorityGrid = false;
            $scope.loading_statusChart = false;
            $scope.loading_statusGrid = false;
        });

        $('#projectPrioritiesModal').on('hide.bs.modal', function (e) {
            $scope.selectedView = {};
            $scope.loading_priorityChart = false;
            $scope.loading_priorityGrid = false;
            $scope.loading_statusChart = false;
            $scope.loading_statusGrid = false;

        });

    }]);