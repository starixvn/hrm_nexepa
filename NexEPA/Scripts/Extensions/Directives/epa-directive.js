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
							'<i class="collapsed" data-ng-show="node.' +
							nodeChildren + '.length && node.collapsed" data-ng-click="' +
							treeId + '.selectNodeHead(node)"></i>' +
							'<i class="expanded" data-ng-show="node.' +
							nodeChildren + '.length && !node.collapsed" data-ng-click="' +
							treeId + '.selectNodeHead(node)"></i>' + '<i class="normal" data-ng-hide="node.' +
							nodeChildren + '.length"></i> ' +
							'<span data-ng-class="node.selected" data-ng-click="' +
							treeId + '.selectNodeLabel(node)">' +
							'{{node.' + nodeLabel + '}}</span>' +
							'<div data-ng-hide="!node.collapsed" data-tree-id="' +
							treeId + '" data-tree-model="node.' + nodeChildren + '" data-node-id=' +
							nodeId + ' data-node-label=' + nodeLabel + ' data-node-children=' +
							nodeChildren + '></div>' +
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
		template:
			'<ul class="pagination pull-right custom-paging">' +
				'<li ng-class="{disabled: currentPage == 0}">' +
					'<a class="first" href ng-click="firstPage()" title="First Page">«</a>' +
				'</li>' +
				'<li ng-class="{disabled: currentPage == 0}">' +
					'<a class="pre" href ng-click="prevPage()" title="Previous Page">‹</a>' +
				'</li>' +
				'<li ng-repeat="n in range(pagedItems.length, currentPage, currentPage + gap) " ng-click="setPage()">' +
					'<a href ng-bind="n + 1" ng-class="{pagingactive: n == currentPage}">1</a>' +
				'</li>' +
				'<li ng-class="{disabled: (currentPage) == pagedItems.length - 1}">' +
					'<a class="next" href ng-click="nextPage()" title="Next Page">›</a>' +
				'</li>' +
				'<li ng-class="{disabled: (currentPage) == pagedItems.length - 1}">' +
					'<a class="last" href ng-click="lastPage()" title="Last Page">»</a>' +
				'</li>' +
			'</ul>'
	};
});

registrationModule.directive('ngContextMenu', function () {
	var renderContextMenu = function ($scope, event, options) {
		//		if (!$) { var $ = angular.element; }
		$(event.target).addClass('context');
		var $contextMenu = $('<div>');
		$contextMenu.addClass('dropdown clearfix');
		var $ul = $('<ul>');
		$ul.addClass('dropdown-menu');
		$ul.attr({ 'role': 'menu' });
		$ul.css({
			display: 'block',
			position: 'absolute',
			left: event.pageX + 'px',
			top: event.pageY + 'px'
		});

		angular.forEach(options, function (item) {
			var $li = $('<li>');
			if (item === null) {
				$li.addClass('divider');
			} else {
				var $a = $('<a>');
				$a.attr({ tabindex: '-1', href: '#' });
				$a.text(item[0]);
				$li.append($a);
				$li.on('click', function () {

					$scope.$apply(function () {
						item[1].call($scope, $scope);
					});
				});
			}
			$ul.append($li);
		});
		$contextMenu.append($ul);
		$contextMenu.css({
			width: '100%',
			height: '100%',
			position: 'absolute',
			top: 0,
			left: 0,
			zIndex: 9999
		});
		$(document).find('body').append($contextMenu);
		$contextMenu.on("click", function () {
			$(event.target).removeClass('context');
			$contextMenu.remove();
		}).on('contextmenu', function (event) {
			$(event.target).removeClass('context');
			event.preventDefault();
			$contextMenu.remove();
		});
	};
	return {
		link: function ($scope, element, attrs) {
			element.on('contextmenu', function (event) {
				$scope.$apply(function () {
					event.preventDefault();
					var options = $scope.$eval(attrs.ngContextMenu);
					if (options instanceof Array) {
						renderContextMenu($scope, event, options);
					} else {
						throw '"' + attrs.ngContextMenu + '" not an array';
					}
				});
			});
		}
	};
});

// Assign Permission to Role
registrationModule.directive('assignPermission', function () {
	var updateParent = function (checked, type) {
		var pid = $(this).attr('pid');

		if (pid && pid.length > 0) {
			var parent = $("#func" + type + pid);

			if (checked && !parent.is(':checked')) {
				parent.prop('checked', true);
			}

			if (!checked) {
				var siblings = $("[name='func" + type + "'][pid='" + parent.val() + "']:checked");

				if (!siblings || (siblings && siblings.length === 0)) {
					parent.prop('checked', false);
				}
			}

			updateParent.call(parent.get(0), checked, type);
		}
	};

	return {
		restrict: "A",
		link: function (scope, iElement) {
			iElement.on("change", function () {
				var permissionType = $(this).attr('name'),
					currentId = $(this).val(),
					checked = $(this).is(':checked');

				if (permissionType === 'funcHost') {
					if (checked) {
						$("#funcRead" + currentId).prop('checked', true).trigger('change');
					}

					// parent changed -> children changed
					$("[name='funcHost'][pid='" + currentId + "']").prop('checked', checked).trigger('change');

					// update parent
					updateParent.call(this, checked, "Host");
				}
				else {
					if (!checked) {
						$("#funcHost" + currentId).prop('checked', false).trigger('change');
					}

					// parent changed -> children changed
					$("[name='funcRead'][pid='" + currentId + "']").prop('checked', checked).trigger('change');

					// update parent
					updateParent.call(this, checked, "Read");
				}
			});
		}
	};
});

// get role permission when click save button
registrationModule.directive('getRolePermission', function () {
	var createTemplate = function (functionId, isRead, isHost) {
		var template = '<Function FunctionId="' + functionId + '"' +
							' IsRead="' + isRead + '"' +
							' IsHost="' + isHost + '"></Function>';
		return template;
	};

	return {
		restrict: "A",
		link: function (scope, iElement) {
			iElement.on("click", function () {
				var result = '<ROOT>';
				var table = $("[name='tableFunction']");
				var roleFunction = $(table).find('[type=checkbox][name=funcRead]');

				if (roleFunction) {
					for (var i = 0; i < roleFunction.length; i++) {
						var element = roleFunction[i];
						var functionId = $(element).val(),
							isRead = $(element).is(':checked') ? 1 : 0,
							isHost = $('#funcHost' + functionId).is(':checked') ? 1 : 0,
							template = createTemplate(functionId, isRead, isHost);
						result += template;
					}

					result += '</ROOT>';

					scope.roleFunction = {
						functions: result,
						roleId: scope.filter.roleId
					};

					// call save function
					scope.save.call(this, scope.roleFunction);
				}
			});
		}
	};
});

registrationModule.directive('listSortable', function () {
	return {
		link: function (scope, el) {
			var currentUserId;
			el.sortable({
				revert: true,
				receive: function (event, ui) {
					currentUserId = ui.sender.attr("id");
				},
				items: "tr:not(.unsortable), li:not(.unsortable)"
			});

			el.on("sortdeactivate", function (event, ui) {
				var from = angular.element(ui.item).scope().$index;
				var to = el.children().index(ui.item);
				if (to >= 0) {
					scope.$apply(function () {
						if (from >= 0) {
							scope.$emit('list-sorted', { from: from, to: to, sender: ui.sender.attr("id") });
						} else {
							scope.$emit('list-created', { to: to, name: ui.item.text(), id: currentUserId, sender: ui.sender.attr("id") });
							ui.item.remove();
						}
					});
				}
			});
		}
	};
});

registrationModule.directive('listDraggable', function () {
	return {
		link: function (scope, el, attrs) {
			el.draggable({
				connectToSortable: attrs.listDraggable,
				helper: "clone", //drag
				revert: "invalid"
			});
			el.disableSelection();
		}
	};
});

// Close other open calendar on Create Appraisal Form page
registrationModule.directive('closeOtherCalendar', function () {
	return {
		restrict: "A",
		link: function (scope, iElement) {
			iElement.on("click", function ($event) {
				var allCalendar = $("[name='appraisalForm']").find("[name='calendar']"),
					currentCalendar = $(this).prevAll().get(1),
					currentIsOpen = $(currentCalendar).attr('is-open');

				for (var i = 0; i < allCalendar.length; i++) {
					var calendar = allCalendar[i];

					if (calendar !== currentCalendar) {
						var isOpen = $(calendar).attr('is-open').replace('calendar.', '');
						scope.calendar[isOpen] = false;
					}
				}

				var dateOpened = currentIsOpen.replace('calendar.', '');
				scope.openPopupDate.call(this, $event, dateOpened);
			});
		}
	};
});

// Get date of all open calendar and create template when create new Appraisal Form
registrationModule.directive('getDetailProcess', function () {

	var createTemplate = function (processId, startDate, endDate, alarmDate, noticeAtBegin) {
		var process = '<Process ProcessId="' + processId + '"' +
								' NoticeAtBegin="' + noticeAtBegin + '"' +
								' StartDate="' + startDate + '"' +
								' EndDate="' + endDate + '"' +
								' RemindDate="' + alarmDate + '"></Process>';

		return process;
	};

	return {
		restrict: "A",
		link: function (scope, iElement) {
			iElement.on("click", function () {
				var allProcess = $("[name='appraisalForm']").find("tbody tr:visible"),
					processTemplate = '<ROOT>',
					requireViolation = false,
					sameProcessDateViolation = false,
					differentProcessDateViolation = false,
					beforeEndDate = '';

				for (var i = 0; i < allProcess.length; i++) {
					var rowProcess = allProcess[i],
						firstCalendarItem = $(rowProcess).find('[name="calendar"]').get(0),
						processId = $(firstCalendarItem).attr("processId"),
						startDate = $(rowProcess).find('[name="calendar"][start=' + processId + ']').val(),
						endDate = $(rowProcess).find('[name="calendar"][end=' + processId + ']').val(),
						alarmDate = $(rowProcess).find('[name="calendar"][alarm=' + processId + ']').val(),
						noticeAtBegin = $(rowProcess).find('[name="notice"]').is(':checked') ? 1 : 0,
						process = createTemplate(processId, startDate, endDate, alarmDate, noticeAtBegin); 

					if (!startDate || !endDate || !alarmDate) {
						requireViolation = true;
					}
					if (new Date(startDate) > new Date(endDate)) {
						sameProcessDateViolation = true;
					}
					if (i > 0 && new Date(startDate) < new Date(beforeEndDate)) {
						differentProcessDateViolation = true;
					}

					processTemplate += process;
					beforeEndDate = endDate;
				}

				processTemplate += '</ROOT>';
				scope.save.call(this, processTemplate, requireViolation, sameProcessDateViolation, differentProcessDateViolation);
			});
		}
	};
});

registrationModule.directive('bindHtmlUnsafe', function ($compile) {
	return function ($scope, $element, $attrs) {

		var compile = function (newHTML) { // Create re-useable compile function
			newHTML = $compile(newHTML)($scope); // Compile html
			$element.html('').append(newHTML); // Clear and append it
		};

		var htmlName = $attrs.bindHtmlUnsafe; // Get the name of the variable 
		// Where the HTML is stored

		$scope.$watch(htmlName, function (newHTML) { // Watch for changes to 
			// the HTML
			if (!newHTML) {
				return;
			}
			compile(newHTML);   // Compile it
		});

	};
});
