/*global projectsSettingModule, registrationModule*/
'use strict';

projectsSettingModule.service('projectMilestoneService', function () {
	var property;
	return {
		getProperty: function () {
			return property;
		},
		setProperty: function (value) {
			property = value;
		}
	};
});

registrationModule.service('tabService', function () {
	var property;
	return {
		getProperty: function () {
			return property;
		},
		setProperty: function (value) {
			property = value;
		}
	};
});