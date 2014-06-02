/*global registrationModule*/
'use strict';

registrationModule.directive('percentage', function () {
	return {
		restrict: 'A',
		require: 'ngModel',
		link: function (scope, element, attrs, modelCtrl) {
			modelCtrl.$parsers.push(function (inputValue) {
				if (inputValue === undefined) {
					return '';
				}

				var transformedInput = inputValue.replace(/[^0-9]/g, '');
				if (transformedInput < 0 || transformedInput > 100) {
					modelCtrl.$setViewValue('');
					modelCtrl.$render();
				}
				else {
					if (transformedInput !== inputValue) {
						modelCtrl.$setViewValue(transformedInput);
						modelCtrl.$render();
					}
				}

				return transformedInput;
			});
		}
	};
});

registrationModule.directive('ngRepeatLoaded', function () {
	return function (scope) {
		if (scope.$last) {
			var ratingScale = $("#ratingScale");
			if (ratingScale.length > 0) {
				var ratingScaleContent = ratingScale.next();
				ratingScaleContent.slideToggle(500, function () {
				});
			}

			$(".boxtitle").on("click", function () {
				var header = $(this);
				//getting the next element
				var content = header.next();
				//open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
				content.slideToggle(500, function () {
				});
			});
		}
	};
});

registrationModule.directive('tableRowLevel', ['$timeout', function (timeout) {
	return function () {
		var moveTableRow = function () {
			$(".up,.down,.top,.bottom").click(function () {
				var row = $(this).parents("tr:first");
				if ($(this).is(".up")) {
					row.insertBefore(row.prev());
				} else if ($(this).is(".down")) {
					row.insertAfter(row.next());
				} else if ($(this).is(".bottom")) {
					row.insertAfter($("table#tblKPIGroup tbody tr:last"));
				} else if ($(this).is(".top")) {
					row.insertBefore($("table#tblKPIGroup tbody tr:first"));
				}
			});
		};
		timeout(moveTableRow, 0);
	};
} ]);

registrationModule.directive('treeModel', ['$compile', function ($compile) {
	return {
		restrict: 'A',
		link: function (scope, element, attrs) {
			//tree id
			var treeId = attrs.treeId;

			//tree model
			var treeModel = attrs.treeModel;

			//node id
			var nodeId = attrs.nodeId || 'id';

			//node label
			var nodeLabel = attrs.nodeLabel || 'label';

			//children
			var nodeChildren = attrs.nodeChildren || 'children';

			//tree template
			var template =
					'<ul>' +
						'<li data-ng-repeat="node in ' + treeModel + '">' +
							'<i class="collapsed" data-ng-show="node.'
							+ nodeChildren + '.length && node.collapsed" data-ng-click="'
							+ treeId + '.selectNodeHead(node)"></i>'
							+ '<i class="expanded" data-ng-show="node.'
							+ nodeChildren + '.length && !node.collapsed" data-ng-click="'
							+ treeId + '.selectNodeHead(node)"></i>' + '<i class="normal" data-ng-hide="node.'
							+ nodeChildren + '.length"></i> ' +
							'<span data-ng-class="node.selected" data-ng-click="'
							+ treeId + '.selectNodeLabel(node)">{{node.' + nodeLabel + '}}</span>' +
							'<div data-ng-hide="!node.collapsed" data-tree-id="'
							+ treeId + '" data-tree-model="node.' + nodeChildren + '" data-node-id='
							+ nodeId + ' data-node-label=' + nodeLabel + ' data-node-children='
							+ nodeChildren + '></div>' +
						'</li>' +
					'</ul>';


			//check tree id, tree model
			if (treeId && treeModel) {

				//root node
				if (attrs.angularTreeview) {

					//create tree object if not exists
					scope[treeId] = scope[treeId] || {};
					//if node head clicks,
					scope[treeId].selectNodeHead = scope[treeId].selectNodeHead || function (selectedNode) {
						//Collapse or Expand

						selectedNode.collapsed = !selectedNode.collapsed;
					};

					//if node label clicks,
					scope[treeId].selectNodeLabel = scope[treeId].selectNodeLabel || function (selectedNode) {

						//remove highlight from previous node
						if (scope[treeId].currentNode && scope[treeId].currentNode.selected) {
							scope[treeId].currentNode.selected = undefined;
						}

						//set highlight to selected node
						selectedNode.selected = 'selected';

						//set currentNode
						scope[treeId].currentNode = selectedNode;
					};
				}

				//Rendering template.
				element.html('').append($compile(template)(scope));
			}
		}
	};
} ]);

registrationModule.directive('calendarImage', function () {
	return {
		restrict: 'AE',
		replace: 'true',
		template: '<img class="calendar-image" src="/Content/Site/images/calendar.png"></img>'
	};
});

// search employee auto-complete directive
registrationModule.directive('searchAutoComplete', ['$timeout', function ($timeout) {
	return {
		scope: { employee: "=employee", searchEmployees: "&searchEmployees", roles: "=roles" },
		link: function (scope, iElement) {
			iElement.autocomplete({
				source: function (request, response) {
					var employees = scope.searchEmployees();
					var data = [];

					angular.forEach(employees, function (employee) {
						data.push(
							{ value: employee.nickName + ' (' + employee.fullName + ')',
								id: employee.employeeId,
								employee: employee
							});
					});

					response(data);
				},
				select: function (event, ui) {
					scope.employee = ui.item.employee;

					// map role
					if (scope.roles != null) {
						$.each(scope.roles, function (i) {
							if (scope.roles[i].projectRoleId === scope.employee.projectRoleId) {
								scope.roles[i].selected = true;
							}
							else {
								scope.roles[i].selected = false;
							}
						});
					}

					$timeout(function () {
						iElement.trigger('input');
					}, 0);
				},
				appendTo: iElement.parent()
			});
		}
	};
} ]);

registrationModule.directive('paginationBar', function () {
	return {
		restrict: 'AE',
		replace: 'true',
		template: '<div class="custom-paging">'
			+ '<ul class="pagination pull-right paging-margin">'
				+ '<li ng-class="{disabled: currentPage == 0}">'
					+ '<a class="first" href ng-click="firstPage()">«</a>'
				+ '</li>'
				+ '<li ng-class="{disabled: currentPage == 0}">'
					+ '<a class="pre" href ng-click="prevPage()">‹</a>'
				+ '</li>'
				+ '<li ng-repeat="n in range(pagedItems.length, currentPage, currentPage + gap) " ng-click="setPage()">'
					+ '<a href ng-bind="n + 1" ng-class="{pagingactive: n == currentPage}">1</a>'
				+ '</li>'
				+ '<li ng-class="{disabled: (currentPage) == pagedItems.length - 1}">'
					+ '<a class="next" href ng-click="nextPage()">›</a>'
				+ '</li>'
				+ '<li ng-class="{disabled: (currentPage) == pagedItems.length - 1}">'
					+ '<a class="last" href ng-click="lastPage()">»</a>'
				+ '</li>'
			+ '</ul>'
		+ '</div>'
	};
});

registrationModule.directive('fixedHeader', ['$timeout', function ($timeout) {
	return {
		restrict: 'A',
		link: function ($scope, $elem) {
			// wait for content to load into table and the tbody to be visible
			$scope.$watch(function () { return $elem.find("tbody").is(':visible'); },
				function (newValue) {
					if (newValue === true) {
						// reset display styles so column widths are correct when measured below
						$elem.find('thead.headerName, tbody').css('display', '');

						// wrap in $timeout to give table a chance to finish rendering
						$timeout(function () {
							// set widths of columns
							$elem.find('th').each(function (i, thElem) {
								thElem = $(thElem);
								var tdElems = $elem.find('tbody tr:first td:nth-child(' + (i + 1) + ')');

								var columnWidth = tdElems.width();
								thElem.width(columnWidth);
								tdElems.width(columnWidth);
							});

							// set css styles on thead and tbody
							$elem.find('thead.headerName').css({
								'display': 'block'
							});

							$elem.find('tbody').css({
								'display': 'block',
								'max-height': '600px',
								'overflow': 'auto'
							});

							// reduce width of last column by width of scrollbar
							var scrollBarWidth = $elem.find('thead.headerName').width() - $elem.find('tbody')[0].clientWidth;
							if (scrollBarWidth > 0) {
								// for some reason trimming the width by 2px lines everything up better
								scrollBarWidth -= 2;
								$elem.find('tbody tr:first td:last-child').each(function (i, elem) {
									$(elem).width($(elem).width() - scrollBarWidth);
								});
							}
						}, 0);
					}
				});
		}
	};
} ]);

registrationModule.directive('tableLoaded', ['scrollDetectService', function (scrollDetectService) {
	return function ($scope) {
		if ($scope.$last) {
			var div = $("table#data").find('tbody')[0];
			var isScroll = div.scrollHeight > div.clientHeight;
			scrollDetectService.setProperty(isScroll);
		}
	};
} ]);