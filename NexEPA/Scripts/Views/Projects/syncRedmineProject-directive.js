'use strict';
/*global projectsSettingModule*/

projectsSettingModule.directive('syncRedmineProjectGrid', [function () {
    var generateColumnHeaders = function () {
        var columnHeaders = [
            { HeaderText: "#", Width: 40, Name: "No", HeaderAlign: "center", CellAlign: "center", ShowCollapseExpand: true },
            { HeaderText: "Project Name", Width: 350, Name: "ProjectName", HeaderAlign: "center", CellAlign: "left" },
            { HeaderText: "Project Manager", Width: 200, Name: "ProjectManager", HeaderAlign: "center", CellAlign: "left" },
            { HeaderText: "Synced Date", Width: 120, Name: "SyncedDate", HeaderAlign: "center", CellAlign: "center", FormatDateTime: "YYYY-MM-DD HH:mm A" }
        ];

        return columnHeaders;
    };

    var generateBodyRows = function (projectData) {
        var rows = [];

        if (projectData) {
            angular.forEach(projectData, function (group) {
                var counter = 1;
                var currentRow = {
                    Columns: [
                        { Name: "No", Value: group.projectName, MergeColNumber: 4, CellAlign: "left" }
                    ],
                    SubRows: []
                };

                rows.push(currentRow);

                angular.forEach(group.subProjects, function (project) {
                    var currentSubRow = {
                        Columns: [
                            { Name: "No", Value: counter++ },
                            { Name: "ProjectName", Value: '<a href data-project-id="' + project.projectId + '" >' + project.projectName + '</a>' },
                            { Name: "ProjectManager", Value: '' },
                            { Name: "SyncedDate", Value: project.syncedDate }
                        ]
                    };

                    currentRow.SubRows.push(currentSubRow);
                });
            });
        }

        return rows;
    };

    return {
        restrict: "A",
        scope: {
            projectData: "="
        },
        link: function ($scope, iElement) {
            $scope.$watch('projectData', function (value) {
                if (value) {
                    var columnHeaders = generateColumnHeaders(value),
                        bodyRows = generateBodyRows(value);

                    var gridOptions = {
                        columns: columnHeaders,
                        bodyRows: bodyRows,
                        showTree: true,
                        showTitle: true,
                        titleInfo: {
                            text: ""
                        },
                        gridExpandHeight: 300
                    };

                    $(iElement).fanexGrid(gridOptions);
                }
            });
        }
    };
}]);