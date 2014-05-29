'use strict';
/*global registrationModule*/

registrationModule.controller("LoginController", ['$window', 'loginFactory',
function ($window, loginFactory) {
	var data = {
		username: Math.random(),
		password: Math.random()
	};

	loginFactory.getIsAuthenticated().success(function (isAuthenticated) {
		if (isAuthenticated === "true") {
			$.ajax({
				type: "GET",
				url: "/api/Login/Logout",
				success: function () {
					$window.location.href = '/';
				},
				error: function (xhr) {
					if (xhr.status === 401) {
						$.ajax({
							type: "GET",
							url: "/api/Login/Logout",
							beforeSend: function (xhr) {
								xhr.setRequestHeader("Authorization", "Basic " + btoa(data.username + ":" + data.password));
							}
						});
					}
				}
			});
		}
	});
} ]);

registrationModule.controller("LogoutController", ['$window', '$scope',
function ($window, $scope) {
	$scope.logout = function () {
		$window.location.href = "/Home/Login";
	};
} ]);