/*global $, registrationModule*/
'use strict';

registrationModule.filter('enumStatusText', function ($http) {
	var statusEnum = [];
	$http.get('/api/Project/RenderProjectStatusSelectList').success(function (data) {
		statusEnum = data;
	});
	return function (input) {
		var statusText;
		$.each(statusEnum, function (i, val) {
			if (statusEnum[i].id === input) {
				statusText = val.name;
			}
		});
		return statusText;
	};
});

registrationModule.filter('targetType', function ($http) {
	var targetType = [];
	$http.get('/api/targetTemplate/GetAllTargetType').success(function (data) {
		targetType = data;
	});
	return function (input) {
		var typeText;
		$.each(targetType, function (i, val) {
			if (targetType[i].targetTypeId === input) {
				typeText = val.name;
			}
		});
		return typeText;
	};
});