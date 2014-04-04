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

	var getTarget = function () {
		targetFactory.getTarget().success(function (targetList) {
			$scope.targetList = targetList;
		}).error();
	};

	// get Target
	getTarget();

});