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
		}).error();
	};

	// get Target
	getTarget(null, null, null);

	// filter data
	$scope.query = function (filterData) {
		getTarget(filterData.targetTypeId, filterData.startDate.toDateString(), filterData.endDate);
	};
});