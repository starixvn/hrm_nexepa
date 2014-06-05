'use strict';
/*global authorityModule*/

authorityModule.controller("AssignPermissionController", ['$scope', 'authorityRepository',
function ($scope, authorityRepository) {

	authorityRepository.getAllSystemRole().success(function (roles) {
		$scope.roles = roles;
	}).error();

	authorityRepository.getAllFunction().success(function (functionList) {
		$scope.functionList = functionList;
	}).error();
} ]);