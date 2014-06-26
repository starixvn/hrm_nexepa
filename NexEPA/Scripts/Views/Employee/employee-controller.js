/* global employeeModule */
'use strict';

employeeModule.controller("ViewAllEmployeeController",
['$scope', '$translate', '$route', 'employeeRepository', 'employeeCacheFactory', 'notificationFactory', 'commonFactory',
function ($scope, $translate, $route, employeeRepository, employeeCacheFactory, notificationFactory, commonFactory) {

	$scope.entrySelected = 20;

	var getEmployee = function (keyword, groupId) {
		employeeRepository.getAllEmployee(keyword, groupId).success(function (employees) {
			$scope.employees = employees;
			$scope.totalEntries = employees.length;
			commonFactory.tablePagination($scope, employees, $scope.entrySelected);
			$scope.disabledSubmitButton = false;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
			$scope.disabledSubmitButton = true;
		});
	};

	//get all employee
	getEmployee(null, null);

	commonFactory.numberEntryList($scope);

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

	$scope.query = function (filter) {
		if (filter) {
			getEmployee(filter.keyword, filter.groupId);
		}
		else {
			$scope.disabledSubmitButton = false;
		}
	};

	$scope.reload = function () {
		$route.reload();
	};

	$scope.showEntries = function () {
		commonFactory.tablePagination($scope, $scope.employees, $scope.entrySelected);
	};
} ]);