/* global epaModule */
'use strict';

epaModule.controller('EmployeeEPAController', ['$scope', 'employeeEPAFactory', function ($scope, employeeEPAFactory) {
	$(".boxtitle").on("click", function () {
		var header = $(this);
		//getting the next element
		var content = header.next();
		//open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
		content.slideToggle(500, function () {
		});
	});

	employeeEPAFactory.getMyEPA(35).then(function (data) {
		$scope.myEPA = data;
		var roleArray = [];
		$.each(data.projectRoles, function (i) {
			roleArray.push(data.projectRoles[i].name);
		});

		$scope.employeeRole = roleArray.join(", ");
	});

	$scope.rate = 0;
	$scope.max = 5;
	$scope.isReadonly = false;
	$scope.percent = 0;


}]);