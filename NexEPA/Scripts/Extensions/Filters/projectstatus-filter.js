/*global $, registrationModule*/
'use strict';

registrationModule.filter('enumStatusText', function ($http) {
	var statusEnum = [];
	$http.get('/api/Project/RenderProjectStatusSelectList').success(function (data) {
		statusEnum = data;
	});
	return function (input) {
		switch (input) {
			case 0:
				return statusEnum[0].name;
			case 1:
				return statusEnum[1].name;
			case 2:
				return statusEnum[2].name;
			case 3:
				return statusEnum[3].name;
			case 4:
				return statusEnum[4].name;
		}
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