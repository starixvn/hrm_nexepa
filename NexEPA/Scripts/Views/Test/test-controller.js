'use strict';
/*global testModule, registrationModule*/


testModule.controller("LoginAsAnotherUserController",
	['$scope', '$window', 'employeeRepository', 'notificationFactory', 'testFactory',
	function ($scope, $window, employeeRepository, notificationFactory, testFactory) {

		$scope.search = {
			searchKey: ''
		};

		// get all employee by target Id 
		$scope.getEmployee = function (searchKey) {
			employeeRepository.getAllEmployee(searchKey, null).success(function (employees) {
				$scope.searchEmployees = employees;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		};

		$scope.loginAsAnotherUser = function (employee) {
			testFactory.loginAsAnotherUser(employee.domainUsername).success(function (code) {
				if (parseInt(code) === 0) {
					$window.location.reload();
				}
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		};
} ]); 