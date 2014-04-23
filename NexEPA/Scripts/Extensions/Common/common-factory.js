'use strict';
/*global registrationModule*/

registrationModule.factory('commonFactory', function () {
	var common = {};

	common.getDateFormat = function ($scope) {
		$scope.dateFormat = "dd MMM, yyyy";
	};

	common.getSourceCalendarImage = function ($scope) {
		$scope.sourceCalendarImage = "/Content/Site/images/calendar.png";
	};

	return common;
});

registrationModule.factory('datePickerFactory', function () {
	var common = {};

	common.openPopupStartDate = function ($event, $scope) {
		$event.preventDefault();
		$event.stopPropagation();

		$scope.calendar.startDateOpened = true;
		$scope.calendar.endDateOpened = false;
	};

	common.openPopupEndDate = function ($event, $scope) {
		$event.preventDefault();
		$event.stopPropagation();

		$scope.calendar.endDateOpened = true;
		$scope.calendar.startDateOpened = false;
	};

	return common;
});

