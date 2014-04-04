'use strict';
/*global targetModule*/

targetModule.controller('ViewTargetController', function ($scope, targetFactory, targetCacheFactory, CommonRepository) {

	var formOptionsCache = targetCacheFactory.get("formOptions");

	if (formOptionsCache) {
		$scope.formOptions = formOptionsCache;
	}
	else {
		CommonRepository.getFormOptions().then(function (listTargetType) {
			$scope.formOptions = listTargetType;
			targetCacheFactory.put("formOptions", listTargetType);
		});
	}

	var getTarget = function (targetTypeId, startDate, endDate) {
		targetFactory.getTarget(targetTypeId, startDate, endDate).success(function (targetList) {
			$scope.targetList = targetList;
		}).error(function () {
			//// TODO : show error message
		});
	};

	// show start date calendar when click button start date
	$scope.openPopupStartDate = function ($event) {
		$event.preventDefault();
		$event.stopPropagation();

		$scope.startDateOpened = true;
		$scope.endDateOpened = false;
	};

	// show end date calendar when click button end date
	$scope.openPopupEndDate = function ($event) {
		$event.preventDefault();
		$event.stopPropagation();

		$scope.endDateOpened = true;
		$scope.startDateOpened = false;
	};

	// get Target
	getTarget(null, null, null);

	// filter data
	$scope.query = function (filterData) {
		if (filterData) {
			var startDate = filterData.startDate ? filterData.startDate.toDateString() : null;
			var endDate = filterData.endDate ? filterData.endDate.toDateString() : null;

			getTarget(filterData.targetTypeId, startDate, endDate);
		}
	};
});