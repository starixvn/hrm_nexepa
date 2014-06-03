/* global employeeModule */
'use strict';

employeeModule.controller("ViewAllEmployeeController", ['$scope', '$translate', '$route', 'employeeRepository',
	'employeeCacheFactory', 'notificationFactory', 'commonFactory',
function ($scope, $translate, $route, employeeRepository, employeeCacheFactory,
notificationFactory, commonFactory) {

	var getEmployee = function (keyword, groupId) {
		employeeRepository.getAllEmployee(keyword, groupId).success(function (employees) {
			$scope.employees = employees;
			commonFactory.tablePagination($scope, employees, 20);
			$scope.disabledSubmitButton = false;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
			$scope.disabledSubmitButton = true;
		});
	};

	//get all employee
	getEmployee(null, null);

	var groupOptions = employeeCacheFactory.get("groupOptions");

	if (groupOptions) {
		$scope.groupOptions = groupOptions;
	}
	else {
		employeeRepository.getAllGroup().success(function (groupList) {
			$scope.groupOptions = groupList;
			employeeCacheFactory.put("groupOptions", groupList);
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	}

	// when filter data
	$scope.query = function (filter) {
		if (filter) {
			getEmployee(filter.keyword, filter.groupId);
		}
		else {
			$scope.disabledSubmitButton = false;
		}
	};

	// reload
	$scope.reload = function () {
		$route.reload();
	};

} ]);