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

registrationModule.controller("LogoutController", ['$scope', '$location', '$window', 
function ($scope, $location, $window) {
    var username = "Test",
		password = "Test";
    $.ajax({
        type: "GET",
        url: "/api/Test/Logout",
        dataType: 'json',
        success: function () {
            $window.location.href = 'Index';
        },
        error: function (xhr) {
            if (xhr.status === 401) {
                $window.location.href = 'Login';
                $.ajax({
                    type: "GET",
                    url: "/api/Test/Logout",
                    dataType: 'json',
                    data: { username: JSON.stringify(username), password: JSON.stringify(password) },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Basic " + btoa(username + ":" + password));
                    },
                    success: function () {
                        $window.location.href = 'Login';
                    }
                });
            }
        }
    });
} ]);