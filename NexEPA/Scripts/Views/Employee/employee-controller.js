/* global employeeModule */
'use strict';

employeeModule.controller("ViewAllEmployeeController", function ($scope, $translate, employeeRepository, employeeCacheFactory) {

	var getEmployee = function (keyword, groupId) {
		employeeRepository.getAllEmployee(keyword, groupId).success(function (employees) {
			$scope.employees = employees;
		}).error(function () {
			$scope.hasError = true;
			$translate("ErrorMessage").then(function (errorMessage) {
				$scope.errorMessage = errorMessage;
			});
		});
	};

	//get all employee
	getEmployee(null, null);

	var groupOptions = employeeCacheFactory.get("groupOptions");

	if (groupOptions) {
		$scope.groupOptions = groupOptions;
	}
	else {
		////TODO: call ajax to get list group
		$scope.groupOptions = [
			{ groupId: 1, name: "Solaris" },
			{ groupId: 2, name: "Rome" },
			{ groupId: 3, name: "Olympus" },
			{ groupId: 4, name: "R&D" },
			{ groupId: 5, name: "Design"}];
	}

	$scope.query = function (filter) {
		getEmployee(filter.keyword, filter.groupId);
	};
});