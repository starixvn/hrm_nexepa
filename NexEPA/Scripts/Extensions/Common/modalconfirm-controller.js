/*global registrationModule*/
'use strict';

registrationModule.controller("ModalConfirmController", ['$scope', '$modalInstance', 'name', 'action', 'extraInfo',
function ($scope, $modalInstance, name, action, extraInfo) {
	$scope.name = name;
	$scope.action = action;
	$scope.extraInfo = extraInfo;

	$scope.ok = function () {
		$modalInstance.close();
	};

	$scope.cancel = function () {
		$modalInstance.dismiss("cancel");
	};
} ]);