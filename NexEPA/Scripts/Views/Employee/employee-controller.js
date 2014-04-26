/* global employeeModule */
'use strict';

employeeModule.controller("ViewAllEmployeeController", ['$scope', '$translate', '$route', 'employeeRepository', 
	'employeeCacheFactory', 'notificationFactory',
function ($scope, $translate, $route, employeeRepository, employeeCacheFactory, notificationFactory) {

	var getEmployee = function (keyword, groupId) {
		employeeRepository.getAllEmployee(keyword, groupId).success(function (employees) {
			$scope.employees = employees;
			$scope.disabledSubmitButton = false;
		}).error(function () {
			notificationFactory.error('', "ErrorMessage");
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
		}).error();
	}

	// when filter data
	$scope.query = function (filter) {
		if (filter) {
			getEmployee(filter.keyword, filter.groupId);
			$scope.disabledSubmitButton = true;
		}
		else {
			$scope.disabledSubmitButton = false;
		}
	};

	// reload
	$scope.reload = function () {
		$route.reload();
	};
}]);