'use strict';
/*global registrationModule*/

registrationModule.controller("LoginController", ['$window', '$scope',
function ($window, $scope) {
	var isClearAuthenticationCache = document.execCommand("ClearAuthenticationCache", "false");

	if (!isClearAuthenticationCache) {
		var data = {
			username: Math.random(),
			password: Math.random()
		};

		$.ajax({
			type: "GET",
			url: "/api/Login/Logout",
			success: function () {
				$window.location.href = '/';
			}
		});

		$.ajax({
			type: "GET",
			url: "/api/Login/Logout",
			beforeSend: function (xhr) {
				xhr.setRequestHeader("Authorization", "Basic " + btoa(data.username + ":" + data.password));
			}
		});
	}

	$scope.login = function () {
		$window.location.href = '/';
	};
} ]);

registrationModule.controller("LogoutController", ['$window', '$scope',
function ($window, $scope) {
	$scope.isShowMenu = false;

	$scope.logout = function () {
		$window.location.href = "/Home/Login";
	};
} ]);