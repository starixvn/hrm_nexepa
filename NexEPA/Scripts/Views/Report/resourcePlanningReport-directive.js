'use strict';
/*global reportModule*/

reportModule.directive('resourcePlanningReport', function () {
	var getDateStatus = function (reportData, nickName, workDate) {
		var result = '';

		var data = jQuery.grep(reportData.resourceDateStatuses, function (item) {
			return item.employee.nickName === nickName && workDate === item.dateStatus.workDate;
		});

		if (data.length > 0) {
			var dateStatus = data[0];

			if (dateStatus.dateStatus.isHoliday) {
				result = 'Holiday';
			} else if (dateStatus.dateStatus.isOffDay) {
				result = 'Off';
			} else if (dateStatus.dateStatus.isLeaveDay) {
				result = 'Leave';
			}
		}

		return result;
	};

	var getTopLevelWorkingClass = function (reportData, nickName, workDate) {
		var data = jQuery.grep(reportData.detailResourcePlannings, function (item) {
			return item.employee.nickName === nickName && item.issue.startDate <= workDate && workDate <= item.issue.dueDate;
		});

		if (data.length > 0) {
			return "top-level-working";
		}
	};

	var getSubLevelWorkingClass = function (reportData, nickName, projectId, workDate) {
		var data = jQuery.grep(reportData.detailResourcePlannings, function (item) {
			return item.employee.nickName === nickName && item.project.projectId === projectId &&
					item.issue.startDate <= workDate && workDate <= item.issue.dueDate;
		});

		if (data.length > 0) {
			return "sub-level-working";
		}
	};

	var generateGroupHeaders = function (reportData) {
		var groupHeaders = [
			{
				Columns: [
					{ HeaderText: "#", Name: "No", HeaderAlign: "center", CellAlign: "center", MergeRowNumber: 2 },
					{ HeaderText: "Members", Name: "fullName", HeaderAlign: "center", CellAlign: "center", MergeRowNumber: 2 },
					{ HeaderText: "Issue Count", Name: "issueCount", HeaderAlign: "center", CellAlign: "center", MergeRowNumber: 2 }
				]
			}
		];

		var count = 1;
		angular.forEach(reportData.monthInfo, function (month) {
			var header = {
				HeaderText: month.monthName,
				Name: "Day" + count.toString(),
				HeaderAlign: "center",
				CellAlign: "center",
				MergeColNumber: month.dayCount
			};
			groupHeaders[0].Columns.push(header);

			count += month.dayCount;
		});

		return groupHeaders;
	};

	var generateColumnHeaders = function (reportData) {
		var columnHeaders = [
			{ HeaderText: "#", Width: 60, Name: "No", HeaderAlign: "center", CellAlign: "center", ShowCollapseExpand: true },
			{ HeaderText: "Members", Width: 120, Name: "fullName", HeaderAlign: "center", CellAlign: "left" },
			{ HeaderText: "Issue Count", Width: 80, Name: "issueCount", HeaderAlign: "center", CellAlign: "right" }
		];

		var count = 1;
		angular.forEach(reportData.dateInfo, function (date) {
			var columnHeader = { HeaderText: date.dateStatus.dayName, Width: 60, Name: "Day" + count.toString(), HeaderAlign: "center", CellAlign: "center" };
			columnHeaders.push(columnHeader);

			count += 1;
		});

		return columnHeaders;
	};

	var generateBodyRows = function (reportData) {
		var rows = [];

		if (reportData) {
			var counter = 1;

			angular.forEach(reportData.projectResources, function (resource) {
				//render member
				var currentRow = {
					Columns: [
							{ Name: "No", Value: counter },
							{ Name: "fullName", Value: resource.employee.fullName },
							{ Name: "issueCount", Value: resource.totalIssueCount }
						],
					SubRows: []
				};

				var colIndex = 1;
				angular.forEach(reportData.dateInfo, function (date) {
					var dateStatus = getDateStatus(reportData, resource.employee.nickName, date.dateStatus.workDate),
						workingClass = '';
					if (dateStatus === '') {
						workingClass = getTopLevelWorkingClass(reportData, resource.employee.nickName, date.dateStatus.workDate);
					}

					var colValue = {
						Name: "Day" + colIndex.toString(),
						Value: dateStatus,
						CssClass: workingClass
					};

					currentRow.Columns.push(colValue);

					colIndex++;
				});

				rows.push(currentRow);

				//render project
				angular.forEach(resource.projects, function (project) {
					var currentSubRow = {
						Columns: [
							{ Name: "No", Value: "" },
							{ Name: "fullName", Value: project.projectName },
							{ Name: "issueCount", Value: project.issueCount }
						]
					};

					var colIndex = 1;
					angular.forEach(reportData.dateInfo, function (date) {
						var dateStatus = getDateStatus(reportData, resource.employee.nickName, date.dateStatus.workDate),
							workingClass = '';
						if (dateStatus === '') {
							workingClass = getSubLevelWorkingClass(reportData, resource.employee.nickName, project.projectId, date.dateStatus.workDate);
						}

						var value = {
							Name: "Day" + colIndex.toString(),
							Value: '',
							CssClass: workingClass
						};
						currentSubRow.Columns.push(value);

						colIndex++;
					});

					currentRow.SubRows.push(currentSubRow);
				});

				counter++;
			});
		}

		return rows;
	};

	return {
		restrict: "A",
		scope: {
			reportData: "="
		},
		link: function ($scope, iElement) {
			$scope.$watch('reportData', function (value) {
				if (value && value.projectResources && value.projectResources.length > 0) {
					var groupHeaderRows = generateGroupHeaders(value),
						columnHeaders = generateColumnHeaders(value),
						bodyRows = generateBodyRows(value);

					var gridOptions = {
						customizeHeader: true,
						groupHeaderRows: groupHeaderRows,
						columns: columnHeaders,
						bodyRows: bodyRows,
						showTree: true,
						gridExpandHeight: 300,
						numberOfFrozenColumn: 3
					};

					$(iElement).fanexGrid(gridOptions);
				}
			});
		}
	};
});