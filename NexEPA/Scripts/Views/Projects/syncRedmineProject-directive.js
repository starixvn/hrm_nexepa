'use strict';
/*global projectsSettingModule*/

projectsSettingModule.directive('syncRedmineProjectGrid', ['$timeout', 'notificationFactory', function ($timeout, notificationFactory) {
    var generateColumnHeaders = function () {
        var columnHeaders = [
            { HeaderText: "", Width: 50, Name: "Checkbox", HeaderAlign: "center", CellAlign: "center" },
            { HeaderText: "#", Width: 50, Name: "No", HeaderAlign: "center", CellAlign: "center", ShowCollapseExpand: true },
            { HeaderText: "Project Name", Width: 350, Name: "ProjectName", HeaderAlign: "center", CellAlign: "left" },
            { HeaderText: "Project Manager", Width: 200, Name: "ProjectManager", HeaderAlign: "center", CellAlign: "left" },
            { HeaderText: "Synced Date", Width: 200, Name: "SyncedDate", HeaderAlign: "center", CellAlign: "center", FormatDateTime: "YYYY-MM-DD HH:mm A" }
        ];

        return columnHeaders;
    };

    var generateCheckbox = function (project) {
        var checkbox = '<label>' +
                            '<input type="checkbox" data-id="' + project.projectId + '" ' + 'data-parent-id="' + project.parentId + '" >' +
                        '</label>';

        return checkbox;
    };

    var generateProjectManagerDropList = function (project) {
        var select = '';

        if (project.projectManagers.length > 0) {
            select = '<select class="form-control" data-id="' + project.projectId + '">';
            var options = '';

            angular.forEach(project.projectManagers, function (projectManager) {
                if (projectManager.employeeId === project.projectManagerId) {
                    options += '<option value="' + projectManager.employeeId + '" selected="true" >' + projectManager.nickName + '</option>';
                } else {
                    options += '<option value="' + projectManager.employeeId + '" >' + projectManager.nickName + '</option>';
                }
            });

            select += options + '</select>';
        }

        return select;
    };

    var generateRecursiveSubRows = function (parentRow, project) {
        var counter = 1;

        angular.forEach(project.subProjects, function (subProject) {
            var currentSubRow = {
                Columns: [
                    {
                        Name: "Checkbox",
                        Value: '',
                        CustomCellRender: function () {
                            return generateCheckbox(subProject);
                        }
                    },
                    { Name: "No", Value: counter++ },
                    {
                        Name: "ProjectName",
                        Value: '<a class="project-name" href data-project-id="' + subProject.projectId + '" ' +
                                    'data-project-name="' + subProject.projectName + '">' + subProject.projectName + '</a>'
                    },
                    {
                        Name: "ProjectManager",
                        Value: '',
                        CustomCellRender: function () {
                            return generateProjectManagerDropList(subProject);
                        }
                    },
                    { Name: "SyncedDate", Value: subProject.syncedDate }
                ]
            };

            parentRow.SubRows.push(currentSubRow);

            if (subProject.subProjects.length > 0) {
                currentSubRow.SubRows = [];

                generateRecursiveSubRows(currentSubRow, subProject);
            }
        });
    };

    var checkRecursiveChildCheckboxes = function (parent) {
        var childCheckboxes = $('input[type=checkbox][data-parent-id=' + parent.data("id") + ']');

        childCheckboxes.each(function () {
            var checkbox = $(this);

            checkbox.prop("checked", parent.prop("checked"));

            checkRecursiveChildCheckboxes(checkbox);
        });
    };

    var registerControlEvents = function (scope) {
        var checkboxSelector = '.grid-container .table-cell input[type=checkbox]';

        $(checkboxSelector).off("change");

        $(checkboxSelector).on("change", function () {
            var self = $(this);

            checkRecursiveChildCheckboxes(self);
        });

        var btnSyncSelector = '#btn-sync-project';

        $(btnSyncSelector).off("click");
        $(btnSyncSelector).on("click", function () {
            var selectedCheckboxes = $('.grid-container .table-cell input:checked[type=checkbox]');

            if (selectedCheckboxes.length === 0) {
                notificationFactory.error("RequireSelectProject");
            } else if (scope.syncClick) {
                scope.isSyncing = true;

                var projectIds = selectedCheckboxes.map(function () { return $(this).data("id"); }).get();
                var projectPMs = [];

                $.each(projectIds, function () {
                    var projectId = this;
                    var pmSelectControl = $('.grid-container tr.highlight-row select[data-id=' + projectId + ']');
                    var pmId = pmSelectControl.val();

                    projectPMs.push({
                        employeeId: pmId,
                        projectId: projectId
                    });
                });

                scope.syncClick(projectPMs);
            }
        });

        //view project member
        var projectNameSelector = ".grid-container tr.highlight-row a.project-name";
        $(projectNameSelector).off("click");
        $(projectNameSelector).on("click", function () {
            var self = $(this);
            var projectId = self.data("project-id");
            var projectName = self.data("project-name");

            if (scope.viewProjectMember) {
                scope.viewProjectMember(projectId, projectName);
            }
        });
    };

    var generateBodyRows = function (projectData) {
        var rows = [];

        if (projectData) {
            angular.forEach(projectData, function (group) {
                var currentRow = {
                    Columns: [
                        {
                            Name: "Checkbox",
                            Value: '',
                            CustomCellRender: function () {
                                return generateCheckbox(group);
                            }
                        },
                        { Name: "No", Value: group.projectName, MergeColNumber: 2, CellAlign: "left" },
                        {
                            Name: "ProjectManager",
                            Value: '',
                            CustomCellRender: function () {
                                return generateProjectManagerDropList(group);
                            }
                        },
                        { Name: "SyncedDate", Value: group.syncedDate }
                    ],
                    SubRows: []
                };

                rows.push(currentRow);

                generateRecursiveSubRows(currentRow, group);
            });
        }

        return rows;
    };

    return {
        restrict: "A",
        scope: {
            projectData: "=",
            syncClick: "=",
            isSyncing: "=",
            viewProjectMember: "="
        },
        link: function ($scope, iElement) {
            var loadingButton = null;

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
                            text: '',
                            customButtons:
                                '<button id="btn-sync-project" type="button" data-style="slide-up" class="btn btn-primary btn-xs btn-custom ladda-button">' +
                                    '<span class="ladda-label" >Sync</span>' +
                                '</button>'
                        },
                        gridExpandHeight: 300
                    };

                    $(iElement).fanexGrid(gridOptions);

                    registerControlEvents($scope);

                    //loading button
                    loadingButton = Ladda.create($('#btn-sync-project')[0]);
                }
            });

            $scope.$watch("isSyncing", function (value) {
                if (loadingButton) {
                    $timeout(function () {
                        if (value) {
                            loadingButton.start();
                        } else {
                            loadingButton.stop();
                        }
                    });
                }
            });
        }
    };
}]);