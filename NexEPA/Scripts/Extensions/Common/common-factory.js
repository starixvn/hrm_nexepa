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

	// For many datepicker in a single page
	// Milestone Add and Edit inline
	// Add Mode
	common.openPopupAddModeStartDate = function ($event, $scope) {
		$event.preventDefault();
		$event.stopPropagation();

		$scope.calendar.addModeStartDateOpened = true;
		$scope.calendar.addModeEndDateOpened = false;
	};

	common.openPopupAddModeEndDate = function ($event, $scope) {
		$event.preventDefault();
		$event.stopPropagation();

		$scope.calendar.addModeEndDateOpened = true;
		$scope.calendar.addModeStartDateOpened = false;
	};

	// Edit Mode
	common.openPopupEditModeStartDate = function ($event, $scope, milestoneId) {
		$event.preventDefault();
		$event.stopPropagation();
		var propertyStart = 'editModeStartDateOpened'.concat(milestoneId);
		var propertyEnd = 'editModeEndDateOpened'.concat(milestoneId);

		$scope.calendar[propertyStart] = true;
		$scope.calendar[propertyEnd] = false;
	};

	common.openPopupEditModeEndDate = function ($event, $scope, milestoneId) {
		$event.preventDefault();
		$event.stopPropagation();

		var propertyStart = 'editModeStartDateOpened'.concat(milestoneId);
		var propertyEnd = 'editModeEndDateOpened'.concat(milestoneId);

		$scope.calendar[propertyEnd] = true;
		$scope.calendar[propertyStart] = false;
	};
	return common;
 });

