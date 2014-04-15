'use strict';
/*global registrationModule*/

registrationModule.factory('commonFactory', function () {
	var common = {};

	common.getDateFormat = function ($scope) {
		$scope.dateFormat = "dd MMM,yyyy";
	};

	common.getSourceCalendarImage = function ($scope) {
		$scope.sourceCalendarImage = "/Content/Site/images/calendar.png";
	};

	return common;
});